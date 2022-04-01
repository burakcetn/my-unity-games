using System.Collections.Generic;
using UnityEngine;

public class RuneDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform Q, W, E, R;

    private List<Transform> enemyRunesToBeDisplayed;
    private bool areRunesloaded = false;

    private void Start()
    {
        enemyRunesToBeDisplayed = GetComponent<EnemyRuneController>().GetEnemyRunes();

        DeactivateAllRunesDisplay();
    }

    private void Update()
    {
        UpdateRunesDisplayOnce();
    }

    private void DeactivateAllRunesDisplay()
    {
        Q.gameObject.SetActive(false);
        W.gameObject.SetActive(false);
        E.gameObject.SetActive(false);
        R.gameObject.SetActive(false);
    }

    public void DeactivateSpecificRune(char runeName)
    {
        switch (runeName)
        {
            case 'Q': Q.gameObject.SetActive(false); break;
            case 'W': W.gameObject.SetActive(false); break;
            case 'E': E.gameObject.SetActive(false); break;
            case 'R': R.gameObject.SetActive(false); break;
        }
    }

    private void UpdateRunesDisplayOnce()
    {
        if (!areRunesloaded)
        {
            foreach (Transform rune in enemyRunesToBeDisplayed)
            {
                if (rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune Q(Clone)")) Q.gameObject.SetActive(true);
                if (rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune W(Clone)")) W.gameObject.SetActive(true);
                if (rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune E(Clone)")) E.gameObject.SetActive(true);
                if (rune.gameObject.activeSelf && rune.gameObject.name.Equals("Rune R(Clone)")) R.gameObject.SetActive(true);
            }

            areRunesloaded = true;
        }
    }

    public void FlipRuneDisplay()
    { 
        Vector3 t;
        t = gameObject.transform.GetChild(2).localScale;
        t.x *= -1;
        gameObject.transform.GetChild(2).localScale = t;
    }

}
