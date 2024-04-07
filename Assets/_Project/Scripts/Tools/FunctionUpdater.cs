using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Utilites
{
    /*
     * Calls function on every Update until it returns true
     * */
    public class FunctionUpdater
    {
        private readonly GameObject _gameObject;
        private readonly string _functionName;
        private readonly Func<bool> _updateFunc; // Destroy Updater if return true;
        private bool _active;

        public FunctionUpdater(GameObject gameObject, Func<bool> updateFunc, string functionName, bool active)
        {
            _gameObject = gameObject;
            _updateFunc = updateFunc;
            _functionName = functionName;
            _active = active;
        }

        /*
         * Class to hook Actions into MonoBehaviour
         * */
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action OnUpdate;

            private void Update() => OnUpdate?.Invoke();
        }

        // Holds a reference to all active updaters
        private static List<FunctionUpdater> _updaterList;

        // Global game object used for initializing class, is destroyed on scene change
        private static GameObject _initGameObject;

        private static void InitIfNeeded()
        {
            if (_initGameObject == null)
            {
                _initGameObject = new GameObject("FunctionUpdater_Global");
                _updaterList = new List<FunctionUpdater>();
            }
        }


        public static FunctionUpdater Create(Action updateFunc)
        {
            return Create(() =>
            {
                updateFunc();
                return false;
            }, "", true, false);
        }

        public static FunctionUpdater Create(Func<bool> updateFunc) =>
            Create(updateFunc, "", true, false);

        public static FunctionUpdater Create(Func<bool> updateFunc, string functionName) =>
            Create(updateFunc, functionName, true, false);

        public static FunctionUpdater Create(Func<bool> updateFunc, string functionName, bool active) =>
            Create(updateFunc, functionName, active, false);

        public static FunctionUpdater Create(Func<bool> updateFunc, string functionName, bool active,
            bool stopAllWithSameName)
        {
            InitIfNeeded();

            if (stopAllWithSameName)
            {
                StopAllUpdatersWithName(functionName);
            }

            GameObject gameObject = new GameObject("FunctionUpdater Object " + functionName, typeof(MonoBehaviourHook));
            FunctionUpdater functionUpdater = new FunctionUpdater(gameObject, updateFunc, functionName, active);
            gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = functionUpdater.Update;

            _updaterList.Add(functionUpdater);
            return functionUpdater;
        }

        private static void RemoveUpdater(FunctionUpdater funcUpdater)
        {
            InitIfNeeded();
            _updaterList.Remove(funcUpdater);
        }

        public static void DestroyUpdater(FunctionUpdater funcUpdater)
        {
            InitIfNeeded();
            funcUpdater?.DestroySelf();
        }

        public static void StopUpdaterWithName(string functionName)
        {
            InitIfNeeded();
            foreach (var t in _updaterList.Where(t => t._functionName == functionName))
            {
                t.DestroySelf();
                return;
            }
        }

        public static void StopAllUpdatersWithName(string functionName)
        {
            InitIfNeeded();
            for (int i = 0; i < _updaterList.Count; i++)
            {
                if (_updaterList[i]._functionName == functionName)
                {
                    _updaterList[i].DestroySelf();
                    i--;
                }
            }
        }
        
        public void Pause() => _active = false;

        public void Resume() => _active = true;

        private void Update()
        {
            if (!_active) 
                return;
            if (_updateFunc())
            {
                DestroySelf();
            }
        }

        public void DestroySelf()
        {
            RemoveUpdater(this);
            if (_gameObject != null)
            {
                UnityEngine.Object.Destroy(_gameObject);
            }
        }
    }
}