using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    Dictionary<string, int> currentInventory = new Dictionary<string, int>(); // public getter private setter
    
    int _maxIngredients = 5;

    [SerializeField] UIManager _UIManager;

    //def some ingreds - game objects to display?
    //def some ingreds - num in inventory

    void Start()
    {
        InitializeInventory();
    }

    void InitializeInventory(string ingredientsToLoad = "GenericScene")
    {
        // would be nice here to dynamically load inventory from a file elsewhere, maybe as enums or populating from a list of gameobjects/sprites
        XElement startingIngredients = XElement.Load("Assets/InputFiles/StartingIngredients.xml");
        
        foreach (var kvp in startingIngredients.Descendants(ingredientsToLoad).Descendants())
        {
            Debug.Log($"name: {kvp.Name.LocalName}");
            string val = kvp.Attribute("value").Value;
            Debug.Log($"value: {val}");
            currentInventory.Add(kvp.Name.LocalName, int.Parse(val));
        }

        _UIManager.UpdateIngredientUI(currentInventory);
    }

    //call from platformer/player script
    public void UpdateInventory(string ingredientName, int changeInTotal)
    {
        int currentAmmount = currentInventory[ingredientName] + changeInTotal;
        currentInventory[ingredientName] = currentAmmount > _maxIngredients ? _maxIngredients : currentAmmount;
        _UIManager.UpdateIngredientUI(currentInventory);
    }
}
