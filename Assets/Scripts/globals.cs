using UnityEngine;
using System.Collections;

public class globals : MonoBehaviour 
{
    public static bool finish;
    public static bool gameOver;
    public static bool pause;
    public static bool preFinish;
    public static bool game;
    public static bool fade;
    public static int go;
    public static int segments;
    public static int difficulty; //lub float, zrobimy trudność regulowaną sliderem :D
    public static float globalRotY;

    public Object fader;
    public GameObject pauseMother;
    public float defaultTimeScale = 1f;

    public static int hp;
    public static AudioSource audioSourceMusic;
    public static AudioSource audioSourceSounds;
    public static AudioSource audioSourceHUD;
    public static GameObject player;

    public static float slowMotionTimer;

    private bool unPaused;
    private float time;

	void Start() 
    {
        hp = 5;
        segments = 0;
        slowMotionTimer = -1;
        game = true;
        finish = false;
        gameOver = false;
        pause = false;
        preFinish = false;
        unPaused = true;
        fade = false;
        go = 0;
        time = 0;
        defaultTimeScale = 1.0f;
        globalRotY = 180;

        if ( !GameObject.Find("MenuController") ) Screen.lockCursor = true;
        Instantiate(Resources.Load("HUD/fadeOut", typeof(GameObject)));
        audioSourceMusic = GetComponent<AudioSource>();
        audioSourceHUD = gameObject.AddComponent<AudioSource>();
	}

    void Update()
    {
        defaultTimeScale += 0.01f * Time.deltaTime;
        slowMotionCalculations();

        if (gameOver || finish)
            if (audioSourceMusic.volume > 0) audioSourceMusic.volume -= .005f;

        audioSourceMusic.pitch = Time.timeScale;
        //if (Time.timeScale < .99f) audioSourceMusic.pitch = Time.timeScale; else audioSourceMusic.pitch = 1;

       //audioSourceSounds.pitch = Time.timeScale;

       //audioSourceMusic.pitch = .75f + Mathf.Sin(time)/2.1f;

        if (gameOver) RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, 0.0125f, .01f);

        if (!pause && !finish && !gameOver)
        {
            if (Input.GetKey(KeyCode.S))
                slowMotionTimer = 1f;

            if (Input.GetKey(KeyCode.F))
                segments = 90;

            /*
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0.2f, .04f);
            else Time.timeScale = Mathf.Lerp(Time.timeScale, 1.0f, .1f);*/
        
        }

        if (!finish && !gameOver && !preFinish)
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            if (pause)
            {
                Screen.lockCursor = false;
                unPaused = false;
                Instantiate(fader);
                pauseMother.SetActive(true);
            }
            else Screen.lockCursor = true;

        }



        if (finish)
        {
            //Time.timeScale = Mathf.Lerp(Time.timeScale, 0.00001f, .1f);
        }
        else
            if (pause) Time.timeScale = Mathf.Lerp(Time.timeScale, 0.00001f, .25f);
            else if (!unPaused) { Time.timeScale = Mathf.Lerp(Time.timeScale, defaultTimeScale, .25f); if (Time.timeScale > .99f) { unPaused = true; pauseMother.SetActive(false); Time.timeScale = 1; } }
    }

    public static void slowMotion(float possibility = 0.1f, float rangeFrom = 0.4f, float rangeTo = 1.6f )
    {
        if (!gameOver && !finish && !preFinish && !pause)
        {
            if ( Random.Range(0.0f, 1.0f) < possibility )
            if (slowMotionTimer < -1) //jak -1 żeby nie można było jeden za drugim błyskawicznie robić
            {
                slowMotionTimer = Random.Range(rangeFrom, rangeTo);
            }
        }
    }

    public static void playBulletSound(string clip, float rangeFrom = .85f, float rangeTo = 1.1f, bool pitcher = true)
    {
        globals.audioSourceSounds.PlayOneShot((AudioClip)Resources.Load("sfx/"+clip, typeof(AudioClip)), 1);
        globals.audioSourceSounds.pitch = Random.Range(rangeFrom, rangeTo);  if ( pitcher ) if ( Time.timeScale < 1 ) globals.audioSourceSounds.pitch *= Time.timeScale;
    }

    public static void playSourceSound(AudioSource audioSource, AudioClip clip, float rangeFrom = .85f, float rangeTo = 1.1f)
    {
        audioSource.PlayOneShot(clip);
        audioSource.pitch = Random.Range(rangeFrom, rangeTo); if ( Time.timeScale < 1 ) globals.audioSourceSounds.pitch *= Time.timeScale;
    }

    void slowMotionCalculations()
    {
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        time += 0.1f;

        if (!gameOver && !finish && !preFinish && !pause)
        {
            slowMotionTimer -= .01f;
            if ( slowMotionTimer < 0 )
                Time.timeScale = Mathf.Lerp(Time.timeScale, defaultTimeScale, .2f);
            else
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0.1f + defaultTimeScale/10, .15f);
        }

    }

}
