using UnityEngine;
using System.Collections;
using GDGeek;

public class Brush : MonoBehaviour {

	enum Action{
		None = 0,
		Add = 1,
		Remove = 2,

	}
	public GameObject _add;
	public GameObject _remove;
	private int action_ = (int)(Action.None);
	private FSM fsm_ = new FSM (); 

	private State getNormal(){

		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("action", delegate {

			if(0 != (this.action_ & (int)(Action.Add))){
				return "add";

			}


			if(0 != (this.action_ & (int)(Action.Remove))){
				return "remove";

			}
			return "";
		});
		return swem;

	}
	private State getAdd(){

		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("action", delegate {

			if( 0 == (this.action_ & (int)(Action.Add))){
				if(0 != (this.action_ & (int)(Action.Remove))){
					return "remove";
				}
				return "normal";

			}
			return "";
		});
		swem.onStart += delegate {

		};
		return swem;


	}
	private State getRemove(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("action", delegate {

			if( 0 == (this.action_ & (int)(Action.Remove))){
				if(0 != (this.action_ & (int)(Action.Add))){
					return "add";
				}
				return "normal";

			}
			return "";
		});
		return swem;
	}
	// Use this for initialization
	void Start () {
		fsm_.addState ("normal", getNormal ());
		fsm_.addState ("add", getAdd ());
		fsm_.addState ("remove", getRemove ());
		fsm_.init ("normal");
	}
	
	// Update is called once per frame
	public void Update(){

		if (Input.GetKeyDown (KeyCode.Space)) {
			action_ |= (int)(Action.Add);//fsm_.post ("add");

			fsm_.post ("action");
		}

		if (Input.GetKeyUp (KeyCode.Space)) {

			action_ ^= (int)(Action.Add); //	action_ |= (int)(Action.Add);	fsm_.post ("no_add");
			fsm_.post ("action");
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			action_ |= (int)(Action.Remove);//	fsm_.post ("remove");
			fsm_.post ("action");
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			action_ ^= (int)(Action.Remove);
			fsm_.post ("action");
		}
	}

}
