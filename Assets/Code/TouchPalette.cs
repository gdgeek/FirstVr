using UnityEngine;
using System.Collections;
using GDGeek;

public class TouchPalette : TouchHandle {


	private FSM fsm_ = new FSM ();
	public Palette _palette = null;
	private PaletteButton button_ = null;
	private Palette palette_ = null;
	private PadState _state = PadState.None;


	public override Vector2 pad2pos(Vector2 pad){
		Vector2 p = pad - new Vector2 (0.5f, 0.5f);
		SphereCollider sc = _palette.gameObject.GetComponent<SphereCollider> ();
		var l = sc.radius * 2f;
		return new Vector2(p.x * l, p.y * l);
	}

	private State getColor(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			_palette.color();
		};
		swem.addAction ("button", "gray");
		return swem;
	}


	private State getGray(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate() {
			_palette.gray();
		};
		swem.addAction ("button", "color");
		return swem;
	}
	void Start () {
		fsm_.addState ("color", getColor ());	
		fsm_.addState ("gray", getGray ());
		fsm_.init ("color");



	}

	public override void init(){
		_palette.init ();
		_state = PadState.Leaved;

		button_ = null;
		palette_ = null;
	}
	public override void shutdown(){
		_state = PadState.None;
		_palette.shutdown ();
	}


	public override void leavedIn(){
		_palette.gameObject.SetActive(false);
		_palette._sample.gameObject.SetActive(true);
		Debug.Log ("leave in");
	}


	public override void touchedIn(){

		_palette.gameObject.SetActive(true);
		_palette._sample.gameObject.SetActive(false);
		Debug.Log ("touched in");

	}


	public override void clickedIn(){
		Debug.Log ("clicked in");

		if (button_ != null) {
			fsm_.post ("button");
		} else if(palette_ != null){
			Color c = palette_.getColor (this.gameObject.transform.position);
			palette_._color.color = c;
			Debug.Log (c);
		}




	}
	public override void touchIn(GameObject obj){
		PaletteButton button = obj.GetComponent<PaletteButton> ();
		if (button != null) {
			button_ = button;
		}
		Palette palette = obj.GetComponent<Palette> ();
		if (palette != null) {
			palette_ = palette;
		}

		Debug.Log (obj.name);
	}


	public override void touchOut(GameObject obj){
		PaletteButton button = obj.GetComponent<PaletteButton> ();
		if (button != null) {
			button_ = null;
		}
		Palette palette = obj.GetComponent<Palette> ();
		if (palette != null) {
			palette_ = null;
		}

	}

}
