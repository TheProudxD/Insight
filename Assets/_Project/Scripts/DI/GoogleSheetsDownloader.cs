using Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GoogleSheetsDownloader : MonoBehaviour
{
	[Inject] private GoogleSheetLoader _googleSheetLoader;
	private async void Awake()
	{
		var allTasks = new List<Task>();

		allTasks.Add(_googleSheetLoader.DownloadTable<PlayerEntitySpecs>("863072486"));
		allTasks.Add(_googleSheetLoader.DownloadTable<LogEntitySpecs>("0"));
		allTasks.Add(_googleSheetLoader.DownloadTable<HeartPowerupEntitySpecs>("1682472823"));
		allTasks.Add(_googleSheetLoader.DownloadTable<CoinPowerupEntitySpecs>("777294485"));
		allTasks.Add(_googleSheetLoader.DownloadTable<NPCEntitySpecs>("80218900"));

		await Task.WhenAll(allTasks);

		SceneManager.LoadScene((int)Levels.Bootstrap);
	}
}