using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    private GameObject target;
    private GameObject target2;
    private GameObject checkpoint;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public int work;
    private Camera mainCamera;
    private GameObject cameraRig;
    private int type = 0;
	// Use this for initialization
	void Start() 
    {
        type = 0;
        work = -1;
        target = GameObject.Find("PlayerBullet");
        target2 = GameObject.Find("PlayerBullet");
        mainCamera = GameObject.Find("Main Camera").camera;
        cameraRig = GameObject.Find("Multipurpose Camera Rig");
	}

    void FixedUpdate()
    {
        switch(work)
        {
            case 0:
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0.1f, .225f);
                break;

            case 1:
                float xx = 10, yy = 5;
                if (globals.gameOver)
                {
                    checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
                    if (target2.transform.localPosition.x < checkpoint.transform.localPosition.x - 5) xx = 9;
                    if (target2.transform.localPosition.x > checkpoint.transform.localPosition.x + 5) xx = -9;
                    if (target2.transform.localPosition.y > checkpoint.transform.localPosition.y + 23) yy = -3.5f; 
                    if (target2.transform.localPosition.y < checkpoint.transform.localPosition.y + 15) yy = 5;
                }
                else if (globals.finish) target = GameObject.FindGameObjectWithTag("LvlTarget");

                targetPoint = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z) - mainCamera.transform.position;
                //else targetPoint = new Vector3(target2.transform.position.x, target2.transform.position.y, target2.transform.position.z) - mainCamera.transform.position;
                //mainCamera.transform.LookAt(targetPoint);
                targetRotation = Quaternion.LookRotation(targetPoint);
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * 6.0f);
                //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(target.transform.position.x + 10, target.transform.position.y + 5, target.transform.position.z + 6), Time.deltaTime * 5.0f);
                if (type == 0 ) cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, new Vector3(target.transform.position.x + xx, target.transform.position.y + yy, target.transform.position.z + 6), Time.deltaTime * 4.5f);
                if (globals.preFinish) Time.timeScale = Mathf.Lerp(Time.timeScale, 0.1f, .15f); else { if (Time.timeScale > 0.94f) Time.timeScale -= 0.001f; else Time.timeScale = Mathf.Lerp(Time.timeScale, .2f, .05f); }
                break;
        }
    }


    void preHit()
    {
        work = 0;
    }

    void gameOver()
    {
        if (!globals.preFinish)
        {
            Destroy((AutoCam)GetComponent(typeof(AutoCam)));
            work = 1;
            chooseType();
        }
    }

	void changeCamera() 
    {   
        //ProtectCameraFromWallClip p = GetComponent<ProtectCameraFromWallClip>();
        //p.sphereCastRadius = 1.5f;
        //p.clipMoveTime = 0.01f;

        target = GameObject.FindGameObjectWithTag("LvlTarget");
        Destroy((AutoCam)GetComponent(typeof(AutoCam)));
        chooseType();

        //SphereCollider s = GameObject.Find("Multipurpose Camera Rig").AddComponent<SphereCollider>();
        //s.radius = 1.5f;
        work = 1;
	}

    void chooseType()
    {
        if (!globals.preFinish)
        {
            float r = Random.Range(0.0f, 1.0f);
            if (r < .3f) type = 1; else type = 0;
        }
    }


}