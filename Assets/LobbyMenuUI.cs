using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Canvas))]
public class LobbyMenuUI : MonoBehaviour
{
    [Inject]
    private void Construct(Camera uiCamera) => GetComponent<Canvas>().worldCamera = uiCamera;
}
