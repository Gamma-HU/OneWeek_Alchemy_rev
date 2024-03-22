using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    //[SerializeField]
    //Text itemNameText;
    [SerializeField]
    EquipmentsSetManager equipmentManager;
    Item setItem;

    private void Start()
    {
        equipmentManager = FindObjectOfType<EquipmentsSetManager>();
    }
    public void SetItem(Item item)
    {
        if (setItem != null)
        {
            setItem.ResetSlot();
        }
        setItem = item;
        //itemNameText.text = item.GetItemName();
        //alchemyManager.SetAlchemyButton();
    }
    public void ResetItem()
    {
        setItem = null;
        //itemNameText.text = string.Empty;
        //alchemyManager.SetAlchemyButton();
    }

    public void ConsumeItem()
    {
        Destroy(setItem.gameObject);
        setItem = null;
        //itemNameText.text = string.Empty;
    }

    public Vector3 GetPos() { return transform.position; }
    public Item GetItem() { return setItem; }
}
