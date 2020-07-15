using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListView : MonoBehaviour {
    public GameObject scrollContainer;
    private List<GameObject> items = new List<GameObject>(5);
    private List<Item> itemss = new List<Item>(5);
    public GameObject selectedItem;

    public List<GameObject> Items { get => items; }

    public void Clear() {
        foreach (Transform child in scrollContainer.transform) {
            Destroy(child.gameObject);
        }
        itemss.Clear();
    }

    public bool IsEmpty() {
        return items.Count == 0;
    }
    public void Add(GameObject toAdd) {
        items.Add(toAdd);
        toAdd.transform.SetParent(scrollContainer.transform, false);
    }
    public void Add(Item toAdd) {
        itemss.Add(toAdd);
        toAdd.transform.SetParent(scrollContainer.transform, false);
    }
    internal void Unselect() {
        selectedItem.GetComponent<CarItem>().UnSelect();
    }

}
