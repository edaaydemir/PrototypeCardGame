using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeCardData", menuName = "ScriptableObjects/MeleeCardData", order = 1)]
public class MeleeCard : ScriptableObject
{
    public int MeleePower;   
    public int MeleeStamina;
    public float MeleeLuck;
}
