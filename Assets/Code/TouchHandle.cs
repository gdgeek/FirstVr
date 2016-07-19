using UnityEngine;
using System.Collections;

public class TouchHandle : MonoBehaviour {

	//public KeyHighlight 
	public enum PadState{
		Leaved,
		Touched,
		Clicked,
		None,
	}

	public virtual Vector2 pad2pos(Vector2 pad){
		return pad;
	}
	public virtual void touchIn(GameObject obj){
		Debug.Log (obj.name);
	}

	public virtual void touchOut(GameObject obj){
	
	}

	public virtual void init(){
	
	}
	public virtual void shutdown(){
	
	}


	public virtual void leavedIn(){

		Debug.Log ("leave in");
	}

	public virtual void leavedOut(){

		Debug.Log ("leave out");
	}

	public virtual void touchedIn(){
		Debug.Log ("touched in");
		
	}


	public virtual void touchedOut(){
		Debug.Log ("touched out");

	}
	public virtual void clickedIn(){
		Debug.Log ("clicked in");

	}

	public virtual void clickedOut(){
		Debug.Log ("clicked out");

	}
}
