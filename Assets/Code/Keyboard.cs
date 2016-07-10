using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;


public class Keyboard : MonoBehaviour {
	private FSM fsm_ = new FSM ();
	private Key[] keys_ = null;
	public Image _sample;
	public Rect _rect;
	private Key[] getKeys(){
		if (keys_ == null) {
			keys_ = this.gameObject.GetComponentsInChildren<Key> (); 
		}
		return keys_;
	}

	/*
	private State getUpper(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			Key[] keys = getKeys();
			for(int i =0; i<keys.Length; ++i){
				keys[i].upper();
			}
		};
		swem.addAction ("shift", "lower");
		swem.addAction ("number", "number");
		return swem;
	}
*/

	public void init(){
		this._sample.gameObject.SetActive (true);
		this.gameObject.SetActive (false);

	}

	public void shutdown(){

		this.gameObject.SetActive (false);
		this._sample.gameObject.SetActive (false);
	}

	public void toLower(){
	
		Key[] keys = getKeys();
		for(int i =0; i<keys.Length; ++i){
			keys[i].lower();
		}
	}
	/*
	private State getLower(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			toLower();
		};
		swem.addAction ("shift", "upper");
		swem.addAction ("number", "number");
		return swem;
	}
	*/
	public void toUpper(){

		Key[] keys = getKeys();
		for(int i =0; i<keys.Length; ++i){
			keys[i].upper();
		}
	}
	public void toNumber(){

		Key[] keys = getKeys();
		for(int i =0; i<keys.Length; ++i){
			keys[i].number();
		}
	}

	public void toPunctuation(){


		Key[] keys = getKeys();
		for(int i =0; i<keys.Length; ++i){

			keys[i].punctuation();
		}
	}

	/*
	private State getNumber(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			toNumber();
		};
		swem.addAction ("shift", "punctuation");
		swem.addAction ("number", "lower");
		//InputField.
		return swem;
	}

	public void _input(Key key){
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
			Key[] keys = getKeys();
			for(int i =0; i<keys.Length; ++i){

				keys[i].punctuation();
			}
		};
		swem.addAction ("shift", "number");
		swem.addAction ("number", "lower");
		return swem;
	}
	void Start () {
		fsm_.addState ("upper", getUpper());
		fsm_.addState ("lower", getLower());
		fsm_.addState ("number", getNumber());
		fsm_.addState ("punctuation", getPunctuation());
		fsm_.init ("lower");
	}
	*/

}
