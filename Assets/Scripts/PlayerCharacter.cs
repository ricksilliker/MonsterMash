using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float stamina;
    
    public void Feed(float energy)
    {
        stamina += energy;
    }
}
