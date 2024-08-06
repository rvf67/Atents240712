using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnInitialize()
    {
        Player player = GameManager.Instance.Player;    
        player.onDie += () => animator.SetTrigger("GameOver"); //���ٽ����� Ʈ���� �ߵ�
    }
}
