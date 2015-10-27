using UnityEngine;
using System.Collections;

public class CellType
{
	public static float uvSize = 32f;
	public static float uvCorrect = (0.5f / uvSize); //(0f / uvSize);
	public static float uvFactor = (16f / uvSize);
	public static float uvFullFactor = (16f / uvSize);
	public static float blockSize = 2f;
	public static float uvPadding = 0f ;//(0f / uvSize);
	
	public string name = "UNNAMED";
	public Color32 colour = new Color32(255, 255, 255, 255);
	public bool invisible = false;
	public Vector2 texCoord = new Vector2(0f, 0f);
	
	public Vector2 uv = new Vector2(0f, 0f);
	
	public CellType(string n, Vector2 tC, Color32 col, bool inv=false)
	{
		name = n;
		colour = col;
		invisible = inv;
		texCoord = tC;

		generateUV();
	}

	public CellType(string n, Vector2 tC, bool inv=false)
	{
		name = n;
		invisible = inv;
		texCoord = tC;

		generateUV();
	}

	void generateUV()
	{
		uv = new Vector2(texCoord.x * uvFullFactor, texCoord.y * uvFullFactor);
	}

	public void MakeFrontFace(float xp, float yp, ChunkMesh msh)
	{
		if(invisible)
			return;
		
		int ti = msh.vertices.Count;
		
		msh.vertices.Add(new Vector3(xp, yp));
		msh.vertices.Add(new Vector3(xp + blockSize, yp));
		msh.vertices.Add(new Vector3(xp + blockSize, yp + blockSize));
		msh.vertices.Add(new Vector3(xp, yp + blockSize));
		
//		msh.triangles.Add(ti);
//		msh.triangles.Add(ti + 3);
//		msh.triangles.Add(ti + 1);
//		msh.triangles.Add(ti + 1);
//		msh.triangles.Add(ti + 3);
//		msh.triangles.Add(ti + 2);

		finishFace(ti, msh);
		
//		for(int i = 0; i < 4; i++)
//			msh.colours.Add(colour);
//
//		msh.uvs.Add(new Vector2(uv.x + uvCorrect, uv.y + uvCorrect));
//		msh.uvs.Add(new Vector2(uv.x + uvFactor - uvCorrect, uv.y + uvCorrect));
//		msh.uvs.Add(new Vector2(uv.x + uvFactor - uvCorrect, uv.y + uvFactor - uvCorrect));
//		msh.uvs.Add(new Vector2(uv.x + uvCorrect, uv.y + uvFactor - uvCorrect));
	}

	public void MakeTopFace(float xp, float yp, ChunkMesh msh)
	{
		if(invisible)
			return;
		
		int ti = msh.vertices.Count;
		
		msh.vertices.Add(new Vector3(xp, yp + blockSize));
		msh.vertices.Add(new Vector3(xp + blockSize, yp + blockSize));
		msh.vertices.Add(new Vector3(xp + blockSize, yp + blockSize, blockSize));
		msh.vertices.Add(new Vector3(xp, yp + blockSize, blockSize));

		finishFace(ti, msh);
	}

	public void MakeBottomFace(float xp, float yp, ChunkMesh msh)
	{
		if(invisible)
			return;
		
		int ti = msh.vertices.Count;

		msh.vertices.Add(new Vector3(xp, yp, blockSize));
		msh.vertices.Add(new Vector3(xp + blockSize, yp, blockSize));
		msh.vertices.Add(new Vector3(xp + blockSize, yp));
		msh.vertices.Add(new Vector3(xp, yp));
		
		finishFace(ti, msh);
	}

	public void MakeLeftFace(float xp, float yp, ChunkMesh msh)
	{
		if(invisible)
			return;
		
		int ti = msh.vertices.Count;
		
		msh.vertices.Add(new Vector3(xp, yp));
		msh.vertices.Add(new Vector3(xp, yp + blockSize));
		msh.vertices.Add(new Vector3(xp, yp + blockSize, blockSize));
		msh.vertices.Add(new Vector3(xp, yp, blockSize));
		
		finishFace(ti, msh);
	}

	public void MakeRightFace(float xp, float yp, ChunkMesh msh)
	{
		if(invisible)
			return;
		
		int ti = msh.vertices.Count;
		
		msh.vertices.Add(new Vector3(xp + blockSize, yp + blockSize));
		msh.vertices.Add(new Vector3(xp + blockSize, yp));
		msh.vertices.Add(new Vector3(xp + blockSize, yp, blockSize));
		msh.vertices.Add(new Vector3(xp + blockSize, yp + blockSize, blockSize));
		
		finishFace(ti, msh);
	}

	void finishFace(int ti, ChunkMesh msh)
	{
		msh.triangles.Add(ti);
		msh.triangles.Add(ti + 3);
		msh.triangles.Add(ti + 1);
		msh.triangles.Add(ti + 1);
		msh.triangles.Add(ti + 3);
		msh.triangles.Add(ti + 2);

		for(int i = 0; i < 4; i++)
			msh.colours.Add(colour);
		
		msh.uvs.Add(new Vector2(uv.x + uvCorrect, uv.y + uvCorrect));
		msh.uvs.Add(new Vector2(uv.x + uvFactor - uvCorrect, uv.y + uvCorrect));
		msh.uvs.Add(new Vector2(uv.x + uvFactor - uvCorrect, uv.y + uvFactor - uvCorrect));
		msh.uvs.Add(new Vector2(uv.x + uvCorrect, uv.y + uvFactor - uvCorrect));
	}

	public void DrawToMesh(float x, float y, ChunkMesh msh, TerrainChunk chunk, int xi, int yi)
	{
		if(invisible)
			return;

		float xp = x * blockSize;
		float yp = y * blockSize;

		MakeFrontFace(xp, yp, msh);

		if(yi == TerrainChunk.ChunkHeight - 1 || chunk[xi, yi + 1].invisible)
			MakeTopFace(xp, yp, msh);
		if(yi == 0 || chunk[xi, yi - 1].invisible)
			MakeBottomFace(xp, yp, msh);
		if(xi == 0 || chunk[xi - 1, yi].invisible)
			MakeLeftFace(xp, yp, msh);
		if(xi == TerrainChunk.ChunkWidth - 1 || chunk[xi + 1, yi].invisible)
			MakeRightFace(xp, yp, msh);
	}
}