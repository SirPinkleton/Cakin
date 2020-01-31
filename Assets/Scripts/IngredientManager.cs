using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IngredientManager : MonoBehaviour
{
    Dictionary<string, int> inventory = new Dictionary<string, int>(); // public getter private setter
    string _ingredientText;
    [SerializeField] Text _uiTextComponent;
    //def some ingreds - game objects to display?
    //def some ingreds - num in inventory

    void Start()
    {
        InitializeInventory();
    }

    void InitializeInventory(int flours = 0, int milks = 0, int sugars = 0)
    {
        var inventory = new Dictionary<string, int>();
        // would be nice here to dynamically load inventory from a file elsewhere, maybe as enums or populating from a list of gameobjects/sprites
        inventory.Add("Flour", flours);
        inventory.Add("Milk", milks);
        inventory.Add("Sugar", sugars);
        UpdateUI();
    }

    // maybe move this to a UIManager later??
    void UpdateUI()
    {
        //responsiveness?? maybe set up via the textbox settings instead
        string ingredients = string.Join(" ", inventory.Select(i => $"{i.Key}: {i.Value}").ToList());
        _uiTextComponent.text = ingredients;
    }

    //call from platformer/player script
    public void UpdateInventory(string ingredientName, int changeInTotal)
    {
        inventory[ingredientName] += changeInTotal;
        UpdateUI();
    }
}
