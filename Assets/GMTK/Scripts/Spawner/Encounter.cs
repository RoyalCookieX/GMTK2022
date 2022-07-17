using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Encounter : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    public UnityEvent OnEncounterStarted;
    public UnityEvent OnEncounterFinished;

    private List<PooledObject> _currentEnemies = new List<PooledObject>();

    public int CurrentEnemyCount => _currentEnemies.Count;


    public virtual void StartEncounter()
    {
        OnEncounterStarted.Invoke();
    }


    protected Vector3 GetRandomSpawnPosition()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
    }


    protected void SpawnEnemy(PooledObject prefab)
    {
        // spawn enemy and add to list of current
        PooledObject spawned = PoolSystem.Instance.Get(prefab, GetRandomSpawnPosition(), Quaternion.identity);
        _currentEnemies.Add(spawned);
    }
}