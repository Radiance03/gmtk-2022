using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    bool idleForward = false;
    private Animator anim;
    private Rigidbody2D rb;
    public float speed = 0;
    public float strength = 0;
    public float WalkSpeed;
    [HideInInspector]
    public string CurrentEmotionName = "Stable";
    public float currentEmotionCooldown;

    bool attacking = false;
    float attackDelay = 0.6f;
    float attackDelaySave;
    public bool AllowedToAttack = true;


    public GameObject star;
    bool AllowStarCreation = true;

    public GameObject Cloud;
    void Start()
    {
        attackDelaySave = attackDelay;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.speed = WalkSpeed;
      
        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x) * 16 , transform.localScale.y, transform.localScale.z);

        if (!attacking)
        {
            //enabling booleans for the next attack
            AllowStarCreation = true;

            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

            if (rb.velocity == Vector2.zero)
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
            AllowedToAttack = false;
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            attacking = true;
            if(rb.velocity.y == 0)
            {
                if(CurrentEmotionName == "Happy")
                {
                    anim.Play("HappyAttack");
                }
                else
                {
                    anim.Play("SideAttack");

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
                    if (rb.velocity.y > 0) { anim.Play("ForwardAttack"); };
                    if (rb.velocity.y < 0) { anim.Play("BackwardsAttack"); };
                }
              
            }


            //-------EXTRA INSTANTIATION--------
            if(CurrentEmotionName == "Sad")
            {
                Instantiate(Cloud, transform.position, Quaternion.identity);
            }
        }
        if (attacking)
        {
            attackDelaySave -= Time.deltaTime;
            if(attackDelaySave <= 0)
            {
                attacking = false;
                attackDelaySave = attackDelay;
            }



            if(CurrentEmotionName == "Happy" && AllowStarCreation)
            {
                var q = Instantiate(star, new Vector2(transform.position.x - 1, transform.position.y - 1), Quaternion.identity);
                q.GetComponent<Rigidbody2D>().velocity = new Vector2(-7, -7);
                var v = Instantiate(star, new Vector2(transform.position.x + 1, transform.position.y + 1),Quaternion.identity);
                v.GetComponent<Rigidbody2D>().velocity = new Vector2(7, 7);
                var x = Instantiate(star, new Vector2(transform.position.x - 1, transform.position.y + 1), Quaternion.identity);
                x.GetComponent<Rigidbody2D>().velocity = new Vector2(-7, 7);
                var t = Instantiate(star, new Vector2(transform.position.x + 1, transform.position.y - 1), Quaternion.identity);
                t.GetComponent<Rigidbody2D>().velocity = new Vector2(7, -7);

                AllowStarCreation = false;

            }
        }
        
    }
}
