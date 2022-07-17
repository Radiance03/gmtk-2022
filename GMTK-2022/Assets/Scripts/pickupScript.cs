using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour
{

    public GameObject managerObj;
    public float setTimer;
    public float timeAlive;

    System.Random rnd = new System.Random();
    
    private int _minX;
    private int _maxX;
    private int _minY;
    private int _maxY;

    private SpriteRenderer sp;
    private Color spriteColor = new Color(1,1,1,1);

    private float xconst = 0.5f; //dont know why these are needed please kill me
    private float yconst = 2.2f; //aaaahhhhhhhhhhhh

    private float timeA;
    // Start is called before the first frame update
    void Start()
    {
        //spawnLocation(-3,3,-2,2);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        timeA -= Time.deltaTime;
        if(timeA <= 0) Destroy(gameObject);
        spriteColor.a = timeA/(timeAlive*2) + 0.5f;
        sp.color = spriteColor;
        
        //sp.color.a = timeA/timeAlive;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player") 
        {
            managerObj.GetComponent<GameManager>().savedSwitch = setTimer;
            Destroy(gameObject);
        } else if (col.gameObject.tag == "map")
        {
            spawnLocation(_minX, _maxX, _minY, _maxY);
        }
    }

    public void spawnLocation(int lx, int hx, int ly, int hy)
    {
        _minX = lx;
        _maxX = hx;
        _minY = ly;
        _maxY = hy;

        int x = _maxX - _minX;
        int y = _maxY - _minY;

        //Debug.Log("x " + ((float)rnd.Next(x*100 + 1)/50 - (x/2)));

        timeA = timeAlive;
        managerObj = GameObject.FindGameObjectWithTag("gameManager");
        sp = gameObject.GetComponent<SpriteRenderer>();

        gameObject.transform.position = new Vector3((float)rnd.Next(x*100 + 1)/100 - x/2 + xconst, (float)rnd.Next(y*100 + 1)/100 - y/2 + yconst, -1);
        //Debug.Log((float)rnd.Next(100*lx,100*hx+1)/100);
    }
}
