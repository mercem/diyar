using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{

	void Update () 
	{
		transform.Rotate(transform.up, 1);
	}
}
