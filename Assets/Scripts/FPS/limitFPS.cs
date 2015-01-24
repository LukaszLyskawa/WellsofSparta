using UnityEngine;
using System.Collections;

public class limitFPS : MonoBehaviour {

	public int targetFrameRate = 60;
	// Use this for initialization
	void Start () {
		Application.targetFrameRate = targetFrameRate;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
