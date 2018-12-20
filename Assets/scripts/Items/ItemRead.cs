using Assets.scripts;
using UnityEngine;

public class ItemRead : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        ReadItem();
    }

    void ReadItem()
    {
        Debug.Log("Reading item " + item.name);
        //if (Inventory.instance.Add(item))
        //{
        //    Destroy(gameObject);
        //}
    }

}
