using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode]
public class DataManager : MonoBehaviour
{
  

    public GameMaster gameMaster;
    public LetterManager letterManager;
    public JournalManager journalManager;

    [Header("Objectname|monologueText@|Chapter")]
    public string pathMonologue = "";
    private string dataMonologue = "";
    
    [Space(20)]
    [Header("Chaptername|text,text2,text3@")]
    public string pathCutscene = "";
    private string dataCutscene = "";

    [Space(20)]
    [Header("Name|Heading|Content|Comment@")]
    public string pathLetters = "";
    private string dataLetters = "";

    [Space(20)]
    [Header("Chaptername|Header|Content|labelA|chapterA|labelB|chapterB")]
    public string pathJournals= "";
    private string dataJournals = "";

    [Space(20)]
    [Header("")]
    public string pathPhonecalls = "";
    private string dataPhonecalls = "";

    public void LoadAll()
    {
        LoadMonologues();
        LoadCutscenes();
        LoadLetters();
        LoadJournals();
        LoadPhonecalls();
    }

    public void LoadPhonecalls()
    {
        if (pathPhonecalls == "")
        {
            Debug.LogError("No Phonecall Path defined...");
            return;
        }

        //LOOK IF ALREADY EXISTING, ELSE CREATE
        try
        {
            File.ReadAllText(pathPhonecalls,System.Text.Encoding.Default);
            print("<color=green>File at pathPhonecalls(" + pathPhonecalls + ") found. Loading Objects</color>");
        }
        catch (Exception)
        {
            print("<color=green>No file found, creating datafile (" + pathPhonecalls + ")</color>");
            System.IO.File.WriteAllText(pathPhonecalls, dataPhonecalls);
        }

        // READ DATA
        dataPhonecalls = File.ReadAllText(pathPhonecalls, System.Text.Encoding.Default);
        dataPhonecalls = dataPhonecalls.Replace(System.Environment.NewLine, "");
        print(dataPhonecalls);
        string[] dataArray = dataPhonecalls.Split('@');


        print("<color=green>Loading " + dataArray.Length + " phonecalls.</color>");


        foreach (string s in dataArray)
        {
            // SPLIT BY | SIGN

            string[] phonecallObject = s.Split('|');

            if (phonecallObject.Length < 2)
            {
                Debug.LogWarning("<color=green>" + phonecallObject + " is not correctly formatted. Skipping...</color>");
            }

            else
            {
                print("<color=green>looking for chapter to fit " + phonecallObject[0] + "</color>");
                foreach (Chapter c in gameMaster.chapters)
                {
                    

                    if (c.name == phonecallObject[0])
                    {
                        print("<color=green>Adding Phonecall for level " + phonecallObject[0] + ".</color>");


                        // ADD TO EVENT UNDER NAME PHONECALLTRIVIAL, STARTS AT 0
                        Event alreadyexists = null;

                        foreach (Event ev in c.events)
                        {
                            if (ev.name == "PhonecallTrivial")
                                alreadyexists = ev;
                        }

                        if (alreadyexists == null)
                        {
                            Event e = new Event();
                            e.name = "PhonecallTrivial";
                            e.time = 0f;

                            string content = s.Replace("|", System.Environment.NewLine);
                            e.monologueText = content;
                            
                            c.events.Add(e);
                        }

                        else
                        {

                            string content = s.Replace("|", System.Environment.NewLine);
                            alreadyexists.monologueText = content;
                        }
                        
                    }
                }

            }
        }
             
     }

    public void LoadMonologues()
    {

        if (pathMonologue == "")
        {
            Debug.LogError("No Monologue Path defined...");
            return;
        }

        //LOOK IF ALREADY EXISTING, ELSE CREATE
        try
        {
            File.ReadAllText(pathMonologue);
            print("<color=green>File at pathMonologue(" + pathMonologue + ") found. Loading Objects</color>");
        }
        catch (Exception)
        {
            print("<color=green>No file found, creating datafile (" + pathMonologue + ")</color>");
            System.IO.File.WriteAllText(pathMonologue, dataMonologue);
        }

        // READ DATA
        dataMonologue = File.ReadAllText(pathMonologue, System.Text.Encoding.Default);
        dataMonologue = dataMonologue.Replace(System.Environment.NewLine, "");
        string[] dataArray = dataMonologue.Split('%');

        // RESET INTERESTINGOBJECTS
        gameMaster.objectsOfInterest = new List<ObjectOfInterest>();

        for (int i = 0; i<dataArray.Length; i++)
        {
            // ODD OBJECTS ARE CHAPTERS
            if(i % 2 != 0)
            {
                print("<color=green>Found chapter: "+ dataArray[i]+"</color>");
                string[] chapterDataArray = dataArray[i+1].Split('@');

                // OBJECTS IN CHAPTER, ADD TO GAMEMASTER
                foreach(string monologueObject in chapterDataArray)
                {
                    // IF NOT CORRECTLY FORMATTED
                    if (monologueObject.Length < 1)
                    {
                        Debug.LogWarning("<color=green>" + monologueObject + " is not correctly formatted. Skipping...</color>");
                    }
                    else
                    {
                        // monologueObject = NAME|Comment

                        string[] monologueObjectSplit = monologueObject.Split('|');
                        if (monologueObjectSplit[0] != "")
                        {
                            print("Adding object " + monologueObjectSplit[0] + " (Chapter: " + dataArray[i] + ") to interesting objects..");
                            ObjectOfInterest interestingObj = new ObjectOfInterest();
                            interestingObj.name = monologueObjectSplit[0];
                            interestingObj.monologueText = monologueObjectSplit[1];
                            interestingObj.chapterName = dataArray[i];
                            gameMaster.objectsOfInterest.Add(interestingObj);
                            EditorUtility.SetDirty(gameMaster);
                        }
                    }
                }
            }
        }

    }

    public void LoadJournals()
    {
        if (pathJournals == "")
        {
            Debug.LogError("No Journal Path defined...");
            return;
        }

        //LOOK IF ALREADY EXISTING, ELSE CREATE
        try
        {
            File.ReadAllText(pathJournals);
            print("<color=green>File at pathJournals(" + pathJournals + ") found. Loading Journals..</color>");
        }
        catch (Exception)
        {
            print("<color=green>No file found, creating datafile (" + pathJournals + ")</color>");
            System.IO.File.WriteAllText(pathJournals, dataJournals);
        }

        // READ DATA
        dataJournals = File.ReadAllText(pathJournals, System.Text.Encoding.Default);
        dataJournals = dataJournals.Replace(System.Environment.NewLine, "");
        // SPLIT BY @ SIGN
        string[] dataArray = dataJournals.Split('@');

        print("<color=green>Loading " + dataArray.Length + " journals.</color>");


        foreach (string s in dataArray)
        {
            // SPLIT BY | SIGN

            string[] journalObject = s.Split('|');
            
            if (journalObject.Length < 6)
            {
                Debug.LogWarning("<color=green>" + journalObject[0] + " is not correctly formatted. Skipping...</color>");
            }

            else
            {
                Debug.Log("<color=green>Loading Journal "+ journalObject[0] + ".</color>");
                foreach (Chapter c in gameMaster.chapters)
                {
                    if (c.name == journalObject[0])
                    {
                        c.journal.heading = journalObject[1];
                        c.journal.content = journalObject[2];
                        c.journal.labelA = journalObject[3];
                        c.journal.chapterA = journalObject[4];
                        c.journal.labelB = journalObject[5];
                        c.journal.chapterB = journalObject[6];
                        
                        EditorUtility.SetDirty(gameMaster);
                        
                        
                    }
                }
            }

            


        }


        }

    public void LoadCutscenes()
    {

        if (pathCutscene == "")
        {
            Debug.LogError("No Cutscene Path defined...");
            return;
        }

        //LOOK IF ALREADY EXISTING, ELSE CREATE
        try
        {
            File.ReadAllText(pathCutscene);
            print("<color=green>File at pathCutscene(" + pathCutscene + ") found. Loading Cutscenes</color>");
        }
        catch (Exception)
        {
            print("<color=green>No file found, creating datafile (" + pathCutscene + ")</color>");
            System.IO.File.WriteAllText(pathCutscene, dataCutscene);
        }

        // READ DATA
        dataCutscene = File.ReadAllText(pathCutscene, System.Text.Encoding.Default);
        dataCutscene = dataCutscene.Replace(System.Environment.NewLine, "");


        // SPLIT BY @ SIGN
        string[] dataArray = dataCutscene.Split('@');

        print("<color=green>Loading " + dataArray.Length + " cutscenes.</color>");


        foreach (string s in dataArray)
        {
            
            // SPLIT Monologue Object BY | SIGN

            string[] cutsceneObject = s.Split('|');

            if (cutsceneObject.Length < 2)
            {
                Debug.LogWarning("<color=green>" + s + " is not correctly formatted. Skipping...</color>");
            }
            else
            {


                // CHECK IF IT'S current Scene...
                foreach (Chapter c in gameMaster.chapters)
                {
                    // ASSIGN CUTSCENE CONTENT TO THE CHAPTER OBJECT
                    if (c.name == cutsceneObject[0])
                    {
                        print("<color=green>Adding cutscene "+cutsceneObject+" to " + c.name + ".</color>");
                        c.cutscene = cutsceneObject;
                        
                        EditorUtility.SetDirty(gameMaster);
                        
                    }
                }
            }
        }
    }


    public void LoadLetters()
    {
        if (pathLetters == "")
        {
            Debug.LogError("No Letters Path defined...");
            return;
        }

        //letterManager.letters = new List<Letter>();

        //LOOK IF ALREADY EXISTING, ELSE CREATE
        try
        {
            File.ReadAllText(pathLetters);
            print("<color=green>File at pathLetters(" + pathLetters + ") found. Loading Objects</color>");
        }
        catch (Exception)
        {
            print("<color=green>No file found, creating datafile (" + pathLetters + ")</color>");
            System.IO.File.WriteAllText(pathLetters, dataLetters);
        }

        // READ DATA
        dataLetters = File.ReadAllText(pathLetters, System.Text.Encoding.Default);
        dataLetters= dataLetters.Replace(System.Environment.NewLine, "");

        // SPLIT BY @ SIGN
        string[] dataArray = dataLetters.Split('@');

        print("<color=green>Loading " + dataArray.Length + " letters.</color>");

        foreach (string s in dataArray)
        {
      
            // SPLIT Object BY | SIGN

            string[] letterObject = s.Split('|');

            if (letterObject.Length < 4)
            {
                Debug.LogWarning("<color=green>" + s + " is not correctly formatted. Skipping...</color>");

            }
            else
            {

                bool exists = false;
                // SEE IF IT ALREADY EXISTS
                foreach (Letter l in letterManager.letters)
                {

                    // IF EXISTS
                    if (l.name == letterObject[0])
                    {
                        Debug.Log("<color=green>Adding existing letter: " + letterObject[0] + " " + letterObject[1] + "</color>");

                        exists = true;
                        if (l.bg != null)
                        {
                            l.header = "";
                            l.content = "";
                            l.commentAfterReading = letterObject[3];
                            
                        }
                        else
                        {
                            l.header = letterObject[1];
                            l.content = letterObject[2];
                            l.commentAfterReading = letterObject[3];
                            l.bg = null;
                        }

                        
                    }
                }
                // IF DOESNT EXIST
                if (!exists)
                {
                    Debug.Log("<color=green>Adding new letter: " + letterObject[0] + " " + letterObject[1] + "</color>");

                    Letter newLetter = new Letter();
                    newLetter.name = letterObject[0];
                    newLetter.header = letterObject[1];
                    newLetter.content = letterObject[2];
                    newLetter.commentAfterReading = letterObject[3];
                    letterManager.letters.Add(newLetter);
                }

                EditorUtility.SetDirty(letterManager);
            }
        }

            }




    // SINGLETON
    public static DataManager instance;
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
