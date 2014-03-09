#pragma strict

var cube : GameObject;
var spawn_position;
//var timer = 0.0;

function spawn_cube () {
	spawn_position = Vector3(0,0,0);
	var temp_spawn_cube = Instantiate(cube, spawn_position, Quaternion.identity);
	Destroy(temp_spawn_cube, 1.0);
}

function Start () {

}

function Update () {
	//timer += Time.deltaTime;
//	if (timer > 5) {
	if(Input.GetKey("s")) {
		spawn_cube();
		//timer = 0.0;
	}
}