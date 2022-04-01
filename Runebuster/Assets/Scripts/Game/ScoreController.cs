using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextUI;

    private float myScore = 0;
    private float highScore = 0;
    private float deltaScore = 0;
    private bool isUpdating = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void AddScore(float points)
    {
        highScore += points;
        isUpdating = true;

        StartCoroutine(AddPoints());
    }

    private IEnumerator AddPoints()
    {
        while (isUpdating)
        {
            deltaScore = highScore - myScore;

            if (deltaScore == 0)
            {
                isUpdating = false;
                StopCoroutine(AddPoints());
            }

            myScore += (deltaScore) / 2;

            UpdateScoreTextUI();

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void UpdateScoreTextUI()
    {
        scoreTextUI.text = ((int)myScore).ToString();
    }

    public int GetScore()
    {
        return ((int)highScore);
    }

}
