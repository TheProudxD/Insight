using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class GoogleSheetsDownloader : ILoadingOperation
{
    private readonly GoogleSheetLoader _googleSheetLoader;

    public GoogleSheetsDownloader(GoogleSheetLoader googleSheetLoader) => _googleSheetLoader = googleSheetLoader;

    public string Description => "Download google sheets configs";
    
    public async UniTask Load(Action<float> onProcess) => await DownloadGoogleSheetsTables(onProcess);

    private Task DownloadGoogleSheetsTables(Action<float> onProcess)
    {
        var allTasks = new List<Task>();

        onProcess?.Invoke(0f);
        allTasks.Add(_googleSheetLoader.DownloadTable<PlayerEntitySpecs>("863072486"));
        onProcess?.Invoke(0.2f);
        allTasks.Add(_googleSheetLoader.DownloadTable<LogEntitySpecs>("0"));
        onProcess?.Invoke(0.4f);
        allTasks.Add(_googleSheetLoader.DownloadTable<HeartPowerupEntitySpecs>("1682472823"));
        onProcess?.Invoke(0.6f);
        allTasks.Add(_googleSheetLoader.DownloadTable<CoinPowerupEntitySpecs>("777294485"));
        onProcess?.Invoke(0.8f);
        allTasks.Add(_googleSheetLoader.DownloadTable<NPCEntitySpecs>("80218900"));
        onProcess?.Invoke(1f);
        
        return Task.WhenAll(allTasks);
    }
}