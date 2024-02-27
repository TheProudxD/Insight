using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class CVSLoader
{
    private readonly bool _debug;
    private readonly string _url;

	public CVSLoader(string docsId, string sheetId, bool debug = false)
	{
        _url = $@"https://docs.google.com/spreadsheets/d/{docsId}/export?format=csv&gid={sheetId}";
		_debug = debug;
	}

	public async void DownloadTable(Action<string> callback)
    {
        using UnityWebRequest request = UnityWebRequest.Get(_url);
        
        await request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError ||
            request.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            if (_debug)
            {
                Debug.Log("Successful download from Google Sheets");
                Debug.Log(request.downloadHandler.text);
            }

            callback(request.downloadHandler.text);
        }
    }
}
