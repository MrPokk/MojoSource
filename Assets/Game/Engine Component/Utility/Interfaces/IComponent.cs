using System;
using System.Collections;


namespace Engin.Utility
{
 public interface IComponent 
 {
  public virtual Type ID { get => GetType(); }
 }
}