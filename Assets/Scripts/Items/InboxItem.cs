using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InboxItem : Panel {
    private Panel inboxPanel;
    public Text fullName ;
    public Text lastMessage;
    public Image profileImage,newMessageImage;
    public Person person = null;
    private Chat chat = null;

    public void Init(Chat chat, Person person, Panel inboxPanel) {
        Clear();
        this.person = person;
        this.chat = chat;
        this.lastMessage.text = chat.Messages[chat.Messages.Count-1].Content;
        this.fullName.text = chat.Person.FirstName+" "+chat.Person.LastName;
        this.profileImage.sprite = Program.GetImage(chat.Person.ProfilePicture);
        this.inboxPanel = inboxPanel;
        if (chat.IsNewMessage) {
            newMessageImage.gameObject.SetActive(true);
        }
    }
    public void OpenChat() {
        ChatPanel panel = PanelsFactory.CreateChat();
        inboxPanel.Open(panel, () => {panel.Init(chat);} );
    }
    public void Search() {

    }
    internal override void Clear() {
        this.person = null;
        this.fullName.text = "";
        newMessageImage.gameObject.SetActive(false);
    }
}