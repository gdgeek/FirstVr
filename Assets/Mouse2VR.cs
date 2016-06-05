using UnityEngine;
using System.Collections;

public class Mouse2VR : MonoBehaviour {
	public Camera _camera = null;
	public GameObject _temp = null;
	public float _deep = 0.0f;
//	public Panel
	// Use this for initialization
	void Start () {
		
		EasyTouch.On_TouchDown += delegate(Gesture gesture) {
//		Debug.Log(gesture.position);
			//_camera.
			var ray = _camera.ScreenPointToRay(gesture.position);
			Plane plane = new Plane (Vector3.forward, new Vector3(0, 0, _deep)/*this.transform.position*/); 
			float dis = 0.0f;
			plane.Raycast(ray, out dis);
		//	Debug.Log(dis);
			Vector3 p = ray.GetPoint(dis);
		//	Debug.Log(p);
			this.transform.position = p;
			//ray.
		};
	}
	

}
