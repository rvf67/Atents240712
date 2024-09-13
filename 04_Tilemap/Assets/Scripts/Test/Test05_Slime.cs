using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Test05_Slime : TestBase
{
    /// <summary>
    /// 대상 슬라임들의 스프라이트 랜더러
    /// </summary>
    public SpriteRenderer[] spriteRenderers;
    
    /// <summary>
    /// 랜더러 안의 머터리얼들
    /// </summary>
    Material[] materials;
    /// <summary>
    /// 변화 속도
    /// </summary>
    public float speed =1.0f;
    /// <summary>
    /// 각 슬라임 이팩트 동작 여부
    /// </summary>
    public bool isOutLineChange;
    public bool isInnerLineChange;
    public bool isPhaseChange;
    public bool isPhaseReverseChange;
    public bool isDissolveChange;
    
    float[] elapsedTimes;

    /// <summary>
    /// 셰이더 프로퍼티 접근용 아이디
    /// </summary>
    readonly int Thickness_ID = Shader.PropertyToID("_Thickness");
    readonly int Split_ID = Shader.PropertyToID("_Split");
    readonly int Fade_ID = Shader.PropertyToID("_Fade");


    private void Start()
    {
        //머터리얼 전부 찾아서 저장
        materials = new Material[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
        //각 슬라임별 누적시간용 배열 생성
        elapsedTimes = new float[spriteRenderers.Length];

        //시작 설정 
        materials[0].SetFloat(Thickness_ID, 0);
        materials[1].SetFloat(Thickness_ID, 0);
        materials[2].SetFloat(Split_ID, 0);
        materials[3].SetFloat(Split_ID, 0);
        materials[4].SetFloat(Fade_ID, 0);
    }
    private void Update()
    {
        
        if (isOutLineChange)
        {
            // 아웃라인의 두께가 커졌다 작아졌다를 반복한다.(두께도 0~1로 설정 가능하게 변경)

            materials[0].SetFloat(Thickness_ID, GetRatio(ref elapsedTimes[0]));

        }
        if (isInnerLineChange)
        {
            // 이너 라인의 두께가 커졌다 작아졌다를 반복한다.(두께도 0~1로 설정 가능하게 변경)
            elapsedTimes[1] += Time.deltaTime * speed;
            float ratio = (Mathf.Cos(elapsedTimes[1]));
            materials[1].SetFloat(Thickness_ID, GetRatio(ref elapsedTimes[1]));
        }

        if (isPhaseChange)
        {
            // Split값이 0~1사이를 반복한다.
            elapsedTimes[2] += Time.deltaTime * speed;
            float ratio = (Mathf.Cos(elapsedTimes[2]));
            materials[2].SetFloat(Split_ID, GetRatio(ref elapsedTimes[2]));
        }
        if(isPhaseReverseChange)
        {
            // Split값이 0~1사이를 반복한다.
            elapsedTimes[3] += Time.deltaTime * speed;
            float ratio = (Mathf.Cos(elapsedTimes[3]));

            materials[3].SetFloat(Split_ID, GetRatio(ref elapsedTimes[3]));
        }
        if (isDissolveChange)
        {
            // fade값이 0~1사이를 반복한다.
            elapsedTimes[4] += Time.deltaTime * speed;
            float ratio = (Mathf.Cos(elapsedTimes[4]));

            materials[4].SetFloat(Fade_ID, GetRatio(ref elapsedTimes[4]));
        }
    }

    /// <summary>
    /// 시간진행에 따른 비율계산
    /// </summary>
    /// <param name="elapsedTime">진행된 시간</param>
    /// <returns></returns>
    float GetRatio(ref float elapsedTime)
    {
        elapsedTime += Time.deltaTime * speed;
        return (Mathf.Cos(elapsedTime) + 1.0f);
    }
    // 아웃라인, PhaseReverse, Dissolve를 하나로 합친 합친 쉐이더 그래프 만들기.(SlimeEffect)
}
