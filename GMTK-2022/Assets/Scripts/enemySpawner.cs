using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

    public int waveTime; //in secs
    public int firstWaveDelay; //in secs
    public int spawnRadius; //in units
    List<int[]> enemyCount = new List<int[]>();
    public int waveCount;
    //if endless, wave 1 will be base wave, and enemies will add onto it.
    public int[] wave1 = new int[3]; //num of spawns of enemy1, enemy2 ...
    public int[] wave2 = new int[3];
    public int[] wave3 = new int[3];
    public GameObject[] enemies = new GameObject[3];
    public GameObject manager;
    public bool isEndless;
    public float endlessStatMultiplier;

    public Transform[] spawnPos;

    //public enemyScript[] enemyScripts = new enemyScript[4];

    private float timeLeft;
    private int i;
    private System.Random rnd = new System.Random();
    private Vector3 pos;
    private int sum;
	// Use this for initialization
	void Start () {
        timeLeft = firstWaveDelay;
        i = 0;
        enemyCount.Add(wave1);
        enemyCount.Add(wave2);
        enemyCount.Add(wave3);
        pos = gameObject.transform.position;
        for (int w = 0; w < wave1.Length; w++) sum += wave1[w];
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 && i < waveCount && !isEndless)
        {
            timeLeft = waveTime;

            for (int j = 0; j < wave1.Length; j++)
            {
                for (int k = 0; k < enemyCount[i][j]; k++)
                {
                    //Debug.Log(j);
                    Instantiate(enemies[j],new Vector2(gameObject.transform.position.x + Random.Range(-spawnRadius,spawnRadius), 
                    gameObject.transform.position.y + Random.Range(-spawnRadius, spawnRadius)), Quaternion.identity);
                }
            }
            i++;
            if(i == waveCount)
            {
                manager.GetComponent<GameManager>().lastWave = true;
                Debug.Log("true");
            }
        }
        else if (timeLeft <= 0 && isEndless)
        {
            timeLeft = waveTime;
            i++;

            if (rnd.Next(1, sum + 1) < wave1[0]) enemyCount[0][0] += 1;
            else if (rnd.Next(1, sum + 1 - wave1[0]) < wave1[1]) enemyCount[0][1] += 1;
            else if (rnd.Next(1, sum + 1 - wave1[0] - wave1[1]) < wave1[2]) enemyCount[0][2] += 1;
            else enemyCount[0][3] += 1;

            for (int j = 0; j < wave1.Length; j++)
            {
                for (int k = 0; k < enemyCount[0][j]; k++)
                {
                    //manager.GetComponent<GameManager>().enemyAmount += 1;
                    //enemyScripts[j].increaseStats(Mathf.Pow(endlessStatMultiplier, i));
                    Instantiate(enemies[j], new Vector3(pos.x + rnd.Next(spawnRadius + 1) - spawnRadius / 2,
                        pos.y + rnd.Next(spawnRadius + 1) - spawnRadius / 2), Quaternion.identity);
                    
                }
            }
        }
            
	}
}
