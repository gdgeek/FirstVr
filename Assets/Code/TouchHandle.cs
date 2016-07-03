using UnityEngine;
using System.Collections;

public class TouchHandle : MonoBehaviour {

	//public KeyHighlight 


	public virtual Vector2 pad2pos(Vector2 pad){
		Vector2 p = pad - new Vector2 (0.5f, 0.5f);
		return new Vector2(p.x * 110f, p.y * 70f);
	}
	public virtual void touchWho(GameObject obj){
		Debug.Log (obj.name);
	}

	public virtual void touchOut(GameObject obj){


	}
	public virtual void init(){
	
	}
	public virtual void shutdown(){
	
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
	/*
	public static Vector2 Square(Vector2 pad){
		if (pad.x == 0 && pad.y == 0) {
			return Vector2.zero;
		}
		if (Mathf.Abs (pad.x) > Mathf.Abs (pad.y)) {
			if (pad.x > 0f) {
				return new Vector2 (1f, pad.y / pad.x);
			} else {
				return new Vector2 (-1f, -pad.y / pad.x);
			}
		} else {
		
			if (pad.y > 0f) {
				return new Vector2 (pad.x/pad.y,1f);
			} else {

				return new Vector2 (-pad.x/pad.y,-1f);
			}
		}

		return Vector2.zero;
	
	}
	public static Vector2 Interpolation(Vector2 from, Vector2 to, Vector2 value){
//		Debug.Log (GDGeek.Tween.easeIt (GDGeek.Tween.Method.Linear, from.y, to.y, 0));
		return new Vector2 (
			GDGeek.Tween.easeIt (GDGeek.Tween.Method.Linear, from.x, to.x, value.x),
			GDGeek.Tween.easeIt (GDGeek.Tween.Method.Linear, from.y, to.y, value.y)
		);
	}
	public static Vector2 Circular (Vector2 pad){
		var c2 =pad.x * pad.x + pad.y*pad.y;
		if (c2 == 0) {
			return Vector2.zero;
		}
		var c = Mathf.Sqrt (c2);

		return new Vector2(pad.x / c, pad.y / c);
	}*/
}
