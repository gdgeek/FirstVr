using UnityEngine;
using System.Collections;
using GDGeek;

public class BrushRemove : Brush {

	public override void brush (VectorInt3 point, Building building){
		//	Debug.Log ("Add");
		Cube cube = building.getCube(point);
		Debug.Log ("@@@@" + point);
		if (cube != null) {
			Debug.Log ("@@@@");
			building.removeCube (point);
		}
	}
}
