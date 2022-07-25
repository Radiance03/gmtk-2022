using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class myButton : MonoBehaviour
{
    public GameObject menu;
    public GameObject transition;
    public Sprite pressed;
    public Sprite notPressed;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GetComponent<SpriteRenderer>().sprite = pressed;
        }
        if (Input.GetMouseButtonDown(0))
        {
            menu.GetComponent<AudioSource>().Pause();
            Instantiate(transition, Vector2.zero, Quaternion.identity);
        }
    }
 
}
