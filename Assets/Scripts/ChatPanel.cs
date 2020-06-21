using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : Panel
{

  public Text fullName;
  private Chat chat;

  [SerializeField]
  private ListView chatList;

  [SerializeField]
  private InputField messageInput;


  public void Init(Chat chat)
  {
    this.chat = chat;
    Fill();
  }
    public void Init(Person person) {
        Person p = person;
        fullName.text = p.FirstName + " " + p.LastName;
    }
    public void Fill()
  {
    Person p = chat.Person;
    fullName.text = p.FirstName + " " + p.LastName;

    foreach (Message m in chat.Messages)
    {
      MessageItem messageItem = ItemsFactory.CreateMessageItem(chatList.scrollContainer, m);
      chatList.Add(messageItem.gameObject);
    }
    StartCoroutine(UpdateListDimensions());
  }

  IEnumerator UpdateListDimensions(){

    yield return null;
    yield return new WaitForEndOfFrame();
    for (int i = 0; i < chatList.Items.Count; i++)
    {
      chatList.Items[i].GetComponent<MessageItem>().UpdateSize();
    }
    chatList.scrollContainer.SetActive(false);
    chatList.scrollContainer.SetActive(true);
    //LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)chatList.transform);
  }

  public void Send(){
    string msg = messageInput.text;

    if(msg != ""){
      //REQUEST SEND
      MessageItem item = ItemsFactory.CreateMessageItem(chatList.scrollContainer, new Message(msg, DateTime.Now, true));
      chatList.Add(item.gameObject);
          StartCoroutine(UpdateListDimension(item));
      messageInput.text = "";
    }
  }

  private IEnumerator UpdateListDimension(MessageItem item)
  {
    yield return null;
    yield return new WaitForEndOfFrame();

    item.UpdateSize();
  }

  internal override void Clear()
  {
    throw new System.NotImplementedException();
  }
}
