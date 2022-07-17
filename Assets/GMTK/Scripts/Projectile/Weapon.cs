//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private Transform _barrel;
    private Vector3 _target;
    private int _selectedWeapon;

    [SerializeField] private GameObject _gravityWell;
    [SerializeField] private GameObject _acidPool;

    RaycastHit _hit = new();
    private int _numberOfShots;

    private int _maxAmmo;
    private int _currentAmmo;
    private float _cooldown;

    private LayerMask _layerMask;
    private float _damage;
    private float _range;
    private float _radius;
    private float _knockback;
    
    



    private void OnShoot()
    {
        switch (_selectedWeapon)
        {
            case 0:
                Projectile.RaycastShot(out _hit,_barrel.position, _target, _radius, _range, _layerMask, _damage, _knockback);
                break;
            case 1:
                for (int i = 0; i < _numberOfShots; i++)
                {
                    Projectile.RaycastShot(out _hit, _barrel.position, _target, _radius, _range, _layerMask, _damage, _knockback);
                }
                break;
            case 2:
                Projectile.RocketFire(_barrel.position, _target, _radius, _range, _layerMask, _damage, _knockback);
                break;
            case 3:
                Projectile.RaycastPierce(_barrel.position, _target, _radius, _range, _layerMask, _wallMask, _damage, _knockback);
                break;
            case 4:
                Projectile.TeleportShot(ref _player,_barrel.position, _target, _radius, _range, _layerMask);
                break;
            case 5:
                Projectile.RaycastPierce(_barrel.position, _target, _radius, _range, _layerMask, _wallMask, _damage, _knockback);
                break;
            case 6:
                Projectile.Poison(_barrel.position, _target, _radius, _range, _layerMask, _damage);
                break;
            case 7:
                Projectile.Freeze(_barrel.position, _target, _radius, _range, _layerMask);
                break;
            case 8:
                Projectile.InstantiateShot(ref _acidPool, _barrel.position, _target, _radius, _range, _layerMask);
                break;
            case 9:
                Projectile.InstantiateShot(ref _gravityWell, _barrel.position, _target, _radius, _range, _layerMask);
                break;
            case 10:
                if (Projectile.RaycastShot(out _hit, _barrel.position, _target, _radius, _range, _layerMask, _damage, _knockback))
                {
                    _currentAmmo += 1;
                }
                break;
            default:

                break;
        }
    }

    private void OnReload()
    {
        _selectedWeapon = Random.Range(0, 10);
    }
}
