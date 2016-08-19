using UnityEngine;
using System.Collections;

public class Grammophone : MonoBehaviour {


    public AudioSource audioS;

    bool onOff = true;

	public void Use () {

        float volume = 0f;

        if (onOff)
            volume = 1f;
        
        audioS.volume = volume;

        onOff = !onOff;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
