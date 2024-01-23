using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class AssetManager
    {
        private readonly Canvas _canvas;

        public bool DialogAlreadySpawned { get; private set; }

        public AssetManager(Canvas canvas) => _canvas = canvas; // TODO

        public GameObject GetWindowPrefab(string windowName)
        {
            var windowGO = Resources.Load(windowName) as GameObject;
            if (windowGO == null)
                throw new Exception($"{windowName} is not in Resources");
            
            var window = Object.Instantiate(windowGO, windowGO.transform.position, Quaternion.identity,
                parent: _canvas.transform);
            return window;
        }

        public GameObject GetDialogBoxPrefab()
        {
            DialogAlreadySpawned = true;
            return GetWindowPrefab("Dialog Box");
        }

        ~AssetManager()
        {
            DialogAlreadySpawned = false;
        }
    }
}