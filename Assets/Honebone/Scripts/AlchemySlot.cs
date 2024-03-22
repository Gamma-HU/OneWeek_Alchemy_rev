using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemySlot : MonoBehaviour
{
    [SerializeField]
    Text itemNameText;
    [SerializeField]
    AlchemyManager alchemyManager;
    Item setItem;

    private void Start()
    {
        alchemyManager=FindObjectOfType<AlchemyManager>();
    }
    public void SetItem(Item item)
    {
        if (setItem != null)
        {
            setItem.ResetSlot();
        }
        setItem = item;
        itemNameText.text=item.GetItemName();
        alchemyManager.SetAlchemyButton();
    }
    public void ResetItem()
    {
        setItem = null;
        itemNameText.text = string.Empty;
        alchemyManager.SetAlchemyButton();
    }

    public void ConsumeItem()
    {
        Destroy(setItem.gameObject);
        setItem = null;
        itemNameText.text = string.Empty;
    }

    public Vector3 GetPos() { return transform.position; }
    public Item GetItem() { return setItem; }
}
