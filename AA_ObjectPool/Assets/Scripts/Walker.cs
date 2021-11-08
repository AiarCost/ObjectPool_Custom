using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zombie
{
    public class Walker : MonoBehaviour
    {
        Camera cam;

        private void Awake()
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)), .005f);
        }
        public void Walk()
        {
            Debug.Log("Zombie Spawned");
        }
    }
}

