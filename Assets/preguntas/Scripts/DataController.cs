using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour 
{
	private  RoundData[] allRoundData;
    private string gameDataFileName = "QuestionsAndAnswers.json";
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
        string filePath2 = Path.Combine(Application.streamingAssetsPath, "");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            allRoundData = loadedData.allRoundData;
        }
        else if (File.Exists(filePath2 + "/" + gameDataFileName))
        {
            string dataAsJson = File.ReadAllText(filePath2 + "/" + gameDataFileName);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            allRoundData = loadedData.allRoundData;
            Debug.LogError("No se puede cargar las preguntas");
        }
    }
}