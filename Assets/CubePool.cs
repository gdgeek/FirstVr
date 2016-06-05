using UnityEngine;
using System.Collections;
using GDGeek;

public class CubePool : MonoBehaviour {
	private Pool pool_ = new Pool();
	
	public Cube _phototype = null;
	private static CubePool instance_ = null;
	// Use this for initialization

	public static CubePool GetInstance(){
		return CubePool.instance_;
	}
	private GameObject createPrototype(){

		return _phototype.gameObject;
	}
	
	void Awake(){
			CubePool.instance_ = this;
			pool_.init (createPrototype());

	}
	
	public GameObject create(){
			return pool_.create ();
	}
}
