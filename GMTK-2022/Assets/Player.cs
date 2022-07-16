using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 1 -- 10 VALUES
    public float speed;
    public int strength;

    private Animator anim;
    private Rigidbody2D rb;

    public float WalkSpeed;
    [HideInInspector]
    public string CurrentEmotionName = "Stable";

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.speed = WalkSpeed;
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
      
        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x) * 16 , transform.localScale.y, transform.localScale.z);

        if(rb.velocity == Vector2.zero) {anim.Play("Idle");}

        if(rb.velocity.x == 0)
        {
            if(rb.velocity.y > 0)
            {
                string parseAnimator = "BackwalkStable";
                anim.Play(parseAnimator);

            }
            else if(rb.velocity.y < 0)
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
    }
}
