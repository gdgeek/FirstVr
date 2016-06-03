using UnityEngine;
using System.Collections;
using GDGeek;

public class BoomTest : MonoBehaviour {

	public VoxelMesh _mesh = null;
//	public bool _isOver = false;
	public Task boomTask () {
		TaskList tl = new TaskList ();
//		tl.init = delegate {
//			_isOver = false;
//		};
//
		TaskWait tw = new TaskWait (0.1f);
		TaskManager.PushBack (tw, delegate {
			_mesh.filter.gameObject.SetActive(false);
			Debug.Log(_mesh);
			Debug.Log(_mesh.vs);
			VoxelBoom.GetInstance().emission(_mesh, 15);
		});

		tl.push (tw);
//		tl.push (TaskWait.Create (0.1f, delegate {
//			_isOver = true;
//		}));
//		tl.isOver = delegate {
//			return _isOver;
//		};
		//TaskManager.Run (tl);
		return tl;
	}

}
