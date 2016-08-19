using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float speed = 1f;
    public bool aktiv = false;
	void Update () {

        if(aktiv)
        transform.Rotate(new Vector3(speed, 0, 0));
	}
}
