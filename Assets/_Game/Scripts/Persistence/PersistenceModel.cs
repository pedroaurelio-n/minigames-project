using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class PersistenceModel : IPersistenceModel
{
    public GameSessionData Data { get; private set; }
    
    readonly IDateTimeProvider _dateTimeProvider;
    readonly string _filePath;

    public PersistenceModel (
        GameVersion gameVersion,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dateTimeProvider = dateTimeProvider;
        
#if UNITY_EDITOR
        _filePath = Path.Combine(Application.persistentDataPath, "ActiveSave_Editor.json");
#else
        _filePath = Path.Combine(Application.persistentDataPath, "ActiveSave.json");
#endif

        Data = Load(out bool saveNewFile);
        Data.MetadataData.GameVersion = gameVersion;
        
        if (saveNewFile)
            Flush();
    }

    public void Flush ()
    {
        Data.MetadataData.LastPlayedTime = _dateTimeProvider.UtcNow;
        Save();
    }

    void Save ()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.None
        };
        
        string json = JsonConvert.SerializeObject(Data, settings);
        File.WriteAllText(_filePath, json);
    }

    GameSessionData Load (out bool saveNewFile)
    {
        saveNewFile = false;
        
        //TODO pedro: transfer this responsability to GameSession
        if (!File.Exists(_filePath))
        {
            saveNewFile = true;
            DebugUtils.LogWarning($"Save file not found. Returning new data.");
            return new GameSessionData();
        }
        
        string json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<GameSessionData>(json);
    }
}