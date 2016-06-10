using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using GDGeek;



public class KeyHighlight : MonoBehaviour{


	public Text _text = null;
	private Text getText(){
		if (_text == null) {
			_text = this.gameObject.GetComponentInChildren<Text> ();
		}
		return _text;

	}
	public void close(){
		this.gameObject.SetActive (false);

	}


	public void open(){
		this.gameObject.SetActive (true);

	}
	public void handle(Key key){
		
	}
}

