using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class VoxelFilterGlow : VoxelFilter {

		public override VoxelGeometry.MeshData filter(VoxelGeometry.MeshData data){
			return (VoxelGeometry.MeshData)(data.Clone ());
		}
	}
}