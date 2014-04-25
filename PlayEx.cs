/******************************************************************
Example script for using the FMOD EX low level sound system.
Initializes the FMOD sound system and loads and plays some sounds.
Visit www.squaretangle.com for updates and more information.
Enjoy.
johnny tangle
********************************************************************/

using UnityEngine;
using System;
using System.Collections;

public class PlayEx : MonoBehaviour {
	public KinectModelControllerV2 controller;
	public color_change color_controller;

	float r = 0; float g = 0; float b = 0; float newalpha = 0;
	float handdistance;
	float musicspeed = 0f;
	float musicvolume = 0f;
	float newvolume = 0f;
	float currentalpha = 0.5f;
	Vector3 rightHandPos;
	Vector3 leftHandPos;
	public GameObject theparticles;
	public GameObject theparticles2;
	public GameObject theplane;
	Vector3 rightHandPrev;
	Vector3 leftHandPrev;

		private FMOD.System     system  = null;
        private FMOD.Sound      sound1  = null, sound2 = null, sound3 = null;
        private FMOD.Channel    channel = null;
		private FMOD.Channel    channeldos = null;
        //private uint            ms      = 0;
        //private uint            lenms   = 0;
        //private bool            playing = false;
        //private bool            paused  = false;
        //private int             channelsplaying = 0;
        private bool			soundPlayed = false;

	// Use this for initialization
	void Start () {
		// initialize kinect shiz
		theplane = GameObject.Find("ThePlane");
		theparticles = GameObject.Find ("main-emitter");
		theparticles2 = GameObject.Find ("main-emitter2");
		controller = GetComponent<KinectModelControllerV2> ();
		color_controller = theplane.GetComponent<color_change> ();
		theparticles.particleSystem.Pause ();
		theparticles2.particleSystem.Pause ();
		Debug.Log("colorcontroller g" + color_controller.g); 
		handdistance = 3.5f;//rightHandPos.x + leftHandPos.x;
		//r = 0.5f;
		//g = 0.46f;
		//b = 0.0092f;
		r = 0.0f; g = 0.0f; b = 0.0f;
		newalpha = 0.5f;

		//





            uint            version = 0;
            FMOD.RESULT     result;

            
			//Create an FMOD System object            
            result = FMOD.Factory.System_Create(ref system);
            ERRCHECK(result);
			//Check FMOD Version
            result = system.getVersion(ref version);
            ERRCHECK(result);
            if (version < FMOD.VERSION.number)
            {
                Debug.Log("Error!  You are using an old version of FMOD " + version.ToString("X") + ".  This program requires " + FMOD.VERSION.number.ToString("X") + ".");
            }
			//Initialize the FMOD system object
            result = system.init(32, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
            ERRCHECK(result);
            if (result == FMOD.RESULT.OK)
            {
                Debug.Log("FMOD init! " + result );
            }
			//Create some sound references to play
		//rina was here
			//result = system.createSound("Assets/FMOD/drumloop.wav", FMOD.MODE.HARDWARE, ref sound1);
			result = system.createSound ("C:/Users/rina/Downloads/flowers-midi.mid", FMOD.MODE.HARDWARE, ref sound1);
			ERRCHECK(result);
		    sound1.getMusicSpeed(ref musicspeed);

		// note to self: maybe initialize to half volume?

            result = sound1.setMode(FMOD.MODE.LOOP_OFF);
            ERRCHECK(result);

			//result = system.createSound("C:/Users/rina/Downloads/flowers-midi.mid", FMOD.MODE.SOFTWARE, ref sound2);
			result = system.createSound("Assets/FMOD/jaguar.wav", FMOD.MODE.SOFTWARE, ref sound2);
            ERRCHECK(result);

            result = system.createSound("Assets/FMOD/swish.wav", FMOD.MODE.HARDWARE, ref sound3);
            ERRCHECK(result);


	}

	//rina zone
	void play_song(object sender, FMOD.Sound s, System.EventArgs e) {
		FMOD.RESULT result;
		result = system.playSound (FMOD.CHANNELINDEX.FREE, s, false, ref channel);
		ERRCHECK(result);
	}

	void pause_song(FMOD.Channel c) {
		FMOD.RESULT result;
		bool paused = false;

		if (channel != null) {
			result = c.getPaused(ref paused);
			ERRCHECK(result);
			result = c.setPaused(!paused);
			ERRCHECK (result);
		}
			
	}

	void set_volume(FMOD.Channel c, float delta) {
		// if sign == 0, increase volume
		// if sign == 1, decrease volume
		float volume = 0;
		c.getVolume (ref volume);
		volume += delta;
		c.setVolume (volume);
	}

	void set_speed(FMOD.Sound s, float delta){
		//float thesound = 0;
		//s.getMusicSpeed (ref thesound);
		//s.setMusicSpeed (thesound + delta);
		s.setMusicSpeed (musicspeed);
	}
	
	// end rina zone
	
	// Update is called once per frame
	void Update () {

		// testing
//		Debug.Log ("right hand" + controller.Hand_Right.transform.position.x);
		// /testing


		FMOD.RESULT     result;

		//rightHandPos = controller.Hand_Right.transform.position;
		//leftHandPos = controller.Hand_Left.transform.position;
		//if (controller == null /*&& controller.Hand_Left != null && controller.Hand_Right != null*/)	{	
		if (controller.Hand_Left.transform.position.y > controller.Head.transform.position.y &&
		    controller.Hand_Right.transform.position.y > controller.Head.transform.position.y &&
		    soundPlayed == false) {
			//Use some keyboard input to play the sound references		
			//if (Input.GetKeyDown ("1")) {
						//result = system.playSound(FMOD.CHANNELINDEX.FREE, sound1, false, ref channel);
						//ERRCHECK(result);
						//rina zone
						result = system.playSound (FMOD.CHANNELINDEX.FREE, sound1, false, ref channel);
						ERRCHECK (result);
						set_volume (channel, -0.4f);
						channel.getVolume(ref musicvolume);
						soundPlayed = true; 
						theparticles.particleSystem.Play ();
						theparticles2.particleSystem.Play ();
			//end rina zone
	//	}
	} else if (Input.GetKeyDown ("2")) {
						result = system.playSound (FMOD.CHANNELINDEX.FREE, sound2, false, ref channel);
						ERRCHECK (result);
				} else if (Input.GetKeyDown ("3")) {

			//channeldos.setLoopCount(-1);
						result = system.playSound (FMOD.CHANNELINDEX.FREE, sound3, false, ref channeldos);
						ERRCHECK (result);
			channeldos.setMode(FMOD.MODE.LOOP_NORMAL);
				}
//rina zone
		else if (Input.GetKeyDown (KeyCode.P)) {
						// initialize
						/*bool paused2 = false;
			
				if (channel != null) {
						result = channel.getPaused (ref paused2);
						ERRCHECK (result);
						// set channel to pause or unpause, depending
						result = channel.setPaused (!paused2);
						ERRCHECK (result);
				}*/
						pause_song (channel);
		} else if (Input.GetKeyDown (KeyCode.L)) {
			pause_song (channeldos);
			//pause_song ();
		}


		else if (Input.GetKeyDown (KeyCode.V)) {
				/*float volume = 0;
				channel.getVolume (ref volume);
				volume += 0.1f;
				channel.setVolume (volume);*/
			set_volume(channel, 0.1f);
		} else if (Input.GetKeyDown (KeyCode.C)) {
				/*float volume = 0;
				channel.getVolume (ref volume);
				volume -= 0.1f;
				channel.setVolume (volume);*/
			set_volume(channel, -0.1f);
		} else if (Input.GetKeyDown (KeyCode.D)) {
				float frequency = 0;
				channel.getFrequency (ref frequency);
				frequency -= 500.0f;
				channel.setFrequency (frequency);
		} else if (Input.GetKeyDown (KeyCode.F)) {
				float frequency = 0;
				channel.getFrequency (ref frequency);
				frequency += 500.0f;
				channel.setFrequency (frequency);
		} else if (Input.GetKeyDown ("[")) {
				float pan = 0;
				channel.getPan (ref pan);
				pan -= 0.1f;
				channel.setPan (pan);
		} else if (Input.GetKeyDown ("]")) {
				float pan = 0;
				channel.getPan (ref pan);
				pan += 0.1f;
				channel.setPan (pan);
		} else if (Input.GetKeyDown (KeyCode.S)) {
				/*float thesound = 0;
				sound1.getMusicSpeed (ref thesound);
				sound1.setMusicSpeed (thesound + 0.1f);*/
			set_speed(sound1, 0.1f);
		} else if (Input.GetKeyDown (KeyCode.A)) {
				/*float thesound = 0;
				sound1.getMusicSpeed(ref thesound);
				sound1.setMusicSpeed (thesound - 0.1f);*/
			set_speed (sound1,-0.1f);
		}

		
		bool playing = false;
		bool paused = false;
		if (channel != null) {
			result = channel.isPlaying(ref playing);
			if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE)){
				ERRCHECK(result);
			}
			result = channel.getPaused(ref paused);
			if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE)){
				ERRCHECK(result);
			}
		}


		// calculate distance between hands
		if (controller != null) {
				//float newspeed = ((2f / 3.5f) * (Mathf.Abs (controller.Hand_Left.transform.position.x) + Mathf.Abs (controller.Hand_Right.transform.position.x))) + musicspeed - 1f;
				//sound1.setMusicSpeed (newspeed);
			if (channel != null){			

				newvolume = ((2f / 0.6f) * (Mathf.Abs (controller.Hand_Left.transform.position.y + controller.Hand_Right.transform.position.y))) + musicvolume - 1f;
				channel.setVolume (newvolume);

				//Debug.Log("VOLUME " + newvolume);
			}
						//Debug.Log ("current y of hand is" + controller.Hand_Left.transform.position.y);
			newalpha = newvolume/10.0f;//(controller.Hand_Left.transform.position.y - 0.19f)/(3.21f-0.19f);
			if (newalpha > 1) {
				newalpha = 1;
			}
			if (newalpha < 0) {
				newalpha = 0;
			}
			//Debug.Log("new alpha is " + newalpha);
			//this changes the alpha channel depending on how high or low the left hand is
			//theplane.renderer.material.color = new Color(theplane.renderer.material.color.r, theplane.renderer.material.color.g, theplane.renderer.material.color.b, newalpha);

			theparticles.particleSystem.startSpeed = newalpha*30.0f;
			theparticles2.particleSystem.startSpeed = newalpha*30.0f;
			//Debug.Log(newalpha);
			//theplane.renderer.material.color = new Color(r,g,b,newalpha);
			//Debug.Log("speed of right hand " + controller.Hand_Right.rigidbody.velocity);
		
		
		}

		if (controller != null) {
			rightHandPos = controller.Hand_Right.transform.position;
			Vector3 velocity = (rightHandPos - rightHandPrev) / Time.deltaTime;
			float speed = velocity.magnitude;
			if (speed > 10) {
				speed = 10;
			}
			if (speed < 0) {
				speed = 0;
			}
		//Debug.Log ("speed is " + speed);

			//float newspeed = ((2f / 3.5f) * (Mathf.Abs (controller.Hand_Left.transform.position.x) + Mathf.Abs (controller.Hand_Right.transform.position.x))) + musicspeed - 1f;
			sound1.setMusicSpeed (speed/4.5f);

			//sound1.getMusicSpeed(ref musicspeed);
			//Debug.Log("speed is" + speed);
			//float deltat = speed/10.0f;

			//this was my attempt to interpolate the color - tis a disaster
			/*System.Math.Round(speed,1);

			r = (1 - speed/10f) * color_controller.blue.r + speed * color_controller.yellow.r;
			g = (1 - speed/10f) * color_controller.blue.g + speed * color_controller.yellow.g;
			b = (1 - speed/10f) * color_controller.blue.b + speed * color_controller.yellow.b;
			if ( r < 0 || g < 0 || b > 1){
				r = 0; g = 0; b = 1;
			}
			theplane.renderer.material.color = new Color(r,g,b,newalpha);
*/
		}


		rightHandPrev = rightHandPos;
		// end rina zone

	} // end Update()
	
	void OnDisable() {
		//cleanup routine here
				
				//Shut down
                FMOD.RESULT     result;
                
                if (sound1 != null)
                {
                    result = sound1.release();
                    ERRCHECK(result);
                }
                if (sound2 != null)
                {
                    result = sound2.release();
                    ERRCHECK(result);
                }
                if (sound3 != null)
                {
                    result = sound3.release();
                    ERRCHECK(result);
                }

                if (system != null)
                {
                    result = system.close();
                    ERRCHECK(result);
                    result = system.release();
                    ERRCHECK(result);
					Debug.Log("FMOD release! " + result );
                }
    }
	//FMOD error checking codes
	void ERRCHECK(FMOD.RESULT result)
        {
            if (result != FMOD.RESULT.OK)
            {
                Debug.Log("FMOD error! " + result + " - " + FMOD.Error.String(result));
               
            }
        }
}



