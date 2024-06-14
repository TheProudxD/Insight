using System.Collections.Generic;
using System.Linq;
using Storage;
using UnityEngine;
using Zenject;

public class RewardsByLevelManager
{
    private readonly List<RewardByLevelSpecs> _specs;

    public RewardsByLevelManager(List<RewardByLevelSpecs> specs) => _specs = specs;

    public Dictionary<Scene, RewardByLevelSpecs> Get => _specs.ToDictionary(i => i.Scene, x => x);
}