using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour 
{
	void OnGUI() 
    {
        GUI.Label(new Rect(10, 10, 100, 100), (Mathf.RoundToInt(1 / Time.deltaTime)).ToString());
	}
}
