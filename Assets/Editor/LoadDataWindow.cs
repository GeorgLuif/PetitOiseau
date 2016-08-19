using UnityEngine;
using System.Collections;
using UnityEditor;
public class LoadDataWindow : EditorWindow {
  
    [MenuItem("Georg/LoadAll")]

    public static void ShowWindow()
    { 
        GameObject.Find("*DataManager").GetComponent<DataManager>().LoadAll();
    }

    [MenuItem("Georg/LoadMonologues")]

    public static void ShowWindow2()
    {
        GameObject.Find("*DataManager").GetComponent<DataManager>().LoadMonologues();
    }

    [MenuItem("Georg/LoadCutscenes")]

    public static void ShowWindow3()
    {
        GameObject.Find("*DataManager").GetComponent<DataManager>().LoadCutscenes();
    }

[MenuItem("Georg/LoadLetters")]

    public static void ShowWindow4()
    {
        GameObject.Find("*DataManager").GetComponent<DataManager>().LoadLetters();
    }

    [MenuItem("Georg/LoadJournals")]

    public static void ShowWindow5()
    {
        GameObject.Find("*DataManager").GetComponent<DataManager>().LoadJournals();
    }
    [MenuItem("Georg/LoadPhoneCalls")]
    public static void ShowWindow6()
    {
        GameObject.Find("*DataManager").GetComponent<DataManager>().LoadPhonecalls();
    }
   
}
