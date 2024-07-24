using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ Ǯ
//Ư�� ������ ������Ʈ�� ���� ������ �뷮���� �����ϰ� ��û�޾��� �� �ϳ��� �����ϴ� Ŭ����
public class ObjectPool<T> : MonoBehaviour where T : RecycleObject //T�� �ݵ�� Recycleobject�� ��ӹ��� ������Ʈ��.
{
    public GameObject prefab;

    /// <summary>
    /// Ǯ�� ũ��. ó���� �����ϴ� ������Ʈ�� ����(ũ��� 2^n�� ����)
    /// </summary>
    public int poolSize=64;

    /// <summary>
    /// ������ ��� ������Ʈ�� ����ִ� �迭(TŸ������ �ؼ� �پ��� ������Ʈ�� ����)
    /// </summary>
    T[] pool;

    /// <summary>
    /// ���� ��밡���� ������Ʈ���� �����ϴ� ť(pool �迭���� ��Ȱ��ȭ �Ǿ� �ִ� ������Ʈ�� ����ִ� �ڷᱸ��)
    /// </summary>
    Queue<T> readyQueue;

    public void Initialized()
    {
        if (pool == null)
        {
            //������ ó�� Ǯ�� ����� ���. Ǯ ����
            pool = new T[poolSize];
            readyQueue = new Queue<T>(poolSize);    // �ڷᱸ���� ����� �� �ִ� ũ�⸦ �˰� �ִٸ� capacity�� �����ϴ�  ���� ����.

            GenereateObjects(0, poolSize, pool);
        }
        else
        {
            //Ǯ�� �̹� ������� �ִ»�Ȳ(���� �ٽ� ���۵ǰų� �ٸ� ���� �߰��� �ε��ǰų� ���)
            foreach(T obj in pool)
            {
                obj.gameObject.SetActive(false);
            }
        }
        
    }


    /// <summary>
    /// Ǯ���� ����� ������Ʈ���� �����ϴ� �Լ�
    /// </summary>
    /// <param name="start">���� ���� ������ �ε���</param>
    /// <param name="end">���� ������ ������ �ε���+1</param>
    /// <param name="result">������ ������Ʈ�� �� �迭(�Է� �� ��¿�)</param>
    void GenereateObjects(int start,int end,T[] result)
    {
        for(int i = start; i < end; i++)
        {
            GameObject obj = Instantiate(prefab,transform); //pool�� �ڽ����� ������Ʈ ����
            obj.name = $"{prefab.name}_{i}";

            T comp = obj.GetComponent<T>();
            comp.onDisable += () =>                 //�����Լ��� comp�� onDisable ��������Ʈ�� ���
            { 
                readyQueue.Enqueue(comp);           //����ť�� ������Ʈ �߰��� ����
            };

            result[i] = comp;       //�迭�� ������� ���� ��� ����
            obj.SetActive(false);   //��Ȱ��ȭ ��Ű��
        }
    }

    //void DisableAction()
    //{
    //    readyQueue.Enqueue(comp); //�������� ���� �ʴ�.
    //}

    /// <summary>
    /// Ǯ���� ��Ȱ�� ���� ������Ʈ�� �ϳ� �����ϴ� �Լ�
    /// </summary>
    /// <param name="position">��ġ�� ��ġ</param>
    /// <param name="eulerAngle">�ʱ� ȸ��</param>
    /// <returns>Ȱ��ȭ�� ������Ʈ</returns>
    public T GetObject(Vector3? position =null,Vector3? eulerAngle=null)
    {
        if(readyQueue.Count > 0)
        {
            //���� ��Ȱ��ȭ �� ������Ʈ�� �����ִ�.
            T comp = readyQueue.Dequeue();          //ť���� �ϳ� ������ 
            comp.gameObject.SetActive(true);        //Ȱ��ȭ ��Ű��
            comp.transform.position = position.GetValueOrDefault();         //��ġ�� ȸ�� ����
            comp.transform.rotation = Quaternion.Euler(eulerAngle.GetValueOrDefault());

            return comp;
        }
        else
        {
            //��� ������Ʈ�� Ȱ��ȭ �Ǿ��ִ�. => �����ִ� ������Ʈ�� ����.
            ExpandPool();               //Ȯ��
            return GetObject(position, eulerAngle);
        }
    }

    /// <summary>
    /// Ǯ�� �ι�� Ȯ���Ű�� �Լ�
    /// </summary>
    void ExpandPool()
    {
        //�ִ��� ������� �ʾƾ� ��. ���� �� ���Ǹ� ���� �Լ�
        Debug.LogWarning($"{gameObject.name} Ǯ������ ���� {poolSize}->{poolSize*2}");

        int newSize = poolSize * 2;
        T[] newPool = new T[newSize];

        for(int i = 0; i < poolSize; i++)
        {
            newPool[i] = pool[i]; //���� Ǯ�� ������ �� Ǯ�� ����
        }

        GenereateObjects(poolSize,newSize,newPool); //�� Ǯ�� ���� �κп� ������Ʈ �����ؼ� �߰�

        pool= newPool;//�� Ǯ ũ�� ����
        poolSize = newSize; //�� Ǯ�� ���� Ǯ�� ����
    }
}
