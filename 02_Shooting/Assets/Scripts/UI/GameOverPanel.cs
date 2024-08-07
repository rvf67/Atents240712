using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Button restart = GetComponentInChildren<Button>();
        restart.onClick.AddListener(()=>SceneManager.LoadScene(0));
    }

    public void OnInitialize()
    {
        Player player = GameManager.Instance.Player;    
        player.onDie += () => animator.SetTrigger("GameOver"); //람다식으로 트리거 발동
    }
}
