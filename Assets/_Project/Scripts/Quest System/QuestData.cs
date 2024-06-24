using System;
using UnityEngine.Serialization;

namespace QuestSystem
{
    [Serializable]
    public class QuestData
    {
        public QuestState State;
        public int QuestStepIndex;
        public QuestStepState[] QuestStepStates;

        public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepStates)
        {
            State = state;
            QuestStepIndex = questStepIndex;
            QuestStepStates = questStepStates;
        }
    }
}