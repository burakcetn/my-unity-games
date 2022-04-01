using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private Text livesText;
    [SerializeField] private int playerLives;
    private SceneLoader sL;

    private void Start()
    {
        livesText.text = playerLives.ToString();

        sL = FindObjectOfType<SceneLoader>();
    }

    public void PlayerTakeDamage()
    {
        if (playerLives > 0)
        {
            playerLives--;
        }
        
        if(playerLives <= 0)
        {
            PlayerDeath();
        }

        livesText.text = playerLives.ToString();
    }

    public void PlayerDeath()
    {
        sL.LoadEndScene();   
    }

}
