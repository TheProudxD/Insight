using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class CVSLoader
{
    private bool _debug;
    private const string URL_TEMPLATE = "https://docs.google.com/spreadsheets/d/1b5Ak77i6ubJFIcFagXtlwf2mwrYZrXJ3qOPp5c85NgQ/export?format=csv&gid=2071689435";
    private string _url;

	public CVSLoader(string docsId, string sheetId = "0", bool debug = false)
	{
        _url = $@"https://docs.google.com/spreadsheets/d/{docsId}/export?format=csv&gid={sheetId}";
		_debug = debug;
	}

	public async void DownloadTable(Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_url))
        {
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
}
