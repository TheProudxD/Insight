using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tools;
using UnityEditor;
using UnityEngine;

public class GoogleSheetLoader
{
    private readonly string _docsId;
    private readonly SheetProcessor _sheetProcessor;

    public GoogleSheetLoader(string docsId)
    {
        _docsId = docsId;
        _sheetProcessor = new SheetProcessor(false);
    }

    public void DownloadTable<T>(string sheetId) where T : EntitySpecs
    {
        var cvsLoader = new CVSLoader(_docsId, sheetId);
        cvsLoader.DownloadTable(rawCVSText =>
        {
            var data = _sheetProcessor.ProcessData<T>(rawCVSText);
            ConfigureEntities(data);
        });
    }

    private void ConfigureEntities(List<EntitySpecs> data)
    {
        foreach (var entity in data)
        {
            var path = Utils.GetEntitySpecsPath();

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            AssetDatabase.CreateAsset(entity, $"{path}/{entity.Id}.asset");
            AssetDatabase.SaveAssets();
            //EditorUtility.FocusProjectWindow();
        }
    }
}