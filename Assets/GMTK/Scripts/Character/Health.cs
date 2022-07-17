//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Team
{
    Teamless,
    Player,
    Enemy,
}

[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDamaged;
    [SerializeField] private UnityEvent _onHealed;
    [SerializeField] private UnityEvent _onDeath;

    [SerializeField] private float _damageTick;
    private Coroutine _overTimeCoroutine;

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 70f;
    [SerializeField] private Team myTeam = 0;

    public float HealthPercentage => _currentHealth / _maxHealth;
    public Team CurrentTeam => myTeam;


    /// <summary>
    /// Subtracts a value to current health, while not allowing to go over max health
    /// </summary>
    /// <param name="value">Value subtracted to current health</param>
    public void ChangeHealth(float value)
    {
        if (value < 0)
        {
            _onHealed.Invoke();
        }
        else
        {
            _onDamaged.Invoke();
        }

        _currentHealth -= value;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
            return;
        }

        if (_currentHealth <= 0)
        {
            //TODO: Die

            _onDeath.Invoke();
        }
    }

    public void ChangeHealthOverTime(float value)
    {
        if (_overTimeCoroutine is not null) return;

        _overTimeCoroutine = StartCoroutine(OverTime(value));

    }

    private IEnumerator OverTime(float value)
    {
        yield return null;

        while (_currentHealth > 0)
        {
            ChangeHealth(value);
            yield return new WaitForSeconds(_damageTick);
        }
        
    }
}
