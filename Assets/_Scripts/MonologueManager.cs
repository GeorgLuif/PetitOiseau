using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class MonologueManager : MonoBehaviour {

    public AudioClip speakSound;

    public float defaultDuration = 8f;
    public Text textObj;
    public float letterPause = 0.3f;
    public float sentencePause = .2f;
    public bool playingMonologue = false;

    public int maxSentenceLength = 40;

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
        if (duration <= 0f) { duration = defaultDuration; }
        textObj.text = "";


        string[] words = msg.Split(' ');

        List<string> sentences = new List<string>();
        
        string sentence = "";
        // for each word
        for (int i = 0; i < words.Length; i++)
        {
            print("<color=orange>Adding word " + words[i] + "</color>");
            // check if word itself isnt over, say 40 letters
            if (words[i].ToCharArray().Length > maxSentenceLength)
            {
                print("word too long, skipping");
                continue;
            }


            // check if sentence + new word is smaller than max sentence length
            if (sentence.ToCharArray().Length + words[i].ToCharArray().Length <= maxSentenceLength) {
                sentence += " " + words[i];
                print("adding word " + words[i] + " to sentence");
            }
            
                
            else
            {
                print("adding sentence  '" + sentence + "'  to sentence array");
                sentences.Add(sentence);
                sentence = "";
            }

            if(i == words.Length -1)
                sentences.Add(sentence);
        }

        print("<color=orange>Showing Monologue</color>");
        for (int e = 0; e < sentences.Count; e++)
        {
            textObj.text += sentences[e] +"\n";
            yield return new WaitForSeconds(sentencePause);
        }
        

        //foreach (char letter in msg)
        //{
        //    textObj.text += letter;
        //    
        //}

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
