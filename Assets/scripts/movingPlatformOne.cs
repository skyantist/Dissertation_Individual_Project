﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class movingPlatformOne : MonoBehaviour
{
    public GameObject player;
    public GameObject platformOne;
    public bool movePlatformOne = true;
    public bool playerGoUp = false;

    void Update()
    {
        //Debug.Log(colliding);
        if (player.transform.position.x == -7.75f && movePlatformOne)
        {
            if (!playerGoUp)
            {
                platformOne.transform.position += Vector3.down * 0.75f * Time.deltaTime;
                player.transform.position += Vector3.down * 0.75f * Time.deltaTime;

                if (player.transform.position.y <= 6.75f)
                {
                    Debug.Log("go up now");
                    playerGoUp = true;
                }
            }

            //Move the player on the platform back and forth
            if (playerGoUp)
            {
                //Debug.Log("right");
                platformOne.transform.position += Vector3.up * 0.75f * Time.deltaTime;
                player.transform.position += Vector3.up * 0.75f * Time.deltaTime;

                if (player.transform.position.y >= 13.75f)
                {
                    Debug.Log("go down now");
                    playerGoUp = false;
                }
            }
        }
    }
}
