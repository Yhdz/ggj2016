using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

/// <summary>
/// 	Mini JSON test.
/// </summary>
public class MiniJSONTest : MonoBehaviour {
	void Start () {
		TextAsset tempAsset = Resources.Load("GlobalSettings") as TextAsset;
		string myString = tempAsset.text;
	
		var dict = Json.Deserialize(myString) as Dictionary<string,object>;

		Debug.Log("Game Name: " + dict["gamename"]);
		Debug.Log("Version: " + dict["version"]);
	}
}
