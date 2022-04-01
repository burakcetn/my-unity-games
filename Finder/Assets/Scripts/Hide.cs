using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    GameObject[] walls;
    Vector3 temp;

    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    void Update()
    {
        foreach (GameObject wall in walls)
        {
            if (wall.transform.position.z != -10f)
            {
                temp = new Vector3(wall.transform.position.x, wall.transform.position.y, -10f);

                wall.transform.position = temp;
            }
        }
    }
}
