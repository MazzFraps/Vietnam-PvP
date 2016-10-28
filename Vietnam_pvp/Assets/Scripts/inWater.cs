using UnityEngine;
using System.Collections;

public class inWater : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Rigidbody2D drag;
            movement speed;
            drag = collision.GetComponent<Rigidbody2D>();
            {
                drag.drag = 8f;
                speed = collision.GetComponent<movement>();
                speed.moveSpeed =1200;
            }
        }

    }

            public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Rigidbody2D drag;
            movement speed;
            drag = collision.GetComponent<Rigidbody2D>();
            {
                drag.drag = 0f;
                speed = collision.GetComponent<movement>();
                speed.moveSpeed = 2000;
            }
        }
    }
}
