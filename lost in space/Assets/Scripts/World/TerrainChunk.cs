using UnityEngine;
using System.Collections;

public class TerrainChunk
{
	public static int ChunkWidth = 16;
	public static int ChunkHeight = 16;
	
	public static CellType DefaultFill = World.Cells[0];

	private CellType[] cells;
	public CellType this[int x, int y]
	{
		get { return cells[y * ChunkWidth + x]; }
		set { cells[y * ChunkWidth + x] = value; }
	}

	public CellType this[int i]
	{
		get { return cells[i]; }
		set { cells[i] = value; }
	}

	public CellType this[Vector2 pos]
	{
		get { return this[Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)]; }
		set { this[Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)] = value; }
	}

	public int Length
	{
		get { return cells.Length; }
	}

	public TerrainChunk()
	{
		cells = new CellType[ChunkWidth * ChunkHeight];

		for(int i = 0; i < cells.Length; i++)
			cells[i] = DefaultFill;
	}

	public TerrainChunk(CellType fill)
	{
		cells = new CellType[ChunkWidth * ChunkHeight];
		
		for(int i = 0; i < cells.Length; i++)
			cells[i] = fill;
	}

	public TerrainChunk(CellType[] _cells)
	{
		cells = _cells;
	}

	public static int getX(int index)
	{
		return index % ChunkWidth;
	}

	public static int getY(int index)
	{
		return index / ChunkWidth;
	}
}
