using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI Stats")]
public class AiStats : ScriptableObject
{
    public float movespeed;
    public float shotpower;
    public float stunduration;
    public float pokecheck;
    public bool isGoon;
}
