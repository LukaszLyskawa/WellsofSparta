using UnityEngine;
using System.Collections;

public class fader : MonoBehaviour 
{
    public bool fade = false;
    public float maxFade = .6f;
    public GUITexture gui;

    [HideInInspector]
    public float alpha;
    
	// Use this for initialization
	void Start () 
    {
        if (fade) alpha = 0.0f; else alpha = maxFade;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (fade && alpha < maxFade)
            alpha += .01f;

        if (!fade && alpha > 0.0f)
            alpha -= .01f;

        if (alpha > .01f) gui.enabled = true; else gui.enabled = false;

        Color col = gui.color;
        col.a = alpha;
        gui.color = col;

	}
}
