using UnityEngine;
using System.Collections;

public class MonologueObject : MonoBehaviour {

    [TextArea(10, 10)]
    public string monologue;
    public float delay = 0f;
	
	public void ShowMonologue () {

        MonologueManager.instance.ShowMonologue(monologue, -1, delay);

	}
}
