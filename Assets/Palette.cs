using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;

public class Palette : MonoBehaviour {
	private FSM fsm_ = new FSM ();
	public Image _palette;
	public Image _change;
	public Sprite _colorPalette;
	public Sprite _colorChange;
	public Sprite _grayPalette;
	public Sprite _grayChange;
	private State getColor(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			_palette.sprite = _colorChange;
			_change.sprite = _colorChange;
		};
	
		return swem;
	}


	private State getGray(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			_palette.sprite = _grayPalette;
			_change.sprite = _grayChange;
		};

		return swem;
	}
	// Use this for initialization
	void Start () {
		fsm_.addState ("color", getColor ());
		fsm_.addState ("gray", getGray ());
		//	gray
	}
	

}
