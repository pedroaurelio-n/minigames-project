using Newtonsoft.Json;
using UnityEngine;

public class SettingsObject<TInterface, TClass> where TClass : TInterface
{
    public TInterface Instance { get; }

    public SettingsObject (TClass instance)
    {
        Instance = instance;
    }
    
    public SettingsObject (string jsonName)
    {
        TextAsset json = Resources.Load<TextAsset>($"Settings/{jsonName}");
        TClass settings = JsonConvert.DeserializeObject<TClass>(json.text);
        Instance = settings;
    }
}
