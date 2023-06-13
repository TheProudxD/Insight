using UnityEngine;

class ExitWindow : WindowCommon
{
    public void Exit()
    {
        Application.Quit();
        Debug.LogWarning("Exit pressed!");
    }
}
