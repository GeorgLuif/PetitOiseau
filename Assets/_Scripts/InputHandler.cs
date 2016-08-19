using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public Rotate[] legs;
	
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach(Rotate r in legs)
            {
                r.aktiv = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            foreach (Rotate r in legs)
            {
                r.aktiv = false;
            }
        }
	}
}
