using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GoogleSheetLoader
{
    private readonly string _docsId;
    private readonly bool _isDebug;
    private readonly SheetProcessor _sheetProcessor;

    public GoogleSheetLoader(string docsId, bool isDebug)
    {
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
                    ProjectContext.Instance.Container.BindInstance(coinPowerupEntitySpecs)
                        .WithId(coinPowerupEntitySpecs.Id);
                    break;
                case HeartPowerupEntitySpecs heartPowerupEntitySpecs:
                    ProjectContext.Instance.Container.BindInstance(heartPowerupEntitySpecs)
                        .WithId(heartPowerupEntitySpecs.Id);
                    break;
                case LogEntitySpecs logEntitySpecs:
                    ProjectContext.Instance.Container.BindInstance(logEntitySpecs).WithId(logEntitySpecs.Id);
                    break;
                case PlayerEntitySpecs playerEntitySpecs:
                    ProjectContext.Instance.Container.BindInstance(playerEntitySpecs).AsSingle();
                    break;
                case NPCEntitySpecs NPCEntitySpecs:
                    ProjectContext.Instance.Container.BindInstance(NPCEntitySpecs).WithId(NPCEntitySpecs.Id);
                    break;
                case OrcEntitySpecs orcEntitySpecs:
                    ProjectContext.Instance.Container.BindInstance(orcEntitySpecs).WithId(orcEntitySpecs.Id);
                    break;
                case RewardByLevelSpecs levelSpecs:
                    var rewards = ProjectContext.Instance.Container.Resolve<List<RewardByLevelSpecs>>();
                    rewards.Add(levelSpecs);
                    ProjectContext.Instance.Container.Rebind<List<RewardByLevelSpecs>>().FromInstance(rewards);
                    break;
                case DamagerSpecs damagerSpecs:
                    ProjectContext.Instance.Container.BindInstance(damagerSpecs).WithId(damagerSpecs.Id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity));
            }
        }
    }
}