using UnityEngine;
using System.Collections;

public class pauseAlphaControll : MonoBehaviour 
{
    private GUITexture gui;

    void Start()
    {
        gui = GetComponent<GUITexture>();
        gui.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (globals.pause) 
            gui.color = Color.Lerp(gui.color, new Color(1, 1, 1, 1), .1f);
        else
            gui.color = Color.Lerp(gui.color, new Color(1, 1, 1, 0), .25f);
    }
}
