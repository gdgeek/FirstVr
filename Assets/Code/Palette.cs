using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;

public class Palette : MonoBehaviour {
	public Image _palette;
	public Image _change;
	public Sprite _colorPalette;
	public Sprite _colorChange;
	public Sprite _grayPalette;
	public Sprite _grayChange;


	public void color(){
		_palette.sprite = _colorPalette;
		_change.sprite = _colorChange;
	}

	public void gray(){

		_palette.sprite = _colorPalette;
		_change.sprite = _colorChange;


	}

	public void init(){
		this.gameObject.SetActive (true);

	}

	public void shutdown(){

		this.gameObject.SetActive (false);
	}


}
