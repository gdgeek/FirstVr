using UnityEngine;
using System.Collections;
using GDGeek;

public class Buoy : MonoBehaviour {

	private VectorInt3 point_;
	private VectorInt3 size_;
	private float unit_;
	public void setup(VectorInt3 size, float unit){
		size_ = size;
		unit_ = unit;

	}
	public VectorInt3 position2cell(Vector3 position){


		Vector3 o = new Vector3((size_.x * unit_), (size_.y * unit_),(size_.z * unit_))/ 2.0f + position - this.gameObject.transform.parent.position;
		VectorInt3 cell = new VectorInt3(Mathf.FloorToInt(o.x/unit_),Mathf.FloorToInt(o.y/unit_),Mathf.FloorToInt(o.z/unit_));
		cell.x = Mathf.Max (cell.x, 0);
		cell.y = Mathf.Max (cell.y, 0);
		cell.z = Mathf.Max (cell.z, 0);
		cell.x = Mathf.Min (cell.x, 5-1);
		cell.y = Mathf.Min (cell.y, 3-1);
		cell.z = Mathf.Min (cell.z, 4-1);
		return cell;
	}
	public Vector3 cell2position(VectorInt3 cell){
		Vector3 ret = new Vector3 (cell.x*unit_,cell.y*unit_, cell.z*unit_) + this.gameObject.transform.parent.position - new Vector3((size_.x * unit_), (size_.y * unit_),(size_.z * unit_))/ 2.0f  +new Vector3(unit_/2.0f,unit_/2.0f,unit_/2.0f);
		return ret;
	}/**/

	public void open(){
		this.gameObject.SetActive (true);
	}
	public void close(){
		this.gameObject.SetActive (false);
	}
	public VectorInt3 point{
		set{ 
			if (value != point_) {
				point_ = value;
				this.transform.position = cell2position (value);
			}
		}
		get{ 
			return point_;
		}

	}
}
