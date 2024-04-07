using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Utilites
{
    /*
     * Triggers a Action after a certain time
     * */
    public class FunctionTimer
    {
        /*
         * Class to hook Actions into MonoBehaviour
         * */
        private class MonoBehaviourHook : MonoBehaviour
        {
            public Action OnUpdate;

            private void Update() => OnUpdate?.Invoke();
        }

        // Holds a reference to all active timers
        private static List<FunctionTimer> _timerList;
        // Global game object used for initializing class, is destroyed on scene change
        private static GameObject _initGameObject;

        private readonly GameObject _gameObject;
        private readonly string _functionName;
        private readonly bool _useUnscaledDeltaTime;
        private readonly Action _action;
        private float _timer;
        private bool _active;

        public FunctionTimer(GameObject gameObject, Action action, float timer, string functionName,
            bool useUnscaledDeltaTime)
        {
            _gameObject = gameObject;
            _action = action;
            _timer = timer;
            _functionName = functionName;
            _useUnscaledDeltaTime = useUnscaledDeltaTime;
        }

        private static void InitIfNeeded()
        {
            if (_initGameObject == null)
            {
                _initGameObject = new GameObject("FunctionTimer_Global");
                _timerList = new List<FunctionTimer>();
            }
        }

        public static FunctionTimer Create(Action action, float timer) => Create(action, timer, "", false, false);

        public static FunctionTimer Create(Action action, float timer, string functionName) =>
            Create(action, timer, functionName, false, false);

        public static FunctionTimer
            Create(Action action, float timer, string functionName, bool useUnscaledDeltaTime) =>
            Create(action, timer, functionName, useUnscaledDeltaTime, false);

        public static FunctionTimer Create(Action action, float timer, string functionName, bool useUnscaledDeltaTime,
            bool stopAllWithSameName)
        {
            InitIfNeeded();

            if (stopAllWithSameName)
            {
                StopAllTimersWithName(functionName);
            }

            GameObject obj = new GameObject("FunctionTimer Object " + functionName, typeof(MonoBehaviourHook));
            FunctionTimer funcTimer = new FunctionTimer(obj, action, timer, functionName, useUnscaledDeltaTime);
            obj.GetComponent<MonoBehaviourHook>().OnUpdate = funcTimer.Update;

            _timerList.Add(funcTimer);

            return funcTimer;
        }

        public static void RemoveTimer(FunctionTimer funcTimer)
        {
            InitIfNeeded();
            _timerList.Remove(funcTimer);
        }

        public static void StopAllTimersWithName(string functionName)
        {
            InitIfNeeded();
            for (int i = 0; i < _timerList.Count; i++)
            {
                if (_timerList[i]._functionName == functionName)
                {
                    _timerList[i].DestroySelf();
                    i--;
                }
            }
        }

        public static void StopFirstTimerWithName(string functionName)
        {
            InitIfNeeded();
            foreach (var t in _timerList.Where(t => t._functionName == functionName))
            {
                t.DestroySelf();
                return;
            }
        }


        private void Update()
        {
            if (_useUnscaledDeltaTime)
            {
                _timer -= Time.unscaledDeltaTime;
            }
            else
            {
                _timer -= Time.deltaTime;
            }

            if (_timer <= 0)
            {
                // Timer complete, trigger Action
                _action();
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            RemoveTimer(this);
            if (_gameObject != null)
            {
                UnityEngine.Object.Destroy(_gameObject);
            }
        }

        /*
         * Class to trigger Actions manually without creating a GameObject
         * */
        public class FunctionTimerObject
        {
            private float timer;
            private Action callback;

            public FunctionTimerObject(Action callback, float timer)
            {
                this.callback = callback;
                this.timer = timer;
            }

            public void Update()
            {
                Update(Time.deltaTime);
            }

            public void Update(float deltaTime)
            {
                timer -= deltaTime;
                if (timer <= 0)
                {
                    callback();
                }
            }
        }

        // Create a Object that must be manually updated through Update();
        public static FunctionTimerObject CreateObject(Action callback, float timer) =>
            new(callback, timer);
    }
}