using UnityEngine;
using System.Collections;

public class selfDestructionTimer : MonoBehaviour 
{
    public float time = 1;

	void Update () 
    {
        time -= Time.deltaTime;
        if (time <= 0) GameObject.Destroy(this.gameObject);
	}
}
