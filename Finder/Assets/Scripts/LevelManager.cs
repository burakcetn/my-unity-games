using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float startingTime;
    [SerializeField] private Text timer;
    
    private GameObject player, light, fov;
    
    private float currentTime, finishTime;

    private void Awake()
    {
        light = GameObject.Find("Light");
        
        player = GameObject.Find("Player");

        fov = GameObject.Find("FieldOfView");
    }

    private void Start()
    {
        currentTime = startingTime;

        Screen.SetResolution(1920, 1080, true);

        ActivateLight();
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        timer.text = currentTime.ToString("0.0");

        if(currentTime <= 0)
        {
            ActivatePlayer();

            finishTime = Mathf.Abs(currentTime);

            timer.text = finishTime.ToString("0.0");
        }
    }
    
    private void ActivatePlayer()
    {
        light.SetActive(false);
        fov.SetActive(false);

        player.SetActive(true);
    }

    private void ActivateLight()
    {
        player.SetActive(false);

        light.SetActive(true);
        fov.SetActive(true);
    }

}
