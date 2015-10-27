using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
	Mesh msh;

	void Start()
	{
		Vector3[] v = new Vector3[4];
		int[] t = new int[6];
		Vector2[] u = new Vector2[4];

		msh = new Mesh();

		v[0] = new Vector3(0f, 0f);
		v[1] = new Vector3(8f, 0f);
		v[2] = new Vector3(0f, 8f);
		v[3] = new Vector3(8f, 8f);

		t[0] = 0;
		t[1] = 1;
		t[2] = 2;
		t[3] = 2;
		t[4] = 1;
		t[5] = 3;

		u[0] = new Vector2(0f, 0.5f);
		u[1] = new Vector2(0.5f, 0.5f);
		u[2] = new Vector2(0f, 1f);
		u[3] = new Vector2(0.5f, 1f);

		msh.vertices = v;
		msh.triangles = t;
		msh.uv = u;

		msh.RecalculateNormals();
		msh.RecalculateBounds();

		GetComponent<MeshFilter>().mesh = msh;
	}
}
