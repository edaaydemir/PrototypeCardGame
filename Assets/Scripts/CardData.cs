using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardData
{
    public CardClass CardClass;
    public int Power;
    public int Stamina;
    public float Luck;
    public Sprite Image;
}

[Serializable]
public enum CardClass
{
    Melee,
    Ranged,
    Hero
}