using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : RecycleObject
{
    // 1. Ǯ���� �����ȴ�.
    // 2. ������ �������� �̵��Ѵ�
    // 3. ���� �ð� ���� ������ ��ȯ�ȴ�.(������ Ȯ���� �÷��̾�� �־����� �������� ��ȯ�Ǿ�� �Ѵ�.)
    // 4. ������ �ε��ĵ� ������ ��ȯ�ȴ�.
    // 5. ���� ȸ�� �̻� ������ ��ȯ�Ǹ� �� �̻� ������ ��ȯ���� �ʴ´�.

    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float moveSpeed = 2.0f;

    /// <summary>
    /// ���� ��ȯ�Ǵ� �ð� ����
    /// </summary>
    public float directionChangeInterval = 1.0f;

    /// <summary>
    /// ���� ��ȯ�� ������ �ִ� ȸ��
    /// </summary>
    public int directionChangeMaxCount = 5;

    /// <summary>
    /// �÷��̾�κ��� ����ĥ Ȯ��
    /// </summary>
    public float fleeChange = 0.7f;

    /// <summary>
    /// ���� ���� ���� ȸ��
    /// </summary>
    int directionChangeCount = 0;

    /// <summary>
    /// ���� �̵� ����
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// �÷��̾��� Ʈ������
    /// </summary>
    Transform playerTransform;

    /// <summary>
    /// ���� �ð� �Ŀ� ������ ��ȯ�ϴ� �ڷ�ƾ ����� ����
    /// </summary>
    IEnumerator directionChangeCoroutine;

    int DirectionChangeCount
    {
        get => directionChangeCount;
        set
        {
            directionChangeCount = value;

            StopCoroutine(directionChangeCoroutine);

            // DirectionChange �ڷ�ƾ ����
            StartCoroutine(directionChangeCoroutine);
        }
    }

    protected override void OnReset()
    {
        playerTransform = GameManager.Instance.Player.transform;
        direction = Vector3.zero;
        DirectionChangeCount = directionChangeMaxCount;
    }

    private void Awake()
    {
        directionChangeCoroutine = DirectionChange();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ������ȯ ȸ���� �����ְ�, �ε�ģ ����� ������� ó��
        if (DirectionChangeCount > 0 && collision.gameObject.CompareTag("Border"))
        {
            // ���� ��ȯ
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);   // �ݻ�� ���͸� ���ο� �������� ����
            DirectionChangeCount--;     // ���� ��ȯ ȸ�� ����
        }
    }

    /// <summary>
    /// ���� �ð� �Ŀ� ������ ��ȯ�ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator DirectionChange()
    {
        yield return new WaitForSeconds(directionChangeInterval);

        if (true /*fleeChangeȮ���� ���� ó��*/)
        {
            // �÷��̾�� �־����� �������� ����
        }
        else
        {
            // �÷��̾�� ��������� �������� ����
        }
        //direction;    // ���� ���� ����
        DirectionChangeCount--;     // ���� ��ȯ ȸ�� ����
    }
}
