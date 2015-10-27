using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour
{
	public CellType fill = World.Cells[4];
	public bool randomise = false;
	public float waitTime = 1f;

	private ChunkRenderer rend;
	private TerrainChunk chunk;
	private bool rand = false;

	private bool ranOnce = false;

	// Use this for initialization
	void Start()
	{
		rend = GetComponent<ChunkRenderer>();
		rend.chunk = chunk = new TerrainChunk(fill);
	}

//	IEnumerator Randomise()
//	{
//		rand = true;
//		while(randomise)
//		{
//			chunk[Random.Range(0, TerrainChunk.ChunkWidth), Random.Range(0, TerrainChunk.ChunkHeight)] = World.Cells[Random.Range(0, 2)];
//			rend.BuildChunk();
//			yield return new WaitForSeconds(waitTime);
//		}
//		rand = false;
//	}

	IEnumerator Randomise()
	{
		CellType type;
		float noise, x, y;
		rand = true;
		while(randomise)
		{
			for(int i = 0; i < chunk.Length; i++)
			{
				x = (float)TerrainChunk.getX(i);
				y = (float)TerrainChunk.getY(i);
				//Debug.Log(x.ToString() + " " + y.ToString());
				noise = Simplex.noise(x, y, Time.time);

				if(noise > 0.5)
					type = World.Cells[0];
				else if(noise > 0)
					type = World.Cells[4];
				else if(noise < -0.5)
					type = World.Cells[3];
				else
					type = World.Cells[2];

				chunk[i] = type;
			}

			rend.BuildChunk();
			yield return new WaitForSeconds(waitTime);
		}
		rand = false;
	}

	void Update()
	{
		if(!ranOnce)
		{
			rend.BuildChunk();
			ranOnce = true;
		}

		if(randomise && !rand)
			StartCoroutine("Randomise");
		else if(!randomise && rand)
		{
			StopCoroutine("Randomise");
			rand = false;
		}
	}

	Vector2 resolveWorldPos(Vector3 pos)
	{
		Vector3 fPos = (pos - transform.position) / CellType.blockSize;
		return new Vector2(fPos.x, fPos.y);

	}
}
