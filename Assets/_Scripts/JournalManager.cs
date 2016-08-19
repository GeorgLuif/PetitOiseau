using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour {

    public Text heading;
    public Text content;
    public Text labelA;
    public Text labelB;
    public Image bg;
    public GameObject journalObj;

    void Start()
    {
        journalObj.SetActive(false);
    }

    public void EnableJournal(bool yesno)
    {
        if (yesno == true)
        {
            Journal j = GameMaster.instance.currentChapter.journal;
            heading.text = j.heading;
            content.text = j.content;
            labelA.text = j.labelA;
            labelB.text = j.labelB;

            GameMaster.instance.timeOfDayEnabled = false;
            GameMaster.instance.currentChapter.timeEnabled = false;

        }
        journalObj.SetActive(yesno);
        Cursor.visible = yesno;
    }

    public void Choice(int i)
    {
        Journal j = GameMaster.instance.currentChapter.journal;


        if (j.chapterA == "" || j.chapterB == "")
        {
            Debug.LogWarning("No chapternames for both choices declared.");
        }

        if (i == 0)
            GameMaster.instance.StartCoroutine(GameMaster.instance.LoadChapter(j.chapterA));

        if (i == 1)
            GameMaster.instance.StartCoroutine(GameMaster.instance.LoadChapter(j.chapterB));


    }

    // SINGLETON
    public static JournalManager instance;
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
