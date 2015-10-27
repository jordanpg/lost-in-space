using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World
{
	public static List<CellType> Cells = new List<CellType>()
	{
		new CellType("Air", new Vector2(0, 0), new Color32(0, 0, 0, 0), true), //new Color32(0, 0, 0, 0), true),
		new CellType("Dirt", new Vector2(1, 1), new Color32(255, 255, 255, 255)), //new Color32(120, 72, 0, 255)),
		new CellType("Stone", new Vector2(0, 1), new Color32(64, 64, 64, 255)), //new Color32(128, 128, 128, 255)),
		new CellType("Dense Stone", new Vector2(1, 0), new Color32(72, 72, 72, 255)), //new Color32(72, 72, 72, 255)),
		new CellType("Soft Stone", new Vector2(0, 0), new Color32(96, 96, 96, 255)) //new Color32(184, 184, 184, 255))
	};
}
