using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class SceneLoader
{
    public abstract IEnumerator LoadSceneAsync(string sceneName);
}
