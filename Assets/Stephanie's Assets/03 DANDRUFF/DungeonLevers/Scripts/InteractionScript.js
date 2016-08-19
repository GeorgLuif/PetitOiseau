#pragma strict
//This script is used as a bridge between the activation script, and the script you actually want to activate.
//Attach this script to an object tagged as "Interact".
var myObject : GameObject[]; //the gameObject(s) we want to activate
var myScript : String[]; //the name of the script(s), attached to myObject[x], that we want to activate
private var obj : GameObject;

//This function is called by the ActivationScript, On Left-Mouse-Click
function Activate() {
	var interval : int = 0;
	//for each gameObject in myObject
	while(interval < myObject.length) {
		obj = myObject[interval];
		//call a function called "Activate" + the name of the script | Example: if myScript[x] is called MoveTowards, function ActivateMoveTowards will be called.
		//in the ActivateMoveTowards function, we will tell obj[x] run the MoveTowards script by calling its primary function. This function will tell the object to move.
		Invoke("Activate" + myScript[interval], 0.0f);
		//move on to next object
		interval += 1;
		yield;
	}
}
//obj[x] is told to run the script corolating to myScript[x]
function ActivateMoveTowards() {
	obj.GetComponent(MoveTowards).Move();
}
function ActivateAnimationSwitch() {
	obj.GetComponent(AnimationSwitch).PlayAnimation();
}
function ActivateLightSwitch() {
	obj.GetComponent(LightSwitch).Lights();
}
function ActivateAddRandomForce() {
	obj.GetComponent(AddRandomForce).Explode();
}
function ActivateSpawnObject() {
	obj.GetComponent(SpawnObject).Spawn();
}
//for additional scripts of your own, simply wright a function:
/*
function ActivateYOUR-SCRIPT-NAME() {
	obj.GetComponent(YOUR-SCRIPT-NAME).YOUR-SCRIPT'S-FUNCTION();
}
*/