using UnityEngine;
using System.Collections;

public class powerUpDeleter : MonoBehaviour 
{
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Vector3.Distance(transform.position, globals.player.transform.position) > 1000) Destroy(this.gameObject);
	}
}
