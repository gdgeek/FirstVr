using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GDGeek;


public class VoxelMapCareater : MonoBehaviour {
	public Sprite _sprite;
	public int _layer = 5;
	public Color _soil;
	public VoxelStruct _vs = null;

	//public Image _image;
	// Use this for initialization
	void Start () {
		Color[] colors = _sprite.texture.GetPixels (0, 0, 128, 128);
		for (int i = 0; i < colors.Length; ++i) {
			Debug.Log (colors [i].r);

		}
		//_vs.
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
