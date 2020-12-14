using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyManager : MonoBehaviour
{

    public GameObject enemy;
    public int maxEnemies;

    private Queue<GameObject> enemyPool;
    // Start is called before the first frame update
    void Start()
    {
        BuildEnemyPool();
    }

    private void BuildEnemyPool()
    {
        enemyPool = new Queue<GameObject>();

        for(int i = 0; i < maxEnemies; i++)
        {
            var tempEnemy = Instantiate(enemy);
            tempEnemy.SetActive(false);
            tempEnemy.transform.parent = transform;
            enemyPool.Enqueue(tempEnemy);
        }
    }

    public GameObject GetEnemy(Vector3 position)
    {
        if(getPoolSize() >= 0)
        {
            var newEnemy = enemyPool.Dequeue();
            newEnemy.SetActive(true);
            newEnemy.transform.position = position;

            return newEnemy;
        }
        else
        {
            Debug.Log("Queue is empty");
            return null;
        }

        
    }

    public void ReturnEnemy(GameObject returnedEnemy)
    {
        returnedEnemy.SetActive(false);
        enemyPool.Enqueue(returnedEnemy);
    }

    public int getPoolSize()
    {
        return enemyPool.Count;
    }
}
