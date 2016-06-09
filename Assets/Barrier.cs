using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
	public Bar _phototype = null;

	public Bar[] _bars = null;


	public void setBar(int n, float unit){
		Bar[] bars = this.GetComponentsInChildren<Bar> ();
		_bars = new Bar[n];

		if (bars.Length < _bars.Length) {
			for (int i = 0; i < bars.Length; ++i) {
				_bars [i] = bars [i];
				_bars [i].name = "Bar" + i;
			}
			for (int i = bars.Length; i < _bars.Length; ++i) {
				_bars [i] = GameObject.Instantiate (_phototype);
				_bars [i].transform.parent = this.transform;
				_bars [i].gameObject.SetActive (true);
				_bars [i].name = "Bar" + i;
			}

		} else {
			for (int i = 0; i < _bars.Length; ++i) {
				_bars [i] = bars [i];
				_bars [i].name = "Bar" + i;
			}
			for (int i = _bars.Length; i < bars.Length; ++i) {
				GameObject.DestroyImmediate (bars [i].gameObject);
			}

		}
		for (int i = 0; i < _bars.Length; ++i) {
			_bars [i].gameObject.transform.localPosition = new Vector3 (0, 0, i*unit);
			_bars [i].gameObject.transform.localEulerAngles = Vector3.zero;
		}

		bars = this.GetComponentsInChildren<Bar> ();
		//_lines = new LineRenderer[n];
//		Debug.Log (bars.Length);
	}
	public void setBarrier(int horizontal, int vertical, int height, float unit){
		this.gameObject.transform.localPosition = new Vector3 (-height*unit/2.0f,-vertical*unit/2.0f,-horizontal*unit/2.0f);
		setBar (horizontal+1, unit);
		for (int i = 0; i < _bars.Length; ++i) {
			_bars [i].setLine (vertical+1, height, unit);
		}

	}
	//20, 10, 10
	// Use this for initialization
	void Start () {
		//setBarrier (2, 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
