using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
    public class SceneLoader : MonoBehaviour
    {
        public string HomeSceneName;
        public string PreviousSceneName;
        public string NextSceneName;

        public bool ReloadActiveScene = false;
        public bool UseAsyncOperation = false;
        public bool UseSingleInstance = false;

        [Serializable]
        public class LoadingEvent : UnityEvent<float> { }

        public LoadingEvent onLoading = new LoadingEvent();

        private static SceneLoader instance;
        public static SceneLoader Instance
        {
            get
            {
                SceneLoader[] templates = FindObjectsOfType<SceneLoader>();
                if (templates.Length > 0)
                {
                    instance = templates[0];
                    instance.enabled = true;
                    instance.gameObject.SetActive(true);
                }
                return instance;
            }
        }

        private IEnumerator Loading(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                onLoading.Invoke(operation.progress);
                yield return null;
            }
        }

        private IEnumerator Loading(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            while (!operation.isDone)
            {
                onLoading.Invoke(operation.progress);
                yield return null;
            }
        }

        public void LoadScene(string sceneName)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (!string.IsNullOrEmpty(sceneName) && SceneManager.GetSceneByName(sceneName) != null)
            {
                if (string.Compare(sceneName, activeScene.name, true) == 0)
                {
                    if (ReloadActiveScene)
                    {
                        NextSceneName = string.Empty;
                    }
                    else
                    {
                        Debug.LogError("Scene is already active!");
                        return;
                    }
                }
                else
                {
                    NextSceneName = string.Empty;
                    PreviousSceneName = activeScene.name;
                }
                if (UseSingleInstance)
                {
                    DontDestroyOnLoad(this);
                }
                if (UseAsyncOperation && isActiveAndEnabled)
                {
                    StartCoroutine(Loading(sceneName));
                }
                else
                {
                    onLoading.Invoke(0f);
                    SceneManager.LoadScene(sceneName);
                }
            }
            else
            {
                Debug.LogError("Invalid scene name!");
            }
        }

        public void LoadScene(int sceneIndex)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings && SceneManager.GetSceneByBuildIndex(sceneIndex) != null)
            {
                if (sceneIndex == activeScene.buildIndex)
                {
                    if (ReloadActiveScene)
                    {
                        NextSceneName = string.Empty;
                    }
                    else
                    {
                        Debug.LogError("Scene is already active!");
                        return;
                    }
                }
                else
                {
                    NextSceneName = string.Empty;
                    PreviousSceneName = activeScene.name;
                }
                if (UseSingleInstance)
                {
                    DontDestroyOnLoad(this);
                }
                if (UseAsyncOperation && isActiveAndEnabled)
                {
                    StartCoroutine(Loading(sceneIndex));
                }
                else
                {
                    onLoading.Invoke(0f);
                    SceneManager.LoadScene(sceneIndex);
                }
            }
            else
            {
                Debug.LogError("Invalid scene index!");
            }
        }

        public void LoadHomeScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (!string.IsNullOrEmpty(HomeSceneName))
            {
                LoadScene(HomeSceneName);
            }
            else
            {
                int sceneIndex = 0;
                if (SceneManager.sceneCountInBuildSettings > 1)
                {
                    if (sceneIndex == activeScene.buildIndex)
                    {
                        sceneIndex++;
                    }
                    LoadScene(sceneIndex);
                }
                else
                {
                    Debug.LogError("There is no scene to load.");
                }
            }
        }

        public void LoadPreviousScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (!string.IsNullOrEmpty(PreviousSceneName))
            {
                LoadScene(PreviousSceneName);
            }
            else
            {
                int sceneIndex = activeScene.buildIndex;
                if (sceneIndex > 0)
                {
                    LoadScene(sceneIndex - 1);
                }
                else
                {
                    Debug.LogError("There is no scene to load.");
                }
            }
        }

        public void LoadNextScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            if (!string.IsNullOrEmpty(NextSceneName))
            {
                LoadScene(NextSceneName);
            }
            else
            {
                int sceneIndex = activeScene.buildIndex;
                if (sceneIndex < SceneManager.sceneCountInBuildSettings - 1)
                {
                    LoadScene(sceneIndex + 1);
                }
                else
                {
                    Debug.LogError("There is no scene to load.");
                }
            }
        }
    }
}
