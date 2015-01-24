using UnityEngine;
using System.Collections;

public class fadePause : MonoBehaviour 
{
    public GUITexture square;
    private bool end;
	// Use this for initialization
	void Start () 
    {
        end = false;
        square.color = new Color(0, 0, 0, 0);
        square.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( globals.finish || globals.gameOver )
            square.color = Color.Lerp(square.color, new Color(0, 0, 0, .35f), .05f);
        else
            if (globals.pause) square.color = Color.Lerp(square.color, new Color(0, 0, 0, .25f), .2f); else end = true;

        if ( end )
        {   square.color = Color.Lerp(square.color, new Color(0, 0, 0, 0), .2f);
            if (square.color.a <= 0.01f) GameObject.Destroy(this.gameObject);
        }
        
	}
}
