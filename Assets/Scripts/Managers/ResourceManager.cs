using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Paradise
{
    public class ResourceManager
    {
        public GameObject Instantiate(string key, Transform parent = null)
        {
            GameObject go = Addressables.InstantiateAsync(key).WaitForCompletion();
            if (parent != null) go.transform.SetParent(parent);
            return go;
        }

        public void LoadScene(string sceneAddress)
        {
            Addressables.LoadSceneAsync(sceneAddress).Completed += _OnSceneLoaded;
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