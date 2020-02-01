using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] int _numInStack = 1;
    // define with FindObjectOfType in Start() and sort this shit out better
    [SerializeField] IngredientManager _inventoryManager;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _inventoryManager.UpdateInventory(gameObject.name, _numInStack);
            Destroy(gameObject);
        }
    }
}
