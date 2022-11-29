using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<Transform> pooledObjects;
    public Transform[] objectToPool;
    public int amountToPool;

    // Start is called before the first frame update
    void Awake()
    {
        SharedInstance = this;

    }

    //// Update is called once per frame
    //void Start()
    //{
    //    pooledObjects = new List<Transform>();
    //    Transform tmp;
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        tmp = Instantiate(objectToPool[Random.Range(0, objectToPool.Length)]);
    //        tmp.SetActive(false);
    //        pooledObjects.Add(tmp);
    //    }

    //}
    //public GameObject GetPooledObject()
    //{
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            return pooledObjects[i];
    //        }
    //    }
    //    return null;
    //}
}
