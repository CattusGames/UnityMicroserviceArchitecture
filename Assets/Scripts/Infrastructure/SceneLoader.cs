using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;


namespace Infrastructure
{ 
    public class SceneLoader : ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLevelLoad)
        {
            _coroutineRunner.StartCoroutine(LoadLevel(name, onLevelLoad));
        }
        
        private IEnumerator LoadLevel(string name, Action onLevelLoad)
        {
            AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(name);
            
            while (!asyncOperationHandle.IsDone)
            {
                yield return null;
            }

            if (!asyncOperationHandle.Result.Scene.IsValid())
            {
                Debug.Log($"scene is not valid!");
                yield break;
            }
            
            onLevelLoad?.Invoke();
        }
    }
}