using UnityEngine;

namespace UI
{
    public class ExitCommonWindow : CommonWindow
    {
        public void Exit()
        {
            Application.Quit();
            Debug.LogWarning("Exit pressed!");
        }
    }
}