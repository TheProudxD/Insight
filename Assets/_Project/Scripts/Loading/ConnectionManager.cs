using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;

public class ConnectionManager
{
    private readonly int _tryAmount;
    private readonly int _waitingDelay;
    private readonly WindowManager _windowManager;

    public ConnectionManager(int tryAmount, int waitingDelay, WindowManager windowManager)
    {
        _windowManager = windowManager;
        _tryAmount = tryAmount;
        _waitingDelay = waitingDelay;
    }

    private async UniTask<bool> TryToConnect(int tryAmount)
    {
        if (HasInternet())
        {
            Debug.Log("<color=green>Connected!</color>");
            return true;
        }

        if (tryAmount <= 0)
        {
            _windowManager.ShowConnectionLostWindow();
            Debug.Log("<color=red>Does not connected!</color>");
            return false;
        }

        Debug.Log("Waiting...");
        await Task.Delay(_waitingDelay);
        Debug.Log("Trying to reconnect");
        await TryToConnect(tryAmount - 1);
        return false;
    }

    private bool HasInternet() => Application.internetReachability != NetworkReachability.NotReachable;
    
    public UniTask<bool> Connect() => TryToConnect(_tryAmount);
}