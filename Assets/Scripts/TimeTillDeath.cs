using UnityEngine;
using System.Collections;

public class TimeTillDeath : MonoBehaviour {
 
	float Timer;
	int spawnpoint=0;
	GameObject []Blocks;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Timer> 0){
		    // Blocks[spawnpoint]   
			Timer-= Time.deltaTime;
		}
		if(Timer<= 0){
			Destroy (this.gameObject);
			Blocks [spawnpoint].GetComponent<CheckIfVacant>().Vacancy();	

		}
	}
	public void SetTheTime(float  t,int value)
	{
		Blocks = GameObject.FindGameObjectsWithTag("GridObject");
		Timer = t;
		spawnpoint = value;
		Blocks [spawnpoint].GetComponent<CheckIfVacant>().Occupied();
		//Blocks[spawnpoint].GetComponent<CheckIfVacant>();
	}
}
