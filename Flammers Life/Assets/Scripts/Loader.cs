using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField] private Text timer;
    [SerializeField] private float startingTime;

    private int currentSceneIndex;

    private float currentTime, finishTime;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        timer.text = currentTime.ToString("0.0");

        if (currentTime <= 0)
        {
            TransitionScreen();
        }
    }

    public void TransitionScreen()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(3);
    }

}
