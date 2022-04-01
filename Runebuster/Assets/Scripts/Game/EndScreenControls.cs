using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenControls : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private ScoreController myScoreController;

    void Start()
    {
        myScoreController = FindObjectOfType<ScoreController>();

        scoreText.text = myScoreController.GetScore().ToString();
    }

}
