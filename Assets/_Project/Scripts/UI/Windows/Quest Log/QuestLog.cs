using QuestSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI;
using UnityEngine.EventSystems;

public class QuestLog : CommonWindow
{
    [Header("Components")] 
    [SerializeField] private QuestLogScrollingList _scrollingList;
    [SerializeField] private TextMeshProUGUI _questDisplayNameText;
    [SerializeField] private TextMeshProUGUI _questDescriptionText;
    [SerializeField] private TextMeshProUGUI _questStatusText;
    [SerializeField] private TextMeshProUGUI _softCurrencyRewardsText;
    [SerializeField] private TextMeshProUGUI _hardCurrencyRewardsText;
    [SerializeField] private TextMeshProUGUI _energyRewardsText;
    [SerializeField] private TextMeshProUGUI _questRequirementsText;

    private Button _firstSelectedButton;
    private QuestManager _questManager;

    private void Awake()
    {
        _questManager = FindObjectOfType<QuestManager>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        if (_firstSelectedButton != null)
        {
            _firstSelectedButton.Select();
        }

        _questManager.QuestStateChanged += QuestStateChange;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _questManager.QuestStateChanged -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton = _scrollingList.CreateButtonIfNotExists(quest, () => SetQuestLogInfo(quest));

        if (_firstSelectedButton == null)
        {
            _firstSelectedButton = questLogButton.Button;
        }

        questLogButton.SetState(quest.State);
    }

    private void SetQuestLogInfo(Quest quest)
    {
        _questDisplayNameText.text = quest.Info.DisplayName;
        _questDescriptionText.text = quest.Info.Description;
        _questStatusText.text = quest.GetFullStatusText();

        _questRequirementsText.text = "";
        foreach (QuestInfo prerequisiteQuestInfo in quest.Info.QuestPrerequisites)
        {
            _questRequirementsText.text += prerequisiteQuestInfo.DisplayName + "\n";
        }

        _softCurrencyRewardsText.text = quest.Info.Rewards[0].Amount.ToString();
        _hardCurrencyRewardsText.text = quest.Info.Rewards[1].Amount.ToString();
        _energyRewardsText.text = quest.Info.Rewards[2].Amount.ToString();
    }
}