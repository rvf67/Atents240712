using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;

    public float frequency = 5f;
    public float height = 3f;
    public float phase = 0f;
    float elapsedTime = 0.0f;
    float spawnY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate(Time.deltaTime);
    }

    void MoveUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
        //transform.Translate(deltaTime * moveSpeed * Vector3.left);
        transform.position = 
            new Vector3(transform.position.x - deltaTime * moveSpeed, height*MathF.Sin(phase+frequency*elapsedTime), 0f);
    }
}
