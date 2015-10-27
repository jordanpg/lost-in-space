using UnityEngine;
using System.Collections;

public class PlanetoidGen : MonoBehaviour
{
	public float seed = 0f;
	public float radius = 32f;
	public float segmentSize = 4f;

	public float maxVariance = 4f;
	public float frequency = 640f;
	public float persistence = 0.6f;
	public int iterations = 8;

	public bool regen = true;

	private float circum = 0f;
	private float segments = 0f;

	private float[] noise;

	private MeshRenderer rend;
	private MeshFilter filter;
	private PolygonCollider2D col;
	private Vector2z[] vex;
	private Vector2[] vex2;
	private Mesh mesh;
	
	void Start()
	{
		rend = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		col = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
	}

	float fbm(float x, float y, float seed, int iter)
	{
		float maxAmp = 0f;
		float amp = 1f;
		float f = 1f / frequency;
		float noise = 0f;
		
		for(int i = 0; i < iter; i++)
		{
			noise += Simplex.noise(x * f, y * f, seed) * amp;
			maxAmp += amp;
			amp *= persistence;
			f *= 2f;
		}
		
		return noise / maxAmp;
	}

	void initGen()
	{
		circum = 2f * Mathf.PI * radius;
		segments = Mathf.Floor((circum / segmentSize) / 2) * 2;

		noise = new float[(int)segments];
		vex = new Vector2z[noise.Length];
		vex2 = new Vector2[noise.Length];
	}

	void genNoiseMap()
	{
		float ang, x, y, c, s;
		int ii;

		for(float i = 0; i < segments; i++)
		{
			ii = (int)i;

			ang = 360 * (i / segments);

			c = Mathf.Cos(ang);
			s = Mathf.Sin(ang);

			x = radius * c;
			y = radius * s;
			noise[ii] = fbm(x, y, seed, iterations);
			//Debug.Log(noise[ii]);

			x = (radius + maxVariance * noise[ii]) * c;
			y = (radius + maxVariance * noise[ii]) * s;
			vex[ii] = new Vector2z(x, y);
			vex2[ii] = new Vector2(x, y);
			//Debug.Log(vex[ii]);
		}
	}

	void genMesh()
	{
		int[] indices = EarClipper.Triangulate(vex);

		Vector3[] vex3 = new Vector3[vex.Length];
		for(int i = 0; i < vex.Length; i++)
			vex3[i] = new Vector3(vex[i].x, vex[i].y, 0);

		if(mesh != null)
			Destroy(mesh);

		mesh = new Mesh();
		mesh.vertices = vex3;
		mesh.triangles = indices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		filter.mesh = mesh;

		col.SetPath(0, vex2);
	}

	void generate()
	{
		initGen();
		genNoiseMap();
		genMesh();
	}

	// Update is called once per frame
	void Update()
	{
		if(regen)
		{
			generate();
			regen = false;
		}
	}
}
