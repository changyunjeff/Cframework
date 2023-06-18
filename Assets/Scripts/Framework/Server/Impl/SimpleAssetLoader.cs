using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SimpleAssetLoader : AssetLoader
{
    public override T Load<T>(string assetName)
    {
        AsyncOperationHandle<T> loadOpHandle = Addressables.LoadAsset<T>(assetName);
        T result = loadOpHandle.WaitForCompletion();
        Debug.Log(result);
        return (T)loadOpHandle.Result;
    }
}
