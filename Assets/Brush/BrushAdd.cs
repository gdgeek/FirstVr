using UnityEngine;
using System.Collections;
using GDGeek;

public class BrushAdd : Brush{


	public override void brush (VectorInt3 point, Building building){
	//	Debug.Log ("Add");
		Cube cube = building.getCube(point);
		if (cube == null) {
			building.addCube (point, Color.red);
		}
	}
}
