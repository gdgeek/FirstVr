using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;

public class Building : MonoBehaviour {
	private float unit_= 1.0f;
	private Dictionary<VectorInt3, Cube> _cubes= new Dictionary<VectorInt3, Cube>();
	public void setup(VectorInt3 offset, float unit){
		unit_ = unit;
		//this.transform.localPosition = new Vector3()
	}
	public void addCube(VectorInt3 position, Color c){
		Debug.Log (position);

		//_cubes.
		Debug.Log (c);
		Cube cube = null;
		if (_cubes.ContainsKey (position)) {
			cube = _cubes [position];
		
		} else {
			cube = CubePool.GetInstance ().create ();
			cube.transform.parent = this.transform;
			cube.gameObject.SetActive (true);
		}


		cube.setup (position, c, unit_);
	}
	public void removeCube(VectorInt3 position){
	
	}

}
