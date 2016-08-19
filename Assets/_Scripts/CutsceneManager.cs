using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour {

    public Text cutsceneTextObject;
    
    public float printSpeed = 0.1f;
    public float endOfSentenceWait = 0.5f;
    [SerializeField]

    void Start()
    {
        cutsceneTextObject.text = "";
    }

    public IEnumerator PlayCutscene(string[] sentences,bool fadeout)
    {
        if(sentences.Length < 1)
        {
            Debug.LogError("Trying to play cutscene, FAIL - no content.");
            yield break;
        }

        if (GameMaster.instance.currentChapter.cutsceneSound != null)
            AudioManager.instance.PlayAudioClip(GameMaster.instance.currentChapter.cutsceneSound, Character.instance.transform.position, false);


        print("<color=magenta> Playing cutscene ( number: "+sentences.Length+ " ) </color>");
        Fader.instance.SetBlack();
        GameMaster.instance.EnableWorld(false);
        //yield return new WaitForSeconds(0.2f);
        // WAIT FOR SENTENCE TO FINISH...
        foreach (string sentence in sentences)
        {

            // SKIP NAME
            if(sentence != sentences[0])
            {
                print("Showing " + sentence);
                yield return StartCoroutine(PrintSentence(sentence));

                if(sentence != sentences[sentences.Length-1])
                cutsceneTextObject.text += "\n\n";
                yield return new WaitForSeconds(endOfSentenceWait);
            }

            
            
        }

        cutsceneTextObject.text = "";

        if(fadeout)
        Fader.instance.fadeClear = true;


        GameMaster.instance.EnableWorld(true);
    }

    public IEnumerator PrintSentence(string s)
    {
        foreach (char c in s)
        {
            cutsceneTextObject.text += c;
            yield return new WaitForSeconds(printSpeed);
        }
        yield break;
    }

    // SINGLETON
    public static CutsceneManager instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }
}
