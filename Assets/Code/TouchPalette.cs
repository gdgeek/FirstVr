using UnityEngine;
using System.Collections;
using GDGeek;

public class TouchPalette : TouchHandle {


	private FSM fsm_ = new FSM ();
	public Palette _palette = null;


	private State getColor(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			_palette.color();
		};
		swem.onOver += delegate() {

		};
		return swem;
	}


	private State getGray(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {


			_palette.gray();
			//_palette.sprite = _grayPalette;
			//_change.sprite = _grayChange;
		};
		swem.onOver += delegate() {

		};
		return swem;
	}
	// Use this for initialization
	void Start () {
		fsm_.addState ("color", getColor ());
		fsm_.addState ("gray", getGray ());
		//	gray
	}

	public override void touchWho(GameObject obj){
		Debug.Log (obj.name);
	}

	public override void touchOut(GameObject obj){


	}

	public override void leave(){

		Debug.Log ("leave");
	}

	public override Vector2 pad2pos(Vector2 pad){
		return new Vector2(pad.x * 110f, pad.y * 70f);
	}

	public override void touched(){
		Debug.Log ("touched");

	}
	public override void clicked(){
		Debug.Log ("clicked");

	}

	public override void init(){
		_palette.init ();
	}
	public override void shutdown(){
		_palette.shutdown ();
	}
}
