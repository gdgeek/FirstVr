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

	
		// Update is called once per frame
		void Update () {
			if (_building == true && _sprite != null) {

				VoxelMapItem item  = createItem();
				item.gameObject.name = "item0";
				item.gameObject.transform.parent = this.transform;
				Color[] highs = _sprite.texture.GetPixels (0, 0, 64, 64);
				item.build (64, 64, highs);
				_building = false;	
			}
		}
	}

}