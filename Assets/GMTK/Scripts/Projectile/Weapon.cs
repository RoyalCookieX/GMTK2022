//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData[] _data;

    [SerializeField] private Team team;
    [SerializeField] private GameObject _player;
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private Transform _firePoint;
    private int _selectedWeapon = 0;

    [SerializeField] private PooledObject _gravityWell;
    [SerializeField] private PooledObject _acidPool;

    RaycastHit _hit = new();
    [SerializeField] private int _numberOfShots = 8;

    [SerializeField] private LayerMask _enemyMask;

    private int MaxAmmo => _data[_selectedWeapon].MaxAmmo;
    private int _currentAmmo = 0;
    private float Cooldown => _data[_selectedWeapon].Cooldown;
    private float _currentCooldown;
    private float Damage => _data[_selectedWeapon].Damage;
    private float Range => _data[_selectedWeapon].Range;
    private float Radius => _data[_selectedWeapon].Radius;
    private float Knockback => _data[_selectedWeapon].Knockback;
    private float Inaccuracy => _data[_selectedWeapon].Inaccuracy;


    private void Update()
    {
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
            return;
        }
        _currentCooldown = 0;

    }

    public void OnShoot()
    {
        if (_currentCooldown > 0)
            return;

        switch (_selectedWeapon)
        {
            case 0:
                Projectile.RaycastShot(out _hit, team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Damage, Knockback, Inaccuracy);
                break;
            case 1:
                for (int i = 0; i < _numberOfShots; i++)
                {
                    Projectile.RaycastShot(out _hit, team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Damage, Knockback, Inaccuracy);
                }
                break;
            case 2:
                Projectile.RocketFire(team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Damage, Knockback, Inaccuracy);
                break;
            case 3:
                Projectile.RaycastPierce(team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, _wallMask, Damage, Knockback);
                break;
            case 4:
                Projectile.TeleportShot(ref _player, team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Inaccuracy);
                break;
            case 5:
                Projectile.RaycastPierce(team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, _wallMask, Damage, Knockback);
                break;
            case 6:
                Projectile.Poison(team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Damage);
                break;
            case 7:
                Projectile.Freeze(team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Inaccuracy );
                break;
            case 8:
                Projectile.InstantiateShot(ref _acidPool, team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Inaccuracy);
                break;
            case 9:
                Projectile.InstantiateShot(ref _gravityWell, team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Inaccuracy);
                break;
            case 10:
                if (Projectile.RaycastShot(out _hit, team, _firePoint.position, _firePoint.forward, Radius, Range, _enemyMask, Damage, Knockback, Inaccuracy))
                {
                    _currentAmmo++;
                }
                break;
            default:

                break;
        }
        _currentAmmo--;
    }

    public void OnReload()
    {
        _selectedWeapon = Random.Range(0, 11);
        _currentAmmo = MaxAmmo;
    }
}
