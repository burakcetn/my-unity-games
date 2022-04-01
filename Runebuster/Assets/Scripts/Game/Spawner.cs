using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 5f;

    private bool canSpawn = true;

    public bool CanSpawn { get => canSpawn; set => canSpawn = value; }

    private IEnumerator Start()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));

            SpawnAttacker(enemy);
        }
    }
    private void SpawnAttacker(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation).transform.parent = transform;
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }
}
