using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SimpleSceneLoader : SceneLoader
{
    private static AsyncOperationHandle<SceneInstance> m_SceneLoadOpHandle;

    public override IEnumerator LoadSceneAsync(string sceneName)
    { 
        Addressables.LoadSceneAsync(sceneName, activateOnLoad: true);
        while (!m_SceneLoadOpHandle.IsDone) {
            Debug.Log($"已经加载了:{m_SceneLoadOpHandle.PercentComplete} %");
            yield return null;
        }
        Debug.Log("场景加载完毕!");
    }
}
