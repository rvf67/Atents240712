using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualStick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    RectTransform handle;
    RectTransform background;
    float leverRange;

    /// <summary>
    /// 이동 입력이 있었음을 알리는 델리게이트
    /// </summary>
    public Action<Vector2> onMoveInput;
    private void Awake()
    {
        Transform child = transform.GetChild(0);
        handle = child.transform as RectTransform;
        background = transform as RectTransform;
        leverRange = (background.rect.width-handle.rect.width)*0.5f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,                 //background영역의 원점 기준으로
            eventData.position,         //이스크린 좌표가
            eventData.pressEventCamera, //이 카메라 기준으로
            out Vector2 localMove);     //이만큼 움직였다.(로컬좌표)

        //핸들은 배경영역을 벗어나지 않아야한다.
        //Vector2.ClampMagnitude() //영역을 정할 수 있게 해준는 함수
        localMove=localMove.magnitude > leverRange ? localMove.normalized*leverRange : localMove;
        InputUpdate(localMove);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handle.anchoredPosition=Vector2.zero;
        InputUpdate(Vector2.zero);
    }

    /// <summary>
    /// 핸들 입력 관련을 실제로 처리하는 함수
    /// </summary>
    /// <param name="inputDelta">핸들이 움직인 정도</param>
    void InputUpdate(Vector2 inputDelta)
    {
        //움직임 처리
        handle.anchoredPosition = inputDelta;

        onMoveInput?.Invoke(inputDelta/leverRange); //크기를  0~1사이로 정규화해서 보냄
    }

    /// <summary>
    /// 가상패드와의 모든 연결을 끊는 함수
    /// </summary>
    public void Disconnct()
    {
        onMoveInput = null;
    }
}
