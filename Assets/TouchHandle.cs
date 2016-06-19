using UnityEngine;
using System.Collections;

public class TouchHandle : MonoBehaviour {


	public virtual Vector2 pad2pos(Vector2 pad){


		return new Vector2(pad.x * 110f, pad.y * 70f);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
