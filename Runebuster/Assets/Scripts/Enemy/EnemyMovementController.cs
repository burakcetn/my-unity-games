using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float enemyMovementSpeed;

    private Rigidbody2D enemyRigidBody2D;
    private Vector3 enemyDirection;
    private bool isEnemyFacingRight = true;
    private RuneDisplay rD;

    private void Start()
    {
        if (!player) player = FindObjectOfType<PlayerMovementController>().gameObject;

        rD = GetComponent<RuneDisplay>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isEnemyFacingRight == false && enemyDirection.x > 0)
        {
            rD.FlipRuneDisplay();
            Flip();
        }
        else if (isEnemyFacingRight == true && enemyDirection.x < 0)
        {
            rD.FlipRuneDisplay();
            Flip();
        }

        MoveEnemyTowardsPlayer();
    }

    private void Update()
    {
        enemyDirection = player.transform.position - transform.position;
        enemyDirection.Normalize();
    }

    private void MoveEnemyTowardsPlayer()
    {
        enemyRigidBody2D.MovePosition(transform.position + (enemyDirection * enemyMovementSpeed * Time.deltaTime));
    }

    private void Flip()
    {
        isEnemyFacingRight = !isEnemyFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

}
