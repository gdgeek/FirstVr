﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	
	public class VoxelProduct{
		
		public class Product{
			public Dictionary<VectorInt3, VoxelHandler> voxels = null;
			public VoxelDrawData draw = null;
		}

		private Vector3 min_ = new Vector3(999, 999, 999);
		public Vector3 min{
			get{ 
				return min_;
			}
			set{ 
				min_ = value;
			}
		}
		private Vector3 max_ = new Vector3(-999, -999, -999);

		public Vector3 max{
			get{ 
				return max_;
			}
			set{ 
				max_ = value;
			}
		}

		private Product main_ = new Product();

		public Product main{
			get{ 
				return main_;
			}
			set{ 
				main_ = value;

				Mesh mesh;

			}
		}



		public Product[] sub_ = null;


		public Product[] sub{
			get{ 
				return sub_;
			}
			set{ 
				sub_ = value;
			}
		}


		private VoxelGeometry.MeshData data_ = null;
		public VoxelGeometry.MeshData getMeshData(){
			Debug.Log ("!!!!!");
			
			if (data_ == null) {
				data_ = new VoxelGeometry.MeshData ();
			}
				
			data_.max = this.max;
			data_.min = this.min;
			Debug.Log ("!!!!!!");
			if (this.sub != null) {

			
				List<int> triangles = new List<int>();
				for (int i = 0; i < this.sub.Length; ++i) {

					int offset = data_.vertices.Count;


					for (int j = 0; j < this.sub [i].draw.vertices.Count; ++j) {
						
						data_.addPoint (this.sub [i].draw.vertices [j].position, this.sub [i].draw.vertices [j].color);
						//data_.colors.Add (this.sub [i].draw.vertices [j].color);
						//data_.vertices.Add (this.sub [i].draw.vertices [j].position);

					}

					for (int n = 0; n < this.sub [i].draw.triangles.Count; ++n) {
						data_.triangles.Add (this.sub [i].draw.triangles[n]+ offset);
					}
				}



			} else {
				for (int i = 0; i < main.draw.vertices.Count; ++i) {

					data_.addPoint (main.draw.vertices [i].position, main.draw.vertices [i].color);
				//	data_.vertices.Add (main.draw.vertices [i].position);
					//data_.colors.Add (main.draw.vertices [i].color);

				}
				data_.triangles = main.draw.triangles;
			}
		
			return data_;
		}

	}

}