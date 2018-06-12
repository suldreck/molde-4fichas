using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour 
{
	private  RoundData[] allRoundData;
    private string gameDataFileName = "Data.json";
    void Start ()  
	{
		DontDestroyOnLoad (gameObject);
        LoadGame();
        SceneManager.LoadScene ("menu");
	}
	
	public RoundData GetCurrentRoundData()
	{
		return allRoundData [0];
	}
    private void LoadGame()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("No se puede cargar las preguntas");
        }
    }
}