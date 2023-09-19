using System;

namespace ResourceService
{
    public interface IResource<T>
    {
        ResourceType Type { get; }
        T Amount { get; }
        event Action<T, T> Changed;
    }
}