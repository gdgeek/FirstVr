using UnityEngine;
using System.Collections;
using GDGeek;

public abstract class Brush:  MonoBehaviour{
	public abstract void brush (VectorInt3 point, Building building);
}
