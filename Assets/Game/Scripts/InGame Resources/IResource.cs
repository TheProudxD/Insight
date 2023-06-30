using System;
using Game.Scripts.InGame_Resources;

public interface IResource<T>
{
    ResourceType Type { get; }
    T Amount { get; }
    event Action<T, T> Changed;
}