using UnityEngine;
using System.Collections;
using GDGeek;

public class Matrix : MonoBehaviour {
	public Barrier _x = null;
	public Barrier _y = null;
	public Barrier _z = null;
	private FSM fsm_ = new FSM(); 
	public GameObject _cursor = null;
	public BoxCollider _box = null;

	private Task goLine(){
		return new Task();
			
	}
	private State getInit(){
		StateWithEventMap init = TaskState.Create (delegate() {
			TaskSet ts = new TaskSet();
			ts.push(goLine());
			return ts;
		}, this.fsm_, "draw");
		return init;
	}
	private State getDraw(){

		return new State();
	}
	public void setMatrix(int x, int y, int z){
		_x.setBarrier (z, y, x);
		_y.setBarrier(z, x, y);
		_z.setBarrier(x, y, z);
		_box.size = new Vector3 (x*10, y*10, z*10);
	}
	void Start () {
		this.setMatrix (5, 3, 4);
		//_line.
		fsm_.addState("init", getInit());
		fsm_.addState ("draw", getDraw ());

		fsm_.init ("init");

	}
	/*
	public void OnTriggerEnter(Collider other){
		Debug.Log ("OnTriggerEnter" + other.name);
	}


	public void OnTriggerExit(Collider other){
		Debug.Log ("OnTriggerExit" + other.name);

	}*/
	private VectorInt3 position2cell(Vector3 position){
		//Debug.Log (_box.size/2);
		//Debug.Log (_box.center);
//		Debug.Log((_box.size/2 + (position-this.gameObject.transform.position)));
		Vector3 o = _box.size / 2 + position - this.gameObject.transform.position;
		return new VectorInt3(Mathf.FloorToInt(o.x/10),Mathf.FloorToInt(o.y/10),Mathf.FloorToInt(o.z/10));
	}
	private Vector3 cell2position(VectorInt3 cell){
		Vector3 ret = new Vector3 (cell.x*10,cell.y*10, cell.z*10) + this.gameObject.transform.position - _box.size / 2 +new Vector3(5,5,5);
		return ret;
	}
	public void OnTriggerStay(Collider other){
		this._cursor.transform.position = cell2position (position2cell (other.gameObject.transform.position));

	}


}
