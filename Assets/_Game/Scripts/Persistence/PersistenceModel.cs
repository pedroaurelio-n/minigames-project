using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class PersistenceModel : IPersistenceModel
{
    readonly GameVersion _gameVersion;
    readonly IPersistence _persistence;
    readonly IDateTimeProvider _dateTimeProvider;
    readonly string _filePath;

    public PersistenceModel (
        GameVersion gameVersion,
        IPersistence persistence,
        IDateTimeProvider dateTimeProvider
    )
    {
        _gameVersion = gameVersion;
        _persistence = persistence;
        _dateTimeProvider = dateTimeProvider;
        
#if UNITY_EDITOR
        _filePath = Path.Combine(Application.persistentDataPath, "ActiveSave_Editor.json");
#else
        _filePath = Path.Combine(Application.persistentDataPath, "ActiveSave.json");
#endif
    }
    
    public void Flush ()
    {
        _persistence.Data.MetadataData.LastPlayedTime = _dateTimeProvider.UtcNow;
        Save();
    }
    
    public GameSessionData Load ()
    {
        if (!File.Exists(_filePath))
        {
            DebugUtils.LogWarning($"Save file not found. Returning null.");
            return null;
        }
        
        string json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<GameSessionData>(json);
    }

    public void ClearSave ()
    {
        if (!File.Exists(_filePath))
        {
            DebugUtils.LogWarning($"Save file not found.", true);
            return;
        }
        
        File.Delete(_filePath);
        DebugUtils.Log($"Save file at {_filePath} has been deleted.", true);
    }

    void Save ()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.None
        };
        
        string json = JsonConvert.SerializeObject(_persistence.Data, settings);
        File.WriteAllText(_filePath, json);
    }
}