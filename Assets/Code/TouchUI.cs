using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GDGeek;
using Valve.VR;

public class TouchUI : MonoBehaviour {
	
	//public TouchKey _touchKey = null;
	//public TouchPalette _palette = null;
	//private TouchHandle handle_ = null;

	public InputManager _inputManager = null;
	public Camera _camera;
	public GameObject _biao;
	public SteamVR_TrackedController _tracked = null;
	private VRControllerState_t controllerState_;

	private FSM fsm_ = new FSM ();
	//private FSM typeFsm_ = new FSM ();

	private State getLeaved(){
		StateWithEventMap swem = new StateWithEventMap ();


		swem.addAction ("touched", "touched");
		swem.addAction ("clicked", "clicked");
		swem.onStart += delegate() {
			_inputManager.handle.leavedIn();

			_biao.SetActive(false);
			SphereCollider c = this.GetComponent<SphereCollider>();
			c.enabled = false;
			_inputManager.locked = false;

		};
		swem.onOver += delegate {

			_inputManager.locked = true;
			_inputManager.handle.leavedOut();
		};

		return swem;
	}


	private State getTouched(){
		StateWithEventMap swem = new StateWithEventMap ();


		swem.onStart += delegate() {
			//_inputManager._touchKey.touched();

			_inputManager.handle.touchedIn();
			SphereCollider c = this.GetComponent<SphereCollider>();
			c.enabled = true;
			_biao.SetActive(true);
		};
		swem.addAction ("clicked", "clicked");
		swem.addAction ("untouched", "leave");

		swem.onOver += delegate {

			_inputManager.handle.touchedOut();
		};
		return swem;
	}

	public void OnTriggerEnter(Collider other){

		_inputManager.handle.touchIn (other.gameObject);
	}

	public void OnTriggerExit(Collider other){
		_inputManager.handle.touchOut (other.gameObject);
	}/*
	private State getInit(){
		StateWithEventMap swem = TaskState.Create (delegate {
			Task task = new Task();
			TaskManager.PushFront(task, delegate {
				this._touchKey.shutdown();
				this._palette.shutdown();
			});
			return task;
		}, this.typeFsm_, "keyboard");

		return swem;
	}
	private State getKeyboard(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			_touchKey.init();
			this.handle_ = _touchKey;
		};
		swem.onOver += delegate {
			
			_touchKey.shutdown();
		};

		swem.addAction ("unmenu", delegate {
			return "palette";
		});
		return swem;
	}

	private State getPalette(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {

			_palette.init();
			this.handle_ = _palette;
		};
		swem.onOver += delegate {
			_palette.shutdown();
		};


		swem.addAction ("unmenu", "keyboard");
		return swem;
	}
*/
	private State getClicked(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			Debug.Log("i am clicked!");

			_inputManager.handle.clickedIn();
		//	_inputManager._touchKey.clicked();
		};
		swem.addAction ("untouched", "leave");
		swem.addAction ("unclicked", "touched");
		swem.onOver += delegate() {
			_inputManager.handle.clickedOut();
		};
		return swem;
	}



	// Use this for initialization
	void Start () {
		
		EasyTouch.On_TouchDown += delegate(Gesture gesture) {
			var pos = _camera.ScreenToViewportPoint(gesture.position);
			updatePosition(_camera.ScreenToViewportPoint(gesture.position));

		};
		//handle_ = this._touchKey;
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
		/*_tracked.MenuButtonClicked += delegate(object sender, ClickedEventArgs e) {
			typeFsm_.post ("menu");
		};


		_tracked.MenuButtonUnclicked += delegate(object sender, ClickedEventArgs e) {
			typeFsm_.post ("unmenu");
		};*/
		fsm_.addState ("leave", getLeaved ());
		fsm_.addState ("touched", getTouched ());
		fsm_.addState ("clicked", getClicked ());
		fsm_.init("leave");
		/*

		typeFsm_.addState ("init", getInit());
		typeFsm_.addState ("keyboard", getKeyboard());
		typeFsm_.addState ("palette", getPalette());
		typeFsm_.init ("init");*/
	}

	private void updatePosition(Vector2 pad){

		var pos = _inputManager.handle.pad2pos (pad);
		var position = this.transform.localPosition;
		position.x = pos.x;
		position.y = pos.y;
		this.transform.localPosition = position;
	}
	void FixedUpdate(){
		
		var system = OpenVR.System;
		if (_inputManager.handle != null && system != null && system.GetControllerState (_tracked.controllerIndex, ref controllerState_)) {
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
		/*
		if (Input.GetKey (KeyCode.Tab)) {
			typeFsm_.post ("menu");
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {

			typeFsm_.post ("unmenu");
		}*/


	}
}
