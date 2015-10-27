using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkRenderer : MonoBehaviour
{
	public float blockSize = 1f;
	public float depth = 1f;
	public TerrainChunk chunk;
		
	private MeshFilter filter;
	private MeshCollider col;
	private Mesh mesh;

	private ChunkMesh chmesh;

	void Start()
	{
		filter = GetComponent<MeshFilter>();
		col = GetComponent<MeshCollider>();

		chmesh = new ChunkMesh();
	}

//	public void _BuildChunk()
//	{
//		if(chunk == null)
//			return;
//
//		vertices = new Vector3[(TerrainChunk.ChunkWidth + 1) * (TerrainChunk.ChunkHeight + 1)];
//		triangles = new int[TerrainChunk.ChunkWidth * TerrainChunk.ChunkHeight * 6];
//
//		float h = (float)TerrainChunk.ChunkHeight + 1;
//		float w = (float)TerrainChunk.ChunkWidth + 1;
//
//		int i = 0;
//		for(float y = 0f; y < h; y++)
//			for(float x = 0f; x < w; x++, i++)
//			{
//				vertices[i] = new Vector3(x * blockSize, y * blockSize);
//				//Debug.Log(vertices[i]);
//			}
//
//		for (int ti = 0, vi = 0, y = 0; y < TerrainChunk.ChunkHeight; y++, vi++)
//		{
//			for (int x = 0; x < TerrainChunk.ChunkWidth; x++, ti += 6, vi++)
//			{
//				//Debug.Log(chunk[x, y].name);
//				if(chunk[x, y].invisible)
//					continue;
//
//				triangles[ti] = vi;
//				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
//				triangles[ti + 4] = triangles[ti + 1] = vi + TerrainChunk.ChunkWidth + 1;
//				triangles[ti + 5] = vi + TerrainChunk.ChunkWidth + 2;
//
//				//Debug.Log("tri " + ti.ToString());
//			}
//		}
//
//		mesh = new Mesh();
//		mesh.vertices = vertices;
//		mesh.triangles = triangles;
//		mesh.RecalculateNormals();
//		mesh.RecalculateBounds();
//
//		col.sharedMesh = null;
//		col.sharedMesh = mesh;
//		//Debug.Log(mesh.vertexCount);
//		//Debug.Log(mesh.triangles);
//
//		filter.mesh = mesh;
//	}
//
//	public void __BuildChunk()
//	{
//		if(chunk == null)
//			return;
//
//		vertList = new List<Vector3>();
//		triList = new List<int>();
//		colList = new List<Color32>();
//		uvList = new List<Vector2>();
//
//		//Debug.Log(chunk.Length);
//
//		float x = 0, y = 0;
//		for(int yi = 0; yi < TerrainChunk.ChunkHeight; y++, yi++)
//		{
//			x = 0;
//			for(int xi = 0; xi < TerrainChunk.ChunkWidth; x++, xi++)
//			{
//				//Debug.Log(xi.ToString() + " " + yi.ToString());
//				MakeBlock(x, y, xi, yi);
//			}
//		}
//
////		vertices = vertList.ToArray();
////		triangles = triList.ToArray();
////		colours = colList.ToArray();
//
//		mesh = new Mesh();
//		mesh.vertices = vertList.ToArray();
//		mesh.triangles = triList.ToArray();
//		mesh.colors32 = colList.ToArray();
//		mesh.uv = uvList.ToArray();
//		mesh.RecalculateNormals();
//		mesh.RecalculateBounds();
//
//		Debug.Log(col);
//
//		col.sharedMesh = null;
//		col.sharedMesh = mesh;
//		//Debug.Log(mesh.vertexCount);
//		//Debug.Log(mesh.triangles);
//		
//		filter.mesh = mesh;
//	}
//
	public void BuildChunk()
	{
		if(chunk == null)
			return;

		chmesh.ResetMesh();
		//Debug.Log(chunk.Length);

		float x = 0, y = 0;
		CellType cell;
		for(int yi = 0; yi < TerrainChunk.ChunkHeight; y++, yi++)
		{
			x = 0;
			for(int xi = 0; xi < TerrainChunk.ChunkWidth; x++, xi++)
			{
				//Debug.Log(xi.ToString() + " " + yi.ToString());
				cell = chunk[xi, yi];

				if(cell.invisible)
				{
//					Debug.Log("Loop Invisible " + xi.ToString() + ", " + yi.ToString());
					continue;
				}

				cell.DrawToMesh(x, y, chmesh, chunk, xi, yi);
			}
		}

//		vertices = vertList.ToArray();
//		triangles = triList.ToArray();
//		colours = colList.ToArray();

		if(mesh != null)
			Destroy(mesh);

		mesh = new Mesh();
		chmesh.ApplyToMesh(mesh);

//		Debug.Log(col);

		col.sharedMesh = null;
		col.sharedMesh = mesh;
		//Debug.Log(mesh.vertexCount);
		//Debug.Log(mesh.triangles);
		
		filter.mesh = mesh;
	}
//	void MakeFrontFace(float xp, float yp, CellType type)
//	{
//		if(type.invisible)
//			return;
//
//		int ti = vertList.Count;
//	
//		vertList.Add(new Vector3(xp, yp));
//		vertList.Add(new Vector3(xp + blockSize, yp));
//		vertList.Add(new Vector3(xp + blockSize, yp + blockSize));
//		vertList.Add(new Vector3(xp, yp + blockSize));
//
//		triList.Add(ti);
//		triList.Add(ti + 3);
//		triList.Add(ti + 1);
//		triList.Add(ti + 1);
//		triList.Add(ti + 3);
//		triList.Add(ti + 2);
//
//		for(int i = 0; i < 4; i++)
//			colList.Add(type.colour);
//	}
//
//	void MakeTopFace(float xp, float yp, CellType type)
//	{
//		if(type.invisible)
//			return;
//
//		int ti = vertList.Count;
//
//		float ypp = yp + blockSize;
//
//		vertList.Add(new Vector3(xp, ypp));
//		vertList.Add(new Vector3(xp, ypp, depth));
//		vertList.Add(new Vector3(xp + blockSize, ypp, depth));
//		vertList.Add(new Vector3(xp + blockSize, ypp));
//
//		triList.Add(ti);
//		triList.Add(ti + 1);
//		triList.Add(ti + 3);
//		triList.Add(ti + 3);
//		triList.Add(ti + 1);
//		triList.Add(ti + 2);
//
//		for(int i = 0; i < 4; i++)
//			colList.Add(type.colour);
//	}
//
//	void MakeBottomFace(float xp, float yp, CellType type)
//	{
//		if(type.invisible)
//			return;
//		
//		int ti = vertList.Count;
//
//		vertList.Add(new Vector3(xp, yp));
//		vertList.Add(new Vector3(xp, yp, depth));
//		vertList.Add(new Vector3(xp + blockSize, yp, depth));
//		vertList.Add(new Vector3(xp + blockSize, yp));
//		
//		triList.Add(ti);
//		triList.Add(ti + 2);
//		triList.Add(ti + 1);
//		triList.Add(ti + 3);
//		triList.Add(ti + 2);
//		triList.Add(ti);
//		
//		for(int i = 0; i < 4; i++)
//			colList.Add(type.colour);
//	}
//
//	void MakeLeftFace(float xp, float yp, CellType type)
//	{
//		if(type.invisible)
//			return;
//		
//		int ti = vertList.Count;
//		
//		vertList.Add(new Vector3(xp, yp));
//		vertList.Add(new Vector3(xp, yp, depth));
//		vertList.Add(new Vector3(xp, yp + blockSize, depth));
//		vertList.Add(new Vector3(xp, yp + blockSize));
//		
//		triList.Add(ti);
//		triList.Add(ti + 1);
//		triList.Add(ti + 3);
//		triList.Add(ti + 3);
//		triList.Add(ti + 1);
//		triList.Add(ti + 2);
//		
//		for(int i = 0; i < 4; i++)
//			colList.Add(type.colour);
//	}
//	
//	void MakeRightFace(float xp, float yp, CellType type)
//	{
//		if(type.invisible)
//			return;
//		
//		int ti = vertList.Count;
//
//		float xpp = xp + blockSize;
//		float ypp = yp + blockSize;
//		
//		vertList.Add(new Vector3(xpp, ypp));
//		vertList.Add(new Vector3(xpp, ypp, depth));
//		vertList.Add(new Vector3(xpp, yp, depth));
//		vertList.Add(new Vector3(xpp, yp));
//		
//		triList.Add(ti);
//		triList.Add(ti + 1);
//		triList.Add(ti + 3);
//		triList.Add(ti + 3);
//		triList.Add(ti + 1);
//		triList.Add(ti + 2);
//		
//		for(int i = 0; i < 4; i++)
//			colList.Add(type.colour);
//	}
//
//	void AddUVs(CellType type)
//	{
//		uvList.Add(type.uv);
//		uvList.Add(new Vector2(type.uv.x, type.uv.y + CellType.uvFactor));
//	}
//
//	void MakeBlock(float x, float y, int xi, int yi)
//	{
//		CellType type = chunk[xi, yi];
//
//		float xp = x * blockSize;
//		float yp = y * blockSize;
//
//		MakeFrontFace(xp, yp, type);
//
//		if(yi == TerrainChunk.ChunkHeight - 1 || chunk[xi, yi + 1].invisible)
//			MakeTopFace(xp, yp, type);
//		if(yi == 0 || chunk[xi, yi - 1].invisible)
//			MakeBottomFace(xp, yp, type);
//		if(xi == 0 || chunk[xi - 1, yi].invisible)
//			MakeLeftFace(xp, yp, type);
//		if(xi == TerrainChunk.ChunkWidth - 1 || chunk[xi + 1, yi].invisible)
//			MakeRightFace(xp, yp, type);
//	}
}
