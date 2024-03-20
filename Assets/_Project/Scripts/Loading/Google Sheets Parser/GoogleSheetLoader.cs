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
                    _container.BindInstance(coinPowerupEntitySpecs).WithId(coinPowerupEntitySpecs.Id);
                    break;
                case HeartPowerupEntitySpecs heartPowerupEntitySpecs:
                    _container.BindInstance(heartPowerupEntitySpecs).WithId(heartPowerupEntitySpecs.Id);
                    break;
                case LogEntitySpecs logEntitySpecs:
                    _container.BindInstance(logEntitySpecs).WithId(logEntitySpecs.Id);
                    break;
                case PlayerEntitySpecs playerEntitySpecs:
                    _container.BindInstance(playerEntitySpecs);
                    break;
                case NPCEntitySpecs NPCEntitySpecs:
                    _container.BindInstance(NPCEntitySpecs).WithId(NPCEntitySpecs.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity));
            }
        }
    }
}