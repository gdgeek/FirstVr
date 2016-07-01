using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GDGeek
{
	public class VoxelGeometry
	{
		//public MeshFilter _mesh = null;
		//public BoxCollider _collider = null;

		[Serializable]
		public class MeshData : ICloneable
		{
			private List<Vector3> vertices_ = new List<Vector3> ();
			public List<Vector3> _vertices{
				get{ 
					return vertices_;
				}

			}
			public int count{

				get{ 
					return vertices_.Count;
				}
			}
			public List<Color> colors = new List<Color> ();
			public List<int> triangles = new List<int> ();
			public List<Vector2> uvs = new List<Vector2> ();
			public Vector3 min;
			public Vector3 max;
			public void addPoint(Vector3 position, Color color){
				vertices_.Add (position);
				colors.Add (color);
				uvs.Add (Vector2.zero);
			}
			public object Clone()
			{
				MeshData data = new MeshData();
				this._vertices.ForEach(i => data._vertices.Add(i));
				this.colors.ForEach(i => data.colors.Add(i));
				this.triangles.ForEach(i => data.triangles.Add(i));
				this.uvs.ForEach(i => data.uvs.Add(i));
				data.min = this.min;
				data.max = this.max;

				return data;
			}


			public MeshData add(MeshData other){
				min = new Vector3(Mathf.Min (min.x, other.min.x),Mathf.Min (min.y, other.min.y),Mathf.Min (min.z, other.min.z));
				max = new Vector3(Mathf.Min (max.x, other.max.x),Mathf.Min (max.y, other.max.y),Mathf.Min (max.z, other.max.z));

				int offset = _vertices.Count;
				for (int i = 0; i < other._vertices.Count; ++i) {
					_vertices.Add (other._vertices [i]);
					colors.Add (other.colors [i]);
				}

				for (int i = 0; i < other.triangles.Count; ++i) {
					triangles.Add (other.triangles [i] + offset);
				}
				return this;
			}

		}
		private static Mesh CreateMesh(MeshData data){

			Mesh m = new Mesh();
			m.name = "ScriptedMesh";
			m.SetVertices (data._vertices);
			m.SetColors (data.colors);
		
			/*for (int i = 0; i < data.colors.Count; ++i) {
				if(data.colors [i].g <0.5f){
					data.uvs.Add (new Vector2 (1, 0));
				} else {
					data.uvs.Add (new Vector2 (0, 0));
				}
			}*/
			m.SetUVs (0, data.uvs);
			m.SetTriangles(data.triangles, 0);
			m.RecalculateNormals();
			return m;
		}

		private static MeshFilter CrateMeshFilter(MeshData data, string name, Material material){
			GameObject go = new GameObject(name);
			MeshFilter meshFilter = go.AddComponent<MeshFilter>();
			meshFilter.mesh = CreateMesh(data);
			MeshRenderer renderer = go.AddComponent<MeshRenderer>();
			renderer.material = material;
			return meshFilter;
		}

		public static VoxelMesh _Draw(string name, MeshData data, GameObject gameObject, Material material){



			VoxelMesh mesh = gameObject.GetComponent<VoxelMesh> ();//gameObject.AddComponent<VoxelMesh> ();

			if(mesh == null){
				mesh = gameObject.AddComponent<VoxelMesh>();
			}
			mesh.filter = CrateMeshFilter (data, name, material);
			mesh.filter.gameObject.transform.SetParent (gameObject.transform);	
			mesh.filter.gameObject.transform.localPosition = Vector3.zero;
			mesh.filter.gameObject.transform.localScale = Vector3.one;
			mesh.filter.gameObject.transform.localRotation = Quaternion.Euler (Vector3.zero);

			mesh.filter.gameObject.SetActive (true);

			Refresh (data, mesh);

			return mesh;


		}
		public static void Refresh(MeshData data, VoxelMesh mesh){
			Vector3 offset = Vector3.zero;
			Vector3 size =  new Vector3 (data.max.x - data.min.x, data.max.z - data.min.z, data.max.y - data.min.y);
			offset = size / -2.0f -new Vector3 ( data.min.x, data.min.z,  data.min.y);

			mesh.filter.transform.localPosition = offset;

			if (mesh.collider == null) {
				mesh.collider = mesh.gameObject.GetComponent <BoxCollider>();
			}

			if (mesh.collider == null) {
				mesh.collider = mesh.gameObject.AddComponent <BoxCollider>();
			}
			mesh.collider.size = size + Vector3.one;
			//mesh.collider = _collider;

		}

	}


}