using UnityEngine;
using System.Collections;

public class CathmenuControll : MonoBehaviour 
{
    public fader fade;
    public GUIText loadingTxt;
    public GameObject progressTxt;
    public GUITexture[] gui = new GUITexture[3];
    public static int go = 0;

    private Color col;
	// Use this for initialization
	void Start () 
    {
        Screen.lockCursor = false;
        Time.timeScale = 1;
        go = 0;
        col = new Color(255, 255, 255);
        col.a = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( go != 0)
        {
            fade.fade = true;
            if (go == 1)
            {
                col.a = fade.alpha;
                fade.gui.color = col;
            }
            
            foreach (GUITexture o in gui)
            {
                //Color col = o.color;
                //col.a = .3f;
                //o.color = col;
            }
            
            if (fade.alpha > .499f)
            {
                switch (go)
                {
                    case -1: /*Application.Quit(); break;*/ Application.LoadLevel("Menu"); break;
                    case 1: loadingTxt.color = Color.black; progressTxt.SetActive(true); break;
                }
            }
        }
	}

}

