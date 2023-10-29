using System;

namespace ResourceService
{
    public interface IResource<out T>
    {
        ResourceType Type { get; }
        T Amount { get; }
        event Action<T, T> Changed;
    }
}