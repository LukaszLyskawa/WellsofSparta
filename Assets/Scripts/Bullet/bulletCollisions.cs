using UnityEngine;
using System.Collections;

public class bulletCollisions : MonoBehaviour 
{
    public PhysicMaterial deadMat;
    public MonoBehaviour rotator;
    public Object hitWallParticle;
    public AudioClip[] sounds;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () 
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.minDistance = 4;
        globals.audioSourceSounds = audioSource;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "NoCollide") { gameObject.SendMessage("Collision", SendMessageOptions.DontRequireReceiver); Physics.IgnoreCollision(collision.collider, collider); }

        if (collision.collider.gameObject.name == "EndWallCollider")
        { transform.Rotate(Random.Range(-10, 10), Random.Range(-20, 20), Random.Range(-15, 15)); rigidbody.AddForce(-transform.forward * 5700); }
        
        if (collision.collider.gameObject.tag == "Wall")
        {
            gameObject.SendMessage("Collision", SendMessageOptions.DontRequireReceiver);
            globals.hp -= 1;
            if ( globals.hp < -1 ) GameOverFunction();
            //
        }

        if (collision.relativeVelocity.magnitude > .45f) 
        {   
            if (!audioSource.isPlaying)
            {
                //audioSource.clip = sounds[Mathf.RoundToInt(Random.Range(0, sounds.Length - 1))];
                //audioSource.pitch = Random.Range(.85f, 1.1f); audioSource.pitch *= Time.timeScale;
                //audioSource.PlayOneShot(audioSource.clip);
                globals.playBulletSound("s_shell" + Random.Range(1, 2).ToString());

                GameObject.Instantiate(Resources.Load("Particles/Sparkles1") as GameObject, transform.localPosition, new Quaternion());
                Debug.Log("DUPA WALNĘŁA");
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "PowerUp")
        {
            globals.hp++;
            globals.slowMotion(.35f);
            globals.playBulletSound("s_splat2");
            GameObject.Instantiate(Resources.Load("Particles/Sparkles1") as GameObject, transform.localPosition, new Quaternion());
            Debug.Log("DUPA POWERYCZNA");
        }

        if (collider.gameObject.tag == "Obstacle" || collider.gameObject.tag == "Wall" )
        {
            globals.hp--;
            if (globals.hp < 0)
            {
                GameOverFunction();
            }

            globals.slowMotion(.35f);
            globals.playBulletSound("s_metalHit");
            GameObject.Instantiate(Resources.Load("Particles/Sparkles1") as GameObject, transform.localPosition, new Quaternion());
            collider.gameObject.SendMessage("Collision", SendMessageOptions.DontRequireReceiver);
            gameObject.SendMessage("Collision", SendMessageOptions.DontRequireReceiver);
            //globals.audioSourceSec.clip = (AudioClip)Resources.Load("sfx/s_click", typeof(AudioClip));
            //globals.audioSourceSec.Play();
            Debug.Log("DUPA RUROWA");
        }

        if (collider.gameObject.tag == "LvlTarget")
        {
            Debug.Log("DUPA HAŁASOWA");
            GameObject.Instantiate(Resources.Load("Particles/Blood1") as GameObject, transform.localPosition, new Quaternion());
            globals.playBulletSound("s_click", 1, 1, false);
        }

        if (collider.gameObject.tag == "Finish")
        {
            if (!globals.preFinish) GameObject.Find("Multipurpose Camera Rig").SendMessage("preHit");
            globals.preFinish = true;
        }

        if ( collider.gameObject.tag == "Finish" && collider.gameObject.name == "TrigFinish" && !globals.gameOver )
        {
            Instantiate( Resources.Load("HUD/fadeInPartial", typeof(GameObject) ) );
            globals.finish = true;
            Screen.lockCursor = false;

            GameOverFunction();

            Instantiate(Resources.Load("HUD/FinishView", typeof(GameObject)));
            Debug.Log("DUPA FINISHOWA");
        }

    }

    void Update()
    {
        RaycastHit hit;
        if ( Physics.Raycast(transform.position, transform.forward, out hit) )
            if (hit.collider.gameObject.tag == "Obstacle")
            {
                //Debug.DrawRay(transform.position, transform.forward*4, Color.yellow);
                //Debug.Log("RURA PRZED: " + hit.distance.ToString() );
                if ( hit.distance < 90f ) { globals.slowMotion(); Debug.Log("RURA PRZED/"); } 
                if ( hit.distance < 40f ) globals.slowMotion(1f, .4f, 1f);
            }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Finish")
        {
            GameObject.Find("Multipurpose Camera Rig").SendMessage("changeCamera");
            Debug.Log("DUPA WYCHODZĄCA");
        }
    }

    void GameOverFunction()
    {
        if (!globals.gameOver )
        {
            gameObject.rigidbody.useGravity = true;
            gameObject.collider.material = deadMat;
            gameObject.rigidbody.velocity = new Vector3(0, 0, 0);
            gameObject.rigidbody.AddForce(gameObject.transform.forward * 2000);

            if (!globals.finish)
            {
                GameObject.Find("Multipurpose Camera Rig").SendMessage("gameOver");
                Instantiate(Resources.Load("HUD/fadeInPartial", typeof(GameObject)));
                Instantiate(Resources.Load("HUD/GameOverView", typeof(GameObject)));
                Screen.lockCursor = false;
                globals.gameOver = true; Debug.Log("DUPA OVER");
            }

            Destroy(rotator);
            Destroy((Move)GetComponent(typeof(Move)));
            Destroy((MouseControll)GetComponent(typeof(MouseControll)));
            //Destroy(this);
        }
    }

}
