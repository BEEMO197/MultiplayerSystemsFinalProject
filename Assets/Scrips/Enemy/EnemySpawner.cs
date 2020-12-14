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

    }

    


    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }


    private void SpawnEnemy()
    {
        if(Time.frameCount % Random.Range(200, 700) == 0)
        {
            enemyManager.GetEnemy(new Vector3(Random.Range(-100.0f, 100.0f), 1.0f , Random.Range(-100.0f, 100.0f)));
        }
    }
}
