using UnityEngine;
using System.Collections;
using GDGeek;

public class TouchKey : TouchHandle {


	private Key key_ = null;
	private Key clicked_ = null;
	public KeyHighlight _hightlight;

	public Keyboard _keyboard;
	private FSM fsm_ = new FSM();

	/*
	enum State{
		Leave,
		Touched,
		Clicked,
	};*/
//	private State state_ = State.Leave;
	private State getLeave(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("touched", "touched");
		swem.addAction ("clicked", "clicked");
		swem.onStart += delegate() {
			_hightlight.normal();
			Debug.Log("leave");
			if(key_ != null){
				_hightlight.fNormal (key_);
			}
			if(clicked_ != null){
				_hightlight.fNormal (clicked_);
			}
			
		};
		return swem;
	}


	private State getTouched(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("leave", "leave");
		swem.addAction ("clicked", "clicked");

		swem.onStart += delegate() {
			Debug.Log("touched");
			if(key_ != null){
				_hightlight.fOver (key_);
			}
		};
		swem.addAction ("in", delegate(FSMEvent evt) {
			if(key_ != null){
				_hightlight.fOver (key_);
			}
		});

		swem.addAction ("out", delegate(FSMEvent evt) {
			if(key_ != null){
				_hightlight.fNormal (key_);
			}
		});

		return swem;
	}

	public override void init(){
		_keyboard.init ();
	}
	public override void shutdown(){
		_keyboard.shutdown ();
	}
	private State getClicked(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("leave", "leave");
		swem.addAction ("touched", "touched");

		swem.onStart += delegate() {

			Debug.Log("clicked");
			if(key_ != null){
				clicked_ = key_;

				_keyboard.input (clicked_);
				_hightlight.fDown(clicked_);//.fOver (key_);
			}
		};
		swem.onOver += delegate() {
			if(clicked_ != null){
				_hightlight.fNormal(clicked_);
				clicked_ = null;
			}
		};

		return swem;
	}
	public void Start(){
		fsm_.addState ("leave", getLeave());
		fsm_.addState ("touched", getTouched ());
		fsm_.addState ("clicked", getClicked ());
		fsm_.init ("leave");
	}

	public override void touchWho(GameObject obj){

		key_ = obj.GetComponent<Key> ();
		this.fsm_.post ("in");
		//	_hightlight.fNormal (clicked_);
		//	_hightlight.fOver (key_);


	}


	public override void touchOut(GameObject obj){

		if (obj.GetComponent<Key> () == key_) {
			this.fsm_.post ("out");
			key_ = null;
		}
	}


	public Vector2 mapped(Vector2 pad){
		return pad;
		
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
	public override void leave(){
		
		//state_ = State.Leave;
	


		fsm_.post ("leave");
	}


	public override void touched(){
		//state_ = State.Touched;

		//_hightlight.fNormal (clicked_);
		//_hightlight.fOver (key_);
		fsm_.post ("touched");
		//Debug.Log ("touched" + key_);

	}
	public override void clicked(){
		//state_ = State.Clicked;
		/*clicked_ = key_;
		if (clicked_ != null) {
			_keyboard.input (clicked_);
			_hightlight.fDown (clicked_);
		}
*/
		fsm_.post ("clicked");
		//Debug.Log ("clicked");

	}
}
