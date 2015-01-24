using UnityEngine;
using System.Collections;

public class faderOut : MonoBehaviour 
{
    public GUITexture gui;

    void Start()
    {
        gui.color = new Color(0, 0, 0, 0.5f);
        gui.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
    }

	void Update() 
    {
        Color col = gui.color;
        col.a -= .025f;
        gui.color = col;
        if (col.a < 0.01f) Destroy(this.gameObject);
	}
}
