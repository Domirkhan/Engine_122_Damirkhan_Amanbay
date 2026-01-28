using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    public int number = 5;
    public Transform[] cubes;
    
    
    void Start()
    {
        Debug.Log("Hello!");
    }
    
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction.Normalize();

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].position = cubes[i].position + direction * Time.deltaTime;

            if (cubes[i].position.x > number)
            {
                cubes[i].position = new Vector3(number, cubes[i].position.y, cubes[i].position.z);
            }
        }
    }
}
