using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    //[SerializeField] private GameObject deathVFX;
    [SerializeField] private float enemyPoints;
    [SerializeField] private int runeCount = 0;

    private ScoreController myScore;
    private EnemyRuneController enemyRuneController;
    private bool isCounted = false;

    private void Start()
    {
        enemyRuneController = GetComponent<EnemyRuneController>();
        myScore = FindObjectOfType<ScoreController>();
    }

    private void Update()
    {
        InitializeRuneCount();

        EnemyDeath();
    }

    private void InitializeRuneCount()
    {
        if (!isCounted)
        {
            foreach (Transform rune in enemyRuneController.GetEnemyRunes())
            {
                if (rune.gameObject.activeSelf) runeCount++;
            }

            isCounted = true;
        }
    }

    public void LoseRunes()
    {
        runeCount--;
    }

    private void EnemyDeath()
    {
        if (runeCount <= 0)
        {
            myScore.AddScore(enemyPoints);
            //TriggerDeathVFX();
            Destroy(gameObject);
        }
    }
    /*
     private void TriggerDeathVFX()
     {
         if (!deathVFX) return;

         GameObject deathVFXHandle = Instantiate(deathVFX, transform.position, transform.rotation);
         Destroy(deathVFXHandle, 1f);
     }
    */
}
