using StorageService;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class NicknameView : MonoBehaviour
    {
        [Inject] private DataManager _dataManager;

        [SerializeField] private TextMeshProUGUI _playerNickname;

        private void Awake()
        {
            var playerName = _dataManager.GetName();
            SetNicknameAfterLoading(playerName);
        }

        public void SetNicknameAfterLoading(string playerName) => _playerNickname.text = playerName;
    }
}