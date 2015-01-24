using UnityEngine;
using System.Collections;

public class faderInPartial : MonoBehaviour 
{
    public GUITexture gui;
    public bool go = true;
    public float speed = 0.015f;
    public float endAlpha = 1.0f;


    void Start()
    {
        gui.color = new Color(0, 0, 0, 0.0f);
        gui.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
    }

	void Update() 
    {
        Color col = gui.color;
        if ( col.a < endAlpha ) col.a += speed;
        gui.color = col;
	}
}
