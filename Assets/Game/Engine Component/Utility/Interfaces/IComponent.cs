using System;
using System.Collections;


namespace Engin.Utility
{
    public interface IComponent
    {
        public Type ID { get => GetType(); }
    }
}
