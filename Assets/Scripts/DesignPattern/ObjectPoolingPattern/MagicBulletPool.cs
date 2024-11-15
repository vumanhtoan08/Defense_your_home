using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletPool : MonoBehaviour
{
    public static MagicBulletPool instance;
    public List<GameObject> pools = new List<GameObject>();
    public GameObject magicBullet;
    public int amountToPool = 30;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(magicBullet);
            tmp.SetActive(false);
            pools.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (!pools[i].activeInHierarchy)
            {
                return pools[i];
            }
        }
        return null;
    }
}
