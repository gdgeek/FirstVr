using UnityEngine;
using System.Collections;
using GDGeek;
using Valve.VR;

public class InputManager : MonoBehaviour {
	public TouchKeyboard _keyboard = null;
	public TouchPalette _palette = null;
	private TouchHandle handle_ = null;
	private bool locked_ = false;
	public SteamVR_TrackedController _tracked = null;

	public bool locked{
		set{ 
			locked_ = value;
		}

	}
	public TouchHandle handle{
		get{ 
			return handle_;
		}
	}

	private FSM fsm_ = new FSM ();
	public void Awake(){
		handle_ = _keyboard;
	}

	private State getKeyboard(){
		
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			//_keyboard.init();
			this.handle_ = _keyboard;
			this.handle_.init();
		};
		swem.onOver += delegate {
			handle_.shutdown();
			handle_ = null;
		};

		swem.addAction ("unmenu", delegate {
			if(!locked_){
				return "palette";
			}
			return "";
		});
		return swem;
	}

	private State getPalette(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {

			//_palette.init(); 
			this.handle_ = _palette;
			this.handle_.init();
		};
		swem.onOver += delegate {
			handle_.shutdown();
			handle_ = null;
		};
		swem.addAction ("unmenu", delegate {
			if(!locked_){
				return "keyboard";
			}
			return "";
		});


		return swem;
	}
	private State getInit(){
		StateWithEventMap swem = TaskState.Create (delegate {
			Task task = new Task();
			TaskManager.PushFront(task, delegate {
				//this._touchKey.shutdown();
				//this._palette.shutdown();
			});
			return task;
		}, this.fsm_, "keyboard");

		return swem;
	}

	// Use this for initialization
	void Start () {
		_tracked.MenuButtonClicked += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("menu");
		};


		_tracked.MenuButtonUnclicked += delegate(object sender, ClickedEventArgs e) {
			fsm_.post ("unmenu");
		};
		fsm_.addState ("init", getInit());
		fsm_.addState ("keyboard", getKeyboard());
		fsm_.addState ("palette", getPalette());
		fsm_.init ("init");
	}

	void Update(){


		if (Input.GetKey (KeyCode.Tab)) {
			fsm_.post ("menu");
		}
		if (Input.GetKeyUp (KeyCode.Tab)) {

			fsm_.post ("unmenu");
		}


	}
}
