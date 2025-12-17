using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Poolin
{
    public class ObjectPool<T> : IObjectPool<T> where T: IPoolItem<T>
    {
        protected readonly Queue<T> _availableObjects = new();
        protected readonly List<T> _allObjects = new();
        protected IObjectPool<T>.ItemCreateAction _createAction;
        protected int _maxCount = 10;
        public ObjectPool(IObjectPool<T>.ItemCreateAction itemCreateAction, int MaxSize) {
            _createAction = itemCreateAction;
            _maxCount = MaxSize;
        }
        public ObjectPool(IObjectPool<T>.ItemCreateAction itemCreateAction, int MaxSize,int MinSize) {
            _createAction = itemCreateAction;
            _maxCount = MaxSize;
            for (int i = 0; i < MinSize; i++)
            {
                T newItem = _createAction();
                PutToPool(newItem);
            }
        }

        public T GetFromPool()
        {
            if (_availableObjects.Count == 0)
            {
                if (_allObjects.Count >= _maxCount)
                {
                    throw new System.Exception("Maximum Object Count reached.");
                }
                PutToPool(_createAction());
            }
            var Item = _availableObjects.Dequeue();

            if (Item is IPoolItem<T> item)
            {
                Item.OnActivationEvent();      
                Item.OnObjectDeathRequest += PutToPool;          
            }
            return Item;
        }

        public void PutToPool(T Item)
        {
            if (_availableObjects.Contains(Item)) 
                return;

            _availableObjects.Enqueue(Item);
        
            if (!_allObjects.Contains(Item))
                _allObjects.Add(Item);
        }

        public bool TryGetFromPool(out T output)
        {

            output = default;

            if (_availableObjects.Count == 0)
            {
                if (_allObjects.Count >= _maxCount)
                {
                    Debug.Log("Max Count reached");
                    return false;
                }
                PutToPool(_createAction());
            }

            T Item = _availableObjects.Dequeue();

            if (!_allObjects.Contains(Item))
                _allObjects.Add(Item);

            if (Item is IPoolItem<T> poolItem)
            {
                poolItem.OnActivationEvent();      
                poolItem.OnObjectDeathRequest += PutToPool;          
            }

            output = Item;

            return true;
        }
    }
}