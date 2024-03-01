using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Data;
    [NonSerialized] public int Highscore;
    [NonSerialized] public string Username;

    void Awake()
    {
        Data = this;
        
        DontDestroyOnLoad(gameObject);
        LoadUserData();
    }

    [Serializable]
    class SaveData
    {
        public int Highscore;
        public string Username;
    }

    public void SaveUserData()
    {
        SaveData data = new SaveData();
        data.Highscore = Highscore;
        data.Username = Username;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadUserData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Highscore = data.Highscore;
            Username = data.Username;
        }
    }
}
