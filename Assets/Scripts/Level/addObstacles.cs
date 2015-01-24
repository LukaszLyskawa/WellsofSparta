using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class addObstacles : MonoBehaviour 
{
    public List<Transform> posFloor;
    public List<Transform> posCeil;
    public List<Transform> posSideLeft;
    public List<Transform> posSideRight;
    public List<Transform> posAlarmLights;
	public List<Transform> posPowerUps;

    public Object[] pipesModels;
    public Object[] extraModels; //0 - kawalki rury 1 - butelka 2 - kura 3 - człowik 4 - kanapka 5 - znak drogowy

    private float rot;

	void Start () 
    {
        int ra = 1;
        if (globals.segments > 40) ra = 2; else ra = 3;

        if ( globals.segments > 1 )
        for (int j = 0; j < Random.Range(ra, 3); j++ )
        {
            float r1 = Random.Range(0.0f, 1.0f);
            int choosed = 0; //0 - floor   1 - sideLeft   2 - sideRight   3 - Ceil

            if (r1 < .25f) choosed = 0; else if (r1 < .45f) choosed = 1; else if (r1 < .65f) choosed = 2; else if (r1 < .85f) choosed = 3; else choosed = 4;
            //choosed = 5;
            switch (choosed)
            {
                case 0: createObstacle(posFloor); break;
                case 1: createObstacle(posSideLeft); break;
                case 2: createObstacle(posCeil); break;
                case 3: createObstacle(posSideRight); break;
                case 4: /*tu mozesz dac switcha*/ int r = Random.Range(0, 3);
                    switch (r)
                    {
                        case 0: createExtraObject(extraModels[0], posAlarmLights); break;
                        case 1: createExtraObject(extraModels[1], posPowerUps); break;
                        case 2: createExtraObject(extraModels[2], posPowerUps); break;
                        default: createExtraObject(extraModels[0], posAlarmLights); break;
                    }//tu bedzie dodawanie extra modeli
                    break;
				case 5: createExtraObject(extraModels[1], posPowerUps); break; //burger
				case 6: createExtraObject(extraModels[2], posPowerUps); break; //laptop
                default: createObstacle(posFloor); break;
            }
        }

	}

    void createExtraObject(Object o, List<Transform> poses)
    {
        int r = Random.Range(0, poses.Count);
        GameObject ob = Instantiate(o, poses[r].position, poses[r].rotation) as GameObject;
        ob.transform.parent = transform.parent.transform.parent;
        Debug.Log("DUPA ŚWIETLISTA");
    }

    void createObstacle(List<Transform> a)
    {
        int r = Random.Range(0,a.Count-1);
        Transform pos = a[r];

        pos.Rotate(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
        if (Random.Range(0.0f, 1.0f) < .25f) pos.Rotate(0, Random.Range(170, 180), 0);

        pos.Translate(Random.Range(-6f, 6f), Random.Range(-1f, 1f), Random.Range(-6f, 6f));
        
        GameObject pipa = Instantiate(pipesModels[Random.Range(0, pipesModels.Length - 1)], pos.position, pos.rotation) as GameObject;
        pipa.transform.parent = a[r];

        //Usuwam z każdej tablicy element r bo są na jednej linii, wtedy modele mogłyby na siebie nachodzić
        posFloor.RemoveAt(r);
        posSideLeft.RemoveAt(r);
        posCeil.RemoveAt(r);
        posSideRight.RemoveAt(r);
        //a.RemoveAt(r);
    }

}
