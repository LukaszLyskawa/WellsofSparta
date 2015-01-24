using UnityEngine;
using System.Collections;

public class alphaFadeIn : MonoBehaviour 
{
    public float speed = 0.1f;
    private GUITexture gui;

    void Start()
    {
        gui = GetComponent<GUITexture>();
        gui.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
            gui.color = Color.Lerp(gui.color, new Color(1, 1, 1, .5f), .3f);
    }
}
