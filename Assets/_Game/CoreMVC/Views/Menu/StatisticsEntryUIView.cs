using TMPro;
using UnityEngine;

public class StatisticsEntryUIView : PoolableView
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI victoryCountText;
    [SerializeField] TextMeshProUGUI defeatCountText;
    
    public void SetNameText (string text) => nameText.text = text;
    
    public void SetVictoryCountText (string text) => victoryCountText.text = text;
    
    public void SetDefeatCountText (string text) => defeatCountText.text = text;
}