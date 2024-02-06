using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CollisionDeath : MonoBehaviour
{
    public bool firstLoop = true;
    public static bool endGame = false;
    private GameTimer GameTimer;
    private PlayerData PlayerData;

    public void Awake()
    {
        endGame = false;
        firstLoop = true;
        //subscribe to objectvisibilitychanged
        NavMeshAgentVision_v2.ObjectVisibilityChanged += OnObjectVisibilityChanged;
    }


    //Check collider with camera
    private void OnObjectVisibilityChanged(string arg1, bool arg2)
    {
        if (arg1 == this.gameObject.name)
        {
            if (arg2 && endGame == false)
            {
                endGame = true;
                // Collision with obstacle detected. Perform  action here.
                //end game
                Debug.Log("Game Over!");

                NotificationManager.Instance.AddNotification("Game Over!", 5f);


                // Load character selection scene when any button is pressed
                StartCoroutine(WaitForKeyPressAndLoadLevel());
            }
        }
    }


    
    // Check collider with laser
    private void OnTriggerEnter(Collider collision)
    {
        // Check if the collided object is in the obstacle layer.- for LASER
        if (collision.gameObject.layer == 7 && endGame == false)
        {
            endGame = true;
            // Collision with obstacle detected. Perform  action here.
            //end game
            Debug.Log("Game Over!");
            collision.enabled = false;
            NotificationManager.Instance.AddNotification("Game Over!", 5f);


            // Load character selection scene when any button is pressed
            StartCoroutine(WaitForKeyPressAndLoadLevel());
        }

    }
    private IEnumerator WaitForKeyPressAndLoadLevel()
        {

            while (firstLoop)
            {
                firstLoop = false;
                NotificationManager.Instance.AddNotification("PRESS\n ANY KEY TO RETURN TO HOME SCREEN", 1f);
                yield return new WaitForSecondsRealtime(5f);

            }

            while (!Input.anyKey)
            {

                    NotificationManager.Instance.AddNotification("PRESS\n ANY KEY TO RETURN TO HOME SCREEN", 0.01f);
                    yield return null;
                
            }

        if (Input.GetKeyDown(KeyCode.R))
        {
            NotificationManager.Instance.ClearAllNotifications();
            LoadingScreen.Instance.LoadLevel(0);

        }
        else
        {
            NotificationManager.Instance.ClearAllNotifications();
            LoadingScreen.Instance.LoadLevel(0);
        }
        }

    public void OnDestroy()
    {
        //unsubscribe to objectvisibilitychanged
        NavMeshAgentVision_v2.ObjectVisibilityChanged -= OnObjectVisibilityChanged;
    }
}