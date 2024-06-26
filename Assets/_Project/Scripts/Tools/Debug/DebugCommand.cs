using System;

namespace Utilites
{
    public class DebugCommand : DebugCommandBase
    {
        private readonly Action _command;

        public DebugCommand(string id, string description, string format, Action command) : base(id, description,
            format) => _command = command;

        public void Invoke() => _command?.Invoke();
    }
    
    public class DebugCommand<T> : DebugCommandBase
    {
        private readonly Action<T> _command;

        public DebugCommand(string id, string description, string format, Action<T> command) : base(id, description,
            format) => _command = command;

        public void Invoke(T value) => _command?.Invoke(value);
    }
}