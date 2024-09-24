using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroCardData", menuName = "ScriptableObjects/HeroCardData", order = 1)]
public class HeroCardData : ScriptableObject
{
    public int HeroPower;
    public int HeroStamina;
    public float HeroLuck;
}
