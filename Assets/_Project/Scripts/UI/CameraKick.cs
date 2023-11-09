using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraKick : MonoBehaviour
{
    [SerializeField] private float magnitude;
    private CinemachineVirtualCamera vCam;

    public void BeginKick()
    {
        vCam = GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;

        StartCoroutine(Kick());
    }

    public IEnumerator Kick()
    {
        float oSize = vCam.m_Lens.OrthographicSize;
        vCam.m_Lens.OrthographicSize -= magnitude;
        yield return null;
        vCam.m_Lens.OrthographicSize = oSize;
    }
}