using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameMaster : MonoBehaviour {
    
    public float dayDuration = 120f;
    public float timer = 0f;
    public bool startRightAway = false;
    public bool timeOfDayEnabled = true;
    public float debugSpeedUp = 1f;
    public List<Chapter> chapters;
    public List<ObjectOfInterest> objectsOfInterest;
    [Header("Readonly")]
    public Chapter currentChapter = null;

    public Text timeMessage;

    IEnumerator Start()
    {
        ChapterErrorCheck();
        InvokeRepeating("CheckForEventsAmbientsounds", 0f, 1.0f);
        currentChapter = chapters[0];

        if (!Application.isEditor)
        {
            timeMessage.enabled = false;
            startRightAway = false;
        }

        if (debugSpeedUp != 1f)
        {
            Debug.LogWarning("Time is not 1!");
            Time.timeScale = debugSpeedUp;
        }
            


            if (!startRightAway)
        {
            Character.instance.EnableMovement(false);
            Fader.instance.SetBlack();
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(CutsceneManager.instance.PlayCutscene(currentChapter.cutscene,true));
            Character.instance.EnableMovement(true);

            yield return new WaitForSeconds(3f);
            InfoManager.instance.ShowInfo("Move with W,S,A,D.\nClick on objects to interact.");

            yield return new WaitForSeconds(InfoManager.instance.displayDuration +2f);

            InfoManager.instance.ShowInfo("You have "+ GameMaster.instance.dayDuration/60 +" minutes to discover objects.");

        }
        Character.instance.EnableMovement(true);
        currentChapter.timeEnabled = true;
        
        yield break;
    }

    public IEnumerator LoadChapter(string chaptername)
    {
       
        print("<color=magenta>Trying to load chapter " + chaptername + ".</color>");

        foreach (Chapter c in chapters)
        {
            if (c.name == chaptername)
            {
                JournalManager.instance.EnableJournal(false);
                currentChapter = c;
               
                if (currentChapter.cutscene.Length > 0)
                yield return StartCoroutine(CutsceneManager.instance.PlayCutscene(currentChapter.cutscene,false));

                StartCoroutine(LoadScene(chaptername));
                print("<color=magenta>Successfully loaded chapter " + chaptername + ".</color>");
            }
        }

        Cursor.visible = false;

    }

    public void ClickedOnInterestingObject(string s)
    {
        foreach(ObjectOfInterest o in objectsOfInterest)
        {
            if(o.name == s && o.chapterName == currentChapter.name && !o.hasPlayed)
            {
                o.hasPlayed = true;
                MonologueManager.instance.ShowMonologue(o.monologueText,-1f,0);

                if (o.audioComment != null)
                    AudioManager.instance.PlayAudioClip(o.audioComment, Character.instance.transform.position, false);

                return;
            }
        }
    }

    public void EnableWorld(bool yesno)
    {
        currentChapter.timeEnabled = yesno;
        Character.instance.EnableMovement(yesno);
        CharacterInteractions.instance.aktiv = yesno;
        Character.instance.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = yesno;
    }

    IEnumerator LoadScene(string name)
    {
        print("<color=magenta>Success - Loading scenefile" + name + ".</color>");

        
        MonologueManager.instance.Reset();
        timer = 0f;
        TimeOfDay.instance.ResetRot();
        EnableWorld(true);
        
        foreach (Chapter c in chapters)
        {
            if (c.journal != null)
                JournalManager.instance.EnableJournal(false);
        }

        Fader.instance.Invoke("SetClear", 2.0f);
        AudioManager.instance.RemoveAllAudio();   
        yield return Application.LoadLevelAsync(name);


    }

    void PlayEvent(Event e)
    {
        
        if (e.eventSound != null)
            AudioManager.instance.PlayAudioClip(e.eventSound, e.eventPos, false);
        
        e.hasPlayed = true;

        if (e.eventObject != null)
        {
            GameObject clone = GameObject.Instantiate(e.eventObject, e.eventPos, Quaternion.identity) as GameObject;
            clone.name = e.eventObject.name;
        }
            

        if(e.name != "Phonecall" && e.name != "PhonecallTrivial")
        {
            MonologueManager.instance.ShowMonologue(e.monologueText, -1, 1f);
            print("<color=yellow>Played event " + e.name + ".</color>");
        }

        print("<color=yellow>Trying to invoke event onPlay.</color>");
        e.onPlay.Invoke();
            
    }


    public void EndDay()
    {
        print("<color=magenta>Ending Day.</color>");
        StartCoroutine(Journal());
    }

    IEnumerator Journal()
    {
        Fader.instance.SetBlack();
        EnableWorld(false);
        JournalManager.instance.EnableJournal(true);
        yield break;
    }

    

    public string[] CheckPhonecall()
    {

        print("<color=magenta>Checking for phonecall</color>");
        foreach (Event e in currentChapter.events)
        {
            if(e.name == "Phonecall" && e.hasPlayed)
            {
                print("<color=magenta>Checked for phonecall - found " + e.name + "</color>");
                if (e.monologueText.Split('\n') != null)
                {
                    string[] s = e.monologueText.Split('\n');
                    currentChapter.events.Remove(e);
                    return s;
                }
                else
                {
                    Debug.LogError("<color=magenta>Phonecall " + e.name + "needs linebreaks to work.</color>");
                }

            }

            if (e.name == "PhonecallTrivial" && e.hasPlayed)
            {
                print("<color=magenta>Checked for phonecall - found "+ e.name +"</color>");
                
                if(e.monologueText.Split('\n') != null)
                {
                    string[] s = e.monologueText.Split('\n');
                    currentChapter.events.Remove(e);
                    return s;
                }

                else
                {
                    Debug.LogError("<color=magenta>TrivialPhonecall " + e.name + "needs linebreaks to work.</color>");
                }
                
            }
         }

        return new string[0];

    }
  

    void Update()
    {
        if( timer < dayDuration)
        {
            if (currentChapter.timeEnabled)
            {
                timer += Time.deltaTime;

                if (timeOfDayEnabled)
                    TimeOfDay.instance.angle = timer;

                if (timeMessage.enabled)
                    timeMessage.text = "Time left: " + ((dayDuration - timer) / dayDuration).ToString("##.0");
            }
  
        }

        else
        {
            print("Time is over...");
            print(timer);
            print(currentChapter.timeEnabled);
            timer = 0f;
            
            
            EndDay();
        }
        
    }

    void CheckForEventsAmbientsounds()
    {

        foreach (Event e in currentChapter.events)
        {
            if (!e.hasPlayed && e.time*dayDuration < timer)
             PlayEvent(e);
        }

        foreach (AmbientSound a in currentChapter.ambientSounds)
        {
            if (!a.hasPlayed && a.time * dayDuration < timer)
            {
                a.hasPlayed = true;
                AudioManager.instance.PlayAmbientSound(a.soundClip, a.loop);
            }
                
        }

    }

    


    void ChapterErrorCheck()
    {
        foreach (Chapter c in chapters)
        {
            if (c.journal == null)
                Debug.LogError("No Journal defined in " + c.name);
        }
    }

    public void EnableTouchableEvents(string name)
    {
        GameObject g = GameObject.Find(name);

      

        if (g != null)
        {
            if (g.GetComponent<Touchable>() != null)
                g.GetComponent<Touchable>().clickCallsEvents = true;

        }
    }

    // SINGLETON
    public static GameMaster instance;
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
