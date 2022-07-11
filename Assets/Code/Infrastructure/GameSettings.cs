using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header ("Player")]

    //PLayerMove
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _brakingSpeed;

}
