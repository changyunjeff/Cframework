using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class ResourceManagerTest : MonoBehaviour
{
    AsyncOperationHandle opHandle;
    void Start()
    {
        // ResourceManager.Instance.LoadSceneAsync("MainScene");
        //opHandle = Addressables.LoadAssetAsync<Texture2D>("unity_engine.jpg");
        //opHandle.Completed += OnLoadComplete;
        Texture2D pic = ResourceManager.Instance.LoadAsset<Texture2D>("unity_engine.jpg");
        Debug.Log(pic);
    }

    void OnLoadComplete(AsyncOperationHandle _opHandle)
    {
        if (_opHandle.Status == AsyncOperationStatus.Succeeded) {
            Debug.Log(_opHandle.Status);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
