using UnityEngine;
using System.Collections;

public class FollowObj : MonoBehaviour {

    public Transform target;

	void Start () {
	
	}
	

	void Update () {
        if(target!=null)
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}
}
