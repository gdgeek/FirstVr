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
	public float _unit = 10.0f;
	private Task goLine(){
		return new Task();
			
	}
	private State getInit(){
		StateWithEventMap init = TaskState.Create (delegate() {
			_cursor.transform.localScale = Vector3.one * _unit;

			TaskSet ts = new TaskSet();

			ts.push(goLine());
			return ts;
		}, this.fsm_, "draw");
		return init;
	}
	private State getDraw(){
		
		StateWithEventMap swem = new StateWithEventMap ();

		/*	swem.addAction ("add", delegate(FSMEvent evt) {
			action_ |= (int)(Action.Add);
		});
		swem.addAction ("no_add", delegate(FSMEvent evt) {
			action_ ^= (int)(Action.Add);
		});
		swem.addAction ("remove", delegate(FSMEvent evt) {
			action_ |= (int)(Action.Remove);
		});
		swem.addAction ("no_remove", delegate(FSMEvent evt) {
			action_ ^= (int)(Action.Remove);
		});
*/
		return swem;
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
	private State getRemove(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("exit", "out");
		swem.addAction ("stay", delegate(FSMEvent evt) {
			GameObject go = (GameObject)(evt.obj);
			this._cursor.transform.position = cell2position (position2cell (go.transform.position));
			_buinding.addCube(position2cell (this._cursor.transform.position),(Color)(evt.obj));
		});


		return swem;
	
	}
	private State getAdd(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("exit", "out");
		swem.addAction ("stay", delegate(FSMEvent evt) {
			GameObject go = (GameObject)(evt.obj);
			this._cursor.transform.position = cell2position (position2cell (go.transform.position));
			_buinding.addCube(position2cell (this._cursor.transform.position),(Color)(evt.obj));
		});


		return swem;
	}
	private State getIn(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("exit", "out");
		swem.addAction ("stay", delegate(FSMEvent evt) {
			GameObject go = (GameObject)(evt.obj);
			this._cursor.transform.position = cell2position (position2cell (go.transform.position));
		});


		return swem;
	}
	public void setMatrix(int x, int y, int z){
		_x.setBarrier (z, y, x, this._unit);
		_y.setBarrier(z, x, y, this._unit);
		_z.setBarrier(x, y, z, this._unit);
		_box.size = new Vector3 (x*_unit, y*_unit, z*_unit);
		//_buinding.setup ();
		_buinding.setup(new VectorInt3(x, y, z), _unit);
	}
	void Start () {
		this.setMatrix (5, 3, 4);
		//_line.
		fsm_.addState("init", getInit());
		fsm_.addState ("draw", "out", getDraw ());
		fsm_.addState ("out", getOut (), "draw");
		fsm_.addState ("in", getIn (), "draw");
		fsm_.addState ("add", getAdd (), "draw");
		fsm_.addState ("remove", getRemove (), "draw");
	//	fsm_.addState ("out", getOut (), "draw");

		fsm_.init ("init");

	}
	private VectorInt3 position2cell(Vector3 position){
		Vector3 o = _box.size / 2 + position - this.gameObject.transform.position;
		VectorInt3 cell = new VectorInt3(Mathf.FloorToInt(o.x/_unit),Mathf.FloorToInt(o.y/_unit),Mathf.FloorToInt(o.z/_unit));
		cell.x = Mathf.Max (cell.x, 0);
		cell.y = Mathf.Max (cell.y, 0);
		cell.z = Mathf.Max (cell.z, 0);
		cell.x = Mathf.Min (cell.x, 5-1);
		cell.y = Mathf.Min (cell.y, 3-1);
		cell.z = Mathf.Min (cell.z, 4-1);
		return cell;
	}
	private Vector3 cell2position(VectorInt3 cell){
		Vector3 ret = new Vector3 (cell.x*_unit,cell.y*_unit, cell.z*_unit) + this.gameObject.transform.position - _box.size / 2 +new Vector3(_unit/2.0f,_unit/2.0f,_unit/2.0f);
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
