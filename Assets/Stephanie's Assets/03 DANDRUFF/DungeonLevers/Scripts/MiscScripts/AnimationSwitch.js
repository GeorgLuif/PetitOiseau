#pragma strict
var animations : String[]; //name of animations to play
private var interval : int = 0; //which animation we're on

function PlayAnimation() {
	//play next animation
	GetComponent.<Animation>().CrossFade(animations[interval]);
	interval += 1;
	//reset to first animation
	if(interval >= animations.length) {
		interval = 0;
	}
}