
using UnityEngine;
using System.Collections;

public class menuEnter : MonoBehaviour
{
    public GUITexture guiOn;
    public GUITexture guiUn;
    public enum idt { START, EXIT, LINK };
    public idt id;
    public AudioClip audio;
	public AudioClip click;

    private bool enter;
    private Color transparent;
    private Color noTransparent;
    private AudioSource audioSource;

    void Start()
    {
        enter = false;
        transparent = new Color(1, 1, 1, .01f);
        noTransparent = new Color(1, 1, 1, .5f);

        if (audio && click)
        {
            audioSource = gameObject.AddComponent("AudioSource") as AudioSource;
            audioSource.playOnAwake = false;
            audioSource.clip = audio;
            audioSource.loop = false;
        }
    }

    void OnMouseEnter()
    {
        //if (menuController.go == 0)
        {
            enter = true;
			if ( audio && click ) audioSource.Play();
        }
    }

    void OnMouseExit()
    {
        //if (menuController.go == 0)
        {
            enter = false;
        }
    }

    void Update()
    {
        if ( enter )
        {
            guiOn.color = Color.Lerp(guiOn.color, noTransparent, Time.deltaTime * 11);
            guiUn.color = Color.Lerp(guiUn.color, transparent, Time.deltaTime * 11);
        }
        else
        {
            guiOn.color = Color.Lerp(guiOn.color, transparent, Time.deltaTime * 11);
            guiUn.color = Color.Lerp(guiUn.color, noTransparent, Time.deltaTime * 11);
        }

        //if (menuController.go == 0) 
            if (enter) if (Input.GetMouseButtonDown(0))
		{	if ( audio && click ) { audioSource.clip = click; audioSource.Play(); }
                    switch (id)
                    {
                        case idt.START:  break;
                        case idt.EXIT:  break;
                        case idt.LINK:
                            Application.ExternalEval("window.focus(); var win = window.open('http://www.shadowsgames.net','_self',''); window.focus();"); 
                            break;
                    }
                }
    }

    


}