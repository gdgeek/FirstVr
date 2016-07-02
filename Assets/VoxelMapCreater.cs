using UnityEngine;
using System.Collections;
using System.IO;


namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMapCreater : MonoBehaviour {
		public Sprite _high = null;
		public Sprite _color = null;
		public bool _building = true;
		public VectorInt2 _itemSize;


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
			if (_building == true && _high != null) {

				int xn = Mathf.CeilToInt (_high.texture.width / _itemSize.x);
				int yn = Mathf.CeilToInt (_high.texture.height / _itemSize.y);
				for (int x = 0; x < xn; ++x) {
					for (int y = 0; y < yn; ++y) {
						VoxelMapItem item  = createItem();
						item.gameObject.name = "item_"+x+"_"+y;
						item.gameObject.transform.parent = this.transform;
						item.gameObject.transform.localPosition = new Vector3 (x*_itemSize.x, 0, y *_itemSize.y);
						Color[] highs = _high.texture.GetPixels (x*_itemSize.x, y *_itemSize.y, _itemSize.x, _itemSize.y);
						Color[] colors = _color.texture.GetPixels (x * _itemSize.x, y * _itemSize.y, _itemSize.x, _itemSize.y);
							item.build (_itemSize.x, _itemSize.y, highs , colors);
					}
				}
			
				_building = false;	
			}
		}
	}

}