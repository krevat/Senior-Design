using UnityEngine;
using System.Collections;

public class color : MonoBehaviour {

	 float r = 255.0f;
	 float g = 0.0f;
	 float b = 0.0f;
	 float a = 255.0f;


	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
		//if ( Input.GetKeyDown( KeyCode.LeftControl )&& Input.GetKeyDown( KeyCode.V ) ){
		if (Input.GetKeyDown(KeyCode.C)){
			if (b <= 235.0f) {
				b += 20.0f;
				gameObject.renderer.material.color = new Color(r/255.0F,g/255.0F,b/255.0F,a/255.0F);
			}
		}
		if (Input.GetKeyDown(KeyCode.V)){
			if (b >= 20.0f){
				b -= 20.0f;
				gameObject.renderer.material.color = new Color(r/255.0F,g/255.0F,b/255.0F,a/255.0F);
			}
		}
	}
}
