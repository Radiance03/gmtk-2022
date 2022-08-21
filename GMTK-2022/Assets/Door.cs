using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool DoorContact = false;
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
        if(DoorContact && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("LEVEL2FINAL");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && FinishedEnemies)
        {
            DoorContact = true;
            Win.SetActive(true);
            GameManage.GetComponent<GameManager>().DisableScreenInfo();
        }
    }

}
