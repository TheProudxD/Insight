using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public interface ILoadingOperation
{
    public string Description { get; }
    public UniTask Load(Action<float> onProcess);
}
