using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

    public float speed;
    public int hp;
    public int damage;
    public int fireRate; //frames between shots
    public float fireDeg; //degrees of randomness in arc of bullets
    public float range; //in units

    public float sightRange; //in units
    public float evasionRange; //in units

    GameObject player;

    private bool firstSpawn = true;
    private float newSpeed;
    private int newHp;
    private int newDamage;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        newSpeed = speed;
        newHp = hp;
        newDamage = damage;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 target = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, newSpeed * Time.deltaTime);
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) >= sightRange)
        {
            gameObject.transform.position = target;
        }
        else if (Vector3.Distance(gameObject.transform.position, player.transform.position) >= evasionRange)
        {
            //shoot
        }
        else
        {
            gameObject.transform.position = gameObject.transform.position - (target - gameObject.transform.position);
        }
	}
    /*
    public void increaseStats(float mult)
    {
        if (firstSpawn)
        {
            newSpeed = speed;
            newHp = hp;
            newDamage = damage;
            firstSpawn = false;
        }
        //Debug.Log(originalSpeed);
        //Debug.Log(originalSpeed * mult);
        newSpeed = speed * mult;
        newHp = (int)Mathf.Floor(hp * mult);
        newDamage = (int)Mathf.Floor(damage * mult);
    }*/
}
