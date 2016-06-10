using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Key : MonoBehaviour {
	public enum Type{
		Letter,
		Shift,
		Del,
		Return,
		Number,
		Space,

	};
	public Type _type;
	public string _upper;
	public string _lower;
	public string _number;
	public string _punctuation; 
	public Text _text = null;
	private Text getText(){
		if (_text == null) {
			_text = this.gameObject.GetComponentInChildren<Text> ();
		}
		return _text;

	}
	public void upper(){
		Text text = this.getText ();
		text.text = this._upper;
	}


	public void lower(){
		Text text = this.getText ();
		text.text = this._lower;
	}
	public void number(){
		Text text = this.getText ();
		text.text = this._number;
	}
	public void punctuation(){
		Text text = this.getText ();
		text.text = this._punctuation;
	}

	public void fingerOver(){


	}

	public void fingerDown(){


	}
	public void fingerNormal(){


	}


}
