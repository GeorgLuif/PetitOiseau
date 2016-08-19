using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

    public bool aktiv = false;
    public bool isPlaying = false;

    public void Phonecall () {

        if (isPlaying)
            return;

        string[] dialogue = GameMaster.instance.CheckPhonecall();

        if (dialogue.Length > 1)
        {
            print("Showing Phonecall - " + dialogue[0]);

            StartCoroutine(PlayPhonecall(dialogue));
        }
	}


    IEnumerator PlayPhonecall(string[] dialogue)
    {
        print("Phone - Showing phonecall " + dialogue[1]);
        isPlaying = true;
        GameMaster.instance.EnableWorld(false);
        yield return StartCoroutine(CutsceneManager.instance.PlayCutscene(dialogue,true));
        isPlaying = false;
        GameMaster.instance.EnableWorld(true);
    }

    public static Phone instance;
    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(instance);
            return;
        }
        instance = this;

    }
}
