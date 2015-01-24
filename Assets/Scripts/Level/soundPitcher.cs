using UnityEngine;
using System.Collections;

public class soundPitcher : MonoBehaviour 
{

    private AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.timeScale < 1) audioSource.pitch = Time.timeScale; else audioSource.pitch = 1;
	}
}
