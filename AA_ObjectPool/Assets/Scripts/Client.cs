using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject walker = ObjectPool.Instance.PullObject("Walker");
            if(walker != null)
            {
                walker.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
                walker.GetComponent<Zombie.Walker>().Walk();
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            object[] objs = GameObject.FindObjectsOfType(typeof(GameObject));

            foreach(object o in objs)
            {
                GameObject obj = (GameObject)o;

                if(obj.gameObject.GetComponent<Zombie.Walker>() != null)
                {
                    ObjectPool.Instance.PoolObject(obj);
                }
            }
        }
    }
}
