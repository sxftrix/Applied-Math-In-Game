using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        public float moveSpeed = 5f;

    void Update()
    {

        float moveX = 0;
        float moveY = 0;

        if (Input.GetKey(KeyCode.W)) moveY = 1;
        if (Input.GetKey(KeyCode.S)) moveY = -1;
        if (Input.GetKey(KeyCode.A)) moveX = -1;
        if (Input.GetKey(KeyCode.D)) moveX = 1;

        float deltaX = moveX * moveSpeed * Time.deltaTime;
        float deltaY = moveY * moveSpeed * Time.deltaTime;

        transform.position = new Vector3(
            transform.position.x + deltaX,
            transform.position.y + deltaY,
            transform.position.z
        );
    }
}