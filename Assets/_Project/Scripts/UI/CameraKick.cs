using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraKick : MonoBehaviour
{
    [FormerlySerializedAs("magnitude")] [SerializeField]
    private float _magnitude;

    [SerializeField] private CinemachineVirtualCamera _vCam;
    private float _oldSize;
    private Coroutine _coroutine;

    public void BeginKick()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _vCam.m_Lens.OrthographicSize = _oldSize;
        }

        _coroutine = StartCoroutine(Kick());
    }

    private IEnumerator Kick()
    {
        _oldSize = _vCam.m_Lens.OrthographicSize;
        _vCam.m_Lens.OrthographicSize -= _magnitude;
        yield return null;

        _vCam.m_Lens.OrthographicSize = _oldSize;
        _coroutine = null;
    }
}