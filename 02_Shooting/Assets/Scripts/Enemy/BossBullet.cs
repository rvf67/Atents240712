using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : RecycleObject
{
    Rigidbody2D rigid;
    float lifeTime = 20.0f;
    float moveSpeed = 5.0f;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    protected override void OnReset()
    {
        DisableTimer(lifeTime);
    }
    // �׻� �������� moveSpeed ��ŭ �̵�
    // ���� ����
    // �÷��̾�� �扟�ϸ� ������ ����Ʈ ����� ��Ȱ��ȭ
    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed*Vector2.left);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Factory.Instance.GetExplosion(transform.position);

            gameObject.SetActive(false);
        }
    }
}
