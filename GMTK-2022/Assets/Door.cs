using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool FinishedEnemies = false;
    public GameObject Win;
    public GameObject EnemySpawner;
    public GameObject GameManage;


    private void Start()
    {
        
    }

    private void Update()
    {

        if (EnemySpawner.GetComponent<enemySpawner>().Waves == 5 && EnemySpawner.GetComponent<enemySpawner>().AmountOfEnemies <= 0)
        {
            FinishedEnemies = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && FinishedEnemies)
        {
            Debug.Log("meow");

            Win.SetActive(true);

            GameManage.GetComponent<GameManager>().DisableScreenInfo();

        }
    }

}
