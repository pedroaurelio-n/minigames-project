using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class IndexedSettingsProvider<TInterface, TClass> where TClass : TInterface
{
    public TInterface Instance => _currentObject != null ? _currentObject.Instance : default;
    
    readonly Dictionary<int, SettingsObject<TInterface, TClass>> _settingsDict;

    SettingsObject<TInterface, TClass> _currentObject;
    string _firebaseTag;
    bool _supportEmptySettings;

    public IndexedSettingsProvider (
        string jsonPrefix,
        string firebaseTag,
        bool supportEmptySettings = false
    )
    {
        _settingsDict = new Dictionary<int, SettingsObject<TInterface, TClass>>();
        _firebaseTag = firebaseTag;
        _supportEmptySettings = supportEmptySettings;
        
        LoadAllPrefixedJsons(jsonPrefix);
    }

    public void SelectSettings (int index)
    {
        if (!_settingsDict.TryGetValue(index, out SettingsObject<TInterface, TClass> value) && !_supportEmptySettings)
        {
            DebugUtils.LogWarning($"Settings with index {index} not found for {_firebaseTag} settings group.", true);
            _currentObject = null;
            return;
        }

        _currentObject = value;
    }

    void LoadAllPrefixedJsons (string jsonPrefix)
    {
        TextAsset[] jsons = Resources.LoadAll<TextAsset>($"Settings/{_firebaseTag}");
        foreach (TextAsset json in jsons)
        {
            if (!json.name.StartsWith(jsonPrefix))
                continue;
            
            string suffix = json.name[jsonPrefix.Length..];
            TClass settings = JsonConvert.DeserializeObject<TClass>(json.text);
            SettingsObject<TInterface, TClass> settingsObject = new(settings);

            if (int.TryParse(suffix, out int index))
                _settingsDict[index] = settingsObject;
            else
                DebugUtils.LogWarning($"Invalid int suffix in file name: {json.name}", true);
        }
    }
}