using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //[SerializeField] string itemName;
    //[SerializeField] bool canEquip;
    //[SerializeField] GameObject passiveAbility;
    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        public Sprite itemSprite;
        public bool canEquip;
       [Header("装備可能アイテムのみ")] public GameObject passiveAbility;
    }
    [SerializeField]
    ItemData itemData;
    bool dragging;
    Rigidbody2D rb;

    AlchemyManager alchemyManager;

    AlchemySlot onSlot_Alchemy;
    EquipmentSlot onSlot_Equipment;
    void Start()
    {
        Init();
    }
    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        alchemyManager = FindObjectOfType<AlchemyManager>();
    }

    public void SetDragging(bool set)
    {
        dragging = set;
        if (dragging)//ドラッグ開始
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            rb.angularVelocity = 0;
            if(onSlot_Alchemy != null)//錬金スロット上にあるなら
            {
                onSlot_Alchemy.ResetItem();
            }
            if (itemData.canEquip)
            {
                alchemyManager.SetDraggingItemText(itemData.passiveAbility.GetComponent<PassiveAbility>().GetInfo());
            }
        }
        else//ドラッグ終了
        {
            rb.velocity = Vector2.zero;
            alchemyManager.SetDraggingItemText("");
            if(onSlot_Alchemy != null)//錬金スロット上にあるなら
            {
                onSlot_Alchemy.SetItem(this);
                rb.MovePosition(onSlot_Alchemy.transform.position);
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void Update()
    {
        if (dragging) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(pos);
        }
    }

    public void ResetSlot()
    {
        onSlot_Alchemy = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Snap();
    }
    public void Snap()
    {
        float deg = Random.Range(0f, 360f);
        Vector2 dir = deg.UnitCircle();
        rb.AddForce(dir * 1000f);
        rb.AddTorque(Random.Range(500f,1500f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dragging && collision.CompareTag("AlchemySlot"))
        {
            onSlot_Alchemy = collision.GetComponent<AlchemySlot>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dragging && collision.CompareTag("AlchemySlot"))
        {
            onSlot_Alchemy = null;
        }
    }

    public string GetItemName() { return itemData.itemName; }
    public ItemData GetItemData() { return itemData; }
}
