using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    private  GameObject EnemySpawn;
    private GameObject GameManage;
    public float HP = 10;
    public float speed;
    public int damage;
    public int fireRate; //frames between shots
    public float fireDeg; //degrees of randomness in arc of bullets
    public float range; //in units
   

    public float sightRange; //in units
    public float evasionRange; //in units
    BoxCollider2D Col;
    Vector2 ColSize;



    GameObject player;

 
    bool reachedMid = true;
    Vector2 randPos;
    float cooldown = 6;
    Vector2 Direction;


    Rigidbody2D rb;
    Animator anim;
   


    // Use this for initialization
    void Start () {
        EnemySpawn = GameObject.Find("Spawner");
        Col = GetComponent<BoxCollider2D>();
        ColSize = Col.size;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.Find("Player");
     
        randPos = new Vector3(0,0 );
         Direction = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0) - gameObject.transform.position;
        GameManage = GameObject.Find("GameManager");
        GameManage.GetComponent<GameManager>().enemyAmount += 1;

    }

    // Update is called once per frame
    void Update () {

        Col.size = ColSize;
        transform.GetComponent<SpriteRenderer>().sortingOrder =300 -(int)(transform.position.y * 10) ;
        transform.GetComponent<SpriteRenderer>().color = new Color(HP/10, HP/10, HP/10, 1);


        if (!AlreadyDamaged)
        {
            if (reachedMid)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 1.3f)
                {
                    Vector2 Direction = player.transform.position - gameObject.transform.position;
                    rb.velocity = Direction.normalized * speed;
                }
                else
                {
                    //rb.velocity = Vector2.zero;

                    if (!player.GetComponent<Player>().DisableHitForAttack)
                    {
                        if (player.GetComponent<Player>().AllowedToHit)
                        {
                            player.GetComponent<Player>().HP -= damage;
                            player.GetComponent<Player>().AllowedToHit = false;
                            player.GetComponent<Player>().damage.Play();
                        }
                    }


                }


            }
            else
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0) { reachedMid = true; }
                rb.velocity = Direction.normalized * speed;



            }
        }
      

        if(Mathf.Abs(rb.velocity.y) > 0.3)
        {

            if(player.transform.position.y > transform.position.y  ) { anim.Play("enemyForward"); }
            else { anim.Play("enemyBackwards"); }

        }
        else
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x) * 0.5f, 0.5f, 1);

            anim.Play("enemySide");
        }


        if (AlreadyDamaged)
        {
            Col.size = Vector2.zero;
            Delay -= Time.deltaTime;
          
            if (Delay <= 0)
            {
                Delay = 0.5f;
                AlreadyDamaged = false;
            }
            rb.velocity = -(player.transform.position - transform.position).normalized * 5;
        }


        Debug.Log(AlreadyDamaged);

    }

    bool AlreadyDamaged = false;
    float Delay = 0.5f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!AlreadyDamaged)
        {
            if (collision.gameObject.tag == "Player" && player.GetComponent<Player>().attacking)
            {
                HP -= GameManage.GetComponent<GameManager>().currentEmotion.STRENGTH;
                if (HP <= 0)
                {

                    EnemySpawn.GetComponent<enemySpawner>().AmountOfEnemies--;
                    GameManage.GetComponent<GameManager>().enemiesDestroyed++;
                    GameManage.GetComponent<GameManager>().EnemyDeath.Play();
                    GameManage.GetComponent<GameManager>().enemyAmount -= 1;
                    Destroy(gameObject);

                }
                AlreadyDamaged = true;
                


            }
           
        }
        
    }


}
