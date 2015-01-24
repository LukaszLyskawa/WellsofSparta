using UnityEngine;
using System.Collections;

public class activateChildren : MonoBehaviour 
{
    public float timer = 1.0f;
	
	void Update () 
    {
        timer -= 0.01f;
        if (timer < 0) if (gameObject.transform.GetChild(0)) gameObject.transform.GetChild(0).active = true;

	}
}
