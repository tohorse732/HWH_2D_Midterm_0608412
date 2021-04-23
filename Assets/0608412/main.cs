using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    player player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player").GetComponent<player>();

    }

    // Update is called once per frame
    void Update()
    {
        playerControl();
    }
    void playerControl()
    {

        if (Input.GetKey("a"))
        {
            player.move("left", 1);
        }
        if (Input.GetKey("d"))
        {
            player.move("right", 1); ;
        }

        if (Input.GetKeyDown("space"))
        {
            player.jump();
        }
        if (Input.GetKeyUp("space"))
        {
            player.fall();
        }

    }
}
