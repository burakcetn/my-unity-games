using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private GameObject enemyCollisionGameObject;

    /*
        private void OnTriggerEnter2D(Collider2D collision)
        {
            enemyCollisionGameObject = collision.gameObject;

            if (enemyCollisionGameObject.GetComponent<PlayerMovementController>())
            {
                enemyCollisionGameObject.GetComponent<PlayerHealthController>().PlayerTakeDamage();
                Destroy(gameObject);
            }
        }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyCollisionGameObject = collision.gameObject;

        if (enemyCollisionGameObject.GetComponent<PlayerMovementController>())
        {
            enemyCollisionGameObject.GetComponent<PlayerHealthController>().PlayerTakeDamage();
            Destroy(gameObject);
        }
    }
}
