using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTargetController : MonoBehaviour
{
    [SerializeField] private Transform playerEnemyCheck;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float enemyCheckRadius;

    private LineRenderer lineRenderer;
    private Collider2D[] enemiesInProximityColliders;
    private List<(GameObject, float)> enemiesInProximityWithDistance;
    private GameObject currentEnemy;

    public GameObject CurrentEnemy { get => currentEnemy; set => currentEnemy = value; }

    private void Start()
    {
        lineRenderer = FindObjectOfType<LineRenderer>();
        lineRenderer.positionCount = 2;
        enemiesInProximityWithDistance = new List<(GameObject, float)>();
    }

    private void FixedUpdate()
    {
        enemiesInProximityColliders = Physics2D.OverlapCircleAll(playerEnemyCheck.position, enemyCheckRadius, whatIsEnemy);
    }

    private void Update()
    {
        if (!currentEnemy)
        {
            SelectClosestEnemy();
        }

            DrawLinePlayerToTarger();
    }

    private void SelectClosestEnemy()
    {
        SetAllEnemiesInProximityWithDistance();
        GetClosestEnemyInProximity();
        ClearAllEnemiesInProximityList();
    }

    private void SetAllEnemiesInProximityWithDistance()
    {
        if (enemiesInProximityColliders.Length != 0)
        {
            float distance = 0;

            for (int i = 0; i < enemiesInProximityColliders.Length; i++)
            {
                    distance = Mathf.Sqrt(Mathf.Pow((transform.position.x - enemiesInProximityColliders[i].gameObject.transform.position.x), 2) +
                                          Mathf.Pow((transform.position.y - enemiesInProximityColliders[i].gameObject.transform.position.y), 2));

                    enemiesInProximityWithDistance.Add((enemiesInProximityColliders[i].gameObject, distance));
            }
        }
    }

    private void GetClosestEnemyInProximity()
    {
        if (enemiesInProximityWithDistance.Count != 0)
        {
            enemiesInProximityWithDistance = enemiesInProximityWithDistance.OrderBy(i => i.Item2).ToList();
            currentEnemy = enemiesInProximityWithDistance[0].Item1;
        }
    }

    private void ClearAllEnemiesInProximityList()
    {
        if (enemiesInProximityWithDistance.Count != 0)
        {
            enemiesInProximityWithDistance.Clear();
        }
    }

    private void DrawLinePlayerToTarger()
    {
        if (currentEnemy)
        {
            lineRenderer.gameObject.SetActive(true);

            Vector3 p, e;

            p = transform.position;
            p.x += 5;

            e = currentEnemy.transform.position;
            e.x += 5;

            lineRenderer.SetPosition(0, p);
            lineRenderer.SetPosition(1, e);
        }
        else
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }

}