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
	public Building _buinding = null;
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


	private State getOut(){

		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("enter", "in");
		swem.addAction ("stay", "in");



		swem.onStart += delegate {
			_cursor.SetActive(false);
		};
		swem.onOver += delegate {

			_cursor.SetActive(true);
		};
		return swem;
	}


	private State getIn(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("exit", "out");
		swem.addAction ("stay", delegate(FSMEvent evt) {
			GameObject go = (GameObject)(evt.obj);
			this._cursor.transform.position = cell2position (position2cell (go.transform.position));
		});
		swem.addAction ("add", delegate(FSMEvent evt) {
			Debug.Log (position2cell (this._cursor.transform.position));
		});
		return swem;
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
		fsm_.addState ("draw", "out", getDraw ());
		fsm_.addState ("out", getOut (), "draw");
		fsm_.addState ("in", getIn (), "draw");
	//	fsm_.addState ("out", getOut (), "draw");

		fsm_.init ("init");

	}
	private VectorInt3 position2cell(Vector3 position){
		Vector3 o = _box.size / 2 + position - this.gameObject.transform.position;
		VectorInt3 cell = new VectorInt3(Mathf.FloorToInt(o.x/10),Mathf.FloorToInt(o.y/10),Mathf.FloorToInt(o.z/10));
		cell.x = Mathf.Max (cell.x, 0);
		cell.y = Mathf.Max (cell.y, 0);
		cell.z = Mathf.Max (cell.z, 0);
		cell.x = Mathf.Min (cell.x, 5-1);
		cell.y = Mathf.Min (cell.y, 3-1);
		cell.z = Mathf.Min (cell.z, 4-1);
		return cell;
	}
	private Vector3 cell2position(VectorInt3 cell){
		Vector3 ret = new Vector3 (cell.x*10,cell.y*10, cell.z*10) + this.gameObject.transform.position - _box.size / 2 +new Vector3(5,5,5);
		return ret;
	}
	public void OnTriggerStay(Collider other){
		FSMEvent evt = new FSMEvent ();
		evt.msg = "stay";
		evt.obj = other.gameObject;
		fsm_.postEvent (evt);
		//this._cursor.transform.position = cell2position (position2cell (other.gameObject.transform.position));

	}
	public void OnTriggerEnter(Collider other){
		fsm_.post ("enter");
	
	}
	public void OnTriggerExit(Collider other){
		fsm_.post ("exit");
	}


}
