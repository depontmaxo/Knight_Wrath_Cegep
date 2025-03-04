using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab1 = default;
    [SerializeField] private GameObject _enemyPrefab2 = default;
    [SerializeField] private GameObject _enemyPrefab3 = default;
    [SerializeField] private GameObject _enemyContainer = default;
    [SerializeField] private Transform _boss = default;
    private bool _stopSpawning = false;
    private float spawnTime = 3f;
   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(spawnTime);
        bool gotHit = false;

        while (!gotHit)
        {

            if (_boss.GetComponent<Ennemy>().gotHit() == true)
            {
                gotHit = true;
                while (!_stopSpawning)
                {
                    float randomNumberEnemy = Random.Range(0, 100);
                    float randomDistX = Random.Range(5, 10);
                    float randomDistZ = Random.Range(5, 10);

                    Vector3 spawnPosition = new Vector3(randomDistX, 0, randomDistZ) + _boss.position;
                    if (randomNumberEnemy <= 60)
                    {
                        GameObject newEnemy = Instantiate(_enemyPrefab1, spawnPosition, Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                    }
                    else if (randomNumberEnemy <= 90)
                    {
                        GameObject newEnemy = Instantiate(_enemyPrefab2, spawnPosition, Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                    }
                    else if (randomNumberEnemy < 100)
                    {
                        GameObject newEnemy = Instantiate(_enemyPrefab3, spawnPosition, Quaternion.identity);
                        newEnemy.transform.parent = _enemyContainer.transform;
                    }
                    yield return new WaitForSeconds(spawnTime);

                }
            }
        }
    }
}
