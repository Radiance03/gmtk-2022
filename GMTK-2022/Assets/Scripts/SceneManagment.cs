using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment :MonoBehaviour
{
    
    public  void StartGame()
    {
        SceneManager.LoadScene("LEVEL1FINAL");
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene("LEVELINFINITE");
    }
    public void LoadOptions()
    {
        SceneManager.LoadScene("MENU");
    }
    public  void QUIT()
    {
        Application.Quit();
    }
}
