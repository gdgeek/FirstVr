using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMapCreater : MonoBehaviour {
		public Sprite _sprite = null;
		public bool _building = true;
	


		VoxelMapItem createItem ()
		{

			var go = new GameObject();
			VoxelMapItem item = null;

			item = go.GetComponent<VoxelMapItem>();

			if(item == null){
				item = go.AddComponent<VoxelMapItem>();
			}
			return item;
		}

		/*
		public static VoxelStruct ReadFromArray(Color[] high){

			VoxelData data;
			VoxelStruct vs = new VoxelStruct ();
			vs.datas.Add (new VoxelData (new VectorInt3(1,1,1), Color.blue));
			return vs;

		}
*/
		// Update is called once per frame
		void Update () {
			if (_building == true && _sprite != null) {

				var item  = createItem();
				//item._building = true;
				item.gameObject.name = "item0";
				item.gameObject.transform.parent = this.transform;
				var highs = _sprite.texture.GetPixels (0, 0, 64, 64);
				item.build (highs);
				_building = false;	
			}
		}
	}

}