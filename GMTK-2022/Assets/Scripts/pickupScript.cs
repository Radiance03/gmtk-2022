using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour
{

    public GameObject managerObj;
    public float setTimer;
    public float timeAlive;

    System.Random rnd = new System.Random();
    
    private int minX;
    private int maxX;
    private int minY;
    private int maxY;

    private SpriteRenderer sp;
    private Color spriteColor = new Color(1,1,1,1);

    private float timeA;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation(-3,3,-2,2);
        
    }

    // Update is called once per frame
    void Update()
    {
        timeA -= Time.deltaTime;
        if(timeA <= 0) Destroy(gameObject);
        spriteColor.a = timeA/timeAlive;
        sp.color = spriteColor;
        
        //sp.color.a = timeA/timeAlive;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") 
        {
            managerObj.GetComponent<GameManager>().savedSwitch = setTimer;
            Destroy(gameObject);
        } else if (col.gameObject.tag == "Map")
        {
            spawnLocation(minX, maxX, minY, maxY);
        }
    }

    public void spawnLocation(int lx, int hx, int ly, int hy)
    {
        minX = lx;
        maxX = hx;
        minY = ly;
        maxY = hy;

        int x = maxX - minX;
        int y = maxY - minY;

        timeA = timeAlive;
        managerObj = GameObject.FindGameObjectWithTag("gameManager");
        sp = gameObject.GetComponent<SpriteRenderer>();

        gameObject.transform.position = new Vector3((float)rnd.Next(x*100 + 1)/100 - x/2, (float)rnd.Next(y*100 + 1)/100 - y/2);
        //Debug.Log((float)rnd.Next(100*lx,100*hx+1)/100);
    }
}
