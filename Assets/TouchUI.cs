using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GDGeek;

public class TouchUI : MonoBehaviour {
	public Camera _camera;
	public Canvas _canvas; 
	public Sprite _image = null;
	public Key _key = null;
	public KeyHighlight _hightlight;

	private FSM fsm_ = new FSM ();

	private State getNormal(){
		StateWithEventMap swem = new StateWithEventMap ();
		return swem;
	}


	private State getUp(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			if(_key!=null){
				_hightlight.handle(_key);
			}
		};

		swem.addAction ("keyin", delegate(FSMEvent evt) {
			_key = (Key)(evt.obj);
			_hightlight.handle(_key);
			_hightlight.open();
		});


		swem.addAction ("keyout", delegate(FSMEvent evt) {
			_hightlight.close();
			//_hightlight.handle(_key);
		});
		return swem;
	}


	private State getDown(){
		StateWithEventMap swem = new StateWithEventMap ();
		return swem;
	}
	// Use this for initialization
	void Start () {

		fsm_.addState ("nomal", getNormal ());
		fsm_.addState ("up", getUp ());
		fsm_.addState ("down", getDown ());
		//fsm_.addState()
		fsm_.init("up");
	}
	
	// Update is called once per frame
	void Update () {
		var point = _camera.WorldToScreenPoint (this.gameObject.transform.position);

		//button.
		var list = IPointerOverUI.IsPointerOverUIObjectS (_canvas, point);
		Key key = null;
		for (int i = 0; i < list.Count; ++i) {
			key = list [i].gameObject.GetComponent<Key>();
		}

		if (key != this._key) {

			fsm_.post ("keyout");
			if (key != null) {
				fsm_.post ("keyin", key);
			}

		//	key.fingerOver ();
		}

	
		
	}
}
