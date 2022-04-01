using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleLoader : MonoBehaviour
{
    private int currentSceneIndex;

    public void InstructionScreen()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(1);
    }

    public void GameScreen()
    {
        GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Week = GameObject.FindGameObjectWithTag("Profit").GetComponent<ProfitSystem>().Week - 1;

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(2);
    } 
    
    public void GameScreen2()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(2);
    }

    public void TransitionScreen()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(3);
    }

    public void LoadQuitScreen()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
