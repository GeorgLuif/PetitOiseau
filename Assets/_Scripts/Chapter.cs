using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Chapter
{

    public string name = "Undefined";
    public List<Event> events;
    public string[] cutscene;
    public AudioClip cutsceneSound;
    public bool timeEnabled = false;
    public Journal journal;
    public AmbientSound[] ambientSounds;
    
    
    
}