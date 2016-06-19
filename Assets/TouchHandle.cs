using UnityEngine;
using System.Collections;

public class TouchHandle : MonoBehaviour {

	//public KeyHighlight 
	public virtual Vector2 pad2pos(Vector2 pad){
		return new Vector2(pad.x * 110f, pad.y * 70f);
	}
	public virtual void touchWho(GameObject obj){
		Debug.Log (obj.name);
	}

	public virtual void leave(){

		Debug.Log ("leave");
	}

	public virtual void touched(){
		Debug.Log ("touched");
		
	}
	public virtual void clicked(){
		Debug.Log ("clicked");

	}
}
