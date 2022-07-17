//Made by Koda Villela
//Contributed by Andrei Cabungcal

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum Team
{
    Ally,
    Enemy1,
    Enemy2,
    Enemy3
}

public class CharacterHealth : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onHealthChanged;
    [SerializeField] private UnityEvent _onDead;

    [Header("Properties")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _health = 70f;
    [SerializeField] private Team _team = 0;

    public float HealthPercentage => _health / _maxHealth;
    public Team CurrentTeam => _team;
    private Coroutine _autoHealthRoutine;


    /// <summary>
    /// Directly sets health, while not allowing to go over max health
    /// </summary>
    /// <param name="amount">Value set to health</param>
    public void SetHealth(float amount)
    {
        Debug.Log($"[{gameObject.name}]: ({_health}/{_maxHealth})");
        _health = Mathf.Clamp(amount, 0f, _maxHealth);
        _onHealthChanged?.Invoke(HealthPercentage);
        if(_health <= 0f)
        {
            Kill();
        }
    }

    /// <summary>
    /// Adds to health, while not allowing to go over max health
    /// </summary>
    /// <param name="amount">Value added to health</param>
    public void AddHealth(float amount)
    {
        SetHealth(_health + amount);
    }

    /// <summary>
    /// Adds to health over time, while not allowing to go over max health
    /// </summary>
    /// <param name="amount">Value added to health</param>
    /// <param name="duration">Duration(seconds) between amounts</param>
    /// <param name="repeat">Number of times to add amount</param>
    public void AddHealth(float amount, float duration, int repeat)
    {
        if (_autoHealthRoutine != null)
            return;
        _autoHealthRoutine = StartCoroutine(AutoHealthRoutine(amount, duration, repeat));
    }

    /// <summary>
    /// Adds damage based on the team
    /// </summary>
    /// <param name="team">The team that instigated the damage</param>
    /// <param name="amount">Value subracted from health</param>
    /// <returns></returns>
    public bool Damage(Team team, float amount)
    {
        if (team == _team)
            return false;

        AddHealth(-amount);
        return true;
    }

    /// <summary>
    /// Adds damage based on the team, over time
    /// </summary>
    /// <param name="team">The team that instigated the damage</param>
    /// <param name="amount">Value subracted from health</param>
    /// <param name="duration">Duration(seconds) between amounts</param>
    /// <param name="repeat">Number of times to add amount</param>
    /// <returns></returns>
    public bool Damage(Team team, float amount, float duration, int repeat)
    {
        if (team == _team)
            return false;

        AddHealth(-amount, duration, repeat);
        return true;
    }

    /// <summary>
    /// Kills the gameObject
    /// </summary>
    public void Kill()
    {
        Debug.Log($"[{gameObject.name}]: (DEAD)");
        _onDead?.Invoke();
    }

    private IEnumerator AutoHealthRoutine(float amount, float duration, int repeat)
    {
        for(int i =0; i < repeat; i++)
        {
            AddHealth(amount);
            yield return new WaitForSeconds(duration);
        }
    }
}
