using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[System.Serializable]
public class Letter{

    public string name;
    public string header;
    public string content;
    public string commentAfterReading;
    public bool read = false;
    public Texture2D bg;
}
