using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    [SerializeField] private float stamina;
    
    public void Feed(NPC pawn, float energy)
    {
        pawn.HitPoints += energy;
    }
}
