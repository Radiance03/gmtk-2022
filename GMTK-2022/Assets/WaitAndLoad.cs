using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitAndLoad : MonoBehaviour
{
   
    public float waitTime;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("LEVEL1FINAL");
        }

    }
}
