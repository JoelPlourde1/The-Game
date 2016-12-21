using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    ItemDatabase database; 

    GameObject InventoryPanel;
    GameObject slotPanel;
    public GameObject InventorySlot;
    public GameObject InventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        database = GetComponent<ItemDatabase>();

        slotAmount = 20;
        InventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = InventoryPanel.transform.FindChild("Slot Panel").gameObject;

        for(int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(InventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(6, 2);
        AddItem(5, 2);

    }

    public void AddItem(int id, int AmountToAdd)
    {
        Item itemtoAdd = database.FetchItemByID(id);

        for( int j = 0; j < AmountToAdd; j++)
        {
            //If the objects is stackable and it's already in the inventory.
            if (itemtoAdd.Stackable && CheckIfItemsIsInInventory(itemtoAdd))
            {
                //For each items,
                for (int i = 0; i < items.Count; i++)
                {
                    //If the items ID matches:
                    if (items[i].ID == id)
                    {
                        //Get the child of the slots (items)
                        ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();

                        //Add the required amount of item.
                        data.amount++;

                        //Get the child of the items which is the text and change it's text value to the amount
                        //that is currently held.
                        if(data.amount != 1)
                        {
                            data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                        }
                        break;
                    }
                }
            }
            //The objects is not stackable or not in the current inventory, thus, simply add item into the inventory
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].ID == -1)
                    {
                        items[i] = itemtoAdd;
                        GameObject itemObj = Instantiate(InventoryItem);
                        itemObj.GetComponent<ItemData>().item = itemtoAdd;
                        itemObj.GetComponent<ItemData>().amount = 1;
                        itemObj.GetComponent<ItemData>().slot = i;
                        itemObj.transform.SetParent(slots[i].transform);
                        itemObj.transform.position = Vector2.zero;
                        itemObj.GetComponent<Image>().sprite = itemtoAdd.Sprite;
                        itemObj.name = itemtoAdd.Title;
                        break;
                    }
                }
            }
        }
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);

        if (itemToRemove.Stackable && CheckIfItemsIsInInventory(itemToRemove))
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].ID == id)
                {
                    ItemData data = slots[j].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    if (data.amount == 0)
                    {
                        Destroy(slots[j].transform.GetChild(0).gameObject);
                        items[j] = new Item();
                        break;
                    }
                    if (data.amount == 1)
                    {
                        slots[j].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                    break;
                }
            }
        }
        else
            for (int i = 0; i < items.Count; i++)
                if (items[i].ID != -1 && items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    items[i] = new Item();
                    break;
                }
    }

    bool CheckIfItemsIsInInventory(Item item)
    {
        for(int i = 0; i < items.Count; i++)
            if( items[i].ID == item.ID)
                return true;

            return false;
    }

}
