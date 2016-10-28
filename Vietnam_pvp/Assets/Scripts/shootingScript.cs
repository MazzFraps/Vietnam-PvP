using UnityEngine;
using System.Collections;

public class shootingScript : MonoBehaviour {

    movement id;

    public int playerID;

    public bool canShoot;
    public bool shoot;
    public bool hasAuto;

    public int dmg;

    public float cd;
    public float counter;

    public int ClipBullets;
    public int clipSizeMax;

    public int reloads;
    public float reloadTime;
    private bool isReloading;



    public GameObject ProjectilePrefab;
    public Transform projectileSpawn;
    public Transform decalSpawn;


    public int projectileSpeed;

    public Light muzzle;

    public int maxbulletCurve;
    public int minbulletCurve;


    void Start()
    {
        id = GetComponent<movement>();
        playerID = id.playerID;    }

    void FixedUpdate()
    {
        if (canShoot) return;

        counter += Time.deltaTime;
        if (counter >= cd)
        {
            if (hasAuto)
            {
                counter = 0;
                canShoot = true;
            }
            else if (!hasAuto) {
                if(Input.GetAxisRaw("Fire_p"+playerID) == 0)
                {
                    canShoot = true;
                    counter = 0;
                }
            }
        }
    }

    void Update()
    {
        Shooting();



        if(Input.GetAxisRaw("Reload_p"+playerID) != 0 && reloads > 0 && !isReloading && ClipBullets < clipSizeMax)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }


        if (ClipBullets > clipSizeMax)
        {
            ClipBullets = clipSizeMax;
        }


    }

    void Shooting()
    {
        if (Input.GetAxisRaw("Fire_p"+playerID) > 0 && canShoot && ClipBullets > 0 && !isReloading)
        {
            if (hasAuto) { 
}
            Quaternion quaterninonRotation = projectileSpawn.rotation;
            quaterninonRotation *= Quaternion.Euler(0, 0, Random.Range(minbulletCurve,maxbulletCurve));
            
            var projectile2 = (GameObject)Instantiate(ProjectilePrefab, decalSpawn.transform.position, quaterninonRotation);
            Rigidbody2D projectile2D2 = projectile2.GetComponent<Rigidbody2D>();
            projectile2.GetComponent<Light>().enabled = false;
            projectile2D2.GetComponent<SpriteRenderer>().color = Color.gray;
            projectile2D2.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            projectile2D2.gravityScale = 2;
            projectile2.GetComponent<BoxCollider2D>().isTrigger = false;
            projectile2.layer = 13;
            projectile2.GetComponent<bulletScript>().lifetime = 4;
            projectile2D2.AddForce(new Vector2(-20f*transform.localScale.x, 5f), ForceMode2D.Impulse);
            



            var projectile = (GameObject)Instantiate(ProjectilePrefab, projectileSpawn.position + new Vector3(0f,0f,-4f), quaterninonRotation);
            Rigidbody2D projectile2D = projectile.GetComponent<Rigidbody2D>();
            projectile2D.velocity = projectile.transform.right * projectileSpeed*transform.localScale.x;
            projectile.GetComponent<bulletScript>().dmg = dmg;
            muzzle.enabled = true;
            projectile.GetComponent<bulletScript>().playerID = playerID;

            ClipBullets -= 1;
            canShoot = false;
            if (!shoot)
            {
                shoot = true;
                StartCoroutine(AnimPlayer());

            }

        }             
    }
    IEnumerator AnimPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        muzzle.enabled = false;
                shoot = false;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ClipBullets = clipSizeMax;
        reloads --;
        isReloading = false;
    }
}
