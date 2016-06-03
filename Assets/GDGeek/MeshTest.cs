using UnityEngine;
using System.Collections;
using GDGeek;

public class MeshTest : MonoBehaviour {
	public VoxelFile _file;
	public VoxelDirector _director;
	// Use this for initialization
	void Start () {
			_director.draw ("test",_file.voxel, _file.mesh);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
