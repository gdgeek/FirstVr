using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMapItem : MonoBehaviour {
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


		public static VoxelStruct ReadFromArray(int width, int height, Color[] high){

			VoxelData data;
			VoxelStruct vs = new VoxelStruct ();
			int i = 0;
			for (int x = 0; x < width; ++x) {
				for (int y = 0; y < height; ++y) {
					float n = (high [i].r + high [i].g + high [i].b) / 3f;
					int layer = Mathf.FloorToInt (n * 5f) + 1;
					Debug.Log (layer);
					for(int l = 0; l<layer; ++l){
						vs.datas.Add (new VoxelData (new VectorInt3(x,y,l), Color.white));
					}
					//Debug.Log (high [i]);
					i++;
					//
				}
			}

			return vs;

		}

		// Update is called once per frame
		public void build (int width, int height, Color[] hi) {
			initDirector ();
			_vs = ReadFromArray (width, height, hi);
			_director.build (_vs);

		}
	}

}