using System;
using TMPro;
using UnityEngine;

public class LevelSelectButtonUIView : PoolableView
{
    public event Action<int> OnClick;
    
    [SerializeField] ButtonUIComponent button;
    [SerializeField] TextMeshProUGUI nameText;
    
    public int LevelIndex { get; set; }

    void Awake ()
    {
        button.OnClick += () => OnClick?.Invoke(LevelIndex);
    }
    
    public void SetNameText (string text) => nameText.text = text;
}