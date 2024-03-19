using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GoogleSheetsDownloader : MonoBehaviour
{
    [Inject] private GoogleSheetLoader _googleSheetLoader;
    private async void Awake()
    {
        await _googleSheetLoader.DownloadTable<PlayerEntitySpecs>("863072486");
        await _googleSheetLoader.DownloadTable<LogEntitySpecs>("0");
        await _googleSheetLoader.DownloadTable<HeartPowerupEntitySpecs>("1682472823");
        await _googleSheetLoader.DownloadTable<CoinPowerupEntitySpecs>("777294485");

        SceneManager.LoadScene((int)Levels.Bootstrap);
    }
}