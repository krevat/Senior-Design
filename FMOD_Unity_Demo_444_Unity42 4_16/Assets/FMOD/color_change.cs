using UnityEngine;
using System.Collections;

public class color_change : MonoBehaviour {
	
	/*float r = 0.5f;
	float g = 0.46f;
	float b = 0.492f;
	float a = 0.5f;
	float delta = 0.1f;
	float deltat = 0.5f;*/
	float r, g, b, a, delta, deltat;
	Color blue = new Color(0,0,1,1);
	Color yellow = new Color(1, 0.92f, 0.016f,1);
	
	// Use this for initialization
	void Start () {
		r = 0.5f;
		g = 0.46f;
		b = 0.0092f;
		a = 0.5f;
		delta = 0.1f;
		deltat = 0.5f;
		renderer.material.shader = Shader.Find ("Transparent/Diffuse");
		renderer.material.color = new Color(r,g,b,a);
	}
	
	
	// Update is called once per frame
	// c: lower volume
	// v: raise volume
	// a: make slower
	// s; make faster
	void Update () {
		//if ( Input.GetKeyDown( KeyCode.LeftControl )&& Input.GetKeyDown( KeyCode.V ) ){
		if (Input.GetKeyDown(KeyCode.V)){
			if (a <= 1 - delta) {
				a += delta;
				renderer.material.color = new Color(r,g,b,a);
			}
			Debug.Log ("detected a c");

		}
		if (Input.GetKeyDown(KeyCode.C)){
			if (a >= delta){
				a -= delta;
				renderer.material.color = new Color(r,g,b,a);
			}
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			//c = (1 - t) * c0 + t * c1
			//slower : make it more blue
			deltat -= 0.1f;
			r = (1 - deltat) * blue.r + deltat * yellow.r;
			g = (1 - deltat) * blue.g + deltat * yellow.g;
			b = (1 - deltat) * blue.b + deltat * yellow.b;
			if ( r < 0 || g < 0 || b > 1){
				r = 0; g = 0; b = 1;
				deltat += 0.1f;
			}
			gameObject.renderer.material.color = new Color(r,g,b,a);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			//c = (1 - t) * c0 + t * c1
			//faster : make it more yellow
			deltat += 0.1f;
			r = (1 - deltat) * blue.r + deltat * yellow.r;
			g = (1 - deltat) * blue.g + deltat * yellow.g;
			b = (1 - deltat) * blue.b + deltat * yellow.b;

			if ( r > 1.0f || g > 0.92f || b < 0.016f){
				r = 1.0f; g = 0.92f; b = 0.016f;
				deltat -= 0.1f;
			}

			gameObject.renderer.material.color = new Color(r,g,b,a);
		}

		/*if (Input.GetKeyDown("1")){
			renderer.material.shader = Shader.Find ("Transparent/Diffuse");
			renderer.material.color = Color.red;
			
		}*/
	}
}
