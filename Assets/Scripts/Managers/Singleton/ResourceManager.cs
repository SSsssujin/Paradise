using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Paradise
{
    public class ResourceManager
    {
        private List<GameObject> _instantiatedAssets = new();
        
        // 로드 & 생성
        public GameObject Instantiate(string key, Transform parent = null)
        {
            GameObject go = Addressables.InstantiateAsync(key).WaitForCompletion();
            if (parent != null) go.transform.SetParent(parent);
            _instantiatedAssets.Add(go);
            return go;
        }

        // Preload들 생성
        public void Instantiate()
        {
            
        }
        
        public void Destroy(GameObject loadedAsset)
        {
            var usedObj = _instantiatedAssets.Find(x => x == loadedAsset);
            if (usedObj != null)
            {
                _instantiatedAssets.Remove(usedObj);
                Addressables.ReleaseInstance(usedObj);
            }
            else
            {
                Debug.LogError($"Can't find [{loadedAsset.name}]. Check before if it is instantiated.");
            }
        }

        public void LoadScene(string sceneAddress)
        {
            Addressables.LoadSceneAsync(sceneAddress).Completed += _OnSceneLoaded;
        }

        // 수정
        public void LoadAssetsAsync<T>(string key, Action<T> onLoadCompleted)
        {
            //Addressables.LoadAssetsAsync<T>(key, onLoadCompleted);
        }

        // 씬 로드 완료 콜백
        private void _OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Scene {handle.Result.Scene.name} loaded successfully!");
            }
            else
            {
                Debug.LogError("Failed to load the scene.");
            }
        }

        public void UnloadScene(string sceneAddress)
        {
            // Addressables.UnloadSceneAsync(sceneAddress).Completed += handle =>
            // {
            //     if (handle.Status == AsyncOperationStatus.Succeeded)
            //     {
            //         Debug.Log($"Scene {sceneAddress} unloaded successfully!");
            //     }
            // };
        }
    }
}