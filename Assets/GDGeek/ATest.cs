using UnityEngine;
using System.Collections;
using GDGeek;

public class ATest : MonoBehaviour {

	// Use this for initialization
	Task task(string name, int i){
		Task task = new TaskWait (0.01f);
		task.init = delegate {
			Debug.Log(name+ ":"+i);	

		};
		return task;

	}
	void Start () {
		TaskList tl1 = new TaskList ();
		TaskList tl2 = new TaskList ();
		for (int i = 0; i < 10; ++i) {
			tl1.push (task ("a", i));
			tl2.push (task ("b", i));
		}
		TaskManager.Run (tl1);
		TaskManager.Run (tl2);
	}
	void Update(){

		Debug.LogWarning ("=================");
	}

}
