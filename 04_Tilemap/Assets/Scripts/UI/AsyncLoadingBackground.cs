using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadingBackground : MonoBehaviour
{
    public string nextSeneName = "LoadSampleScene";
    public TextMeshProUGUI loadingText;
    public float tickTime=0.2f;
    public Slider loadingBar;
    int loadingDotCount = 0;
    AsyncOperation async; //비동기 관련 정보나 명령을 내리기 위한 객체. 비동기 함수의 return값으로 받음.
    /// <summary>
    /// slider의 value가 증가하는 속도
    /// </summary>
    public float loadingBarSpeed = 1.0f;
    private bool loadSuccess;
    PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    private void Start()
    {
        loadingText.text = "Loading";
        loadingBar.value = 0;
        async = SceneManager.LoadSceneAsync(nextSeneName);
        async.allowSceneActivation = false;
        StartCoroutine(LoadSceneCorutine());
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.AnyKey.performed += OnLoad;
        inputActions.UI.Click.performed += OnLoad;
    }

    private void OnDisable()
    {
        inputActions.UI.Click.performed -= OnLoad;
        inputActions.UI.AnyKey.performed -= OnLoad;
        inputActions.UI.Disable();
    }
    IEnumerator LoadSceneCorutine()
    {
        while (true)
        {
            if (loadingDotCount < 5)
            {
                loadingText.text = loadingText.text + " .";
                loadingDotCount++;
            }
            else
            {
                loadingText.text = "Loading";
                loadingDotCount = 0;
            }

            loadingBar.value += Time.deltaTime * loadingBarSpeed;
            if (async.progress == 0.9f) //progress는 0.9가 최대
            {
                if (loadingBar.value == 1)
                {
                    LoadSuccess();
                    break;
                }
            }
            yield return new WaitForSeconds(tickTime);
        }
    }

    private void LoadSuccess()
    {
        loadingText.text = "Loading Comlete!";
        loadSuccess = true;
    }

    private void OnLoad(InputAction.CallbackContext _)
    {
        if(loadSuccess)
        {
            async.allowSceneActivation = true;
        }
    }
    //1. LoadingText에 표시되는 글자가 "Loading"부터 "Loading....."까지 
    // tickTime마다 변경되면서 반복된다.

    //2.slider의 value가 loadingBarSpeed의 속도로 증가한다.
    //3.slider의 value가 1이 되기 전에 로딩이 완료되면 slider 의 value가 1이 될때 까지 기다린다.
    //4. 로딩이 완료되면 LoadingText에 표시되는 글자가 "Loading Complete!"로 변경된다.
    //5. 로딩이 완료된 이후에 마우스나 키보드 중 아무키나 눌려지면 씬 전환이 일어난다.
}
