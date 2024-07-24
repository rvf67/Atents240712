using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Getcomponent를 했을 때 
        if (collision.GetComponent<RecycleObject>() != null)
        {
            //리사이클 오브젝트
            collision.gameObject.SetActive(false);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
   
}
