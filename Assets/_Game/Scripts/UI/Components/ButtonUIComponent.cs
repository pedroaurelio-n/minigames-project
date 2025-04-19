using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonUIComponent : MonoBehaviour
{
    public event Action OnClick;
    
    [SerializeField] Button button;

    public bool Interactable
    {
        get => button.interactable;
        set => button.interactable = value;
    }
    
    void OnValidate ()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    void Awake ()
    {
        button.onClick.AddListener(() => OnClick?.Invoke());
    }
}
