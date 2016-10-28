using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_vision : MonoBehaviour
{
    public bool moving; // bool to see if the AI is moving
    public int MoveSpeed; // movement speed
    public bool HasPatrol; //if true the AI has a patrol else it idles on spot

    public float patrolDistance; //how far in both directions the AI is patrolling

    public float visionRange; // how far does the AI see
    public LayerMask mask; //what layers is it looking at
    public RaycastHit2D PlayerSee; //raycast for visio ncheck
    public Vector2 dir; //direction of looking
    public int RaysCast; //how many rays does the check cast

   public Transform[] targets; //array of possible targets
   public Transform mainTarget; //target that the enemy is currently following


    bool seekTarget; //if true the AI is actively seeking the target

    Rigidbody2D body2d; //initialization of the required componets

   
    public bool grounded; //is the AI on the ground?
    public bool edge; //if AI is on the edge of a slope but still grounded true else false
    public bool obstacle; //returns true if AI is facing an obstacle
    public Transform groundCheck; //transform checking for the ground
    public Transform edgeCheck; //transform that checks if the AI is on the edge
    public Transform obstacleCheck; //transform for checking if there are any obstacles in front of the AI
    public float groundCheckRadius; //how wide is the groundCheck
    public LayerMask whatIsGround; //layermask for the ground layers

    Vector2 startPos;
    int direction;
    bool checkpoint;
    float distance;



    bool VisCheck = true;
    // Use this for initialization
    void Start()
    {
        direction = 1;
        targets = new Transform[6];
        body2d = GetComponentInParent<Rigidbody2D>();
        startPos = body2d.transform.position;
    }

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        edge = Physics2D.OverlapCircle(edgeCheck.position, groundCheckRadius, whatIsGround);
        obstacle = Physics2D.OverlapCircle(obstacleCheck.position, groundCheckRadius, whatIsGround);

        if (body2d.velocity.x == 0)
        {
            moving = false;
        }
        else moving = true;
    }

        // Update is called once per frame
        void Update()
    {
        if (seekTarget) //if tere is a possible target look for it
        {
            visionCheck();
            Debug.Log("do stuff2222222222222");
        }
        if(targets != null)
        {
            mainTargetSeek();
        }  
        if (mainTarget == null && HasPatrol)
        {
            patrol();
        }
    
        else if (mainTarget)
        {
            Debug.Log("HAS MAIN TARGET");
        }     
    }
   void mainTargetSeek()
    {
        int mainTargetPos = 0;
        float distanceHolder = 0;
        float minDistance = float.MaxValue;
        for (int i = 0; i< targets.Length; i++)
        {
            if(targets[i] != null)
            {
                distanceHolder =Mathf.Abs( transform.position.x - targets[i].transform.position.x);
                if(distanceHolder < minDistance)
                {
                    minDistance = distanceHolder;
                    mainTargetPos = i;
                }
            }

        }
        mainTarget = targets[mainTargetPos];
        distance = minDistance;
    }  

    void visionCheck()
    {

        for (int i = 0; i < RaysCast; i++)
        {
            PlayerSee = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + i + 0.5f), dir, visionRange * transform.localScale.x, mask);
            if (PlayerSee)
            {
                if (PlayerSee.transform.tag == "Player")
                {
                    Debug.Log("Nesmem bit tle");     
                    if(targets[PlayerSee.transform.GetComponent<movement>().playerID] == null)
                    {
                    int z = PlayerSee.transform.GetComponent<movement>().playerID;
                    targets[z] = PlayerSee.transform;
                    }
                }
            }
        }
    }


    void followTarget()
    {
        Debug.Log("FollowTarget");
    }

    void removePlayer(int i)
    {
        targets[i] = null;
    }

    void patrol()
    {
        if (!edge || obstacle)
        {
            if (checkpoint)
            {
                checkpoint = false;
                moveAI(-direction);
                Debug.Log("1");
            }
            else {
                checkpoint = true;
                moveAI(direction);
                Debug.Log("2");
            }
            
        }
        else if (body2d.transform.position.x < startPos.x + patrolDistance && edge && grounded && checkpoint && !obstacle)
        {
            moveAI(direction);
            Debug.Log("3");
        }
        else if (body2d.transform.position.x > startPos.x + patrolDistance)
        {
            checkpoint = false;
            Debug.Log("4");
        }

        else if(body2d.transform.position.x > startPos.x - patrolDistance && edge && grounded && !checkpoint && !obstacle)
        {
            moveAI(-direction);
            Debug.Log("5");
        }
        else if (body2d.transform.position.x < startPos.x - patrolDistance)
        {
            checkpoint = true;
            Debug.Log("6");
        }

    }

    void moveAI(int dir)
    {
        body2d.velocity = new Vector2(MoveSpeed * Time.deltaTime*dir, body2d.velocity.y);
        body2d.transform.localScale = new Vector3(dir,1f,1f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
        if(targets[collision.GetComponent<movement>().playerID] == null)  seekTarget = true;

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawSphere(edgeCheck.transform.position, groundCheckRadius);
        Gizmos.DrawSphere(obstacleCheck.transform.position, groundCheckRadius);
    }
}
