using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;

public class Building : MonoBehaviour {
	private float unit_= 1.0f;
	private Dictionary<VectorInt3, Cube> _cubes= new Dictionary<VectorInt3, Cube>();
	public void setup(VectorInt3 offset, float unit){
		unit_ = unit;
		//Debug.Log ("offset" + offset);
		this.transform.localPosition = new Vector3 (-((float)(offset.x) - 1f) /2f*unit_,
			-((float)(offset.y) - 1f) /2f*unit_,
			-((float)(offset.z) - 1f) / 2f *unit_);
			
		//this.transform.localPosition = new Vector3()
	}
	public Cube getCube(VectorInt3 position){
		if (_cubes.ContainsKey (position)) {
			return _cubes [position];

		}
		return null;

	}
	public void removeCube(VectorInt3 point){
		Debug.Log (point);

		//_cubes.
		Cube cube = null;
		if (_cubes.ContainsKey (point)) {
			cube = _cubes [point];
			_cubes.Remove (point);
			cube.gameObject.SetActive (false);

		} 
	
	}
	public void addCube(VectorInt3 point, Color c){

		Cube cube = null;
		if (_cubes.ContainsKey (point)) {
			cube = _cubes [point];
		} else {
			cube = CubePool.GetInstance ().create ();
			cube.transform.parent = this.transform;
			cube.gameObject.SetActive (true);
			_cubes [point] = cube;
		}

		Debug.Log (unit_ +"ace");
		cube.setup (point, c, unit_);
	}

}
