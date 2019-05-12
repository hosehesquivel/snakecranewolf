using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFunctionsTest : MonoBehaviour
{
	public UEFunctions[] stuff;
	
	public GameObject[] Menu;
	
	public void Destination (string sceneto)
	{
		//SceneManager.LoadScene(sceneto);
		Debug.Log(sceneto);
	}
}
