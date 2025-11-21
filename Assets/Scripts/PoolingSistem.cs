using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSistem : MonoBehaviour
{
    public static PoolingSistem Instance;
    public List<GameObject> bulletPrefab;
    public GameObject bullet;
    public int poolSize = 20;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bulletPrefab = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolSize; i++)
        {
            tmp = Instantiate(bullet);
            tmp.SetActive(false);
            bulletPrefab.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!bulletPrefab[i].activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
    }


}
