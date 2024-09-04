using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour
{
    //1. 누르면 플레이어가 점프한다.
    //2. 플레이어 쿨다운 변화에 따라 CoolTime이미지의 FillAmount가 변화한다.
    public Action onJump;
    Player player;
    float coolTime;
    Image coolTimeImage;
    private void Awake()
    {
        coolTimeImage= transform.GetChild(1).GetComponent<Image>();
    }
    private void Start()
    {
        player = GameManager.Instance.Player;
        coolTime = player.jumpCoolTime;
        coolTimeImage.fillAmount = 0f;
    }
    
    public void Jump()
    {
        Debug.Log("점프");
        onJump?.Invoke();
        StartCoroutine(OnJump());
    }

    IEnumerator OnJump()
    {
        coolTimeImage.fillAmount = player.JumpCoolRemains/coolTime;
        yield return new WaitForSeconds(coolTime);
    }
}
