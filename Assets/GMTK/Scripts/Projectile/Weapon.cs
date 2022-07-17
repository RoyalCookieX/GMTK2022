using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _barrel;
    private int _selectedWeapon;

    private int _numberOfShots;

    



    private void OnShoot()
    {
        switch (_selectedWeapon)
        {
            case 0:
                //Projectile.RaycastShot();
                break;
            case 1:
                for (int i = 0; i < _numberOfShots; i++)
                {
                    //Projectile.RaycastShot();
                }
                break;
            case 3:
                //TODO:Rocket Shoot
                break;
            case 4:
                //TODO:Wind Blast
                break;
            case 5:
                //TODO:Teleport
                break;
            case 6:
                //TODO:Pierce
                break;
            case 7:
                //TODO:Poison Dart
                break;
            case 8:
                //TODO:Freeze
                break;
            case 9:
                //TODO:Poison
                break;
            case 10:
                //TODO:Gravity Well
                break;
            case 11:
                //TODO:Golden Gun
                break;
            default:
                break;
        }
    }

    private void OnReload()
    {
        _selectedWeapon = Random.Range(0, 20);
    }
}
