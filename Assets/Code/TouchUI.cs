using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GDGeek;
using Valve.VR;

public class TouchUI : MonoBehaviour {
	
	public TouchKey _touchKey = null;
	public TouchPalette _palette = null;
	private TouchHandle handle_ = null;
	public Camera _camera;
	public GameObject _biao;
	public SteamVR_TrackedController _tracked = null;
	private VRControllerState_t controllerState_;

	private FSM fsm_ = new FSM ();
	private FSM fsm2_ = new FSM ();

	private State getLeave(){
		StateWithEventMap swem = new StateWithEventMap ();

		swem.addAction ("touched", "touched");
		swem.addAction ("clicked", "clicked");
		swem.onStart += delegate() {
			handle_.leave();
			_biao.SetActive(false);
			SphereCollider c = this.GetComponent<SphereCollider>();
			c.enabled = false;
		};


		return swem;
	}


	private State getTouch(){
		StateWithEventMap swem = new StateWithEventMap ();


		swem.onStart += delegate() {
			handle_.touched();

			SphereCollider c = this.GetComponent<SphereCollider>();
			c.enabled = true;
			_biao.SetActive(true);
		};
		swem.addAction ("clicked", "clicked");
		swem.addAction ("untouched", "leave");
		return swem;
	}

	public void OnTriggerEnter(Collider other){
		
		handle_.touchWho (other.gameObject);
	}

	public void OnTriggerExit(Collider other){
//		Debug.Log ("==+"+other.gameObject);
		handle_.touchOut (other.gameObject);
	}
	private State getKeyboard(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {

			Debug.Log ("nna");
			_touchKey.init();
			this.handle_ = _touchKey;
		};
		swem.onOver += delegate {

			Debug.Log ("nnb");
			_touchKey.shutdown();
		};

		swem.addAction ("unmenu", "palette");
		return swem;
	}

	private State getPalette(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {

			Debug.Log ("nnc");
			_palette.init();
			this.handle_ = _touchKey;
		};
		swem.onOver += delegate {
			_palette.shutdown();
		};


		swem.addAction ("unmenu", "keyboard");
		return swem;
	}

	private State getClicked(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			Debug.Log("i am clicked!");
			handle_.clicked();
		};
		swem.addAction ("untouched", "leave");
		swem.addAction ("unclicked", "touched");
		return swem;
	}
	// Use this for initialization
	void Start () {
		EasyTouch.On_TouchDown += delegate(Gesture gesture) {
			var pos = _camera.ScreenToViewportPoint(gesture.position);
			updatePosition(_camera.ScreenToViewportPoint(gesture.position));

		};
		handle_ = this._touchKey;
		_tracked.PadClicked += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("clicked");
		};

		_tracked.PadTouched += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("touched");
		};
		_tracked.PadUntouched += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("untouched");
		};
		_tracked.PadUnclicked += delegate(object sender, ClickedEventArgs e) {

			fsm_.post ("unclicked");
		};
		_tracked.MenuButtonClicked += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("menu");
		};


		_tracked.MenuButtonUnclicked += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("unmenu");
		};
		fsm_.addState ("leave", getLeave ());
		fsm_.addState ("touched", getTouch ());
		fsm_.addState ("clicked", getClicked ());
		fsm_.init("leave");

		fsm2_.addState ("keyboard", getKeyboard());
		fsm2_.addState ("palette", getPalette());
		fsm2_.init ("keyboard");
	}

	private void updatePosition(Vector2 pad){

		var pos = handle_.pad2pos (pad);
//		Debug.Log (pad + "!" + pos);
		var position = this.transform.localPosition;
		position.x = pos.x;
		position.y = pos.y;
		this.transform.localPosition = position;
	}
	void FixedUpdate(){
		
		var system = OpenVR.System;
		if (handle_ != null && system != null && system.GetControllerState (1, ref controllerState_)) {
			updatePosition (new Vector2 ((controllerState_.rAxis0.x + 1f) / 2, (controllerState_.rAxis0.y + 1f) / 2));
		}
	}
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Z)) {

			fsm_.post ("clicked");
		}

		if (Input.GetKeyUp (KeyCode.Z)) {
			
			Debug.Log("i going unclicked!");
			fsm_.post ("unclicked");
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) {

			fsm_.post ("touched");
		}

		if (Input.GetKeyUp (KeyCode.LeftAlt)) {

			fsm_.post ("untouched");
		}
		if (Input.GetKey (KeyCode.Tab)) {

			fsm_.post ("menu");
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {
			Debug.Log ("nn");
			fsm2_.post ("unmenu");
		}


	}
}
