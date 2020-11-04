using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private CharacterProp characterSheet;

    private GameObject _instancedCharacter;
    private float _attackPoints;
    private float _hitPoints;

    void Start()
    {
        _hitPoints = characterSheet.characterStamina;
        _attackPoints = characterSheet.characterAttack;
        _instancedCharacter = Instantiate(characterSheet.characterPrefab, transform.position, transform.rotation);
        _instancedCharacter.transform.SetParent(transform);
    }

    private void OnDrawGizmos()
    {
        Vector3 center = new Vector3(0, 0.5f, 0);
        center += transform.position;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, new Vector3(1, 1, 1));
        
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(center, direction);
    }

    public void Attack(Tile tile, bool moveToTile)
    {
        RaycastHit hit;
        if (Physics.Raycast(tile.GetTilePosition(), Vector3.up, out hit, 10))
        {
            NPC pawn = hit.transform.GetComponent<NPC>();
            if (pawn)
            {
                pawn.TakeDamage(AttackPoints);
            }
        }
    }

    public float AttackPoints
    {
        get => _attackPoints;
        set => _attackPoints = value;
    }

    public float HitPoints
    {
        get => _hitPoints;
        set => _hitPoints = value;
    }

    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
