using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������������ֻ��һ��ʵ�����󣬵�������Ա������״̬��Ϣ�������״̬��Ϣ����ͨ������������Ŀ���κεط����ʡ�
/// ��ˣ������඼������Ƴɵ����ࡣ
/// </summary>

public abstract class Singleton<T> where T : new()   // ��ȷָ��T����������
{
    // �������ʵ�ʶ���
    private static T m_Instance;
    // ��������������߳�ʱ�����ض������
    private static object mutex = new object();
    public static T Instance {
        get {
            if (m_Instance == null) {
                lock (mutex)
                {
                    // ��ȡ���������
                    // ������󲻴��ڣ��򴴽�֮

                    if (m_Instance == null)
                    {
                        m_Instance = new T();
                    }
                }
            }
            // ���������ڣ��򷵻�֮
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
            // ��������е���Ϸ�����д������T�����ȡ֮��
            if (m_Instance == null) {
                m_Instance = FindObjectOfType(typeof(T)) as T;
                // ��������в����ڰ���T�������Ϸ�����򴴽�һ����Ϸ���󣬲������T��ӵ�����Ϸ�����ϡ�
                if (m_Instance == null) {
                    obj = new GameObject();
                    m_Instance = obj.AddComponent<T>() as T;
                    // ��GameObject��HideFlags����ΪDontSave�����ζ�ţ�����Ϸ���󲻻ᱻ���浽��Ϸ�����У�
                    // ֻ��������ʱ�Żᴴ������Ϸ���󣬶����ڳ����л�ʱ������Ϸ����Ҳ���ᱻ����,
                    // ��������Ŀ����Ϊ�˱�֤�����Ϸ�����Ψһ�Ժͳ־��ԣ����ⱻ�༭����������ʱ�Ĳ���Ӱ�졣
                    // ���ǣ���Ҳ��ζ������Ҫ�Լ��������������Ϸ���󣬷�������һֱռ���ڴ档
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
