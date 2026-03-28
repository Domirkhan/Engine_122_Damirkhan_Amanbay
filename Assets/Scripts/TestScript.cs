using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Transform[] movementObjects;
    public float speed;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        transform.position = transform.position + movementVector * speed * Time.deltaTime;
    }
}
