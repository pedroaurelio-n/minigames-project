using UnityEngine;

public class MainMenuUIView : MenuUIView
{
    [field: SerializeField] public MainMenuPanelUIView MainMenuPanelUIView { get; private set; }
    [field: SerializeField] public LevelSelectPanelUIView LevelSelectPanelUIView { get; private set; }
    [field: SerializeField] public LoginPanelUIView LoginPanelUIView { get; private set; }
}