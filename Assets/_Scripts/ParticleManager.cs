using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {



    public static ParticleManager instance;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
}
