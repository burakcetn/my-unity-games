using System.Collections.Generic;
using UnityEngine;

public class EnemyRuneController : MonoBehaviour
{
    [SerializeField] private GameObject[] runes;
    [SerializeField] private int enemyNumberOfRunes;

    private Transform runesChildObject;
    private EnemyHealthController enemyHealth;
    private RuneDisplay runeDisplay;
    private GameObject randomRuneTemp;

    private void Awake()
    {
        runesChildObject = transform.GetChild(1);
        runeDisplay = GetComponent<RuneDisplay>();
        enemyHealth = GetComponent<EnemyHealthController>();

        AddRandomRunesToEnemy();
    }

    private void AddRandomRunesToEnemy()
    {
        if (runes.Length == 0) return;

        int randomIndex = 0;
        int pRandomIndex = 0;

        for (int i = 0; i < enemyNumberOfRunes; i++)
        {
            randomIndex = Random.Range(0, runes.Length);

            if (pRandomIndex == randomIndex)
            {
                i--;
                continue;
            }

            pRandomIndex = randomIndex;

            randomRuneTemp = Instantiate(runes[randomIndex]);
            randomRuneTemp.transform.parent = runesChildObject;
        }
    }

    public List<Transform> GetEnemyRunes()
    {
        List<Transform> enemyCurrentRunes = new List<Transform>();

        foreach (Transform rune in runesChildObject)
        {
            enemyCurrentRunes.Add(rune);
        }

        return enemyCurrentRunes;
    }

    public void DestroyRunes(char runeName)
    {
        foreach (Transform rune in GetEnemyRunes())
        {
            if (runeName == 'Q' && rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune Q(Clone)"))
            {
                rune.gameObject.SetActive(false);
                enemyHealth.LoseRunes();
                runeDisplay.DeactivateSpecificRune(runeName);
            }
            else if (runeName == 'W' && rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune W(Clone)"))
            {
                rune.gameObject.SetActive(false);
                enemyHealth.LoseRunes();
                runeDisplay.DeactivateSpecificRune(runeName);
            }
            else if (runeName == 'E' && rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune E(Clone)"))
            {
                rune.gameObject.SetActive(false);
                enemyHealth.LoseRunes();
                runeDisplay.DeactivateSpecificRune(runeName);
            }
            else if (runeName == 'R' && rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune R(Clone)"))
            {
                rune.gameObject.SetActive(false);
                enemyHealth.LoseRunes();
                runeDisplay.DeactivateSpecificRune(runeName);
            }
        }
    }

}
