#pragma strict
//This script is used for the player to activate objects by clicking on the obejct | recomened in top-down views 
var hit : RaycastHit; //used to detect interactable objects
private var activationDistance : float = Mathf.Infinity; //must be at least this distance from object to activate it

function Update() {
	//When Left-Mouse-Button is clicked
	if(Input.GetMouseButtonDown(0)) {
		//Raycast from cursor
		var ray : Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, hit, activationDistance)) {
			//if there is an object tagged as "Interact"
			if(hit.transform.tag == "Interact") {
				//Invoke the objects script
				hit.transform.GetComponent(InteractionScript).Activate();
			}
		}
	}
}