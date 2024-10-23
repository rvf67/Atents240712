using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVCam : MonoBehaviour
{
    CinemachineVirtualCamera cinemachine;

    private void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        cinemachine.Follow=GameManager.Instance.Player.transform;
    }
}
