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

        private void Awake() => _dataManager.DataLoaded += SetNicknameAfterLoading;

        public void SetNicknameAfterLoading(PlayerData playerData) => _playerNickname.SetText(playerData.Name);
    }
}