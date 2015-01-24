using UnityEngine;
using System.Collections;

public class collidestypendium : MonoBehaviour 
{
    public GameObject stypDestroyed;
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
            GameObject t = GameObject.FindGameObjectWithTag("LvlTarget");
            t.tag = "NoCollide"; stypDestroyed.tag = "LvlTarget";
            stypDestroyed.SetActive(true);

            Rigidbody[] rigs = stypDestroyed.transform.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rigid in rigs)
            {
                rigid.AddExplosionForce(10000f, globals.player.transform.position + globals.player.transform.forward * -100f, 300f);
            }

            Destroy(this.gameObject);
        }
    }
}
