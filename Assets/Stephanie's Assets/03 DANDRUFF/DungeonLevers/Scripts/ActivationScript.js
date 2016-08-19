#pragma strict
//This script is used for the player to activate objects, by looking at the object and pressing the left mouse button | recomened in first or third person views
var hit : RaycastHit; //used to detect interactable objects
private var activationDistance : int = 5; //must be at least this distance from object to activate it

function Update() {
	//When Left-Mouse-Button is clicked
	if(Input.GetMouseButtonDown(0)) {
		//Raycast outward
		if(Physics.Raycast(transform.position, transform.forward, hit, activationDistance)) {
			//if there is an object tagged as "Interact"
			if(hit.transform.tag == "Interact") {
				//Invoke the objects script
				hit.transform.GetComponent(InteractionScript).Activate();
			}
		}
	}
}