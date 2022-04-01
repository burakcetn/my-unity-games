using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Slider cooldownSlider;
    [SerializeField] private RectTransform Qui, Wui, Eui, Rui;

    private PlayerTargetController targetController;
    private EnemyRuneController targetsRuneController;
    private int runeCounter;
    private bool canCastRune;

    private void Start()
    {
        runeCounter = 0;
        canCastRune = true;
        targetController = GetComponent<PlayerTargetController>();
    }

    private void Update()
    {
        if (targetController.CurrentEnemy)
        {
            targetsRuneController = targetController.CurrentEnemy.GetComponent<EnemyRuneController>();

            if (canCastRune)
            {
                AttackCurrentEnemy();
                StartCoroutine(IncurRuneCooldown());
            }
        }
    }

    private void AttackCurrentEnemy()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            runeCounter++;
            ActivateSpecificRuneUI('Q');
            targetsRuneController.DestroyRunes('Q');
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            runeCounter++;
            ActivateSpecificRuneUI('W');
            targetsRuneController.DestroyRunes('W');
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            runeCounter++;
            ActivateSpecificRuneUI('E');
            targetsRuneController.DestroyRunes('E');
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            runeCounter++;
            ActivateSpecificRuneUI('R');
            targetsRuneController.DestroyRunes('R');
        }
    }

    private IEnumerator IncurRuneCooldown()
    {
        if (runeCounter >= 2)
        {
            canCastRune = false;

            for (int i = 0; i < 50; i++)
            {
                yield return new WaitForSeconds(0.01f);

                cooldownSlider.value = i;
            }

            runeCounter = 0;
            canCastRune = true;

            DeactivateAllRunes();
        }
    }

    public void ActivateSpecificRuneUI(char runeName)
    {
        switch (runeName)
        {
            case 'Q': Qui.gameObject.SetActive(true); break;
            case 'W': Wui.gameObject.SetActive(true); break;
            case 'E': Eui.gameObject.SetActive(true); break;
            case 'R': Rui.gameObject.SetActive(true); break;
        }
    }

    public void DeactivateAllRunes()
    {
        Qui.gameObject.SetActive(false);
        Wui.gameObject.SetActive(false);
        Eui.gameObject.SetActive(false);
        Rui.gameObject.SetActive(false);
    }

}
