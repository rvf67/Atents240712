using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // GetComponent를 했을 때 <>사이의 클래스나 그 클래스를 상속받은 클래스가 없으면 return은 null
        if ( collision.GetComponent<RecycleObject>() != null)   
        {
            // 리사이클 오브젝트
            collision.gameObject.SetActive(false);
        }
        else
        {
            // 일반 오브젝트
            Destroy(collision.gameObject);
        }
    }
}
