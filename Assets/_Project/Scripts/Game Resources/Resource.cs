using System;

namespace ResourceService
{
    public class Resource : IResource<int>
    {
        public event Action<int, int> Changed;
        public ResourceType Type { get; }
        private int _amount;

        public Resource(ResourceType type, int amountByDefault = default)
        {
            Type = type;
            Amount = amountByDefault;
        }

        public int Amount
        {
            get => _amount;
            set
            {
                var oldValue = _amount;
                _amount = value;
                if (oldValue != _amount) 
                    Changed?.Invoke(oldValue, _amount);
            }
        }
    }
}