using System;
using UnityEngine;

namespace Core.Poolin
{
    public interface IPoolItem<out T>
    {
        public void OnActivationEvent();
        public event Action<T> OnObjectDeathRequest;
    }     
}
