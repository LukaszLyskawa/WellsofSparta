using UnityEngine;
using System.Collections;

public class collidePowerUp : MonoBehaviour 
{
    public GameObject powerUpDestroyed;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            powerUpDestroyed.SetActive(true);

            Rigidbody[] rigs = powerUpDestroyed.transform.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rigid in rigs)
            {
                rigid.AddExplosionForce(10000f, globals.player.transform.position + globals.player.transform.forward * -100f, 300f);
            }

            Destroy(this.gameObject);
        }
    }
}
