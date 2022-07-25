using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        transform.position = cam.ScreenToViewportPoint(Input.mousePosition);
    }
}
