using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : UnitySingleton<ResourceManager>
{
    private SceneLoader m_SceneLoader;
    private AssetLoader m_AssetLoader;
    public override void Awake()
    {
        base.Awake();
        m_SceneLoader = new SimpleSceneLoader();
        m_AssetLoader = new SimpleAssetLoader();
    }

    #region Scene_Load
    public void LoadSceneAsync(string SceneName)
    {
        StartCoroutine(m_SceneLoader.LoadSceneAsync(SceneName));
    }
    #endregion Scene_Load


    #region Asset_Load

    private AsyncOperationHandle<UnityEngine.Object> m_LoadGameObjectOpHandle;
    public UnityEngine.Object Result;
    private Transform m_Parent;

    public void LoadAssetAsync(string assetName)
    {
        m_LoadGameObjectOpHandle = Addressables.LoadAssetAsync<UnityEngine.Object>(assetName);
        m_LoadGameObjectOpHandle.Completed += OnAssetLoadComplete;

    }

    private void OnAssetLoadComplete(AsyncOperationHandle<UnityEngine.Object> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Result = handle.Result;
            m_LoadGameObjectOpHandle.Completed -= OnAssetLoadComplete;
        }
    }

    public T LoadAsset<T>(string assetName)
    {
        return m_AssetLoader.Load<T>(assetName);
    }

    #endregion Asset_Load




}
