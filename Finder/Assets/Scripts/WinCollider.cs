using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCollider : MonoBehaviour
{
    int currentSceneIndex;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
