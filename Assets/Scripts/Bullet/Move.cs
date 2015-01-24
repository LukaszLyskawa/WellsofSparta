using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour 
{
    public float velocity = 200;
    public float currentVelocity;

    private float trTime = 2.0f;
    public ParticleEmitter trailRenderer;

    void Start() 
    {
        switch (globals.difficulty)
        {
            case 1: velocity = 200; break;
            case 2: velocity = 250; break;
            case 3: velocity = 300; break;
            case 4: velocity = 500; break;
            default: velocity = 150; break;
        }

        globals.player = gameObject;
        //globals.globalRotY = globals.player.transform.rotation.eulerAngles.y;
        currentVelocity = velocity; 
    }

    void Update()
    {
        if (trailRenderer) { trTime -= .01f; if (trTime < .5f) GetComponent<MouseControll>().enabled = true; if (trTime < 0f) Destroy(trailRenderer); else if (trTime > 0f) trailRenderer.maxEnergy = trTime; }
        
        /*
        Debug.Log(globals.globalRotY);
        if (globals.segments > 1)
        //
        {
            Debug.Log("DUPA SKRECONA");
            //float yy = transform.rotation.eulerAngles.y;
            //yy = Mathf.Lerp(yy, globals.globalRotY, .1f);
            //yy = globals.globalRotY;
            //transform.rotation.SetEulerAngles(transform.rotation.eulerAngles.x,yy,transform.rotation.eulerAngles.z);
            //transform.rotation.SetEulerRotation(transform.rotation.eulerAngles.x, yy, transform.rotation.eulerAngles.z);
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yy, transform.rotation.eulerAngles.z);
        }*/

    }

	void Collision () 
    {
        if (currentVelocity > 50) currentVelocity = velocity / 4 + Random.Range(0, velocity / 8);
        
        //Unikanie odwracania sie pocisku przy kolizjach
        Quaternion rot = transform.rotation;
        Quaternion conv = rot; conv = Quaternion.Euler(rot.eulerAngles.x, globals.globalRotY, rot.eulerAngles.z);
        //rot.SetEulerAngles(rot.eulerAngles.x, globals.globalRotY, rot.eulerAngles.z);
        rot.y = Mathf.Lerp(rot.y, conv.eulerAngles.y, Random.Range(.5f,.75f) );
        if (transform.rotation.eulerAngles.y < globals.globalRotY - 60 || transform.rotation.eulerAngles.y > globals.globalRotY + 60) rot.y = Mathf.Lerp(rot.y, conv.eulerAngles.y, Random.Range(.5f, .75f));
        transform.rotation = rot;
	}

	
	void FixedUpdate () 
    {
        rigidbody.angularVelocity = Vector3.Lerp(rigidbody.angularVelocity, Vector3.zero, Time.deltaTime * 10);
        currentVelocity = Mathf.Lerp(currentVelocity, velocity, Time.deltaTime * 1f);
		rigidbody.velocity = transform.forward * currentVelocity;
    }

}
