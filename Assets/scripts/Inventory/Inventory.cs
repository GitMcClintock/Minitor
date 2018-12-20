using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts
{
    public class Inventory : MonoBehaviour
    {
        public int capacity = 1;
        public List<Item> items = new List<Item>();

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        #region Singleton
        public static Inventory instance;

        private void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("More than one inventory found!");
                return;
            }

            instance = this;
        }

        #endregion

        public bool Add(Item item) 
        {
            Debug.Log("Adding item " + item.name + " to inventory.");
            if(!item.isDefaultItem)
            {
                if(items.Count < capacity)
                {
                    items.Add(item);
                    if(onItemChangedCallback != null) {
                        onItemChangedCallback.Invoke();
                    }
                }
                else
                {
                    Debug.Log("Your inventory is full!");
                    return false;
                }
            }
            return true;
        }

        public void Remove(Item item)
        {
            items.Remove(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

    }
}
