using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tools;
using UnityEditor;
using UnityEngine;

public class GoogleSheetLoader
{
    private string _docsId = "1b5Ak77i6ubJFIcFagXtlwf2mwrYZrXJ3qOPp5c85NgQ";

    private SheetProcessor _sheetProcessor;

	public GoogleSheetLoader() => _sheetProcessor = new SheetProcessor();

	public void DownloadTable<T>(string sheetId="0") where T : EntitySpecs
    { 
        var cvsLoader = new CVSLoader(_docsId, sheetId);
        cvsLoader.DownloadTable(OnRawCVSLoaded<T>);
    }

    private void OnRawCVSLoaded<T>(string rawCVSText) where T: EntitySpecs
    {
        var data = _sheetProcessor.ProcessData<T>(rawCVSText);
        ConfigureEntities(data.EntitiesOptions);
    }

    private void ConfigureEntities(List<EntitySpecs> data)
    {
        foreach (var entity in data)
        {
            var path = Utils.GetEntitySpecsPath();
            /*
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            
            AssetDatabase.CreateAsset(entity, $"{path}/{entity.Id}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();*/

            /*
            var e =_entitySpecs.FirstOrDefault(e => e.Id == entity.Id);
            if (e == null || e==entity)
                continue;

            e.Hp = entity.Hp;
            e.Damage = entity.Damage; 
            e.MoveSpeed = entity.MoveSpeed;
            e.ChaseRadius = entity.ChaseRadius;
            e.AttackRadius = entity.AttackRadius;
            */
        }
    }
}
