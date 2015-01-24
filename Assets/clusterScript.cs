using UnityEngine;
using System.Collections;

public class clusterScript : MonoBehaviour 
{
    private float timer = 1.0f;
	// Use this for initialization
	void Start () 
    {
        collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer -= Time.deltaTime * 2;
        if (timer < 0f) { collider.enabled = true; Destroy(this); }
	}
}
