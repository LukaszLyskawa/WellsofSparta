using UnityEngine;
using System.Collections;

public class menuButton : MonoBehaviour
{
    private GUIText gui;
    //public GUITexture guiUn;
    public enum idt { START, EXIT, D };
    public idt id;
    public GameObject[] o;
    public AudioClip hover;
    public AudioClip click;

    private bool enter;
    private Color transparent;
    private Color noTransparent;
    private AudioSource audioSource;
    private int c = 0;

    void Start()
    {
        gui = GetComponent<GUIText>();
        //gui.color = new Color(1, 1, 1, 0);

        enter = false;
        transparent = new Color(1, 1, 1, .21f);
        noTransparent = new Color(1, 1, 1, 1.0f);

        if (hover && click)
        {
            audioSource = gameObject.AddComponent("AudioSource") as AudioSource;
            audioSource.playOnAwake = false;
            audioSource.clip = hover;
            audioSource.loop = false;
        }
    }

    void OnMouseEnter()
    {
         enter = true;
         if (hover) audioSource.Play();
    }

    void OnMouseExit()
    {
         enter = false;
    }

    void Update()
    {
            if (enter)
            {
                gui.color = Color.Lerp(gui.color, noTransparent, .25f);
                //guiUn.color = Color.Lerp(guiUn.color, transparent, .25f);
            }
            else
            {
                gui.color = Color.Lerp(gui.color, transparent, .25f);
                //guiUn.color = Color.Lerp(guiUn.color, noTransparent, .25f);
            }

            /*if (menuControll.go == 0) */
            if (enter) if ( Input.GetMouseButtonDown(0) )
                {
                    if (click) { audioSource.clip = click; audioSource.Play(); }
                    switch (id)
                    {
                        case idt.START: globals.audioSourceHUD.PlayOneShot(click); for (int i = 0; i < o.Length; i++) o[i].SetActive(true); gameObject.SetActive(false); break;
                        case idt.EXIT: c++; if (c > 1) { globals.go = -2; Instantiate(Resources.Load("HUD/fadeIn")); } break;
                        case idt.D: 
                            globals.audioSourceHUD.PlayOneShot(click); 
                            globals.go = 2;
                            switch (name) { case "D0": globals.difficulty = 0; break; case "D1": globals.difficulty = 1; break; case "D2": globals.difficulty = 2; break; case "D3": globals.difficulty = 3; break; case "D4": globals.difficulty = 4; break; }
                            Debug.Log(globals.difficulty); 
                            Instantiate(Resources.Load("HUD/fadeIn")); 
                            for (int i = 0; i < o.Length; i++) o[i].SetActive(false); 
                            break;
                    }
                }
        }
}