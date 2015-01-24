using UnityEngine;
using System.Collections;

public class bulletRotator : MonoBehaviour 
{
    public float speed = 100;
	void Update () 
    {
        gameObject.transform.Rotate(0, 0, Time.deltaTime * speed);
	}
}
