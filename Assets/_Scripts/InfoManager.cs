using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour {

    public Text txtObject;
    public float displayDuration = 5.0f;
    void Start()
    {
        txtObject.text = "";
    }

    public void ShowInfo(string txt){
        StartCoroutine(ShowInfoCoroutine(txt));
    }

    IEnumerator ShowInfoCoroutine(string txt)
    {
        txtObject.text = txt;
        yield return new WaitForSeconds(displayDuration);
        txtObject.text = "";
    
    }

    // SINGLETON
    public static InfoManager instance;
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
