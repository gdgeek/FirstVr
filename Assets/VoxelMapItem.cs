using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMapItem : MonoBehaviour {
		//public Sprite _sprite = null;
		//public bool _building = true;
		public VoxelStruct _vs = null;
		public VoxelDirector _director = null;



		void initDirector ()
		{
			if(_director == null){
				this._director = this.gameObject.GetComponent<VoxelDirector>();
			}
			if(_director == null){
				this._director = this.gameObject.AddComponent<VoxelDirector>();

			}


			#if UNITY_EDITOR
			if(this._director._material == null){
				this._director._material = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/GdGeek/Media/Voxel/Material/VoxelMesh.mat");
			}
			#endif
		}


		public static VoxelStruct ReadFromArray(Color[] high){

			VoxelData data;
			VoxelStruct vs = new VoxelStruct ();
			vs.datas.Add (new VoxelData (new VectorInt3(1,1,1), Color.blue));

//			vs.datas.Add ();
//			vs.arrange ();
			return vs;

		}

		// Update is called once per frame
		public void build (Color[] hi) {
			initDirector ();
			_vs = ReadFromArray (hi);
			_director.build (_vs);

		}
	}

}