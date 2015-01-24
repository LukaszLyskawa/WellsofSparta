using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public long intscore=0;
	// Update is called once per frame
	void LateUpdate () {
        intscore++;
        guiText.text = "Score: " + intscore;
	}
}
