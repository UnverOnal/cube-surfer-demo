using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<GameObject> pooledObjects = new List<GameObject>();
    public GameObject objectToPool;

    public int amountToPool;

    private void Start() 
    {
        for(int i = 0; i < amountToPool; i++ )
        {
            ExpandPool(objectToPool);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return ExpandPool(objectToPool);
    }

    private GameObject ExpandPool(GameObject obj)
    {
        GameObject temp = Instantiate(obj);
        temp.SetActive(false);
        pooledObjects.Add(temp);

        return temp;
    }
}
