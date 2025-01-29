using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FillWords.Utils;
namespace FillWords.Root.SceneManagment
{
    public class ScenesLoader : IScenesLoader
    {
        readonly Coroutines coroutines;
        const float fullLoadingPercent = 1f;
        public ScenesLoader(Coroutines _coroutines)
        {
            coroutines = _coroutines;
        }
        public void LoadScene(string sceneName, Action completeCallback = null, Action<float> progressUpdate = null)
        {
            coroutines.StartCoroutine(SceneLoadRoutine(sceneName, completeCallback, progressUpdate));
        }
        IEnumerator SceneLoadRoutine(string sceneName, Action completeCallBack, Action<float> progressUpdate)
        {
            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName);
            while (!sceneLoading.isDone)
            {
                   yield return null;
                   progressUpdate?.Invoke(sceneLoading.progress);
            }
            progressUpdate?.Invoke(fullLoadingPercent);
            completeCallBack?.Invoke();
        }
    }
}
