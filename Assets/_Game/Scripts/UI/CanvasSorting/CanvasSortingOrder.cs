using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasSortingOrder : MonoBehaviour
{
    public CanvasId CanvasId
    {
        get => canvasId;
        set
        {
            canvasId = value;
            if (canvas == null)
                canvas = GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = CanvasSortingOrderOptions.Instance.GetSortingOrder(canvasId);
        }
    }

    [SerializeField] CanvasId canvasId;
    [SerializeField] Canvas canvas;

    void OnValidate ()
    {
        CanvasId = canvasId;
        if (canvas == null)
            canvas = GetComponent<Canvas>();
    }
}
