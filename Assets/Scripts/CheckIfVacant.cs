using UnityEngine;
using System.Collections;

public class CheckIfVacant : MonoBehaviour {
	public int vacant=0;
	// Use this for initialization
	void Start () {
		vacant = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Occupied()
	{
		vacant = 1;
		//return vacant;
	}
	
	public void Vacancy()
	{
		vacant = 0;
		//return vacant;
	}
	public int ValueofTheSpace()
	{
		return vacant;
	}

}
