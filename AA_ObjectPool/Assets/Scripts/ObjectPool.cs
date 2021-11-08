using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    // The objects to pool
    public GameObject[] objects;

    //The list of pooled objects
    public List<GameObject>[] pooledObjects;

    // the amount of objects to buffer
    public int[] amountToBuffer;

    public int defaultBufferAmount = 3;

    //The container of pooled objects
    protected GameObject containerObject;


    //Observer pattern to allow text to state when there are no more objects to spawn
    public delegate void NoMoreEnemies();
    public static event NoMoreEnemies OnNoMoreEnemies;


    private void Start()
    {
        //Creates the list and object pool data and will create all the objects now 
        containerObject = new GameObject("ObjectPool");
        pooledObjects = new List<GameObject>[objects.Length];
        int i = 0;

        foreach(GameObject obj in objects)
        {
            pooledObjects[i] = new List<GameObject>();

            int bufferAmount;

            if (i < amountToBuffer.Length)
            {
                bufferAmount = amountToBuffer[i];
            }
            else
            {
                bufferAmount = defaultBufferAmount;
            }

            for(int n = 0; n< bufferAmount; n++)
            {
                GameObject newobj = Instantiate(obj) as GameObject;
                newobj.name = obj.name;
                PoolObject(newobj);
            }

            i++;
        }
    }

    // Pull an object of an specific type from the pool
    public GameObject PullObject(string objectType)
    {
        //Looks through the the collection of the pool and check if there are any with the same name that we are trying to spawn
        bool onlyPooled = false;
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject prefab = objects[i];


            if(prefab.name == objectType)
            {
                //if there is a match and there is still objects left in the pool... spawn
                if (pooledObjects[i].Count > 0)
                {
                    GameObject pooledObject = pooledObjects[i][0];
                    pooledObject.SetActive(true);
                    pooledObject.transform.parent = null;

                    pooledObjects[i].Remove(pooledObject);

                    return pooledObject;
                }

                //else let user know they cannot spawn more right now
                else if (!onlyPooled)
                {
                    OnNoMoreEnemies();
                }

                break;
            }
        }

        //Null if there's a hit miss
        return null;
    }

    //Add object of a specific type to the pool
    public void PoolObject(GameObject obj)
    {
        //Takes the objects that are in the scene and will despawn all of them
        //They will get added to the object pool once this is completed
        for(int i = 0; i< objects.Length; i++)
        {
            if(objects[i].name == obj.name)
            {
                obj.SetActive(false);
                obj.transform.parent = containerObject.transform;
                pooledObjects[i].Add(obj);
                return;
            }
        }

        Destroy(obj);
    }
}
