using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWave
{
    public float InitialDelay = 2f;
    public float SpawnDelay = 0.5f;
    public PooledObject[] Enemies;
}

public class WavesEncounter : Encounter
{
    [SerializeField] private EnemyWave[] _waves;

    public override void StartEncounter()
    {
        base.StartEncounter();

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            // get current wave
            EnemyWave wave = _waves[i];

            // pause if currently fighting enemies
            while (CurrentEnemyCount > 0) yield return null;

            // wait for initial wave delay
            yield return new WaitForSeconds(wave.InitialDelay);

            foreach (PooledObject enemy in wave.Enemies)
            {
                // spawn individual enemy and waiting
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(wave.SpawnDelay);
            }
        }

        // all enemies are spawned, pause until all defeated
        while (CurrentEnemyCount > 0) yield return null;

        OnEncounterFinished.Invoke();
    }
}