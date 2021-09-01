using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporting : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            if (teleportTarget != null) // checks if there is a position to teleport to
            {
                thePlayer.transform.position = teleportTarget.transform.position;
            }
            else // Go back to Menu
            {
                SceneManager.LoadScene(0);
            }
        }
        
    }
}
