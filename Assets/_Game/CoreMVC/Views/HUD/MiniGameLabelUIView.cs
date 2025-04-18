using TMPro;
using UnityEngine;

public class MiniGameLabelUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mainText;

    public void SetText (string text, Color color)
    {
        mainText.color = color;
        mainText.text = text;
    }
}