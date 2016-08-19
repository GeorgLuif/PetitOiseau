using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class MonologueManager : MonoBehaviour {

    public AudioClip speakSound;

    public float defaultDuration = 8f;
    public Text textObj;
    public float letterPause = 0.3f;
    
    public bool playingMonologue = false;

    void Start()
    {
        textObj.text = "";
        
    }

    

    public void ShowMonologue(string msg, float duration, float delay)
    {
        if (!playingMonologue)
        {
            StartCoroutine(SM(msg, duration, delay));
        }
        else
        {
            StopAllCoroutines();
            print("<color=orange>Stopped previous Monologue...</color>");
            textObj.text = "";
            StartCoroutine(SM(msg, duration, delay));
        }
        
    }

    public IEnumerator SM(string msg, float duration, float delay)
    {
        playingMonologue = true;
        yield return new WaitForSeconds(delay);
        AudioManager.instance.PlayAudioClip(speakSound, transform.position, false);
        print("<color=orange>Showing Monologue</color>");
        if (duration <= 0f) { duration = defaultDuration; }
        textObj.text = "";

        foreach (char letter in msg)
        {
            textObj.text += letter;
            yield return new WaitForSeconds(letterPause);
        }

        playingMonologue = false;

        yield return new WaitForSeconds(duration);
        textObj.text = "";
        
    }

    public void Reset()
    {
        StopAllCoroutines();
        textObj.text = "";
        playingMonologue = false;
    }





    public static MonologueManager instance;
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
