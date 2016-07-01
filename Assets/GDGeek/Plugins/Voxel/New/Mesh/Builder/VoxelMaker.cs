using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMaker : MonoBehaviour {
		public TextAsset _voxFile = null;
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

		// Update is called once per frame
		void Update () {
			if (_building == true && _voxFile != null) {
			
				//initLoader();
				initDirector();

				if (_voxFile != null) {
					Stream sw = new MemoryStream(_voxFile.bytes);
					System.IO.BinaryReader br = new System.IO.BinaryReader (sw); 
					_vs = VoxelFormater.ReadFromMagicaVoxel (br);
				
					_director.build (_vs);
				}
				_building = false;	
			}
		}
	}

}