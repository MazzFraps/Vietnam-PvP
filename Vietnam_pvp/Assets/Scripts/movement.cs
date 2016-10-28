using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{


    public int playerID;


    public int moveSpeed;
    public int jumpStr;
    bool canJump;

    [HideInInspector]
    public bool ducking;
    [HideInInspector]

    public bool action;

    Rigidbody2D body2d;

    public bool moving;

    public bool grounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;


    // Use this for initialization
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (body2d.velocity.x == 0)
        {
            moving = false;
        }
        else moving = true;

        if (Input.GetAxisRaw("jump_p"+ playerID) == 0)
        {
            canJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        JumpAndDuck();
    }

    void JumpAndDuck()
    {
        if (Input.GetAxisRaw("jump_p" + playerID) > 0 && grounded && canJump && !ducking)
        {
            body2d.velocity = new Vector2(body2d.velocity.x, jumpStr);
            canJump = false;
        }
        if (Input.GetAxisRaw("Vertical_p" + playerID) > 0) action = true;
        else action = false;
        if (Input.GetAxisRaw("Vertical_p" + playerID) < 0) ducking = true;
        else ducking = false;
    }

    void Movement()
    {
        if (Input.GetAxisRaw("Horizontal_p" + playerID) > 0)
        {
            body2d.transform.localScale = new Vector3(1f, 1f, 1f);
            if (!ducking && !action) body2d.velocity = new Vector2(moveSpeed * Time.deltaTime, body2d.velocity.y);
            else if (ducking) body2d.velocity = new Vector2(moveSpeed * Time.deltaTime / 4, body2d.velocity.y);
            else if (action) body2d.velocity = new Vector2(moveSpeed * Time.deltaTime / 2, body2d.velocity.y);
        }

        else if (Input.GetAxisRaw("Horizontal_p" + playerID) < 0)
        {
            body2d.transform.localScale = new Vector3(-1f, 1f, 1f);
            if (!ducking && !action) body2d.velocity = new Vector2(-moveSpeed * Time.deltaTime, body2d.velocity.y);
            else if (ducking) body2d.velocity = new Vector2(-moveSpeed * Time.deltaTime / 4, body2d.velocity.y);
            else if (action) body2d.velocity = new Vector2(-moveSpeed * Time.deltaTime / 2, body2d.velocity.y);
        }

        else body2d.velocity = new Vector2(0f, body2d.velocity.y);

    }

void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.transform.position, groundCheckRadius);
    }
    }
