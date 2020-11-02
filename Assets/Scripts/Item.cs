using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float energy;
    [SerializeField] private GameObject modelPrefab;

    private GameObject _instancedModel;
    
    void Start()
    {
        _instancedModel = Instantiate(modelPrefab, transform.position, transform.rotation);
        _instancedModel.transform.SetParent(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player)
        {
            player.Feed(Energy);
            Eat();
        }
    }

    public float Energy
    {
        get => energy;
    }

    public void Eat()
    {
        Destroy(_instancedModel);
        Destroy(gameObject);
    }
}
