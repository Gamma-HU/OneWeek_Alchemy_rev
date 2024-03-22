using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/AlchemyRecipe")]
public class AlchemyRecipe : ScriptableObject
{
    public GameObject material_1;
    public GameObject material_2;

    public GameObject product;
    public bool CheckMaterial(string name_1, string name_2)
    {
        string material_n1 = material_1.GetComponent<Item>().GetItemName();
        string material_n2 = material_2.GetComponent<Item>().GetItemName();
        if (material_n1 == name_1 && material_n2 == name_2) { return true; }
        if (material_n1 == name_2 && material_n2 == name_1) { return true; }
        return false;
    }
}
