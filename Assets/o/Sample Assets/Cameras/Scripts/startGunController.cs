using UnityEngine;
using System.Collections;

public class startGunController : MonoBehaviour 
{
    public MonoBehaviour[] toActivate;
    public GameObject trigger;
    public GameObject fireBlast;
    public ParticleRenderer smokeTrail;
    public ParticleEmitter smokeEmitter;
    public AudioSource[] audioSource;

    private bool shot;
    public float counter;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.A)) counter -= 3f;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ) counter -= 3f;
        trigger.transform.localRotation = Quaternion.Euler(0, counter, 0);//.SetEulerAngles( 0, counter, 0 );

        if (!shot) { if (counter < -30) { Activator(); shot = true; } }
            else { transform.Rotate(0, 0, -100 * Time.deltaTime); 
            if (transform.rotation.eulerAngles.z < 280) Destroy(gameObject); }

        if (counter < -0.1f) counter += .15f;
	}

    void Activator()
    {
        Debug.Log("DUPA STRZAŁOWA");
        foreach(AudioSource a in audioSource )
            a.enabled = true;
        foreach (MonoBehaviour b in toActivate)
            b.enabled = true;
        fireBlast.SetActive(true);

        smokeTrail.enabled = true;
        smokeEmitter.enabled = true;
        audioSource[0].enabled = true;
        globals.slowMotionTimer = .7f;
        Screen.lockCursor = true;
    }
}
