using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawoner : MonoBehaviour
{
	Game_Manager manager;
	public List<Transform> enemyPos;
	public GameObject[] enemyPrefab;

	int EnemiesInRound = 10;
	int EnemiesSpawnedInRound = 0;
	float EnemiesSpawnedTimer = 0;
	public int EnemiesLeftInRound = 10;

	private void Start()
	{
		manager = FindObjectOfType<Game_Manager>();
	}

	private void Update()
	{
		if (EnemiesSpawnedInRound < EnemiesInRound && manager.CountDown == 0)
		{
			if (EnemiesSpawnedTimer > 2)
			{
				SpaewnEnemy();
				EnemiesSpawnedTimer = 0;
			}
			else
			{
				EnemiesSpawnedTimer += Time.deltaTime;
			}
		}
		else if (EnemiesLeftInRound == 0)
		{
			StartNextRound();
		}

		if (manager.CountDown > 0)
		{
			manager.CountDown -= Time.deltaTime;
			manager.NextWavePanel.SetActive(true);
		}
		else
		{
			manager.CountDown = 0;
			manager.NextWavePanel.SetActive(false);
		}
	}

	void SpaewnEnemy()
	{
		Vector3 RandomSpawnPoint = enemyPos[Random.Range(0, enemyPos.Capacity)].position;
		Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], RandomSpawnPoint, Quaternion.identity);
		EnemiesSpawnedInRound++;
	}

	void StartNextRound()
	{
		EnemiesInRound = EnemiesLeftInRound = manager.Wave * 5;
		EnemiesSpawnedInRound = 0;
		manager.CountDown = 15;
		manager.Wave++;
	}
}
