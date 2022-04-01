using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private float timeToWait;

    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime(timeToWait));
        }
    }

    private IEnumerator WaitForTime(float t)
    {
        yield return new WaitForSeconds(t);

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(++currentSceneIndex);
    }

}
