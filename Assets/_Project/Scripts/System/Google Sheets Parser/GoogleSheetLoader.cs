using UnityEngine;

public class GoogleSheetLoader : MonoBehaviour
{    
    private string _docsId = "1b5Ak77i6ubJFIcFagXtlwf2mwrYZrXJ3qOPp5c85NgQ";
    private string _sheetId = "2071689435";

    private CVSLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;

    private void Awake()
    {
        _cvsLoader = new CVSLoader(_docsId, debug: true);
        _sheetProcessor = new SheetProcessor();
        DownloadTable();
    }

	private void DownloadTable() => _cvsLoader.DownloadTable(OnRawCVSLoaded);

	private void OnRawCVSLoaded(string rawCVSText)
    {
        var data = _sheetProcessor.ProcessData(rawCVSText);
        ConfigureEntities(data);
    }

    private void ConfigureEntities(EntityData data)
    {
        foreach (var entity in data.EntitiesOptions)
        {
            Debug.Log(entity);
        }
    }
}
