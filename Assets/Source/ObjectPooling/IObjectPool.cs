using UnityEngine;

namespace Core.Poolin
{
    public interface IObjectPool<T> where T: IPoolItem<T>
    {
        T GetFromPool();
        bool TryGetFromPool(out T output);
        void PutToPool(T Item);
        public delegate T ItemCreateAction();
    }
}