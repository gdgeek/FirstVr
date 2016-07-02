using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMapCareater : MonoBehaviour {
		public Sprite _sprite = null;
		public bool _building = true;
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


//			vs.datas.Add ();
//			vs.arrange ();
			return vs;

		}

		// Update is called once per frame
		void Update () {
			if (_building == true && _sprite != null) {

				//initLoader();
				initDirector();

				var highs = _sprite.texture.GetPixels (0, 0, 64, 64);
				_vs = ReadFromArray (highs);

				_director.build (_vs);

				_building = false;	
			}
		}
	}

}