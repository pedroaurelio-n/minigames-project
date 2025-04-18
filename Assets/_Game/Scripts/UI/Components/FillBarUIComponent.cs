using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillBarUIComponent : MonoBehaviour
{
    [SerializeField] FillDirection fillDirection;
    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI fillText;
    [SerializeField] bool repositionText;
    [SerializeField] Image fillHandle;
    [SerializeField] Gradient fillGradient;
    [SerializeField] bool useGradient;
    
    float _fillWidth;
    float _fillHeight;

    void OnValidate ()
    {
        fillImage.type = Image.Type.Filled;
        fillImage.fillMethod = fillDirection switch
        {
            FillDirection.Vertical => Image.FillMethod.Vertical,
            FillDirection.Horizontal => Image.FillMethod.Horizontal,
            _ => fillImage.fillMethod
        };
    }

    void Awake ()
    {
        UpdateRectSizes();
    }

    void OnRectTransformDimensionsChange ()
    {
        UpdateRectSizes();
    }

    public void SetFillAmount (float current, float max)
    {
        fillImage.fillAmount = max == 0 ? 0 : current / max;
        SyncUI();
    }

    public void SetFillText (string text) => fillText.text = text;
    
    public void SetFillHandleActive (bool active) => fillHandle.SetActive(active);

    void UpdateRectSizes ()
    {
        _fillWidth = fillImage.rectTransform.rect.width;
        _fillHeight = fillImage.rectTransform.rect.height;
    }

    void SyncUI ()
    {
        if (useGradient)
            fillImage.color = fillGradient.Evaluate(fillImage.fillAmount);
        UpdateFillHandlePosition();
        UpdateTextPosition();
    }

    void UpdateFillHandlePosition ()
    {
        if (fillHandle == null || !fillHandle.gameObject.activeInHierarchy)
            return;

        Vector2 newPos = fillHandle.rectTransform.anchoredPosition;
        switch (fillDirection)
        {
            case FillDirection.Vertical:
                newPos.y = _fillHeight * fillImage.fillAmount;
                break;
            case FillDirection.Horizontal:
                newPos.x = _fillWidth * fillImage.fillAmount;
                break;
        }
        fillHandle.rectTransform.anchoredPosition = newPos;
    }
    
    void UpdateTextPosition ()
    {
        if (!repositionText || fillText == null || !fillText.gameObject.activeInHierarchy)
            return;
        
        float textHalfWidth = fillText.rectTransform.rect.width * 0.5f;
        float range = fillDirection == FillDirection.Vertical ? _fillHeight : _fillWidth;
        float clampedPos = range * fillImage.fillAmount * 0.5f;
        clampedPos = Mathf.Clamp(clampedPos, textHalfWidth, range - textHalfWidth);

        Vector2 newPos = fillText.rectTransform.anchoredPosition;
        newPos.x = fillDirection == FillDirection.Horizontal ? clampedPos : newPos.x;
        newPos.y = fillDirection == FillDirection.Vertical ? clampedPos : newPos.y;
        fillText.rectTransform.anchoredPosition = newPos;
    }

    enum FillDirection
    {
        Horizontal = 0,
        Vertical = 1
    }
}