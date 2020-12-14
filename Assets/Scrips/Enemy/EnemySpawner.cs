using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    NetworkClient connectedClient;
    NetworkObjects.NetworkPlayer connectedPlayer;

    public EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    


    // Update is called once per frame
    void Update()
    {
        //SpawnEnemy();
    }


    private IEnumerator SpawnEnemy()
    {
        if (enemyManager.getPoolSize() > 0) {
            enemyManager.GetEnemy(new Vector3(Random.Range(-100.0f, 100.0f), 1.0f, Random.Range(-100.0f, 100.0f)));
        }

        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));

    }
}
