using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// json 유틸리티에서 사용하기 위해서는 반드시 직렬화 가능한 클래스이어야한다.
[Serializable] // 이 아래 클래스는 직렬화되는 클래스이다ㅏ.
public class SaveData
{
    public string[] rankers;
    public int[] highRecords;
}
