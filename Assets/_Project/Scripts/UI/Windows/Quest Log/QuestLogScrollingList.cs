using System.Collections.Generic;
using QuestSystem;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private GameObject _contentParent;

    [Header("Rect Transforms")] [SerializeField]
    private RectTransform _scrollRectTransform;

    [SerializeField] private RectTransform _contentRectTransform;

    [Header("Quest Log Button")] [SerializeField]
    private GameObject _questLogButtonPrefab;

    private readonly Dictionary<string, QuestLogButton> _idToButtonMap = new();

    // Below is code to test that the scrolling list is working as expected.
    // For it to work, you'll need to change the QuestInfoSO id field to be publicly settable
    // private void Start()
    // {
    //     for (int i = 0; i < 20; i++) 
    //     {
    //         QuestInfoSO questInfoTest = ScriptableObject.CreateInstance<QuestInfoSO>();
    //         questInfoTest.id = "test_" + i;
    //         questInfoTest.displayName = "Test " + i;
    //         questInfoTest.questStepPrefabs = new GameObject[0];
    //         Quest quest = new Quest(questInfoTest);

    //         QuestLogButton questLogButton = CreateButtonIfNotExists(quest, () => {
    //             Debug.Log("SELECTED: " + questInfoTest.displayName);
    //         });

    //         if (i == 0)
    //         {
    //             questLogButton.button.Select();
    //         }
    //     }
    // }

    public QuestLogButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = null;
        questLogButton = _idToButtonMap.ContainsKey(quest.Info.ID) == false
            ? InstantiateQuestLogButton(quest, selectAction)
            : _idToButtonMap[quest.Info.ID];
        return questLogButton;
    }

    private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = Instantiate(
            _questLogButtonPrefab,
            _contentParent.transform).GetComponent<QuestLogButton>();
        questLogButton.gameObject.name = quest.Info.ID + "_button";

        RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
        questLogButton.Initialize(quest.Info.DisplayName, quest.Info.Description, () =>
        {
            selectAction();
            UpdateScrolling(buttonRectTransform);
        });

        _idToButtonMap[quest.Info.ID] = questLogButton;
        return questLogButton;
    }

    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        // calculate the min and max for the selected button
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        // calculate the min and max for the content area
        float contentYMin = _contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + _scrollRectTransform.rect.height;

        // handle scrolling down
        if (buttonYMax > contentYMax)
        {
            _contentRectTransform.anchoredPosition = new Vector2(
                _contentRectTransform.anchoredPosition.x,
                buttonYMax - _scrollRectTransform.rect.height
            );
        }
        // handle scrolling up
        else if (buttonYMin < contentYMin)
        {
            _contentRectTransform.anchoredPosition = new Vector2(
                _contentRectTransform.anchoredPosition.x,
                buttonYMin
            );
        }
    }
}