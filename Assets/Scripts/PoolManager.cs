using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Singleton

    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [Serializable]
    public struct poolData
    {
        public GameObject poolPrefab;
        public int size;
    }

    [SerializeField] private poolData[] objectiList;
    public Queue<GameObject>[] pools;

    private void Start()
    {
        pools = new Queue<GameObject>[objectiList.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new Queue<GameObject>();

            for (int i = 0; i < objectiList[index].size; i++)
            {
                pools[index].Enqueue(CreateNew(index));
            }
        }
    }

    private GameObject CreateNew(int index)
    {
        GameObject Go = Instantiate(objectiList[index].poolPrefab, transform);
        Go.gameObject.SetActive(false);
        return Go;
    }

    public GameObject Get(int index)
    {
        if (pools[index].Count > 0)
        {
            GameObject Go = pools[index].Dequeue();
            Go.SetActive(true);
            return Go;
        }
        else
        {
            return CreateNew(index);
        }
    }

    public void Release(GameObject Go, int index)
    {
        Go.SetActive(false);
        pools[index].Enqueue(Go);
    }
   
}
