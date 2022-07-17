using System.Collections;
using UnityEngine;

public class BotController : MonoBehaviour
{
    enum State
    {
        Chase
    }

    [Header("Components")]
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private Transform _target;

    private State _state;

    private IEnumerator Start()
    {
        while(true)
        {
            switch(_state)
            {
                case State.Chase:
                {
                    _characterMovement.SetMoveLocation(_target.position);
                } break;
            }
            yield return null;
        }
    }
}