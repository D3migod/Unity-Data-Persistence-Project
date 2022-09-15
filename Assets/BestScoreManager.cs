using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BestScoreManager : MonoBehaviour
{
    public static BestScoreManager SharedInstance;
    public string bestPlayerName;
    public int bestPlayerScore;
    public string currentPlayerName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (SharedInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        SharedInstance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }

    [System.Serializable]
    class StoredData
    {
        public string bestName;
        public int bestScore;

        public string currentName;
        //public int currentScore;
    }

    public void SaveBestScore()
    {
        StoredData data = new StoredData();
        data.bestName = bestPlayerName;
        data.bestScore = bestPlayerScore;

        data.currentName = currentPlayerName;
        //data.currentScore = currentPlayerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/storedData.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/storedData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            StoredData data = JsonUtility.FromJson<StoredData>(json);

            bestPlayerName = data.bestName;
            bestPlayerScore = data.bestScore;

            currentPlayerName = data.currentName;
            //currentPlayerScore = data.bestScore;
        }
    }
}
