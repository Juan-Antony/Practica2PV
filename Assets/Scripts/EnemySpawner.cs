using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy1;
    [SerializeField]
    private float Enemy1Interval = 1.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(Enemy1Interval, Enemy1));
    }

    private IEnumerator spawnEnemy (float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(130f, 135f), Random.Range(3f, 4.5f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    public void DestroyThings (GameObject EnemySpawner)
    {
        Destroy(EnemySpawner,45);
    }
}
