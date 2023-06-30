using System;
using Game.Scripts.InGame_Resources;

public interface IResource<T>
{
    event Action<T, T> Changed;
    ResourceType Type { get; }
    T Amount { get; }
}
