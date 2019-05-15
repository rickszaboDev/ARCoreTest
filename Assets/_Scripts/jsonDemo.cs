using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class jsonDemo : MonoBehaviour 
{
	string path;
	string jsonStr;

	void Start()
	{
		path = Application.streamingAssetsPath + "/levelSetting.json";
		jsonStr = File.ReadAllText(path);

		LevelSetting level1 = JsonUtility.FromJson<LevelSetting>(jsonStr);
		Debug.Log(level1.Name);
		level1.Level = 25;
		string newLevel = JsonUtility.ToJson(level1);
		Debug.Log(newLevel);
	}
}

[System.Serializable]
public class LevelSetting 
{
	public string Name;
	public int Level;
	public int[] Stats;
}