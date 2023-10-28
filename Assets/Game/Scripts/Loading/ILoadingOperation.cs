using System;
using Cysharp.Threading.Tasks;

public interface ILoadingOperation
{
    public string Description { get; }
    public UniTask Load(Action<float> onProcess);
}