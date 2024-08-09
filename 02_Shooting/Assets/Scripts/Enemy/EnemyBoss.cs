using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyBoss : EnemyBase
{
    [Header("���� �⺻ ������")]
    //�Ѿ��� �ֱ������� �߻� (Fire1, Fire2 ��ġ)
    //�̻����� ������ȯ�� �Ҷ� ���� ���� ��(barrageCount)��ŭ ����

    /// <summary>
    /// �Ѿ� �߻� ����
    /// </summary>
    public float bulletInterval = 1.0f;

    /// <summary>
    /// �̻��� ���� �߻綧 �߻纰 ����
    /// </summary>
    public float barrageInterval = 0.2f;
    /// <summary>
    /// �����߻� �� �߻� Ƚ��
    /// </summary>
    public int barrageCount = 3;

    /// <summary>
    /// ó�� �������� ���� �ð�
    /// </summary>
    public float appearTime = 2.0f;
    /// <summary>
    /// ���� Ȱ������ �ּ� ��ġ
    /// </summary>
    public Vector2 areaMin = new Vector2(2, -3);
    /// <summary>
    /// ���� Ȱ������ �ִ� ��ġ
    /// </summary>
    public Vector2 areaMax = new Vector2(7, 3);

    /// <summary>
    /// �Ѿ˹߻� ��ġ 1
    /// </summary>
    Transform fire1;
    /// <summary>
    /// �Ѿ˹߻� ��ġ 2
    /// </summary>
    Transform fire2;
    /// <summary>
    /// �Ѿ˹߻� ��ġ 3(�̻���)
    /// </summary>
    Transform fire3;

    /// <summary>
    /// ���� �̵���ġ
    /// </summary>
    Vector3 moveDirection = Vector3.left;
    protected override void OnReset()
    {
        base.OnReset();
        StartCoroutine(MovePaternProcess());
    }

    

    private void Awake()
    {
        fire1= transform.GetChild(1).GetChild(0); //�� 3���� �Ѿ� �߻���ġ�� ã��
        fire2= transform.GetChild(1).GetChild(1);
        fire3= transform.GetChild(1).GetChild(2);
    }

    protected override void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * moveSpeed * moveDirection); //�׻� movedirection�������� �̵�
    }

    /// <summary>
    /// ������ ���� ��� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator FireCoroutine()
    {
        while (true) // ���� ����
        {
            Factory.Instance.GetBossBullet(fire1.position);
            Factory.Instance.GetBossBullet(fire2.position);
            yield return new WaitForSeconds(bulletInterval);  // fireInterval�ʸ�ŭ ��ٷȴٰ� �ٽ� �����ϱ�
        }

    }

    IEnumerator MovePaternProcess()
    {
        moveDirection = Vector3.left;

        yield return null;          // Ǯ���� ������ �� OnReset�� ���� ����� �� ��ġ ������ �ϱ� ������,
                                    // ��ġ ���� ���Ŀ� �Ʒ� �ڵ尡 ����ǵ��� �������� ���

        float middleX = (areaMax.x - areaMin.x) *0.5f +areaMin.x;
        while(transform.position.x >middleX)
        {
            yield return null; //������ x ��ġ�� middleX�� ������ �ɶ����� ���
        }
        StartCoroutine(FireCoroutine());
        ChangeDirection(); //�ϴ� �Ʒ��� �̵�
        while (true)
        {
            if(transform.position.y >areaMax.y || transform.position.y< areaMin.y)
            {
                ChangeDirection();
                StartCoroutine(FIreMissileCorutine());
            }
            yield return null;
        }
    }
    IEnumerator FIreMissileCorutine()
    {
        for(int i = 0;i<barrageCount;i++)
        {
            Factory.Instance.GetBossMissile(fire3.position); //���� �߻� ������ŭ ����
            yield return new WaitForSeconds(barrageInterval);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 p0 = new(areaMin.x, areaMin.y);
        Vector3 p1 = new(areaMax.x, areaMin.y);
        Vector3 p2 = new(areaMax.x, areaMax.y);
        Vector3 p3 = new(areaMin.x, areaMax.y);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);

    }

    /// <summary>
    /// ������ �̵� ������ �����ϴ� �Լ�(�̵� ������ ���� ���� ���� ����Ǿ�� ��)
    /// </summary>
    void ChangeDirection()
    {
        Vector3 target = new Vector3();
        target.x = Random.Range(areaMin.x, areaMax.x);  //x��ġ�� areaMin.x ~ areaMax.x ����
        target.y = (transform.position.y > areaMax.y) ? areaMin.y : areaMax.y; //areaMax���� ���� ������ �Ʒ���, �ƴϸ� ����

        moveDirection =(target - transform.position).normalized; //���� ����(target���� ���� ����)
    }
}
