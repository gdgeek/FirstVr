using UnityEngine;
using System.Collections;

public class TouchKey : TouchHandle {


	public Key _key = null;
	public Key _clicked = null;
	public KeyHighlight _hightlight;
	public Keyboard _keyboard;
	enum State{
		Leave,
		Touched,
		Clicked,
	};
	private State state_ = State.Leave;
	public override void touchWho(GameObject obj){

		_key = obj.GetComponent<Key> ();
		if (state_ == State.Touched) {
			_hightlight.fNormal (_clicked);
			_hightlight.fOver (_key);

		}

	


	public override Vector2 pad2pos(Vector2 pad){
		/*
		 * 
		 *   __
			/  \
			\__/

			___
			|  |
			|  |
			---

		*/
		return new Vector2(pad.x * 220f -110f, pad.y * 140f - 70f);
	}
	public override void leave(){
		state_ = State.Leave;
		_hightlight.fNormal (_key);
	}


	public override void touched(){
		state_ = State.Touched;

		_hightlight.fNormal (_clicked);
		_hightlight.fOver (_key);
		Debug.Log ("touched");

	}
	public override void clicked(){
		state_ = State.Clicked;
		_clicked = _key;

		_keyboard.input (_clicked);
		//_key.getText
		_hightlight.fDown (_clicked);
		Debug.Log ("clicked");

	}
}
