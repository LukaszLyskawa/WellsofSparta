using UnityEngine;
using System.Collections;

public class faderIn : MonoBehaviour 
{
    public GUITexture gui;
    public bool go = true;
    public float speed = 0.015f;


    void Start()
    {
        gui.color = new Color(0, 0, 0, 0.0f);
        gui.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
    }

	void Update() 
    {
        Color col = gui.color;
        if ( col.a < .5 ) col.a += speed;
        if (go) if (col.a > .5 - 0.01f)
            {
                Debug.Log("DUPA ZANIKOWA");
                switch (globals.go)
                {
                    case -2: Application.Quit(); break;
                    case -1: Application.LoadLevel("TestMenu"); break;
                    case 1: Application.LoadLevel(Application.loadedLevel); break;
                    case 2: Application.LoadLevel("TestScene3"); break;
                }
            }

        gui.color = col;
	}
}
