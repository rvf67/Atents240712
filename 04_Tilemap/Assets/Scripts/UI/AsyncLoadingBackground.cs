using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadingBackground : MonoBehaviour
{
    /// <summary>
    /// 로딩할 다음 씬의 이름
    /// </summary>
    public string nextSceneName = "LoadSampleScene";

    /// <summary>
    /// loadingText에 글자가 변경되는 간격
    /// </summary>
    public float tickTime = 0.2f;

    /// <summary>
    /// slider의 value가 증가하는 속도
    /// </summary>
    public float loadingBarSpeed = 1.0f;

    /// <summary>
    /// 로딩이 완료되었는지를 표시하는 변수(true면 로딩완료, false면 로딩 중)
    /// </summary>
    bool loadingDone = false;

    /// <summary>
    /// 비동기 명령 처리를 위해 필요한 객체
    /// </summary>
    AsyncOperation async;

    // UI
    TextMeshProUGUI loadingText;
    Slider loadingSlider;

    // 입력용
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        loadingText = GetComponentInChildren<TextMeshProUGUI>();
        loadingSlider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += OnPressAnyKey;
        inputActions.UI.AnyKey.performed += OnPressAnyKey;
    }

    private void OnDisable()
    {
        inputActions.UI.AnyKey.performed -= OnPressAnyKey;
        inputActions.UI.Click.performed -= OnPressAnyKey;
        inputActions.UI.Disable();
    }

    private void Start()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName); // 비동기 로딩 시작
        async.allowSceneActivation = false;                 // 자동 씬전환 막기

        StartCoroutine(LoadingTextProgress());              // 글자 표시용 코루틴 시작
        StartCoroutine(LoadingSliderProgress());            // 슬라이더 조절용 코루틴 시작
    }

    /// <summary>
    /// Loading 글자를 출력하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingTextProgress()
    {
        // 출력할 문자열들 미리 준비하기
        string[] texts = {
            "Loading",
            "Loading .",
            "Loading . .",
            "Loading . . .",
            "Loading . . . .",
            "Loading . . . . .",
        };
        WaitForSeconds wait = new WaitForSeconds(tickTime); // tickTime동안 기다리기 위해 WaitForSeconds 만들기

        int index = 0;          // 출력할 문자열의 인덱스
        while (!loadingDone)    // 로딩 중이면 계속 반복
        {
            loadingText.text = texts[index];        // tickTime 간격으로 인덱스 변경하며 문자열 출력하기
            index++;
            index %= texts.Length;
            yield return wait;
        }

        loadingText.text = "Loading\nComplete!";    // 로딩이 완료되었으면 완료 출력
    }

    /// <summary>
    /// 슬라이더를 변경하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingSliderProgress()
    {
        loadingSlider.value = 0;            // 초기값 설정

        while (async.progress < 0.9f)       // 로딩 완료 전에는 속도에 맞춰서 슬라이더 계속 증가
        {
            loadingSlider.value += Time.deltaTime * loadingBarSpeed;
            yield return null;
        }

        // 로딩이 완료된 이후의 처리(씬은 백그라운드에서 대기중. 슬라이더 남아있는 부분 처리)
        float elapsedTime = 0.0f;
        float remainTime = (1 - loadingSlider.value) / loadingBarSpeed; // 슬라이더 증가 속도와 슬라이더의 남은 양에 따라 대기해야 할 시간 계산
        while (remainTime > elapsedTime)    // 남은 시간 동안 슬라이더 증가 처리
        {
            elapsedTime += Time.deltaTime;  // 진행 시간 누적
            loadingSlider.value += Time.deltaTime * loadingBarSpeed;    // 슬라이더는 원래 증가 속도에 따라 계속 증가
            yield return null;
        }

        loadingDone = true;        // 로딩 완료 표시(Complete 글자 출력과 타이밍을 맞추기 위해 사용)
    }

    private void OnPressAnyKey(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        async.allowSceneActivation = loadingDone;   // UI에서 표시되는 것과 타이밍을 맞춰서 씬 전환 하기 위한 용도
    }
}
