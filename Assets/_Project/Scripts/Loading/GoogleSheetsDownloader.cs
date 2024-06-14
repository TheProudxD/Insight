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

        onProcess?.Invoke(0.1f);
        allTasks.Add(_googleSheetLoader.DownloadTable<PlayerEntitySpecs>("863072486"));
        onProcess?.Invoke(0.3f);
        allTasks.Add(_googleSheetLoader.DownloadTable<LogEntitySpecs>("0"));
        onProcess?.Invoke(0.4f);
        allTasks.Add(_googleSheetLoader.DownloadTable<HeartPowerupEntitySpecs>("1682472823"));
        onProcess?.Invoke(0.5f);
        allTasks.Add(_googleSheetLoader.DownloadTable<CoinPowerupEntitySpecs>("777294485"));
        onProcess?.Invoke(0.6f);
        allTasks.Add(_googleSheetLoader.DownloadTable<NPCEntitySpecs>("80218900"));
        onProcess?.Invoke(0.7f);
        allTasks.Add(_googleSheetLoader.DownloadTable<OrcEntitySpecs>("1123816206"));
        onProcess?.Invoke(0.8f);
        allTasks.Add(_googleSheetLoader.DownloadTable<RewardByLevelSpecs>("1330076383"));        
        onProcess?.Invoke(0.9f);
        allTasks.Add(_googleSheetLoader.DownloadTable<DamagerSpecs>("2071689435"));
        onProcess?.Invoke(1f);

        return Task.WhenAll(allTasks);
    }
}