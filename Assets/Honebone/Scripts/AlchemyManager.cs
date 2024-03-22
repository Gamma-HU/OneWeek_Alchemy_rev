using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyManager : MonoBehaviour
{
    [SerializeField]
    List<AlchemyRecipe> alchemyRecipes = new List<AlchemyRecipe>();
    [SerializeField]
    AlchemySlot slot_L;
    [SerializeField]
    AlchemySlot slot_R;
    [SerializeField]
    GameObject alchemyButton;
    [SerializeField]
    Text productNameText;
    [SerializeField]
    Text draggingItemText;

    [SerializeField]
    Vector2 spawnPos;

    List<AlchemyRecipe> unlockedRecipes;
    void Start()
    {
        unlockedRecipes = new List<AlchemyRecipe>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDraggingItemText(string s)
    {
        draggingItemText.text = s;
    }
    public void SetAlchemyButton()
    {
        if (slot_L.GetItem() != null && slot_R.GetItem() != null)
        {
            alchemyButton.SetActive(true);
            string name1 = slot_L.GetItem().GetItemName();
            string name2 = slot_R.GetItem().GetItemName();
            foreach (AlchemyRecipe recipe in alchemyRecipes)
            {
                if (recipe.CheckMaterial(name1, name2))
                {
                    if (unlockedRecipes.Contains(recipe)) { productNameText.text = recipe.product.GetComponent<Item>().GetItemName(); }
                    else { productNameText.text = "???"; }
                    return;
                }
            }
            productNameText.text = "???";
        }
        else
        {
            alchemyButton.SetActive(false);
        }
    }
   
    public void Alchemy()
    {
        if (slot_L.GetItem() != null && slot_R.GetItem() != null)
        {
            string name1 = slot_L.GetItem().GetItemName();
            string name2 = slot_R.GetItem().GetItemName();
            foreach (AlchemyRecipe recipe in alchemyRecipes)
            {
                if (recipe.CheckMaterial(name1, name2))
                {
                    var p = Instantiate(recipe.product, spawnPos, Quaternion.identity);
                    p.GetComponent<Item>().Init();
                    p.GetComponent<Item>().Snap();

                    slot_L.ConsumeItem();
                    slot_R.ConsumeItem();
                    if (!unlockedRecipes.Contains(recipe)) { unlockedRecipes.Add(recipe); }
                    SetAlchemyButton();
                }
            }
        }
    }
}
