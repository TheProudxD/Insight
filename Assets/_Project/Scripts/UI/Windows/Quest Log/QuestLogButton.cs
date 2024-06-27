using System.Collections;
using System.Collections.Generic;
using Extensions;
using QuestSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    [field: SerializeField] public Button Button { get; private set; }
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _location;
    [SerializeField] private GameObject[] _stars;

    [Header("Status")] 
    [SerializeField] private GameObject _requirementsNotMetStatus;
    [SerializeField] private GameObject _canStartStatus;
    [SerializeField] private GameObject _inProgressStatus;
    [SerializeField] private GameObject _canFinishStatus;
    [SerializeField] private GameObject _finishedStatus;

    private UnityAction _onSelectAction;
    private GameObject[] _allStatuses;

    public void Initialize(string displayName, string description, UnityAction selectAction)
    {
        _title.SetText(displayName);
        _description.SetText(description);
        
        _onSelectAction = selectAction;
        _allStatuses = new[]
        {
            _requirementsNotMetStatus, _canStartStatus, _inProgressStatus, _canFinishStatus, _finishedStatus
        };
    }

    public void OnSelect(BaseEventData eventData) => _onSelectAction?.Invoke();

    public void SetState(QuestState state)
    {
        switch (state)
        {
            case QuestState.RequirementsNotMet:
                ActivateStatus(_requirementsNotMetStatus);
                break;
            case QuestState.CanStart:
                ActivateStatus(_canStartStatus);
                break;
            case QuestState.InProgress:
                ActivateStatus(_inProgressStatus);
                break;
            case QuestState.CanFinish:
                ActivateStatus(_canFinishStatus);
                break;
            case QuestState.Finished:
                ActivateStatus(_finishedStatus);
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement: " + state);
                break;
        }
    }

    private void ActivateStatus(GameObject status)
    {
        _allStatuses.ForEach(x => x.SetActive(false));
        status.SetActive(true);
    }
}