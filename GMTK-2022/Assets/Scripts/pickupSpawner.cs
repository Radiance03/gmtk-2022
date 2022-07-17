using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSpawner : MonoBehaviour
{

    public GameObject[] pickups = new GameObject[2];
    public float timeBetweenSpawns;

    System.Random rnd = new System.Random();

    public int minX;
    public int maxX;
    public int minY;
    public int maxY;


    private float timeSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSpawn +=  Time.deltaTime;
        if(timeSpawn >= timeBetweenSpawns)
        {
            GameObject pickup = Instantiate(pickups[rnd.Next(0,pickups.Length)]);
            pickup.GetComponent<pickupScript>().spawnLocation(minX,maxX,minY,maxY);
            timeSpawn = 0;
        }
    }
}
