using UnityEngine;
using System.Collections;

public class sinusMoves : MonoBehaviour 
{
    public float range = 0.1f;
    public float speed = 4.0f;

    public float sxFactor = 1.0f;
    public float syFactor = 1.2f;
    public float szFactor = .8f;
    public float rxFactor = 1.0f;
    public float ryFactor = 1.0f;
    public float rzFactor = .5f;

    private float timer = 0.0f;
    private Quaternion startRot;

	void Start () {
        startRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Quaternion rot = transform.localRotation;
        rot.x = startRot.x + Mathf.Sin(timer * speed * sxFactor) * range * rxFactor;
        rot.y = startRot.y - Mathf.Cos(timer * speed * syFactor) * range * ryFactor;
        rot.z = startRot.z - Mathf.Cos(timer * speed * szFactor) * range * rzFactor;
        transform.localRotation = rot;
	}
}
