#pragma strict

function Explode() {
	//add random force to all of object's children
	for(var child : Transform in transform) {
		var thisChild : Transform = child as Transform;
		thisChild.GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.None;
		thisChild.GetComponent.<Rigidbody>().useGravity = true;
		
		var randomVector : Vector3 = Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
		thisChild.GetComponent.<Rigidbody>().AddForce(2 * randomVector);
		
		thisChild.transform.parent = null;
	}
}