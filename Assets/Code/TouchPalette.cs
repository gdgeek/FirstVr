using UnityEngine;
using System.Collections;
using GDGeek;

public class TouchPalette : TouchHandle {


	private FSM fsm_ = new FSM ();
	public Palette _palette = null;
	private PaletteButton button_ = null;
	private Palette palette_ = null;

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
			Debug.Log("gray");
			_palette.gray();
		};
		swem.addAction ("button", "color");
		return swem;
	}
	void Start () {
		fsm_.addState ("color", getColor ());

		fsm_.addState ("gray", getGray ());
		fsm_.init ("gray");
	}

	public override void touchWho(GameObject obj){
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

	public override void leave(){

		Debug.Log ("leave");
	}

	public override Vector2 pad2pos(Vector2 pad){
		return new Vector2(pad.x * 110f, pad.y * 70f);
	}

	public override void touched(){
		Debug.Log ("touched");

	}
	public override void clicked(){
		Debug.Log ("clicked" + button_);
		if (button_ != null) {
			fsm_.post ("button");
		} else if(palette_ != null){
			Color c = palette_.getColor (this.gameObject.transform.position);
			Debug.Log (c);
		}

	}

	public override void init(){
		_palette.init ();
	}
	public override void shutdown(){
		_palette.shutdown ();
	}
}
