using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GDGeek;
using Valve.VR;

public class TouchUI : MonoBehaviour {
	
	public TouchKey _touchKey = null;
	private TouchHandle handle_ = null;
	public Camera _camera;

	public SteamVR_TrackedController _tracked = null;

	private FSM fsm_ = new FSM ();

	private State getLeave(){
		StateWithEventMap swem = new StateWithEventMap ();

		swem.addAction ("touched", "touched");
		swem.addAction ("clicked", "clicked");
		return swem;
	}


	private State getTouch(){
		StateWithEventMap swem = new StateWithEventMap ();


		swem.addAction ("clicked", "clicked");
		swem.addAction ("untouched", "leave");
		return swem;
	}


	private State getClicked(){
		StateWithEventMap swem = new StateWithEventMap ();
	
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

		fsm_.addState ("leave", getLeave ());
		fsm_.addState ("touched", getTouch ());
		fsm_.addState ("clicked", getClicked ());
		fsm_.init("leave");
	}

	private VRControllerState_t controllerState_;
	private void updatePosition(Vector2 pad){

		var pos = handle_.pad2pos (pad);
		var position = this.transform.localPosition;
		position.x = pos.x;
		position.y = pos.y;
		this.transform.localPosition = position;
	}
	void FixedUpdate(){
		
		var system = OpenVR.System;
		if (handle_ != null && system != null && system.GetControllerState(3, ref controllerState_))
		{

			updatePosition(new Vector2((controllerState_.rAxis0.x +1f)/2, (controllerState_.rAxis0.y+1f)/2));
		
		}
	}
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {

			fsm_.post ("clicked");
		}

		if (Input.GetKeyUp (KeyCode.Space)) {

			fsm_.post ("unclicked");
		}

		if (Input.GetKeyDown (KeyCode.LeftCommand)) {

			fsm_.post ("touched");
		}

		if (Input.GetKeyUp (KeyCode.LeftCommand)) {

			fsm_.post ("untouched");
		}

	}
}
