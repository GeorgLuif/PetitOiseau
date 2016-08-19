using UnityEngine;
using System.Collections;

public class PlaceAfterTouch : MonoBehaviour {

    public Transform targetPos;
    private bool once = true;
	
	public void Place() {

        if (once)
        {
            once = false;
            transform.position = targetPos.position;
            transform.rotation = targetPos.rotation;
        }

	}
}
