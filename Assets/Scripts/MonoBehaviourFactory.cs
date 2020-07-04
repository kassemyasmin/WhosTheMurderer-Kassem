using UnityEngine;

public class MonoBehaviourFactory {

    public static T Create<T>(bool dontDestroyOnLoad = true) where T : MonoBehaviour
    {
        var name = typeof(T).Name;
        var go = GameObject.Find(name);
        if (go == null)
        {
            go = new GameObject(name);
            if (dontDestroyOnLoad)
            {
                GameObject.DontDestroyOnLoad(go);
            }
        }

        return go.GetComponent<T>() ?? go.AddComponent<T>();
    }
}
