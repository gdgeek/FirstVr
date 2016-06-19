using UnityEngine;
using System.Collections;

public class TouchKey : TouchHandle {


	public Key _key = null;
	public KeyHighlight _hightlight;
	public Keyboard _keyboard;
	public override Vector2 pad2pos(Vector2 pad){


		return new Vector2(pad.x * 220f -110f, pad.y * 140f - 70f);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
