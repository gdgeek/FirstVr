using UnityEngine;
using System.Collections;

public class VRCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device dev = SteamVR_Controller.Input (1);

		this.gameObject.transform.position = dev.transform.pos;

	}
}
