using Storage;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Create LevelSpawnPosition", fileName = "LevelSpawnPosition", order = 0)]
public class LevelSpawnPosition : ScriptableObject
{
	[field: SerializeField] private List<LevelSpawnData> _spawnPosition;

	public IReadOnlyDictionary<Scenes, Vector3> SpawnPosition
	{
		get
		{
			var levels = _spawnPosition.Select(x => x.ID).OrderBy(x => x);
			var data = new Dictionary<Scenes, Vector3>();

			foreach (var level in levels)
			{
				data[level] = _spawnPosition.First(l => l.ID == level).Position;
			}
			return data;
		}
	}
}

[System.Serializable]
public class LevelSpawnData
{
	public Scenes ID;
	public Vector3 Position;
}