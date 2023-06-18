using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例类是这个类只有一个实例对象，单例类可以保存类的状态信息，该类的状态信息可以通过公共对象被项目中任何地方访问。
/// 因此，管理类都可以设计成单例类。
/// </summary>

public abstract class Singleton<T> where T : new()   // 明确指定T是引用类型
{
    // 单例类的实际对象
    private static T m_Instance;
    // 互斥锁，避免多线程时，返回多个对象
    private static object mutex = new object();
    public static T Instance {
        get {
            if (m_Instance == null) {
                lock (mutex)
                {
                    // 获取单例类对象
                    // 如果对象不存在，则创建之

                    if (m_Instance == null)
                    {
                        m_Instance = new T();
                    }
                }
            }
            // 如果对象存在，则返回之
            return m_Instance;
        }
    }
}

public class UnitySingleton<T> : MonoBehaviour where T : Component 
{
    private static T m_Instance;
    private static GameObject obj=null;
    public static T Instance
    {
        get {
            // 如果场景中的游戏对象中存在组件T，则获取之。
            if (m_Instance == null) {
                m_Instance = FindObjectOfType(typeof(T)) as T;
                // 如果场景中不存在包含T组件的游戏对象，则创建一个游戏对象，并将组件T添加到该游戏对象上。
                if (m_Instance == null) {
                    obj = new GameObject();
                    m_Instance = obj.AddComponent<T>() as T;
                    // 将GameObject的HideFlags设置为DontSave后就意味着，该游戏对象不会被保存到游戏场景中，
                    // 只有在运行时才会创建该游戏对象，而且在场景切换时，该游戏对象也不会被销毁,
                    // 这样做的目的是为了保证这个游戏对象的唯一性和持久性，避免被编辑器或者运行时的操作影响。
                    // 但是，这也意味着你需要自己负责销毁这个游戏对象，否则它会一直占用内存。
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    obj.name = typeof(T).Name;
                }
            }
            return m_Instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (m_Instance == null)
            m_Instance = this as T;
        else
            GameObject.Destroy(this.gameObject);
    }

    public virtual void OnDestroy()
    {
        if (obj != null)
            DestroyImmediate(obj);
    }
}
