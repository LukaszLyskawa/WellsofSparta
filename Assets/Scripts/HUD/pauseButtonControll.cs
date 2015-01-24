using UnityEngine;
using System.Collections;

public class pauseButtonControll : MonoBehaviour
{
    private GUITexture gui;
    public GUITexture guiUn;
    public enum idt { UNPAUSE, EXIT, LINK, RESTART };
    public idt id;
    public AudioClip hover;
    public AudioClip click;

    private bool enter;
    private Color transparent;
    private Color noTransparent;
    private AudioSource audioSource;

    void Start()
    {
        gui = GetComponent<GUITexture>();
        gui.color = new Color(1, 1, 1, 0);

        enter = false;
        transparent = new Color(1, 1, 1, .01f);
        noTransparent = new Color(1, 1, 1, .5f);

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
        if (globals.pause)
        {
            if (enter)
            {
                gui.color = Color.Lerp(gui.color, noTransparent, .25f);
                guiUn.color = Color.Lerp(guiUn.color, transparent, .25f);
            }
            else
            {
                gui.color = Color.Lerp(gui.color, transparent, .25f);
                guiUn.color = Color.Lerp(guiUn.color, noTransparent, .25f);
            }

            /*if (menuControll.go == 0) */
            if (enter) if ( Input.GetMouseButtonDown(0) )
                {
                    if (click) { audioSource.clip = click; audioSource.Play(); }
                    switch (id)
                    {
                        case idt.UNPAUSE: Screen.lockCursor = true; if (globals.game) globals.pause = false; break;
                        case idt.EXIT: globals.go = -1; if (globals.game) { globals.game = false; Instantiate(Resources.Load("HUD/fadeIn", typeof(GameObject))); } break;
                        case idt.LINK:
                            Application.ExternalEval("window.focus(); var win = window.open('http://www.gameshed.com/Scary-Games/','_self',''); window.focus();");
                            break;
                        case idt.RESTART: globals.go = 1; if (globals.game) { globals.game = false; Instantiate(Resources.Load("HUD/fadeIn", typeof(GameObject))); } break;
                    }
                }
        }
        else
        {
            gui.color = Color.Lerp(gui.color, transparent, .2f);
            guiUn.color = Color.Lerp(guiUn.color, transparent, .2f);
        }
    }
}