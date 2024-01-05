using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    [DisallowMultipleComponent]
    public sealed class ActiveGroup : MonoBehaviour, IEnumerable
    {
        [SerializeField]
        private int activedIndex = -1;

        public int ActivedIndex
        {
            get
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i] == activedObject)
                    {
                        return i;
                    }
                }
                return -1;
            }
            set
            {
                activedIndex = value;
                if (value >= 0 && value < gameObjects.Count)
                {
                    activedObject = gameObjects[value];
                }
                else
                {
                    activedObject = null;
                }
                SetGameObjectActive();
            }
        }

        [NonSerialized]
        private GameObject activedObject;
        public GameObject ActivedObject
        {
            get
            {
                return activedObject;
            }
            set
            {
                if (value != null)
                {
                    activedIndex = gameObjects.IndexOf(value);
                }
                else
                {
                    activedIndex = -1;
                }
                activedObject = value;
                SetGameObjectActive();
            }
        }

        [SerializeField]
        private bool active0ToIndex = false;

        public bool Active0ToIndex
        {
            get
            {
                return active0ToIndex;
            }
            set
            {
                active0ToIndex = value;
                SetGameObjectActive();
            }
        }

        [SerializeField]
        private List<GameObject> gameObjects = new List<GameObject>();

        public GameObject this[int index]
        {
            get
            {
                return gameObjects[index];
            }
        }

        public int Count
        {
            get
            {
                return gameObjects.Count;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return gameObjects.GetEnumerator();
        }

#if UNITY_EDITOR
        [NonSerialized]
        private int objectCount = 0;

        [NonSerialized]
        private bool zeroToIndex = false;

        private void OnValidate()
        {
            if (ActivedIndex != activedIndex)
            {
                ActivedIndex = activedIndex;
            }
            else if (objectCount != gameObjects.Count || zeroToIndex != active0ToIndex)
            {
                objectCount = gameObjects.Count;
                zeroToIndex = active0ToIndex;
                SetGameObjectActive();
            }
        }
#endif

        private void Start()
        {
            if (activedObject == null)
            {
                if (activedIndex >= 0 && activedIndex < gameObjects.Count)
                {
                    activedObject = gameObjects[activedIndex];
                }
            }
            SetGameObjectActive();
        }

        private void SetGameObjectActive()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] != null)
                {
                    gameObjects[i].SetActive(active0ToIndex ? (i <= ActivedIndex) : (gameObjects[i] == activedObject));
                }
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void Add(GameObject go)
        {
            if (go != null)
            {
                gameObjects.Add(go);
                SetGameObjectActive();
            }
        }

        public void Remove(GameObject go)
        {
            if (go != null)
            {
                if (go == activedObject)
                {
                    activedIndex = -1;
                    activedObject = null;
                }
                gameObjects.Remove(go);
                SetGameObjectActive();
            }
        }

        public void Clear()
        {
            activedIndex = -1;
            activedObject = null;
            gameObjects.Clear();
            SetGameObjectActive();
        }
    }
}
