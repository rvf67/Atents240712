using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    /// <summary>
    /// 숫자 스프라이트들(0~9) 
    /// </summary>
    public Sprite[] numberImages;

    /// <summary>
    /// 자리수별 이미지 컴포넌트들(0:1자리, 1:10자리, 2:100자리, 3:1000자리, 4:10000자리)
    /// </summary>
    Image[] digits;

    /// <summary>
    /// 보여줄 술자
    /// </summary>
    int number = -1;        // 처음 0을 설정할 때 변경이 일어나게 하기 위해 0이 아닌 값이 초기값이어야 함

    /// <summary>
    /// 보여줄 술자를 확인하고 설정하는 프로퍼티
    /// </summary>
    public int Number
    {
        get => number;
        set
        {
            if (number != value)
            {
                number = Mathf.Clamp(value, 0, 99999);  // number의 범위는 0 ~ 99999(최대 5자리)

                int temp = number;
                for (int i = 0; i < digits.Length; i++)
                {
                    if (temp != 0 || i == 0)
                    {
                        // temp가 0이 아닐 때만 보여준다. 단 1의자리 숫자는 표시한다.
                        int digit = temp % 10;                  // 1의 자리 숫자 추출하기
                        digits[i].sprite = numberImages[digit]; // 추출한 숫자에 맞는 이미지를 설정하기
                        digits[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        // temp가 0이면 더이상 보여줄 필요가 없다
                        digits[i].gameObject.SetActive(false);
                    }
                    temp /= 10;                             // 1의 자리 숫자 제거하기
                }
            }


            // number가 123일 경우 만자리와 천자리는 없어야 한다.
            // number가 0일 경우 일의 자리에는 0이 나와야 한다.
        }
    }

    private void Awake()
    {
        digits = GetComponentsInChildren<Image>();
    }
}
