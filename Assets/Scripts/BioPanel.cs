using Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BioPanel : Panel
{
  public InputFieldScript bio;

  public override void Init()
  {
    Clear();
    bio.SetText(Program.Person.Bio);
  }
  public void submit()
  {
    if (validate())
    {
      Person oldPerson = Program.Person;
      User oldUser = Program.User;

      Person editedPerson = new Person(oldPerson.id, oldPerson.FirstName, oldPerson.LastName, oldPerson.Chattiness,oldPerson.Phone,
      oldPerson.CountryInformations, bio.text.text, oldPerson.RateAverage, oldPerson.Gender, oldPerson.Birthday,
      DateTime.Now, oldPerson.profilePictureUrl);
      User editedUser = new User(editedPerson, oldUser.Driver, oldUser.phone, oldUser.password, oldUser.Email, oldUser.Id, oldUser.Token);
      Request<User> request = new EditAccount(editedUser);
      Task.Run(() => request.Send(response));
    }
  }
  private void response(User result, int code, string message)
  {
    if (!code.Equals(HttpStatusCode.OK))
    {
      Panel p = PanelsFactory.CreateDialogBox("There was an error adding bio", false);
      OpenDialog(p);
    }
    else
    {
      Panel p = PanelsFactory.CreateDialogBox("Bio has been added", true);
      OpenDialog(p);
      Panel panel = PanelsFactory.createProfile();
      openCreated(panel);
    }
  }

  private bool validate()
  {
    bool valid = true;
    if (bio.text.text.Equals(""))
    {
      bio.Error();
      Panel p = PanelsFactory.CreateDialogBox("There was an error adding bio", false);
      OpenDialog(p);
      valid = false;
    }
    if (bio.text.text.Length < 20)
    {
      bio.Error();
      Panel p = PanelsFactory.CreateDialogBox("Bio is too short", false);
      OpenDialog(p);
      valid = false;
    }
    if (bio.text.text.Length > 190)
    {
      bio.Error();
      Panel p = PanelsFactory.CreateDialogBox("Bio is too long", false);
      OpenDialog(p);
      valid = false;
    }
    return valid;
  }
  internal override void Clear()
  {
    //clear content of all inptfield.
    bio.Reset();
  }
}
