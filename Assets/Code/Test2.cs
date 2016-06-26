using UnityEngine;
using System.Collections;
public class Test2 : MonoBehaviour {
	void Update(){
		//IPointerOverUI ip = new IPointerOverUI ();
		if (IPointerOverUI.IsPointerOverUIObjectA ()) {
			Debug.Log ("xxxxxx");


		} else {


		}
	}	

}