using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InboxPanel : Panel {
    public ListView listView;
    public InputField search;
    public Image backImage;
    public Text title;
    public Person person;
    private List<InboxItem> inboxItems = new List<InboxItem>();

  public static readonly string PANELNAME = "INBOXPANEL";

  internal override void Clear() {
        backImage.gameObject.SetActive(false);
        listView.Clear();
        title.transform.position = new Vector3(56f, title.transform.position.y, title.transform.position.z);
    }
    public void Init(List<Chat> chats) {
        Clear();
    Status = StatusE.VIEW;
        foreach (Chat c in chats) {
            var item = ItemsFactory.CreateInboxItem(listView.scrollContainer, c, person, this);
            listView.Add(item.gameObject);
            //storing the list
            inboxItems.Add(item);
            Debug.Log("adding in lists");
        }
    }
    public void Search() {
        Debug.Log("search");

        bool isNoResult = true;
        string searchText = search.text;
        for (int i = 0; i < inboxItems.Count; i++) {
            //indoxOf to search ignoring capital case
            if (inboxItems[i].fullName.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 || inboxItems[i].lastMessage.text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) {
                inboxItems[i].gameObject.SetActive(true);
                //when we found result,then no result become false
                isNoResult = false;
            } else {
                inboxItems[i].gameObject.SetActive(false);
            }
        }
        //when checking list complete and no result true,display message
        if (isNoResult) {
            OpenDialog("No results found", false);
        }
    }
    internal void Init(Person personToChat) {
        Clear();
    Status = StatusE.VIEW;
        backImage.gameObject.SetActive(true);
        title.transform.position = new Vector3(title.transform.position.x + 50, title.transform.position.y, title.transform.position.z);
        Panel panel = PanelsFactory.createChat(personToChat);
        openCreated(panel);
    }
}
