using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class multiTargetPosision : MonoBehaviour
{

    public float smoothing;
    private Vector3 velocity = Vector3.zero;

    public float lookAhead;
    public float lookUp;

    GameObject[] targets;
    Vector2 newPos;

    Vector3 cameraPos;

    Camera cam;

    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {   

        if (targets != null)
        {
            int i = targets.Length;
            if (i == 2)
            {
                Vector3 player1Pos = targets[0].transform.position;
                Vector3 player2Pos = targets[1].transform.position;               
                cameraPos = new Vector3(player1Pos.x / 2 + player2Pos.x / 2, player1Pos.y / 2 + player2Pos.y / 2, cameraPos.z); 
              cameraSize(player1Pos,player2Pos);
            }
            if (i == 1)
            {
                lookAhead = 20f;
                lookUp = 40f;
                cameraPos = targets[0].transform.position;
            }

            cameraPosDamp();
        }

    }

    void cameraSize(Vector3 pos1,Vector3 pos2)
    {

    float distance = Mathf.Abs (pos1.x - pos2.x);
        cam.orthographicSize = Mathf.Clamp(distance,40,80);
        
    }
    void cameraPosDamp()
    {
        
        Vector3 targetPos = (new Vector3(lookAhead + cameraPos.x, lookUp + cameraPos.y, -10));
            
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothing);
    }

    public void UpdatePlayers()
    {
        targets = null;
        targets = GameObject.FindGameObjectsWithTag("Player");
    }
}
