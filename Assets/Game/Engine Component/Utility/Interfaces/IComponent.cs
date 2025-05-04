using System;

namespace Engin.Utility
{
    public interface IComponent
    {
        public Type ID { get => GetType(); }
    }
}
