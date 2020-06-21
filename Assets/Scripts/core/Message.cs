using System;
using System.Collections;
using System.Collections.Generic;

public class Message {

    private bool owner;
    private DateTime sendDate;
    private string content;

    public bool Owner { get => owner; }
    public DateTime SendDate { get => sendDate; }
    public string Content { get => content; }

    public Message(string content, DateTime sendDate, bool owner) {
        this.owner = owner;
        this.sendDate = sendDate;
        this.content = content;
    }
}
