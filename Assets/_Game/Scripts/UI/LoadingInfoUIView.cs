using TMPro;
using UnityEngine;

public class LoadingInfoUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesAmount;
    [SerializeField] TextMeshProUGUI pointsAmount;
    
    public void SetLivesAmount (string amount) => livesAmount.text = amount;
    
    public void SetScoreAmount (string amount) => pointsAmount.text = amount;
    
    public void SetLivesColor (Color color) => livesAmount.color = color;
    
    public void SetScoreColor (Color color) => pointsAmount.color = color;
}
