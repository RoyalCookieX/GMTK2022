using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_Data", menuName = "Weapon/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private int _maxAmmo;
    public int MaxAmmo => _maxAmmo;

    [SerializeField]
    private float _cooldown;
    public float Cooldown => _cooldown;

    [SerializeField]
    private float _damage;
    public float Damage => _damage;

    [SerializeField]
    private float _range;
    public float Range => _range;

    [SerializeField]
    private float _radius;
    public float Radius => _radius;

    [SerializeField]
    private float _knockback;
    public float Knockback => _knockback;

    [SerializeField]
    private float _inaccuracy;
    public float Inaccuracy => _inaccuracy;
}
