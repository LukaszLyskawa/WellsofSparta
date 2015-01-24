using UnityEngine;
using System.Collections;

public class alarmlightrotation : MonoBehaviour 
{
	public float rot=500.0f;

	// Update is called once per frame
	void Update () {

        transform.Rotate(0, rot*Time.deltaTime, 0);
	}
}
