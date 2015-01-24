using UnityEngine;
using System.Collections;

public class MoveCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//offset = new Vector3(target.position.x, target.position.y-17, target.position.z-20 );
		transform.position = target.position - new Vector3 (0, -1, -15);
		transform.LookAt (target);
		//transform.LookAt (target);
	}
	//private Vector3 offset;
	public float speed=10;
	public Transform target;
	//public float turnSpeed=4;
	// Update is called once per frame
	void Update () {
		//transform.LookAt (target);

		/*if (Input.GetKey ("q")) 
		{
			//transform.rotate
			//transform.RotateAround(target.position,Vector3.up,10.0F);
			transform.position= Vector3.Lerp(transform.position,Quaternion.AngleAxis(speed,Vector3.up)*transform.position,Time.deltaTime);
			target.LookAt(target);
		}*/
		//Vector3 direction = new Vector3 (0, 0, -10);
		//transform.position = target.position - 5.0f * direction.normalized;
		//transform.position = target. - new Vector3 (0, 1, -2);
		//transform.LookAt (target);
		//transform.Rotate (Vector3.back * -Time.deltaTime*1000);
		//Vector3 camerapos = target.position - new Vector3(0,0,-10);
		//float move = Input.GetAxis ("Vertical") * speed;
		//move *= Time.deltaTime;
		//transform.position = new Vector3 (transform.position.x, transform.position.y, target.position.z - 10);
		//transform.position = target.position - new Vector3(0,-2,-10);
		//transform.position = Vector3.Lerp (transform.position,temp,Time.deltaTime);
		//transform.position = new Vector3 (Mathf.Lerp(transform.position.x,target.position.x,Time.deltaTime*8), 
		                               // target.position.y+2, 
		                               // target.position.z+10);
		//transform.LookAt (target);
		//offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
		//transform.position = target.position + offset; 
		//transform.LookAt(target.position);
	}
}
