	using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.UI;

public class Palette : MonoBehaviour {
	public Image _palette;
	public Image _change;
	public Image _color = null;
	public Image _sample = null;
	public Sprite _colorPalette;
	public Sprite _colorChange;
	public Sprite _grayPalette;
	public Sprite _grayChange;

	public GameObject _pen;
	public void color(){
		_palette.sprite = _colorPalette;
		_change.sprite = _colorChange;
	}

	public void gray(){

		_palette.sprite = _grayPalette;
		_change.sprite = _grayChange;


	}

	public void init(){

		this._sample.gameObject.SetActive (true);
		this.gameObject.SetActive (false);
	}
	public Color getColor(Vector3 point){
		_pen.transform.position = point;
		float x = _pen.transform.localPosition.x/_palette.rectTransform.sizeDelta.x +0.5f;// +_palette.rectTransform.sizeDelta.x/2f)/
		float y= _pen.transform.localPosition.y/_palette.rectTransform.sizeDelta.y +0.5f;

		return _palette.sprite.texture.GetPixel (Mathf.FloorToInt(x * _palette.sprite.texture.width), Mathf.FloorToInt(y * _palette.sprite.texture.height));
		//return Color.white;
	}
	public void shutdown(){

		this.gameObject.SetActive (false);
		this._sample.gameObject.SetActive (false);
	}


}
