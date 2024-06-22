using ResourceService;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Create QuestInfo", fileName = "QuestInfo", order = 0)]
    public class QuestInfo : ScriptableObject
    {
        [field: NaughtyAttributes.ReadOnly] public string ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public int LevelRequirement { get; private set; }
        [field: SerializeField] public QuestInfo[] QuestPrerequisites { get; private set; }
        [field: SerializeField] public QuestStep[] QuestStepPrefabs { get; private set; }
        [SerializeField] private Resource[] _rewards;

        private void OnValidate()
        {
#if UNITY_EDITOR
            ID = name;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}