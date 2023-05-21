using UnityEngine;
class PauseWindow : WindowCommon
{
    private void Update()
    {
        SetToPauseMode();
    }

    private void EnablePauseMode()
    {
        WindowManager.IsPause = true;
        Time.timeScale = 0;
        WindowManager.TryShow(WindowType.Pause);
    }

    private void DisablePauseMode()
    {
        WindowManager.IsPause = false;
        Time.timeScale = 1;
        WindowManager.TryClose(WindowType.Pause);
    }
    private void SetToPauseMode()
    {
        if (!WindowManager.IsPause && Input.GetKeyDown(KeyCode.Escape))
        {
            EnablePauseMode();
        }
        else if (WindowManager.IsPause && Input.GetKeyDown(KeyCode.Escape))
        {
            DisablePauseMode();
        }
    }
}

