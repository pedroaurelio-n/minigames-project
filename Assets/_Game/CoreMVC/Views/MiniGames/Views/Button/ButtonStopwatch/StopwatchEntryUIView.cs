using TMPro;
using UnityEngine;

public class StopwatchEntryUIView : PoolableView
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Color defaultColor;
    [SerializeField] Color successColor;
    [SerializeField] Color failureColor;

    public void SetTimeText (string time) => timeText.text = time;
    
    public void SetSuccessful (bool success) => timeText.color = success ? successColor : failureColor;

    public void ResetColor () => timeText.color = defaultColor;
}