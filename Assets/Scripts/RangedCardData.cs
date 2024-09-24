using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedCardData", menuName = "ScriptableObjects/RangedCardData", order = 1)]
public class RangedCardData : ScriptableObject
{
    public int RangedPower;
    public int RangedStamina;
    public float RangedLuck;
    
}
