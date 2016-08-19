using UnityEngine;
using System.Collections;

public class Readable : MonoBehaviour {

	public void Read () {
        LetterManager.instance.ReadLetter(gameObject.name);
	}
	
}
