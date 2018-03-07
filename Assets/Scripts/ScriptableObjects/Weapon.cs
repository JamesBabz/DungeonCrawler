using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public float Damage;
    public float Durability;
    public float Range = 0;
    public AnimationClip Attack_Left;
    public AnimationClip Attack_Right;
    public AnimationClip Attack_Up;
    public AnimationClip Attack_Down;
}
