using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LetterManager : MonoBehaviour {

    public List<Letter> letters;
    public Image letterBG;
    public Sprite defaultBG;
    public Text letterHeader;
    public Text letterContent;
    [Space(20)]
    [Header("Readonly")]
    public bool readingLetter = false;
    private Letter currentLetter;

    void Start()
    {
        StartCoroutine(EnableLetterGUI(false));
    }

    public void ReadLetter(string name) {

        foreach(Letter l in letters)
        {
            if(l.name == name)
            {
                StartCoroutine(EnableLetterGUI(true));
                print("<color=white>Successfully tried reading letter " + name + "</color>");

                if (l.bg == null)
                {
                    letterHeader.text = l.header;
                    letterContent.text = l.content;
                    letterBG.sprite = defaultBG;
                }

                else
                {
                    letterHeader.text = "";
                    letterContent.text = "";
                    print("Creating letter with width/length " + l.bg.width + "  " + l.bg.height);
                    letterBG.sprite = Sprite.Create(l.bg, new Rect(0,0,l.bg.width,l.bg.height), new Vector2(0.5f, 0.5f), 100);

                }


                currentLetter = l;

            }
        }
        print("<color=white>Unsuccessfully tried reading letter " + name + "</color>");

    }

       IEnumerator EnableLetterGUI(bool yesno)
    {
        letterBG.gameObject.SetActive(true);
        letterBG.enabled = yesno;
        letterHeader.enabled = yesno;
        letterContent.enabled = yesno;
        yield return 0;
        readingLetter = yesno;
    }

    void Update()
    {
        
        if (readingLetter && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(EnableLetterGUI(false));
            
            if(!currentLetter.read)
            MonologueManager.instance.ShowMonologue(currentLetter.commentAfterReading, -1f, 2f);

            currentLetter.read = true;
        }
            

    }

    // SINGLETON
    public static LetterManager instance;
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
