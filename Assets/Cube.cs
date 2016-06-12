using UnityEngine;
using System.Collections;
using GDGeek;

public class Cube : MonoBehaviour {
	private VectorInt3 position_;
	private Color color_;
	public void setup(VectorInt3 position, Color color, float unit){
		Debug.Log (unit);
		position_ = position;
		this.transform.localPosition = new Vector3 (position.x * unit, position.y * unit, position.z * unit);

		color_ = color;
		this.transform.localScale = Vector3.one * unit;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
