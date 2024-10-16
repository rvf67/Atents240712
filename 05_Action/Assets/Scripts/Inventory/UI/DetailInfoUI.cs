using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanvasGroup))]
public class DetailInfoUI : MonoBehaviour
{
    /// <summary>
    /// 알파값이 변하는 속도
    /// </summary>
    public float alphaChangeSpeed = 10.0f;

    /// <summary>
    /// 일시정지 모드 상태(true면 일시정지모드, false면 일반모드)
    /// </summary>
    bool isPause = false;

    Image icon;
    TextMeshProUGUI itemName;
    TextMeshProUGUI price;
    TextMeshProUGUI description;

    CanvasGroup canvasGroup;

    /// <summary>
    /// 일시 정지 모드 확인 및 설정용 프로퍼티
    /// </summary>
    public bool IsPaused
    {
        get => isPause;
        set
        {
            isPause = value;
            if(isPause)
            {
                Close();    // 일시 정지 모드로 들어가면 무조건 닫기
            }
        }
    }

    // 마우스 커서가 아이템이 들어있는 슬롯 위에 올라갔을 때 열린다
    // 열릴 때 ItemData를 확인해서 UI의 정보를 채운다.
    // 마우스 커서가 슬롯 밖으로 나갈 때 닫힌다.
    // 닫힐 때 alphaChangeSpeed의 속도로 canvasGroup의 alpha가 1 -> 0으로 감소한다.
    // DetailInfo는 아이템이 있는 슬롯 위에 있을 때 마우스 커서를 따라 다녀야 한다.
    // DetailInfo의 영역이 화면 밖으로 벗어나지 않게 만들어야 한다.
    // 아이템을 옮기는 도중에는 DetailInfo 창이 보이지 않아야 한다.

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;

        Transform child = transform.GetChild(0);
        icon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        itemName = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        price = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(4);
        description = child.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 상세정보창을 여는 함수
    /// </summary>
    /// <param name="itemData">열릴 때 표시할 아이템 데이터</param>
    public void Open(ItemData itemData)
    {
        // 일시정지 상태가 아니고, 아이템 데이터 필수
        if (!isPause && itemData != null)
        {
            icon.sprite = itemData.itemIcon;
            itemName.text = itemData.itemName;
            price.text = itemData.price.ToString("N0"); // 3자리마다 ','찍기
            description.text = itemData.itemDescription;

            canvasGroup.alpha = 0.001f;     // MovePosition를 실행시키기 위해 0보다 커야 함
            MovePosition(Mouse.current.position.ReadValue());   // 커서 위치로 창을 옮기기

            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    public void MovePosition(Vector2 screen)
    {
        if(canvasGroup.alpha > 0.0f)
        {
            RectTransform rect = (RectTransform)transform;
            int over = (int)(screen.x + rect.sizeDelta.x) - Screen.width;   // 얼마만큼 넘쳤는지 확인
            // over가 음수 = 창이 화면 안에 있다, over가 0양수다 = 창이 화면밖으로 넘쳤다.
            screen.x -= Mathf.Max(0, over); // over가 양수인 경우만 x위치를 빼준다.
            rect.position = screen;
        }
    }

    IEnumerator FadeIn()
    {
        // canvasGroup.alpha가 1이 될때까지 계속 증가
        while (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    IEnumerator FadeOut()
    {
        // canvasGroup.alpha가 0이 될때까지 계속 감소
        while (canvasGroup.alpha > 0.0f)
        {
            canvasGroup.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}
