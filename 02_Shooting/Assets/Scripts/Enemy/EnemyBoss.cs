using System.Collections;
using System.Collections.Generic;
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
        StartCoroutine("BossStart");
    }

    private void Awake()
    {
        fire1= transform.GetChild(1).GetChild(0);
        fire2= transform.GetChild(1).GetChild(1);
        fire3= transform.GetChild(1).GetChild(2);
    }

    /// <summary>
    /// ������ ���۽� �����Ÿ��� �̵��ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator BossStart()
    {
        //moveDirection = new Vector2(Random.Range(areaMin.x, areaMax.x), areaMax.y).normalized;
        //while (transform.position.y<moveDirection.y)
        //{
        //    transform.Translate(Time.deltaTime * moveSpeed * moveDirection);
        //}
        
        transform.Translate(Time.deltaTime*moveSpeed*moveDirection);
        yield return new WaitForSeconds(appearTime);
        moveSpeed = 0;
        StartCoroutine("FireCoroutine");
        StopCoroutine("BossStart");
    }

    /// <summary>
    /// ������ ���� ��� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator FireCoroutine()
    {
        // �ڷ�ƾ : ���� �ð� �������� �ڵ带 �����ϰų� ���� �ð����� �����̸� �� �� ����

        while (true) // ���� ����
        {
            Factory.Instance.GetBossBullet(fire1.position);
            Factory.Instance.GetBossBullet(fire2.position);
            yield return new WaitForSeconds(bulletInterval);  // fireInterval�ʸ�ŭ ��ٷȴٰ� �ٽ� �����ϱ�
        }

        // ������ ����ñ��� ���
        // yield return null;
        // yield return new WaitForEndOfFrame();
    }
}
