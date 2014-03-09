#pragma strict

	var objCube:GameObject;

function Start () {
	objCube.transform.position.x = 0;
	objCube.transform.position.z = 0;
}

function Update () {
	if (Input.GetKey("up")) { 
		objCube.transform.position.z += 1;
	}
	if (Input.GetKey("down")) { 
		objCube.transform.position.z -= 1;
	}
	if (Input.GetKey("left")) { 
		objCube.transform.position.x -= 1;
	}
	if (Input.GetKey("right")) { 
		objCube.transform.position.x += 1;
	}
	if (Input.GetKey("r")) { 
		objCube.transform.position.x = 0;
		objCube.transform.position.z = 0;
	}
}