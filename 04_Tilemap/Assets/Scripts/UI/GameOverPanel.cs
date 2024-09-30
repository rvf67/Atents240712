using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public float alphaChangeSpeed = 1.0f;
    private bool isDie;
    float totalTime;
    int totalKillCount = 0;
    CanvasGroup canvasGroup;
    Player player;
    SubmapManager submapManager;
    TextMeshProUGUI playTime;
    TextMeshProUGUI killCount;
    Button reloadButton;

    //1.플레이어가 죽으면 alphaChangeSpeed속도에 따라 캔버스 그룹의 알파값이 증가한다.
    private void Awake()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        playTime = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        killCount =  transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        reloadButton = transform.GetChild(3).GetComponent<Button>();
        reloadButton.onClick.AddListener(()=>StartCoroutine(LoadingSceneLoad()));
        canvasGroup.alpha = 0;
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
        submapManager = GameManager.Instance.SubmapManager;
        player.onDie += OnDie;
        player.onKillCountChange += OnKillCountChange;
    }
    private void Update()
    {
        if (isDie)
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
            if(canvasGroup.alpha > 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else
        {
            totalTime += Time.deltaTime;
        }
       
    }

    private void OnKillCountChange(int totalKill)
    {
        totalKillCount = totalKill;
    }
    void OnDie()
    {
        isDie = true;
        playTime.text = $"Total Play Time\r\n< {totalTime:f2} Sec >";
        killCount.text = $"Total Kill Count\r\n< {totalKillCount} Kill >";
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    //2. 플레이어가 죽으면 플레이어의 전체 플레이 시간과 킬 수를 UI에서 갱신해야 한다.
    //3. 플레이어가 죽은 이후에만 버튼이 눌러져야한다.
    //4. 버튼을 누르면 모든 씬이 언로드 된 이후에 LoadingScene을 로딩한다.
    IEnumerator LoadingSceneLoad()
    {
        
        while (isDie && !submapManager.IsUnloadAll)
        {
            
        }
        SceneManager.LoadScene("LoadingScene");
        yield return null;
    }
}
