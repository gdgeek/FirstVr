using UnityEngine;
using System.Collections;
using GDGeek;

public class Matrix : MonoBehaviour {
	
	public Barrier _x = null;
	public Barrier _y = null;
	public Barrier _z = null;

	private FSM fsm_ = new FSM(); 
	public Buoy _buoy = null;
	public BoxCollider _box = null;
	public Building _buinding = null;
	public float _unit = 10.0f;
	private int action_ = 0;
	private Brush brush_;
	private bool drawing_ = false;
	public void setBrush(Brush brush){
		brush_ = brush;
		fsm_.post ("brush");
	}
	public void draw(bool isDrawing){
		if (isDrawing) {
			drawing_ = true;
			fsm_.post ("drawing");
		} else {

			drawing_ = false;
			fsm_.post ("drawed");
		}
	}

	private Task goLine(){
		return new Task();
	}

	private State getInit(){
		StateWithEventMap init = TaskState.Create (delegate() {
			_buoy.transform.localScale = Vector3.one * _unit*1.01f;

			TaskSet ts = new TaskSet();

			ts.push(goLine());
			return ts;
		}, this.fsm_, "draw");
		return init;
	}
	private State getDraw(){
		
		StateWithEventMap swem = new StateWithEventMap ();

		return swem;
	}


	private State getOut(){

		StateWithEventMap swem = new StateWithEventMap ();
		swem.addAction ("enter", 
			delegate (FSMEvent evt){
				
				GameObject go = (GameObject)(evt.obj);
				VectorInt3 point = this._buoy.position2cell(go.transform.position);
				if(point != this._buoy.point){
					this._buoy.point = point;
				}
				return "in";
		});

		swem.addAction ("stay", 
			delegate(FSMEvent evt) {

				GameObject go = (GameObject)(evt.obj);
				VectorInt3 point = this._buoy.position2cell(go.transform.position);
				if(point != this._buoy.point){
					this._buoy.point = point;
				};
				return "in";
			});



		return swem;
	}
		
	private State getIn(){
		StateWithEventMap swem = new StateWithEventMap ();

		swem.onStart += delegate {
			if(drawing_ && brush_){
				brush_.brush(this._buoy.point, this._buinding);
			}

			_buoy.open();
		};
		swem.onOver += delegate {

			_buoy.close();
		};

		swem.addAction ("drawing", delegate(FSMEvent evt) {
			if(drawing_ && this.brush_ != null){
				this.brush_.brush(this._buoy.point, this._buinding);
			}
		});


		swem.addAction ("brush", delegate(FSMEvent evt) {
//			Debug.Log("!!!!!!DSFDSF");
			if(drawing_ && this.brush_ != null){
				
				this.brush_.brush(this._buoy.point, this._buinding);
			}
		});
		swem.addAction ("exit", "out");
		swem.addAction ("stay", delegate(FSMEvent evt) {
//			Debug.Log(drawing_+ this.brush_.gameObject.name);
			GameObject go = (GameObject)(evt.obj);
			VectorInt3 point = this._buoy.position2cell(go.transform.position);
			if(point != this._buoy.point){
				this._buoy.point = point;
				if(drawing_ && this.brush_ != null){
					
					this.brush_.brush(point, this._buinding);
				}

			}
		});

		
		return swem;
	}
	public void setMatrix(int x, int y, int z){
		_x.setBarrier (z, y, x, this._unit);
		_y.setBarrier(z, x, y, this._unit);
		_z.setBarrier(x, y, z, this._unit);
		_box.size = new Vector3 (x*_unit, y*_unit, z*_unit);
		_buinding.setup(new VectorInt3(x, y, z), _unit);
		_buoy.setup (new VectorInt3(x, y, z), _unit);
	}
	void Start () {
		this.setMatrix (5, 3, 4);
		//_line.
		fsm_.addState("init", getInit());
		fsm_.addState ("draw", "out", getDraw ());
		fsm_.addState ("out", getOut (), "draw");
		fsm_.addState ("in", getIn (), "draw");
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
	/**/
	public void OnTriggerStay(Collider other){
		FSMEvent evt = new FSMEvent ();
		evt.msg = "stay";
		evt.obj = other.gameObject;
		fsm_.postEvent (evt);
		//this._cursor.transform.position = cell2position (position2cell (other.gameObject.transform.position));

	}
	public void OnTriggerEnter(Collider other){
		FSMEvent evt = new FSMEvent ();
		evt.msg = "enter";
		evt.obj = other.gameObject;
		fsm_.postEvent (evt);
	}
	public void OnTriggerExit(Collider other){
		fsm_.post ("exit");
	}



}
