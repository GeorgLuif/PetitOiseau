#pragma strict
var myObject : Transform; //object to spawn
var randomColors : Color[];

function Spawn() {
	var obj : Transform = Instantiate(myObject, transform.position, transform.rotation) as Transform;
	obj.GetComponent.<Renderer>().material.color = randomColors[Random.Range(0, (randomColors.length))];
}