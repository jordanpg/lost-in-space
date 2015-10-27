using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkMesh
{
	public List<Vector3> vertices;
	public List<int> triangles;
	public List<Color32> colours;
	public List<Vector2> uvs;

	public ChunkMesh()
	{
		vertices = new List<Vector3>();
		triangles = new List<int>();
		colours = new List<Color32>();
		uvs = new List<Vector2>();
	}

	public void ResetMesh()
	{
		vertices.Clear();
		triangles.Clear();
		colours.Clear();
		uvs.Clear();
	}

	public void ApplyToMesh(Mesh mesh)
	{
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.colors32 = colours.ToArray();
		mesh.uv = uvs.ToArray();

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
}
