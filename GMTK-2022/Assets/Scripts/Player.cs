using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public BoxCollider2D HitCheckUp;

    public bool AllowedToHit = true;
    public bool DisableHitForAttack = false;
    private bool StartLastSecondCooldown = false;
    float HitCooldown = 2;
    float extraSecondAttackCooldown = 1;

    float savedCooldown;
    public int HP = 100;

    bool idleForward = false;
    private Animator anim;
    private Rigidbody2D rb;
    public float speed = 0;
    public float strength = 0;
    public float WalkSpeed;
    [HideInInspector]
    public string CurrentEmotionName = "Stable";
    public float currentEmotionCooldown;

    public bool attacking = false;
    float attackDelay = 0.8f;
    float attackDelaySave;
    public bool AllowedToAttack = true;

    float redwhiteEffect = 0.2f;

    public AudioSource kick;
    public AudioSource damage;
    public AudioSource starSound;

    public GameObject star;
    bool AllowStarCreation = true;

    public GameObject Cloud;
    void Start()
    {
       
        savedCooldown = HitCooldown;
        attackDelaySave = attackDelay;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.GetComponent<SpriteRenderer>().sortingOrder = 300 - (int)(transform.position.y * 10);

        if (!AllowedToHit && !DisableHitForAttack)
        {
            redwhiteEffect -= Time.deltaTime;
            savedCooldown -= Time.deltaTime;
            //Debug.Log(savedCooldown);
            if (savedCooldown <= 0)
            {
                AllowedToHit = true;
                savedCooldown = HitCooldown;
            }
            if (redwhiteEffect <= 0)
            {
                if (transform.GetComponent<SpriteRenderer>().color == Color.red) { transform.GetComponent<SpriteRenderer>().color = Color.white; }
                else { transform.GetComponent<SpriteRenderer>().color = Color.red; }
                redwhiteEffect = 0.1f;

            }
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;

        }
        anim.speed = WalkSpeed;

        FlipSprite();

        if (!attacking)
        {
            //enabling booleans for the next attack
            AllowStarCreation = true;

            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                if (!idleForward) { anim.Play($"Idle{CurrentEmotionName}"); }
                else { anim.Play("Idleback"); }
            }
            if (rb.velocity.x == 0)
            {
                if (rb.velocity.y > 0)
                {
                    string parseAnimator = "BackwalkStable";
                    anim.Play(parseAnimator);

                }
                else if (rb.velocity.y < 0)
                {
                    string parseAnimator = $"Frontwalk{CurrentEmotionName}";
                    anim.Play(parseAnimator);
                }
            }
            else
            {
                string parseAnimator = $"Sidewalk{CurrentEmotionName}";
                anim.Play(parseAnimator);


            }

            if (rb.velocity != Vector2.zero)
            {
                if (rb.velocity.y > 0) { idleForward = true; }
                else { idleForward = false; }
            }

        }


        if (Input.GetKeyDown(KeyCode.Space) && AllowedToAttack)
        {
            kick.Play();
            DisableHitForAttack = true;
            AllowedToAttack = false;
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 3;

            //HitCooldown = 3;

            attacking = true;
            if (rb.velocity.y == 0)
            {
                if (CurrentEmotionName == "Happy")
                {
                    anim.Play("HappyAttack");
                }
                else
                {
                    anim.Play("SideAttack");
                    if (CurrentEmotionName == "Angry")
                    {
                        anim.Play("AngrySideAttack");
                    }

                }

                if (CurrentEmotionName == "Sad")
                {
                    anim.Play("SadAttack");
                }
                if (CurrentEmotionName == "angry")
                {
                    anim.Play("AngryAttack");
                }
            }
            else
            {
                if (CurrentEmotionName == "Happy")
                {
                    anim.Play("HappyAttack");
                }
                else
                {
                    if (rb.velocity.y > 0)
                    {
                        anim.Play("ForwardAttack");
                    };
                    if (rb.velocity.y < 0)
                    {
                        anim.Play("BackwardsAttack");
                        if (CurrentEmotionName == "Angry")
                        {
                            anim.Play("AngryFrontAttack");
                        }
                    };
                }

                if (CurrentEmotionName == "Sad")
                {
                    anim.Play("SadAttack");
                }

            }


            //-------EXTRA INSTANTIATION--------
            if (CurrentEmotionName == "Sad")
            {
                var q = Instantiate(Cloud, transform.position, Quaternion.identity);
                q.GetComponent<Cloud>().foolowPlayer = true;
                Instantiate(Cloud, new Vector2(transform.position.x + 6, transform.position.y), Quaternion.identity);
                Instantiate(Cloud, new Vector2(transform.position.x + -6, transform.position.y), Quaternion.identity);
                Instantiate(Cloud, new Vector2(transform.position.x, transform.position.y + 6), Quaternion.identity);
                Instantiate(Cloud, new Vector2(transform.position.x, transform.position.y + 6), Quaternion.identity);







            }
        }
        if (attacking)
        {
            attackDelaySave -= Time.deltaTime;
            if (attackDelaySave <= 0)
            {
                StartLastSecondCooldown = true;

                attacking = false;
                attackDelaySave = attackDelay;
            }



            if (CurrentEmotionName == "Happy" && AllowStarCreation)
            {
                var q = Instantiate(star, new Vector2(transform.position.x - 1, transform.position.y - 1), Quaternion.identity);
                q.GetComponent<Rigidbody2D>().velocity = new Vector2(-7, -7);
                var v = Instantiate(star, new Vector2(transform.position.x + 1, transform.position.y + 1), Quaternion.identity);
                v.GetComponent<Rigidbody2D>().velocity = new Vector2(7, 7);
                var x = Instantiate(star, new Vector2(transform.position.x - 1, transform.position.y + 1), Quaternion.identity);
                x.GetComponent<Rigidbody2D>().velocity = new Vector2(-7, 7);
                var t = Instantiate(star, new Vector2(transform.position.x + 1, transform.position.y - 1), Quaternion.identity);
                t.GetComponent<Rigidbody2D>().velocity = new Vector2(7, -7);
                starSound.Play();



                AllowStarCreation = false;

            }

        }
        if (StartLastSecondCooldown)
        {
            extraSecondAttackCooldown -= Time.deltaTime;
            if (extraSecondAttackCooldown <= 0)
            {
                extraSecondAttackCooldown = 1;
                DisableHitForAttack = false;
                StartLastSecondCooldown = false;
            }
        }

    }

    private void FlipSprite()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.2 && !attacking)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x) * 16, transform.localScale.y, transform.localScale.z);

        }
    }
}
