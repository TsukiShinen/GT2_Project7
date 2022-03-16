using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entity Data")]
public class EntityData : ScriptableObject
{
    [Header("Stats")]
    public int MaxLife;
    public int Attack;
    public int Defense;

    [Header("Movement")]
    public float MoveSpeed;
    public float Acceleration;
    public float Decceleration;
    public float VelPower;
    [Space(10)]
    public float FrictionAmount;
}
