using TMPro;
using UnityEngine;

namespace UI
{
    public class DialogBox : CommonWindow
    {
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
    }
}