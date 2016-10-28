using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour
{
    [HideInInspector]
    public int dmg;
    [HideInInspector]
    public bool friendly;
    public int playerID;

    public float lifetime;



    gameManager mode;
    bool gamemode;

    // Use this for initialization
    void Start()
    {
        mode = FindObjectOfType<gameManager>();
        gamemode = mode.gamemode_Pve;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destroyable")
        {
            collision.GetComponent<particleSpawn>().BarrelHP -= dmg;
            Destroy(gameObject);
        }
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<AI_health>().CurrentHP -= dmg;
            collision.GetComponent<AI_health>().splatterBlood();
            Destroy(gameObject);
        }
        if(collision.tag == "Player" && collision.GetComponent<movement>().playerID != playerID && !gamemode)
        {
            collision.GetComponent<AI_health>().CurrentHP -= dmg;
        }
    }
}
