using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GDGeek;
using Valve.VR;

public class TouchUI : MonoBehaviour {
	public Rect _rect;
	public Camera _camera;
	public Canvas _canvas; 
	public Sprite _image = null;
	public Key _key = null;
	public KeyHighlight _hightlight;
	public Keyboard _keyboard;
	public SteamVR_TrackedController _tracked = null;

	private FSM fsm_ = new FSM ();

	private State getNormal(){
		StateWithEventMap swem = new StateWithEventMap ();
		return swem;
	}


	private State getUp(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			if(_key!=null){
				_hightlight.fOver(_key);
			}
		};
		swem.onOver += delegate() {
			_hightlight.fNormal(_key);
		};

		swem.addAction ("keyin", delegate(FSMEvent evt) {

			_key = (Key)(evt.obj);
			if(_key){
				_hightlight.fOver(_key);
			}
		});

		swem.addAction ("down", delegate(FSMEvent evt) {
			if(_key != null){

				return "down";
			}
			return "";
		});
		swem.addAction ("keyout", delegate(FSMEvent evt) {
			if(_key != null){
				_hightlight.fNormal(_key);
			}
		});
		return swem;
	}


	private State getDown(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			if(_key!=null){
				_hightlight.fDown(_key);
				_keyboard.input(_key);

			}
		};
		swem.onOver += delegate() {
			_hightlight.fNormal(_key);
		};

		swem.addAction ("up", "up");
		return swem;
	}
	// Use this for initialization
	void Start () {
		_tracked.PadClicked += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("down");
		};
		_tracked.PadUnclicked += delegate(object sender, ClickedEventArgs e) {

			fsm_.post ("up");
		};

		fsm_.addState ("nomal", getNormal ());
		fsm_.addState ("up", getUp ());
		fsm_.addState ("down", getDown ());
		fsm_.init("up");
	}

	VRControllerState_t controllerState;
	void Update () {

		var system = OpenVR.System;

		if (system != null && system.GetControllerState(3, ref controllerState))
		{
			var pad = new Vector2(controllerState.rAxis0.x, controllerState.rAxis0.y);
			Debug.Log (pad);
			var pos = this.transform.localPosition;
			pos.x = pad.x * 110f;
			pos.y = pad.y * 70f;
			this.transform.localPosition = pos;


		}
		var point = _camera.WorldToScreenPoint (this.gameObject.transform.position);

		//button.
		var list = IPointerOverUI.IsPointerOverUIObjectS (_canvas, point);
		Key key = null;
		for (int i = 0; i < list.Count; ++i) {
			key = list [i].gameObject.GetComponent<Key>();
		}

		if (key != this._key) {
			if (this._key != null) {
				fsm_.post ("keyout");
			}
			fsm_.post ("keyin", key);


		//	key.fingerOver ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {

			fsm_.post ("down");
		}
	
		if (Input.GetKeyUp (KeyCode.Space)) {

			fsm_.post ("up");
		}
	}
}
