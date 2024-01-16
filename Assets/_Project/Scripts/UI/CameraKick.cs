using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraKick : MonoBehaviour
{
    [FormerlySerializedAs("magnitude")] [SerializeField] private float _magnitude;
    [SerializeField] private CinemachineVirtualCamera _vCam;

    public void BeginKick()
    {
        StartCoroutine(Kick());
    }

    private IEnumerator Kick()
    {
        var oSize = _vCam.m_Lens.OrthographicSize;
        _vCam.m_Lens.OrthographicSize -= _magnitude;
        yield return null;
        _vCam.m_Lens.OrthographicSize = oSize;
    }
}