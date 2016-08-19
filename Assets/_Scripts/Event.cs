using UnityEngine;
using System.Collections;
using UnityEngine.Events;
[System.Serializable]
public class Event  {
    public string name = "Undefined";

    [Range(0.1f,0.9f)]
    public float time = 0.1f;
   

    public AudioClip eventSound;
    public GameObject eventObject;
    public Vector3 eventPos;
    [TextArea(8,10)]
    public string monologueText;
    
    public bool hasPlayed = false;
    public UnityEvent onPlay;

}
