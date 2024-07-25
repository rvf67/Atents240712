using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundPlanet : MonoBehaviour
{
    //실습
    //배경에 랜덤한 간격으로 보이는 행성 만들기
    public float moveSpeed = 5.0f;
    float minRightEnd=30.0f;
    float maxRightEnd=50.0f;

    float minY=-1.4f;
    float maxY=-4.5f;

    float baseLineX;
    private void Start()
    {
        baseLineX = transform.position.x;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;

        int height=sprite.texture.height - (int)sprite.border.w;
    }
    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * Vector3.left);
        if (transform.position.x < baseLineX)
        {
            transform.position = new Vector3(Random.Range(minRightEnd, maxRightEnd),
                Random.Range(minY,maxY));
        }
    }
}
