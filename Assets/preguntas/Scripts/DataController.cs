using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour 
{
	public RoundData[] allRoundData;
	
	void Start ()  
	{
		DontDestroyOnLoad (gameObject);
		
		SceneManager.LoadScene ("menu");
	}
	
	public RoundData GetCurrentRoundData()
	{
		return allRoundData [0];
	}
}