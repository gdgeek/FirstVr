using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;


public class Keyboard : MonoBehaviour {
	private FSM fsm_ = new FSM ();
	private Key[] keys_ = null;

	private Key[] getKeys(){
		if (keys_ == null) {
			keys_ = this.gameObject.GetComponentsInChildren<Key> (); 
		}
		return keys_;
	}
	private State getUpper(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			Key[] keys = getKeys();
			for(int i =0; i<keys.Length; ++i){

				keys[i].upper();
			}
		};
		//InputField.
		return swem;
	}



	private State getLower(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			Key[] keys = getKeys();
			for(int i =0; i<keys.Length; ++i){

				keys[i].lower();
			}
		};
		//InputField.
		return swem;
	}


	private State getNumber(){
		return new State ();
	}



	private State getPunctuation(){
		return new State ();
	}
	void Start () {
		fsm_.addState ("upper", getUpper());
		fsm_.addState ("lower", getLower());
		fsm_.addState ("number", getNumber());
		fsm_.addState ("punctuation", getPunctuation());
		fsm_.init ("lower");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
