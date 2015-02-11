using UnityEngine;
using System.Collections;

public class SpawntheEnemy : MonoBehaviour {
	
	#region Variables
	GameObject[]Blocks;
	int val=0;
	private string[] EnemyName = new string[20];
	int []Vacancy;
	int spawnpoint=0;
	public float[] TimerforSpawning;
	public float[] TimerforDying;
	GameObject GlobalStats;
	private float health;
	private int level;
	private int stars;
	private int coins;
	float Timer=1;
	
	#endregion
	
	void Start () {
		callthis ();
		
		//stats();
		TimerControl();

	
	}
	void callthis()
	{
		Blocks = GameObject.FindGameObjectsWithTag("GridObject");
		Vacancy = new int[Blocks.Length];
		for (int i=0; i<Blocks.Length; i++)
			Vacancy [i] = 0;
		EnemyName [0] = "Bunny1";
		EnemyName [1] = "Bunny2";
		EnemyName [2] = "Bunny3";
	
	}
	/*void stats()
	{
        int LevelEnemies;
		GlobalStats = GameObject.FindGameObjectWithTag ("Game Manager");
		health = GlobalStats.GetComponent <"NameofTheScript">().health;
		level=GlobalStats.GetComponent<"NameofTheScript">().level;
		stars=GlobalStats.GetComponent<"NameofTheScript">().stars;
		coins=GlobalStats.GetComponent<"NameofTheScript">().coins;
 		LevelEnemies=EnemiesBasedOnLevel(level);
		for(int j=0;j<LevelEnemies;j++)
		RepeatingSwarm(TimerforSpawning[j],TimerforDying[j]);
	}*/
	#region LevelCreator
	int EnemiesBasedOnLevel(int level)
	{
		int numberofenemies=0;
		switch(level)
		{
		case 1: numberofenemies=1;
			    break;
		case 2: numberofenemies=2;
				break;
		case 3: numberofenemies=3;
				break;
		}
		return numberofenemies;
	}
	#endregion 
	void TimerControl()
	{
		TimerforDying = new float[20];
		TimerforSpawning = new float[20];
		#region InitializeTimers
		TimerforDying[0]=3;
		TimerforDying[1]=3;
		TimerforDying[2]=3;
		
		TimerforSpawning[0]=1;
		TimerforSpawning[1]=1;                 
		TimerforSpawning[2]=1;
		#endregion
	}
	void checkifVacant()
	{
		int x;
		spawnpoint = Random.Range (0, 9);
		CheckIfVacant c = Blocks [spawnpoint].GetComponent<CheckIfVacant> ();
		x = c.vacant;
		return x;

	}
	void Spawning(float Timer,float DiesIn)
	{
		int x;
		float Countdown=Timer;
		Countdown-= Time.deltaTime;  
	
			if (x == 0)
			{  
				val = Random.Range (0, 3);
				GameObject Enemyspawn = (GameObject)Instantiate (Resources.Load (EnemyName[val]), Blocks [spawnpoint].transform.position, Blocks [spawnpoint].transform.rotation);
				Enemyspawn.GetComponent<TimeTillDeath> ().SetTheTime (DiesIn, spawnpoint);
			} 
			else 
			{  
				//choose another spawn point and call that above part untill one thing gets assigned
			}


		}
	IEnumerator RepeatingSwarm(float Timer,float DiesIn)
	{

			while(checkIfVacant()) {
				SearchForTarget(param1, param2, ...);
				yield return new WaitForSeconds(repeatRate);
			}
		}
		}

	void Update () {
	//	StartCoroutine(RepeatingSwarm(param1, param2,..., repeatRate));
			//Spawning(TimerforSpawning[j],TimerforDying[j]);

		//InvokeRepeating ("somefunction", 1.0f, 1.0f);

	}
}
