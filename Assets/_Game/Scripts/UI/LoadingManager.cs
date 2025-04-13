using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour, ILoadingManager
{
    //TODO pedro: maybe create new ui canvas that instantiates during loading screen
    const string PERCENTAGE_FORMAT = "{0}%";
    const float LOADING_SPEED = 2f;
    const string START_SCENE = "MiniGame1";
    
    [field: SerializeField] public LoadingInfoUIView LoadingInfoUIView { get; private set; }
    
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] GameObject[] loadingSceneObjects;
    [SerializeField] GameObject[] loadingUIObjects;
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI loadingNumber;
    [SerializeField] Image loadingFillBar;
    [SerializeField] FadeToBlackManager fadeToBlackManager;
    
    public ApplicationSession ApplicationSession { get; private set; }
    
    LoadingInfoUIController _loadingInfoUIController;

    float _loadingProgress;
    float _targetProgress;
    string _newScene;

    AsyncOperation _mainSceneLoad;
    AsyncOperation _currentSceneUnload;

    void Awake ()
    {
        loadingText.text = "Play Minigames";
        if (startButton == null || !startButton.gameObject.activeInHierarchy)
        {
            StartLoading(false);
            return;
        }
        
        startButton.onClick.AddListener(() => StartLoading(false));
        foreach (GameObject obj in loadingUIObjects)
            obj.SetActive(false);
    }

    public void LoadNewScene (string newScene)
    {
        //TODO pedro: review loading flow/order
        ApplicationSession.DisposeCurrentScope();
        _currentSceneUnload = SceneManager.UnloadSceneAsync(ApplicationSession.GameSession.CurrentScene);
        _currentSceneUnload.allowSceneActivation = false;
        
        _loadingProgress = 0;
        UpdateLoadingUI();
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        foreach (GameObject obj in loadingSceneObjects)
            obj.SetActive(true);
        
        if (ApplicationSession?.GameSession?.PlayerInfoModel != null && _loadingInfoUIController == null)
            _loadingInfoUIController = new LoadingInfoUIController(
                LoadingInfoUIView,
                ApplicationSession.GameSession.PlayerInfoModel
            );
        _loadingInfoUIController?.Enable();
        
        loadingText.text = "Loading...";
        fadeToBlackManager.FadeOut(CompleteFadeOut);

        void CompleteFadeOut ()
        {
            if (ApplicationSession?.GameSession?.PlayerInfoModel is { HasLivesRemaining: false })
            {
                foreach (GameObject obj in loadingUIObjects)
                    obj.SetActive(false);
                
                loadingText.text = "Game Over";
                return;
            }
            
            _newScene = newScene;
            StartLoading(true);
        }
    }

    void StartLoading (bool unloadCurrentScene)
    {
        loadingText.text = "Loading...";
        
        _loadingProgress = 0;
        if (string.IsNullOrEmpty(_newScene))
            _newScene = START_SCENE;
        
        if (startButton != null)
            startButton.gameObject.SetActive(false);
        
        foreach (GameObject obj in loadingUIObjects)
            obj.SetActive(true);

        UpdateLoadingUI();
        
        if (unloadCurrentScene)
            _currentSceneUnload.allowSceneActivation = true;

        _mainSceneLoad = SceneManager.LoadSceneAsync(_newScene, LoadSceneMode.Additive);
        StartCoroutine(UpdateLoadingProgress(_mainSceneLoad, _currentSceneUnload));
    }

    void CompleteLoad ()
    {
        _loadingInfoUIController?.Disable();
        ApplicationSession.OnInitializationComplete -= CompleteLoad;
        
        foreach (GameObject obj in loadingSceneObjects)
            obj.SetActive(false);
    }

    IEnumerator UpdateLoadingProgress (AsyncOperation loadOperation, AsyncOperation unloadOperation)
    {
        loadOperation.allowSceneActivation = false;

        while (_loadingProgress < 100)
        {
            float loadProgress = loadOperation.progress < 0.9f ? loadOperation.progress : 1f;
            float unloadProgress = unloadOperation?.progress ?? 1f;
            float unloadWeight = unloadOperation != null ? 0.5f : 0f;
            float loadWeight = 1f - unloadWeight;
            
            _targetProgress = Mathf.RoundToInt((loadProgress * loadWeight + unloadProgress * unloadWeight) * 100);

            while (_loadingProgress < _targetProgress && _targetProgress != 0)
            {
                _loadingProgress += LOADING_SPEED;
                UpdateLoadingUI();
                yield return null;
            }

            UpdateLoadingUI();
            yield return null;
        }
        
        _loadingProgress = 100;
        UpdateLoadingUI();

        fadeToBlackManager.FadeIn(() => loadOperation.allowSceneActivation = true);

        while (!loadOperation.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_newScene));

        if (unloadOperation == null)
        {
            ApplicationSession = new ApplicationSession(this);
            ApplicationSession.OnInitializationComplete += CompleteLoad;
            ApplicationSession.Initialize(START_SCENE);
        }
        else
        {
            ApplicationSession.OnInitializationComplete += CompleteLoad;
            ApplicationSession.ChangeScene(_newScene);
        }
    }

    void UpdateLoadingUI ()
    {
        loadingNumber.text = string.Format(PERCENTAGE_FORMAT, Mathf.RoundToInt(_loadingProgress));
        loadingFillBar.fillAmount = _loadingProgress / 100;
    }

    //TODO pedro: maybe create an ApplicationStatusModel/View to manage quit and pause states
    void OnApplicationQuit ()
    {
        ApplicationSession.DisposeCurrentScope();
    }
}
