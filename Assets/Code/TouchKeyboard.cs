using UnityEngine;
using System.Collections;
using GDGeek;

public class TouchKeyboard : TouchHandle {


	private FSM fsm_ = new FSM ();
	private Key key_ = null;
	private Key clicked_ = null;
	public KeyHighlight _hightlight;
	public Keyboard _keyboard;
	private PadState _state = PadState.None;

	public override void init(){
		_state = PadState.Leaved;


		key_ = null;
		clicked_ = null;
		_keyboard.init ();
	}
	public override void shutdown(){
		_state = PadState.None;
		_keyboard.shutdown ();
	}


	public override void touchIn(GameObject obj){

		if (_state == PadState.Touched) {
			
			if (key_ != null) {
				_hightlight.fOver (key_);
			}
		}
		key_ = obj.GetComponent<Key> ();
	}


	public override void touchOut(GameObject obj){

		if (obj.GetComponent<Key> () == key_) {
			if (_state == PadState.Touched) {
				if (key_ != null) {
					_hightlight.fNormal (key_);
				}
			}
			key_ = null;
		}
	}



	public override Vector2 pad2pos(Vector2 pad){
		
		var l = Mathf.Sqrt (2f) / 2f;
		var n = (1f - l) / 2f;
		if (pad.x < n) {
			pad.x = n;
		}
		if (pad.x > l + n) {
			pad.x = l + n;
		}
		pad.x = (pad.x-n)/l;


		if (pad.y < n) {
			pad.y = n;
		}
		if (pad.y > l + n) {
			pad.y = l + n;
		}
		pad.y = (pad.y-n)/l;

		return new Vector2((pad.x -0.5f)* _keyboard._rect.width, (pad.y -0.5f)* _keyboard._rect.height);
	}




	public override void leavedIn(){

		_state = PadState.Leaved;
		Debug.Log ("leave in");
		_hightlight.normal();
		if(key_ != null){
			_hightlight.fNormal (key_);
		}
		if(clicked_ != null){
			_hightlight.fNormal (clicked_);
		}

		_keyboard.gameObject.SetActive(false);
		_keyboard._sample.gameObject.SetActive(true);

	}

	public override void touchedIn(){

		_state = PadState.Touched;
		_keyboard.gameObject.SetActive(true);
		_keyboard._sample.gameObject.SetActive(false);
		Debug.Log("touched");
		if(key_ != null){
			_hightlight.fOver (key_);
		}



	}


	public override void clickedIn(){
		_state = PadState.Clicked;

		if(key_ != null){
			clicked_ = key_;
			this.input (clicked_);
			_hightlight.fDown(clicked_);
		}

	}

	public override void clickedOut(){

		if(clicked_ != null){
			_hightlight.fNormal(clicked_);
			clicked_ = null;
		}

		if(key_ != null){
			_hightlight.fOver (key_);
		}

	}
	/// <summary>
	/// ///////////////////////////
	/// </summary>
	/// <returns>The lower.</returns>

	private State getLower(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {

			_keyboard.toLower();
		};
		swem.addAction ("shift", "upper");
		swem.addAction ("number", "number");
		return swem;
	}

	private State getUpper(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			_keyboard.toUpper();
		};
		swem.addAction ("shift", "lower");
		swem.addAction ("number", "number");
		return swem;
	}



	private State getNumber(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			_keyboard.toNumber();

		};
		swem.addAction ("shift", "punctuation");
		swem.addAction ("number", "lower");
		//InputField.
		return swem;
	}

	public void input(Key key){
		if (key._type == Key.Type.Shift) {
			fsm_.post ("shift");
		}else if (key._type == Key.Type.Del) {
			fsm_.post ("del");
		}else if (key._type == Key.Type.Letter) {
			fsm_.post ("letter",key.getText().text);
		}else if (key._type == Key.Type.Shift) {
			fsm_.post ("shift");
		}else if (key._type == Key.Type.Space) {
			fsm_.post ("space");
		}else if (key._type == Key.Type.Number) {
			fsm_.post ("number");
		}
	}

	private State getPunctuation(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			this._keyboard.toPunctuation();
		
		};
		swem.addAction ("shift", "number");
		swem.addAction ("number", "lower");
		return swem;
	}



	////////////////////////
	/// 
	/// 
	/// 
	public void Start(){


		fsm_.addState ("upper", getUpper());
		fsm_.addState ("lower", getLower());
		fsm_.addState ("number", getNumber());
		fsm_.addState ("punctuation", getPunctuation());
		fsm_.init ("lower");

	}




}
