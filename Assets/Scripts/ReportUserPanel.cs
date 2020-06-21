using Requests;
using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ReportUserPanel : Panel
{

    public Dropdown reason;
    public InputFieldScript comment;
    public Person person = null;
    public void ReportUser()
    {
        if (Validate())
        {
            Request<Person> request = new ReportUser(person, reason.options[reason.value].text, comment.text.text);
            Task.Run(() => request.Send(response));
        }

    }

    public void Init(Person person)
    {
        Clear();
        this.person = person;
    }
    internal override void Clear()
    {
        reason.value = 0;
        comment.Reset();
    }
    private void response(Person result, HttpStatusCode code, string message)
    {
        if (!code.Equals(HttpStatusCode.OK))
        {
            OpenDialog("Something went wrong", false);
        }
        else
        {

            OpenDialog("Report Done", false);
            back();
        }
    }

    private bool Validate()
    {
        bool valid = true;
        if (reason.value == 0)
        {
            valid = false;
            OpenDialog("Please select a reason", false);

        }
        if (comment.text.text.Equals(""))
        {
            comment.Error();

            OpenDialog("Comment Can't be empty", false);
            valid = false;
        }
        if (comment.text.text.Length < 10)
        {
            comment.Error();

            OpenDialog("Comment Too Short", false);
            valid = false;
        }
        return valid;

    }
}
