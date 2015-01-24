using UnityEngine;
using System.Collections;

public class pipeController : MonoBehaviour 
{
    public Object destroyed;
    private Rigidbody[] rigs;
	void Collision() 
    {
        GameObject o = GameObject.Instantiate(destroyed, transform.parent.position, transform.rotation) as GameObject;
        o.transform.parent = null;
        //o.transform.localPosition = transform.localPosition;
        

        int size = o.transform.childCount;

        rigs = o.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigid in rigs) 
        {
            //rigid.useGravity = false;   rigid.AddExplosionForce(10000f, globals.player.transform.position + globals.player.transform.forward * -100f, 300f);
            //rigid.AddForce(globals.player.transform.forward * 1000f); 
            rigid.AddExplosionForce(8000f, globals.player.transform.position + globals.player.transform.forward * -100f, 300f);
        }

        Destroy(this.gameObject);
	}
	
}
