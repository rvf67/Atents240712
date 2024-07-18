using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Vector3 pos;
    // Start is called before the first frame update
    public float moveSpeed = 7.0f;
    private int poolSize = 100;

    // Update is called once per frame
    void Update()
    {
        pos = Camera.main.WorldToViewportPoint(transform.position);
        //transform.position += Time.deltaTime * moveSpeed * transform.right;
        transform.Translate(Time.deltaTime * moveSpeed *Vector3.right,Space.Self);
        if (pos.x > 1f)
        {
            gameObject.SetActive(false);
        }
    }
}
