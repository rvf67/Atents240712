using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01_Sprite : MonoBehaviour
{
    // 테스트용 인풋액션을 저장할 맴버 변수
    TestInputActions inputActions;

    private void Awake()
    {
        // 이 스크립트가 씬에서 생성되면 실행되는 함수
        inputActions = new TestInputActions();  // TestInputActions을 새로 생성

        Debug.Log("Awake");
    }

    private void OnEnable()
    {
        // 이 스크립트가 활성화되면 실행되는 함수
        Debug.Log("OnEnable");
        inputActions.Test.Enable(); // Test액션맵 활성화하기
        //inputActions.Test.Test1.started += OnTest1_started;   // 입력이 들어오면 발동
        inputActions.Test.Test1.performed += OnTest1_performed; // 입력이 충분히 들어오면 발동
        //inputActions.Test.Test1.canceled += OnTest1_canceled;   // 입력을 취소(버튼에서 손을 때면)하면 발동
    }

    private void OnTest1_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Canceled");
    }

    private void OnTest1_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Performed");
    }

    private void OnTest1_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Started");
    }

    private void OnDisable()
    {
        // 이 스크립트가 비활성화되면 실행되는 함수
        Debug.Log("OnDisable");

        inputActions.Test.Test1.performed -= OnTest1_performed; // Test1.performed에 등록되어 있던 함수 제거
        inputActions.Test.Disable(); // Test액션맵 비활성화하기
    }

    void Start()
    {
        // 시작할 때 한번 실행되는 함수(첫번째 업데이트 함수가 실행되기 직전에 한번 호출되는 함수)
        Debug.Log("Start");
    }

    void Update()
    {
        // 프레임마다 매번 실행되는 함수
        //Debug.Log("업데이트");

        ////InputManager 방식
        //if ( Input.GetKeyDown(KeyCode.A) )   // pooling 방식. 
        //{
        //    Debug.Log("A눌러짐");
        //}
    }
}
