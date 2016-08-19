using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

	
	void Start () {
        if (GetComponent<Renderer>() != null)
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }
	

}
