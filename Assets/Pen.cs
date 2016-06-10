using UnityEngine;
using System.Collections;
using GDGeek;

public class Pen : MonoBehaviour {

	enum Action{
		None = 0,
		Add = 1,
		Remove = 2,

	}
	public BrushAdd _add;
	public BrushRemove _remove;
	public BrushNone _none;
	private int action_ = (int)(Action.Add);
	private FSM fsm_ = new FSM (); 
	public Matrix _matrix = null;
	private State getPen(){

		StateWithEventMap swem = new StateWithEventMap ();

		swem.addAction ("drawing", delegate {
			_matrix.draw(true);
		});
		swem.addAction ("drawed", delegate {
			_matrix.draw(false);
		});

		swem.addAction ("action", delegate {


			if(0 != (this.action_ & (int)(Action.Remove))){
				return "remove";
			}
			if(0 != (this.action_ & (int)(Action.Add))){
				return "add";
			}

			return "add";
		});
		return swem;

	}
	private State getAdd(){

		StateWithEventMap swem = new StateWithEventMap ();

		swem.onStart += delegate {
			_matrix.setBrush(_add);
			_add.gameObject.SetActive(true);
		};
		swem.onOver += delegate {

			_add.gameObject.SetActive(false);
		};
		return swem;


	}
	private State getRemove(){
		StateWithEventMap swem = new StateWithEventMap ();

		swem.onStart += delegate {
			_remove.gameObject.SetActive(true);
			_matrix.setBrush(_remove);
		};
		swem.onOver += delegate {
			_remove.gameObject.SetActive(false);
		};
		return swem;
	}
	// Use this for initialization
	void Start () {
		fsm_.addState ("pen", getPen ());

		fsm_.addState ("add", getAdd (), "pen");
		fsm_.addState ("remove", getRemove (), "pen");
	//	fsm_.addState ("none", getNone (), "pen");
		fsm_.init ("pen");
	}
	
	// Update is called once per frame
	public void Update(){

		if (Input.GetKeyDown (KeyCode.Space)) {
			fsm_.post ("drawing");
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			
			fsm_.post ("drawed");
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			action_ |= (int)(Action.Remove);
			action_ ^= (int)(Action.Add);
			fsm_.post ("action");
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			action_ ^= (int)(Action.Remove);
			action_ |= (int)(Action.Add);
			fsm_.post ("action");
		}
	}

}
