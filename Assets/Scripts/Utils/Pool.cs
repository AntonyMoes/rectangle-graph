using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils {
    public class Pool : MonoBehaviour {
        [SerializeField] GameObject objectToPool;
        [SerializeField] int initialPoolSize;

        readonly HashSet<GameObject> _pool = new HashSet<GameObject>();

        void Start() {
            for (var i = 0; i < initialPoolSize; i++) {
                _pool.Add(CreateNewObject());
            }
        }

        GameObject CreateNewObject() {
            var newObject = Instantiate(objectToPool);
            newObject.SetActive(false);

            return newObject;
        }

        public GameObject Get() {
            var objectToReturn = _pool.FirstOrDefault(obj => !obj.activeSelf);
            if (objectToReturn == null) {
                objectToReturn = CreateNewObject();
                _pool.Add(objectToReturn);
            }
            
            objectToReturn.SetActive(true);
            return objectToReturn;
        }

        public T Get<T>() {
            if (!objectToPool.TryGetComponent(out T _)) {
                throw new ArgumentException($"Can't return component of type {typeof(T)}");
            }

            return Get().GetComponent<T>();
        }

        public void Release(GameObject releasedObject) {
            if (!_pool.Contains(releasedObject)) {
                throw new ArgumentException("Passed object does not belong to this pool");
            }

            releasedObject.SetActive(false);
        }

        public void Release<T>(T releasedComponent) where T : MonoBehaviour {
            Release(releasedComponent.gameObject);
        }
    }
}
