using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {
	public Material _material = null;
	public LineRenderer[] _lines = null;
	public LineRenderer _phototype = null;
	public void setLine(int n, int length, float unit){
		LineRenderer[] lines = this.GetComponentsInChildren<LineRenderer> ();
		_lines = new LineRenderer[n];

		if (lines.Length < _lines.Length) {
			for (int i = 0; i < lines.Length; ++i) {
				_lines [i] = lines [i];
				_lines [i].name = "Line" + i;
				_lines [i].SetWidth (unit*0.1f, unit * 0.1f);
			}
			for (int i = lines.Length; i < _lines.Length; ++i) {
				_lines [i] = GameObject.Instantiate (_phototype);
				_lines [i].transform.parent = this.transform;
				_lines [i].gameObject.SetActive (true);
				_lines [i].name = "Line" + i;
				_lines [i].SetWidth (unit*0.1f, unit * 0.1f);
			}

		} else {
			for (int i = 0; i < _lines.Length; ++i) {
				_lines [i] = lines [i];
				_lines [i].name = "Line" + i;
				_lines [i].SetWidth (unit*0.1f, unit * 0.1f);
			}
			for (int i = _lines.Length; i < lines.Length; ++i) {
				GameObject.DestroyImmediate (lines [i].gameObject);
			}
		
		}
//		Debug.Log (_lines.Length);
		for (int i = 0; i < _lines.Length; ++i) {
			_lines [i].SetPosition (0, new Vector3(0, 0, 0));
			_lines [i].SetPosition (1, new Vector3(length * unit, 0, 0));
			//
			_lines [i].gameObject.transform.localPosition = new Vector3 (0, unit * i, 0);

			_lines [i].gameObject.transform.localEulerAngles = Vector3.zero;
		}

		lines = this.GetComponentsInChildren<LineRenderer> ();
		//_lines = new LineRenderer[n];
//		Debug.Log (lines.Length);
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
