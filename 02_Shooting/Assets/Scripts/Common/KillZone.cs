using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Getcomponent�� ���� �� 
        if (collision.GetComponent<RecycleObject>() != null)
        {
            //������Ŭ ������Ʈ
            collision.gameObject.SetActive(false);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
   
}
