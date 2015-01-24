using UnityEngine;
using System.Collections;

public class lightIntensityAnimate : MonoBehaviour 
{
    public float speed = .5f;
	
	void Update () 
    {
        if (light.intensity > 0) light.intensity -= speed * Time.timeScale;
	}
}
