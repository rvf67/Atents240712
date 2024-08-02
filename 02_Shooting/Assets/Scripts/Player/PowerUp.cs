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
    // 6. ������ ��ȯ�� ������ �� ������ �����Ÿ���.

    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float moveSpeed = 2.0f;

    /// <summary>
    /// ���� ��ȯ�Ǵ� �ð� ����
    /// </summary>
    public float directionChangeInterval = 3.0f;

    /// <summary>
    /// ���� ��ȯ�� ������ �ִ� ȸ��
    /// </summary>
    public int directionChangeMaxCount = 5;
    Animator animator;

    readonly int Count_Hash = Animator.StringToHash("Count");
    [Range(0f, 1f)]
    /// <summary>
    /// �÷��̾�κ��� ����ĥ Ȯ��
    /// </summary>
    public float fleeChange = 0.7f;

    /// <summary>
    /// �������� �Ծ��� �� �ְ�ܰ��� �����̶�� ���ʽ������� ����
    /// </summary>
    public const int BonusPoint = 1000;
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

    int DirectionChangeCount
    {
        get => directionChangeCount;
        set
        {
            directionChangeCount = value;
            animator.SetInteger(Count_Hash, directionChangeCount);
            StopAllCoroutines(); //���� �ڷ�ƾ ���ſ�(���� �ε������� ���),������ �ǹ� ������

            // DirectionChange �ڷ�ƾ ����
            //������ȯȰ ȸ���� �����ְ�, Ȱ��ȭ�Ǿ�������
            if (directionChangeCount > 0 && gameObject.activeSelf)
            {
                StartCoroutine(DirectionChange());
            }
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
        animator = GetComponent<Animator>();
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

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���� �ð� �Ŀ� ������ ��ȯ�ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator DirectionChange()
    {
        yield return new WaitForSeconds(directionChangeInterval);
        float randomAngle = Random.Range(-90, 90);
        Vector2 playerToItem = (playerTransform.position-transform.position).normalized;
        if (Random.value < fleeChange  /*fleeChangeȮ���� ���� ó��*/)
        {
            // �÷��̾�� �־����� �������� ����
            direction = Quaternion.Euler(0, 0, randomAngle)*playerToItem;
            
        }
        else
        {
            // �÷��̾�� ��������� �������� ����
            direction = Quaternion.Euler(0,0,randomAngle)*-playerToItem;
        }
        //direction;    // ���� ���� ����
        //direction = Random.insideUnitCircle.normalized; //�ӽ�

        DirectionChangeCount--;     // ���� ��ȯ ȸ�� ����
    }
}
