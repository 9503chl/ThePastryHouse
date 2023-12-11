using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    public sealed class ActiveGroup : MonoBehaviour
    {
        [SerializeField]
        private int activedIndex = -1;

        [SerializeField]
        private bool active0ToIndex = false;

        public GameObject[] GameObjects = new GameObject[0];

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
                GameObject deactivedObject = activedObject;
                activedObject = value;
                SetGameObjectActive();
            }
        }

        public int ActivedIndex
        {
            get
            {
                for (int i = 0; i < GameObjects.Length; i++)
                {
                    if (GameObjects[i] == activedObject)
                    {
                        return i;
                    }
                }
                return -1;
            }
            set
            {
                GameObject deactivedObject = activedObject;
                activedObject = null;
                if (value >= 0 && value < GameObjects.Length)
                {
                    activedObject = GameObjects[value];
                    activedIndex = value;
                }
                SetGameObjectActive();
            }
        }

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

#if UNITY_EDITOR
        [NonSerialized]
        private int objectCount = 0;

        [NonSerialized]
        private bool zeroToIndex = false;

        public void OnValidate()
        {
            if (ActivedIndex != activedIndex)
            {
                ActivedIndex = activedIndex;
            }
            else if (objectCount != GameObjects.Length || zeroToIndex != active0ToIndex)
            {
                objectCount = GameObjects.Length;
                zeroToIndex = active0ToIndex;
                SetGameObjectActive();
            }
        }
#endif

        private void Start()
        {
            if (activedObject == null)
            {
                if (activedIndex >= 0 && activedIndex < GameObjects.Length)
                {
                    activedObject = GameObjects[activedIndex];
                }
            }
            SetGameObjectActive();
        }

        private void SetGameObjectActive()
        {
            for (int i = 0; i < GameObjects.Length; i++)
            {
                if (GameObjects[i] != null)
                {
                    GameObjects[i].SetActive(active0ToIndex ? (i <= ActivedIndex) : (GameObjects[i] == activedObject));
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
                List<GameObject> gameObjects = new List<GameObject>(GameObjects);
                gameObjects.Add(go);
                GameObjects = gameObjects.ToArray();
                SetGameObjectActive();
            }
        }

        public void Remove(GameObject go)
        {
            if (go != null)
            {
                if (go == activedObject)
                {
                    activedObject = null;
                }
                List<GameObject> gameObjects = new List<GameObject>(GameObjects);
                gameObjects.Remove(go);
                GameObjects = gameObjects.ToArray();
                SetGameObjectActive();
            }
        }

        public void Clear()
        {
            GameObjects = new GameObject[0];
            activedObject = null;
        }
    }
}
