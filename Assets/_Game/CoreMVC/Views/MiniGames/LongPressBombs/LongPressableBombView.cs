using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LongPressableBombView : PoolableView, ILongPressable
{
    public event Action OnDefuseTimerReached;
    public event Action OnTimerEnded;
    
    [SerializeField] TextMeshPro timerText;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material defusedMaterial;
    [SerializeField] Material explodedMaterial;
    
    public string Name => gameObject.name;
    public bool Defused { get; private set; }

    float _delayTimer;
    float _timeToDefuse;
    float _timerGrace;
    
    float _bombTimer;
    float _defuseTimer;
    
    bool _active;
    bool _isDefusing;

    //TODO pedro: maybe pass logic to a LongPressableBombController (or possible transition to humble object?)
    public void Setup (float delay, float bombTimer, float timeToDefuse, float timerGrace)
    {
        Defused = false;
        _isDefusing = false;
        _active = true;
        
        timerText.SetActive(false);
        meshRenderer.material = defaultMaterial;

        _timerGrace = timerGrace;
        _delayTimer = delay;
        _bombTimer = bombTimer + timerGrace;
        _timeToDefuse = timeToDefuse;
    }

    public void SetDefusingState (bool isDefusing)
    {
        _defuseTimer = 0f;
        _isDefusing = isDefusing;
        timerText.SetActive(!isDefusing);
    }

    public void UpdateBomb ()
    {
        if (Defused || !_active)
            return;

        if (_delayTimer > 0f)
        {
            _delayTimer -= Time.deltaTime;
            return;
        }
        
        if (!timerText.ActiveInHierarchy())
            timerText.SetActive(true);

        if (_isDefusing)
        {
            _defuseTimer += Time.deltaTime;
            if (_defuseTimer < _timeToDefuse)
                return;
            
            OnDefuseTimerReached?.Invoke();
            Defused = true;
            _active = false;
            meshRenderer.material = defusedMaterial;
            return;
        }
        
        _bombTimer -= Time.deltaTime;
        float value = Mathf.Max(0f, _bombTimer - _timerGrace);
        timerText.text = value.ToString("F1");
        if (_bombTimer > 0f)
            return;
        
        OnTimerEnded?.Invoke();
        _active = false;
        meshRenderer.material = explodedMaterial;
    }
    
    //TODO pedro: besides visual effects, i should change material here? (possible check for refactor in these input interfaces)
    //or maybe it's a humble object transition as well
    public void OnLongPressBegan ()
    {
    }

    public void OnLongPressEnded ()
    {
    }

    public void OnLongPressCancelled ()
    {
    }
}