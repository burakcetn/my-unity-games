using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerDeath : MonoBehaviour
{
    SceneLoader sL;

    private void Start()
    {
        sL = FindObjectOfType<SceneLoader>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerHealthController>().PlayerDeath();

        sL.LoadEndScene();
    }
}
