using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEditor;
using UnityEngine;
using Zenject;

public class GoogleSheetLoader
{
    private readonly DiContainer _container;
    private readonly string _docsId;
    private readonly bool _isDebug;
    private readonly SheetProcessor _sheetProcessor;

    public GoogleSheetLoader(DiContainer container, [Inject(Id = "Google Sheets")] string docsId, [Inject(Id = "Debug")] bool isDebug)
    {
        _container = container;
        _docsId = docsId;
        _isDebug = isDebug;
        _sheetProcessor = new SheetProcessor(_isDebug);
    }

    public Task DownloadTable<T>(string sheetId) where T : EntitySpecs, new()
    {
        var cvsLoader = new CVSLoader(_docsId, sheetId, _isDebug);
        return cvsLoader.DownloadTable(rawCVSText =>
        {
            var data = _sheetProcessor.ProcessData<T>(rawCVSText);
            ConfigureEntities(data);
        });
    }

    private void ConfigureEntities(List<EntitySpecs> data)
    {
        foreach (var entity in data)
        {
            switch (entity)
            {
                case CoinPowerupEntitySpecs coinPowerupEntitySpecs:
                    _container.BindInstance(coinPowerupEntitySpecs).WithId(coinPowerupEntitySpecs.Id).AsTransient();
                    break;
                case HeartPowerupEntitySpecs heartPowerupEntitySpecs:
                    _container.BindInstance(heartPowerupEntitySpecs).WithId(heartPowerupEntitySpecs.Id).AsTransient();
                    break;
                case LogEntitySpecs logEntitySpecs:
                    _container.BindInstance(logEntitySpecs).WithId(logEntitySpecs.Id).AsTransient();
                    break;
                case PlayerEntitySpecs playerEntitySpecs:
                    _container.BindInstance(playerEntitySpecs).AsSingle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity));
            }
        }
        /*
        _container.Bind<LogEntitySpecs>().WithId("dynamic log").FromResources(@"Entity Specs\dynamic log");
        _container.Bind<LogEntitySpecs>().WithId("turret log").FromResources(@"Entity Specs\turret log");
        _container.Bind<PlayerEntitySpecs>().FromResources(@"Entity Specs\player");
        _container.Bind<HeartPowerupEntitySpecs>().FromResources(@"Entity Specs\one heart");
        _container.Bind<CoinPowerupEntitySpecs>().FromResources(@"Entity Specs\soft coin");
        */
        /*
        foreach (var entity in data)
        {
            var path = Utils.GetEntitySpecsPath();

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            AssetDatabase.CreateAsset(entity, $"{path}/{entity.Id}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();

        }*/
    }
}