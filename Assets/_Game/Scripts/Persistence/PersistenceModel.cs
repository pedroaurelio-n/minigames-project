using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class PersistenceModel : IPersistenceModel
{
    readonly IPersistence _persistence;
    readonly IDateTimeProvider _dateTimeProvider;
    readonly string _filePath;

    public PersistenceModel (
        IPersistence persistence,
        IDateTimeProvider dateTimeProvider
    )
    {
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