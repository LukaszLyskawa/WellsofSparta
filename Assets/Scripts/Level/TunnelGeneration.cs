using UnityEngine;
using System.Collections;

public class TunnelGeneration : MonoBehaviour 
{
    public Object checkpointPrefab;
    public Object tunnelModel;
    public Object tunnelModelEnd;
    public int tunnelLength = 100;
    private GameObject[] tunnel;
    private string[] history = new string[5];
    private int nextGenChange = 6;
    private GameObject checkpoint;
    private static float RAD = Mathf.PI / 180;
    private float distance;

    #region generation variables
    //te last moze nawet sie nie przydadza, ale na razie niech beda
    private string genLastType = "forward"; //Typ ostatnio tworzonego ciągu tunelu
    private int genLastLength = 6;          //Ilość członów ostatnio tworzonego ciągu tunelu
    private float genLastFactor = 0.0f;     //Skrętność ostatnio tworzonego ciagu tunelu

    private string genNowType = "forward";  //Aktualny typ tworzonego ciągu tunelów
    private int genNowLength = 1;           //Aktualna długość tworzonego ciągu tunelów
    private int genNowToCreate = 1;         //(zmienia się) Ilość tuneli do stworzenia by zakończyć działanie
    private float genNowFactor = 0.0f;      //Skrętność aktualnego ciągu tworzenia tuneli
    private float genNowFactor2 = 0.0f;     //Skrętność dodatkowa (gdy np. "left up")

    //rPossibilities prawdopodobnieństwa, a w rPosTypes nazwy członów do wylosowania indexami stosownie do rPossibilities
    private int[] rPossibilities = new int[19];       //Podajemy np [0] = 50 [1] = 30 [2] = 20
    private string[] rPosTypes = new string[19];      //Podajemy np [0] = "forward" [1] = "left" [2] = "up left"
    private int rPossibilitiesLength;                 //Nie łogarniam dynamicznych tablic w C#
    #endregion

    void Start () 
    {
        tunnel = new GameObject[5];
        tunnel[0] = (GameObject)Instantiate(tunnelModel, Vector3.zero, Quaternion.identity);
        tunnel[1] = (GameObject)Instantiate(tunnelModel, new Vector3(0, 0, -174.1596f), Quaternion.identity);
        tunnel[2] = (GameObject)Instantiate(tunnelModel, new Vector3(0, 0, -174.1596f * 2), Quaternion.identity);
        tunnel[3] = (GameObject)Instantiate(tunnelModel, new Vector3(0, 0, -174.1596f * 3), Quaternion.identity);
        tunnel[4] = (GameObject)Instantiate(tunnelModel, new Vector3(0, 0, -174.1596f * 4), Quaternion.identity);
        //for (int i = 0; i < history.Length - 1; i++) history[0] = "forward"; //forward, left, right, up, down, leftup etc.

        //checkpointPrefab = Resources.Load<GameObject>("checkPoint");
        //checkpointPrefab = Resources.Load( "checkPoint.prefab", typeof(Object) );
        checkpoint = (GameObject)Instantiate(checkpointPrefab);
	}
	
	void Update () 
    {
        if (!globals.pause && !globals.finish && !globals.gameOver) if (Input.GetKey(KeyCode.F))
            tunnelLength = 10;

        if (checkpoint)
        {
            distance = Vector3.Distance(transform.localPosition, checkpoint.transform.position);
            if ( !globals.gameOver && !globals.finish ) if (distance > 300) createCheckPoint();
            if (distance > 500) { globals.hp = -1; globals.player.SendMessage("GameOverFunction", SendMessageOptions.DontRequireReceiver); }
            if (globals.segments > 1)
            {
                //globals.globalRot = Quaternion.LookRotation( new Vector3(transform.position.x, transform.position.y+18,transform.position.z) - new Vector3(globals.player.transform.position.x, globals.player.transform.position.y, globals.player.transform.position.z));
                globals.globalRotY = checkpoint.transform.rotation.eulerAngles.y;
            }
        }

	}

    void createCheckPoint()
    {
        GameObject.Destroy(checkpoint);
        checkpoint = (GameObject)Instantiate(checkpointPrefab, tunnel[1].transform.position, transform.rotation);
        pushTunnel();
        generateTunnel();
    }

    void pushTunnel() //niszczy ostatni człon i przesówa tablice o 1 miejsce do tyłu żeby nie zaśmiecać pamięci
    {
        globals.segments++;
        Destroy(tunnel[0]); 
        for(int i = 1; i<tunnel.Length; i++)
            tunnel[i-1] = tunnel[i];
    }

    void generateTunnel()
    {
        tunnelLength--;
        //generacja kolejnego członu w odpowiednim położeniu wobec poprzedniego członu
        Vector3 pos = tunnel[tunnel.Length - 1].transform.localPosition;
        float[] angle = {0,0}; 
        angle[0] = tunnel[tunnel.Length - 1].transform.localEulerAngles.y+90;
        angle[1] = tunnel[tunnel.Length - 1].transform.localEulerAngles.x;

        pos.x += Mathf.Cos(angle[0] * RAD) * 174.1596f;
        pos.z -= Mathf.Sin(angle[0] * RAD) * Mathf.Cos(angle[1] * RAD) * 174.1596f;
        pos.y += Mathf.Sin(angle[1] * RAD) * 174.1596f;

        Quaternion rot = tunnel[tunnel.Length - 1].transform.localRotation;

        if (tunnelLength > 0)
        {
            rot = tunnelGenRotation(rot);
            tunnel[tunnel.Length - 1] = (GameObject)Instantiate(tunnelModel, pos, rot);

            genNowToCreate--;

            if (genNowToCreate == 0) //generowanie nowego zadania tworzenia ciągu tuneli
            {
                genLastType = genNowType;
                genLastLength = genNowLength;
                genLastFactor = genNowFactor; //cza zrobić to generowanie w miare inteligentne teraz

                rPossibilities[0] = 30;
                rPossibilities[1] = 25;
                rPossibilities[2] = 25;
                rPossibilities[3] = 20;
                rPossibilities[4] = 20;
                rPossibilities[5] = 22;
                rPossibilities[6] = 22;
                rPosTypes[0] = "forward";
                rPosTypes[1] = "down";
                rPosTypes[2] = "up";
                rPosTypes[3] = "left";
                rPosTypes[4] = "right";
                rPosTypes[5] = "left up";
                rPosTypes[6] = "right down";

                rPossibilitiesLength = 7;

                genNowType = tunnelGenPossibility_HYPER_INTELLIGENT_TypeChoosingFunction();
                genNowLength = 3;
                genNowFactor = 0.05f;
                genNowFactor2 = 0.04f;
                genNowToCreate = genNowLength;
            }
        }
        else //tworzymy ostatni człon tunelu
        {
            tunnel[tunnel.Length - 1] = (GameObject)Instantiate(tunnelModelEnd, pos, rot); //ostatni człon jako osobny model na końcu
            //a tu tworzymy cel lotu
            //****
            Destroy(this); //Generator zakończył zadanie, można go usunąć
        }
        
    }

    string tunnelGenPossibility_HYPER_INTELLIGENT_TypeChoosingFunction() //Funkcja wylosowywania typu tunelu według danych w rPossibilities[] i rPosTypes[], pamiętaj o rPossibilitiesLength!
    {
        //w rPossiblities podajemy np [0] = 50 [1] = 30 [2] = 20
        for (int i = 1; i < rPossibilitiesLength; i++) //należy zwiększyć liczby o poprzedników aby łatwiej można było wylosować
            rPossibilities[i] += rPossibilities[i - 1]; //tablica bedzie miala postac np [0] = 50 [1] = 80 [2] = 100

        float r = Random.Range(0, rPossibilities[rPossibilitiesLength - 1]); //Będziemy zawsze robić do 100, ale tak w razie czego żeby zapobiedz przypadkowym błędom damy tak

        for (int i = 0; i < rPossibilitiesLength; i++)
            if (r <= rPossibilities[i]) return rPosTypes[i];

        return "forward";
    }

    Quaternion tunnelGenRotation(Quaternion rot) //Obraca człon tunelu według genNowType
    {
        genNowType = genNowType.ToLower();
        switch (genNowType)
        {
            case "forward":
                break;

            case "left":
                rot.y -= genNowFactor;
                break;

            case "right":
                rot.y += genNowFactor;
                break;

            case "up":
                rot.x += genNowFactor;
                break;

            case "down":
                rot.x -= genNowFactor;
                break;

            case "down right":
                rot.y += genNowFactor;
                rot.x -= genNowFactor2;
                break;

            case "down left":
                rot.y -= genNowFactor;
                rot.x -= genNowFactor2;
                break;

            case "up right":
                rot.y += genNowFactor;
                rot.x += genNowFactor2;
                break;

            case "up left":
                rot.y -= genNowFactor;
                rot.x += genNowFactor2;
                break;

            case "roll left":
                rot.z += genNowFactor;
                break;

            case "roll right":
                rot.z -= genNowFactor;
                break;

            case "up roll left":
                rot.x += genNowFactor;
                rot.z += genNowFactor2;
                break;

            case "up roll right":
                rot.z += genNowFactor;
                rot.x += genNowFactor2;
                break;

        }

        return rot;
    }

    bool lastHistory(string type = "forward", int tunnels = 4) //(chyba będzie niepotrzebne) sprawdza czy ostatnia liczba członów była taka sama pod rząd
    {
        for (int i = history.Length - 1; i > history.Length - 1 - tunnels; i--)
            if (history[i] != type) return false;
            return true;
    }

    bool veryNear(float source, float nearTo = 0.0f, float precision = .05f) //(chyba będzie niepotrzebne) taka funkcja którą można wyrównywać liczby które są bardzo bliskie zmiennej nearTo
    {
        if (source > nearTo - precision && source < nearTo + precision) return true;
        return false;
    }

    void pushTunnelSimple() //kopia zapasowa tworzenia tunelu
    {
        Destroy(tunnel[0]);
        for (int i = 1; i < tunnel.Length; i++)
            tunnel[i - 1] = tunnel[i];

        //generacja kolejnego członu ze skręcaniem
        Vector3 pos = tunnel[tunnel.Length - 1].transform.localPosition;
        float[] angle = { 0, 0 };
        angle[0] = tunnel[tunnel.Length - 1].transform.localEulerAngles.y + 90;
        angle[1] = tunnel[tunnel.Length - 1].transform.localEulerAngles.x;

        pos.x += Mathf.Cos(angle[0] * RAD) * 174.1596f;
        pos.z -= Mathf.Sin(angle[0] * RAD) * Mathf.Cos(angle[1] * RAD) * 174.1596f;
        pos.y += Mathf.Sin(angle[1] * RAD) * 174.1596f;

        Quaternion rot = tunnel[tunnel.Length - 1].transform.localRotation;
        rot.y += .085f; // lewo - prawo
        rot.x += .115f; // gora - dol

        tunnel[tunnel.Length - 1] = (GameObject)Instantiate(tunnelModel, pos, rot);


    }

    /* Gónwo, nadrukowałem się a nie będzie potrzebne
    void tunnelGenSetPossiblitySimple(int forward = 50, int left = 15, int right = 15, int up = 10, int down = 10)
    { rForward = forward; rLeft = left; rRight = right; rUp = up; rDown = down; }

    void tunnelGenSetPossiblityAdvanced(int upLeft = 10, int upRight = 10, int downLeft = 10, int downRight = 10, int rollLeft = 10, int rollRight = 10)
    { rUpLeft = upLeft; rUpRight = upRight; rDownLeft = downLeft; rDownRight = downRight; rRollLeft = rollLeft; rRollRight = rollRight; }

    void tunnelGenSetPossiblityExtra(int upLeftRollLeft = 5, int upRightRollLeft = 5, int downLeftRollLeft = 5, int downRightRollLeft = 5, int upLeftRollRight = 5, int upRightRollRight = 5, int downLeftRollRight = 5, int downRightRollRight = 5)
    { rUpLeftRollLeft = upLeftRollLeft; rUpRightRollLeft = upRightRollLeft; rDownLeftRollLeft = downLeftRollLeft; rDownRightRollLeft = downRightRollLeft; rUpLeftRollRight = upLeftRollRight; rUpRightRollRight = upRightRollRight; rDownLeftRollRight = downLeftRollRight; rDownRightRollRight = downRightRollRight; }

    void tunnelGenSetPossibilityZero()
    { rForward = 0; rLeft = 0; rRight = 0; rUp = 0; rDown = 0; rUpLeft = 0; rUpRight = 0; rDownLeft = 0; rDownRight = 0; rRollLeft = 0; rRollRight = 0; rUpLeftRollLeft = 0; rUpRightRollLeft = 0; rDownLeftRollLeft = 0; rDownRightRollLeft = 0; rUpLeftRollRight = 0; rUpRightRollRight = 0; rDownLeftRollRight = 0; rDownRightRollRight = 0; }
    */

}
