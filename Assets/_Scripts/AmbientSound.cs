using UnityEngine;
using System.Collections;
[System.Serializable]
public class AmbientSound {

	public AudioClip soundClip;
    [Range(0, 1)]
    public float time = 0.2f;
    public bool hasPlayed = false;
    public bool loop = false;
}
