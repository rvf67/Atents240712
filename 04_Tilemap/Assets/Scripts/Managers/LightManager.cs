using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if (player != null)
        {
            player.onDie += () =>
            {
                System.Reflection.FieldInfo field = typeof(Light2D).GetField(   // 특정 필드(변수) 찾기   
            "m_ApplyToSortingLayers",                                   // 찾을 이름
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);    //찾을 때의 옵션

                int[] sortinfLayers = new int[]
                {
            SortingLayer.NameToID("Player")
                };
                field.SetValue(light2D, sortinfLayers);
            };
        }
    }
}
