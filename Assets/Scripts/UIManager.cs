using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    string _ingredientText;
    [SerializeField] Text _uiIngredientTextComponent;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void UpdateIngredientUI(Dictionary<string, int> newInventory)
    {
        //responsiveness?? maybe set up via the textbox settings instead
        string ingredients = string.Join("  ", newInventory.Select(i => $"{i.Key}: {i.Value}").ToList());
        _uiIngredientTextComponent.text = ingredients;
    }
}
