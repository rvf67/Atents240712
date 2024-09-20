using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test05_Slime : TestBase
{
    /// <summary>
    /// 변화 속도
    /// </summary>
    public float speed = 3.0f;

    /// <summary>
    /// 대상 슬라임들의 스프라이트 랜더러
    /// </summary>
    public SpriteRenderer[] spriteRenderers;

    /// <summary>
    /// 랜더러 안에 있는 머티리얼들
    /// </summary>
    Material[] materials;

    // 각 슬라임 이팩트 동작 여부
    public bool isOutLineChange;
    public bool isInnerLineChange;
    public bool isPhaseChange;
    public bool isPhaseReverseChange;
    public bool isDissolveChange;

    /// <summary>
    /// 각 슬라임별 진행 시간
    /// </summary>
    float[] elapsedTimes;

    /// 셰이더 프로퍼티 접근용 아이디
    readonly int Thickness_ID = Shader.PropertyToID("_Thickness");
    readonly int Split_ID = Shader.PropertyToID("_Split");
    readonly int Fade_ID = Shader.PropertyToID("_Fade");

    private void Start()
    {
        // 머티리얼 전부 찾아서 저장하기
        materials = new Material[spriteRenderers.Length];
        for(int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }

        // 각 슬라임별 누적시간용 배열 생성
        elapsedTimes = new float[spriteRenderers.Length];

        // 시작 설정
        materials[0].SetFloat(Thickness_ID, 0);
        materials[1].SetFloat(Thickness_ID, 0);
        materials[2].SetFloat(Split_ID, 0);
        materials[3].SetFloat(Split_ID, 0);
        materials[4].SetFloat(Fade_ID, 0);
    }

    private void Update()
    {        
        //float ratio = (Mathf.Cos(elapsedTime) + 1.0f) * 0.5f;   // cos결과를 0~1사이로 변경

        if (isOutLineChange)
        {
            // 아웃라인의 두께가 커졌다 작아졌다를 반복한다.(두께도 0~1로 설정 가능하게 변경)
            materials[0].SetFloat(Thickness_ID, GetRatio(ref elapsedTimes[0]));
        }
        if (isInnerLineChange)
        {
            // 이너 라인의 두께가 커졌다 작아졌다를 반복한다.(두께도 0~1로 설정 가능하게 변경)
            materials[1].SetFloat(Thickness_ID, GetRatio(ref elapsedTimes[1]));
        }
        if (isPhaseChange)
        {
            // Split값이 0~1사이를 반복한다.
            materials[2].SetFloat(Split_ID, GetRatio(ref elapsedTimes[2]));
        }
        if(isPhaseReverseChange)
        {
            // Split값이 0~1사이를 반복한다.
            materials[3].SetFloat(Split_ID, GetRatio(ref elapsedTimes[3]));
        }
        if (isDissolveChange)
        {
            // fade값이 0~1사이를 반복한다.
            materials[4].SetFloat(Fade_ID, GetRatio(ref elapsedTimes[4]));
        }
    }

    /// <summary>
    /// 시간 진행에 따른 비율 계산하기
    /// </summary>
    /// <param name="elapsedTime">진행 시간</param>
    /// <returns>0~1 사이의 값</returns>
    float GetRatio(ref float elapsedTime)
    {
        elapsedTime += Time.deltaTime * speed;  // ref로 받았기 때문에 수정 가능
        return (Mathf.Cos(elapsedTime) + 1.0f) * 0.5f;
    }



    // 아웃라인, PhaseReverse, Dissolve를 하나로 합친 합친 쉐이더 그래프 만들기.(SlimeEffect)
}
