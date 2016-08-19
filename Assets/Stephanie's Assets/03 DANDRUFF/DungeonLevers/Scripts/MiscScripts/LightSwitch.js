#pragma strict
var maxIntensity : float = 1; //the intesnsity of the light
var speed : float = 2; //how fast the lights turn on and off
var lightsOn = true; //whether lights ar on or off
var endLoop = true; //stop loop

function Lights() {
	endLoop = true;
	yield;
	
	if(lightsOn == true) {
		if(GetComponent(Light).intensity > 0) {
			lightsOn = false;
			LightsOff();
		}
	}
	else {
		if(GetComponent(Light).intensity < maxIntensity) {
			lightsOn = true;
			LightsOn();
		}
	}
}
function LightsOff() {
	endLoop = false;
	while(endLoop == false) {
		GetComponent(Light).intensity -= speed * Time.deltaTime;
		
		if(GetComponent(Light).intensity <= 0) {
			endLoop = true;
		}
		yield;
	}
}
function LightsOn() {
	endLoop = false;
	while(endLoop == false) {
		GetComponent(Light).intensity += speed * Time.deltaTime;
		
		if(GetComponent(Light).intensity >= maxIntensity) {
			endLoop = true;
		}
		yield;
	}
}