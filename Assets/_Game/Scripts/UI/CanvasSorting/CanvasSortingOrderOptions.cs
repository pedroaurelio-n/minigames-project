using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanvasSortingOrderOptions : ScriptableObject
{
    const string ASSET_NAME = "CanvasSortingOrderOptions";

    public static CanvasSortingOrderOptions Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<CanvasSortingOrderOptions>(ASSET_NAME);
            return _instance;
        }
    }
    static CanvasSortingOrderOptions _instance;

    [SerializeField] List<CanvasId> canvasIds = new();

    public int GetSortingOrder (CanvasId id) => canvasIds.IndexOf(id);

    [MenuItem("Assets/Create/Canvas Sorting Order Options")]
    public static void Create ()
    {
        CanvasSortingOrderOptions asset = CreateInstance<CanvasSortingOrderOptions>();
        AssetDatabase.CreateAsset(
            asset,
            $"Assets/_Game/Scripts/UI/CanvasSorting/Resources/{ASSET_NAME}.asset"
        );
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}