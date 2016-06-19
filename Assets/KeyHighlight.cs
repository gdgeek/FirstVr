using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GDGeek;



public class KeyHighlight : MonoBehaviour{

	public RectTransform _transform;
	public Text _text = null;
	public Sprite _normal;
	public Sprite _down;
	private Text getText(){
		if (_text == null) {
			_text = this.gameObject.GetComponentInChildren<Text> ();
		}
		return _text;

	}
	public void fOver(Key key){
		if (key != null) {
			this._text.text = key.getText ().text;
			this.transform.position = key.gameObject.transform.position;
			this._transform.sizeDelta = key.GetComponent<RectTransform> ().sizeDelta;
		}
		this.gameObject.SetActive (true);

	}
	public void fDown(Key key){
		if (key) {
			key.getImage ().sprite = _down;
		}
		this.gameObject.SetActive (false);
	}
	public void normal(){

		this.gameObject.SetActive (false);
	}
	public void fNormal(Key key){
		if (key) {
			key.getImage ().sprite = _normal;
		}
		this.gameObject.SetActive (false);
	}
}

