using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ConnectionChecker : ILoadingOperation
{
    private readonly ConnectionManager _connectionManager;

    public ConnectionChecker(ConnectionManager connectionManager) => _connectionManager = connectionManager;

    public string Description => "Check connection";
    
    public async UniTask Load(Action<float> onProcess)
    {
        var isConnected = await _connectionManager.Connect();
        if (!isConnected)
        {
            Application.Quit();
        }
    }
}