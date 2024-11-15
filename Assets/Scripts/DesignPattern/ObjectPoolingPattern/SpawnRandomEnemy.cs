using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomEnemy : MonoBehaviour
{
    List<GameObject> pools = new List<GameObject>();
    public GameObject[] enemyObjs;
    public int amountToPool;

    // thời gian spawn
    public float timeToSpawn = 5f;
    public float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(enemyObjs[Random.Range(0, enemyObjs.Length)], transform.position, Quaternion.identity);
            tmp.SetActive(false);
            pools.Add(tmp);
        }
    }

    public GameObject GetRandomObject()
    {
        spawnTimer = 0f;

        List<int> inactiveIndices = new List<int>();

        for (int i = 0; i < pools.Count; i++)
        {
            if (!pools[i].activeInHierarchy)
            {
                inactiveIndices.Add(i);
            }
        }

        if (inactiveIndices.Count > 0)
        {
            // Lấy một chỉ mục ngẫu nhiên từ danh sách các chỉ mục không hoạt động
            int randomIndex = inactiveIndices[Random.Range(0, inactiveIndices.Count)];
            return pools[randomIndex];
        }

        return null;
    }

    private void Update()
    {
        if (spawnTimer >= timeToSpawn)
        {
            GameObject enemy = GetRandomObject();

            if (enemy != null) // Kiểm tra xem enemy có null không
            {
                enemy.transform.position = transform.position;
                enemy.SetActive(true);
            }

            spawnTimer = 0; // Đặt lại spawnTimer sau khi spawn
        }

        spawnTimer += Time.deltaTime;
    }
}
