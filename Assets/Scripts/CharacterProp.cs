using System;
using UnityEngine;

[Serializable]
public enum Alignment {Good, Bad, Neutral};

[CreateAssetMenu(fileName = "CharacterProp", menuName = "Character Property", order = 0)]
public class CharacterProp : ScriptableObject
{
    public string characterName;
    public Alignment characterAlignment;
    public float characterStamina;
    public float characterAttack;
    public GameObject characterPrefab;
}