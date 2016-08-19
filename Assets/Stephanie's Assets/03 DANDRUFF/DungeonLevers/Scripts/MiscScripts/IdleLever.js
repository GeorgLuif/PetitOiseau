#pragma strict
//set starting animation to end | lever will sit idle, instead of playing animation at start
function Start () {
	var anim : String = GetComponent.<Animation>().clip.name;
	GetComponent.<Animation>()[anim].time = GetComponent.<Animation>()[anim].length;
}