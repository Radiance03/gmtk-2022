using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public bool foolowPlayer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (foolowPlayer)
        {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 2);

        }
        Destroy(gameObject, 3);
    }
}
