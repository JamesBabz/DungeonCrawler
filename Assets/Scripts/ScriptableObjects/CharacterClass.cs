using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class CharacterClass : ScriptableObject
{

    public float Life = 100;
    public float WalkSpeed = 1;
    public float RunSpeed = 2;
    public RuntimeAnimatorController RuntimeAnimatorController;
    
}
