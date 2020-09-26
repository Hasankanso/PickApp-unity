using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

public class Language : MonoBehaviour
{

  static Hashtable XML_Strings; //The hashtable that we create to contain the data
  private static readonly string relativeFolderPath = "Lang/";
  private static readonly string languageURL = "http://yourdomainname.dx.am/MLS_Languages/";
  private static string currentLanguage;
  private readonly string defaultLanguage = "LatArabic";

  ///This constructor is called when we instantiate this class in the "LanguageManager" class.
  ///It's called when we set a new language to read it thanks to the functions "SetLanguageWeb" and "SetLocalLanguage".
  ///
  ///The parameters are: the XML file is gived as WWW.text result for the Web and as the physical file for the local.
  ///The "language" is the language that we've selected
  ///The "isLocal" define what function to call, if the one to open a web file or the one to open a file stored on the computer
  private Action<bool, long> OnSetFinished;

  public static Language language; //For the singleton pattern.
  public static Language Arabic;
    public void Awake()
  {
    if (language == null)
    {
      language = this;
    }

    var currlang = Cache.GetLanguage();
        if (currlang == string.Empty)
            currlang = defaultLanguage;
            OpenLocalXML(currlang);
  }

  /// Get a string from the hastable by the index gived in it.
  public string GetString(string _name)
  {
    if (!XML_Strings.ContainsKey(_name))
    {
      Debug.LogError("This string is not present in the XML file where you're reading: " + _name);

      return "";
    }

    return (string)XML_Strings[_name];
  }

  public void LoadLanguage(string Language, Action NeedInternet, Action<bool, long> OnSetFinished)
  {
    bool exist = PlayerPrefs.GetInt(Language, 0) == 1 ? true : false;
    this.OnSetFinished = OnSetFinished;

    if (exist)
    {
      OpenLocalXML(Language);
      OnSetFinished(true, default);
    }
    else
    {
      NeedInternet();
    }
  }

  private void DownloadFromInternet(string Language, Action<bool, long> OnSetFinished)
  {
    this.OnSetFinished = OnSetFinished;
    StartCoroutine(OpenWebXML(Language));
  }

  /// Read a XML stored on the web
  private static void SetLanguageOnWeb(string xmlText, string language)
  {
    var xml = new XmlDocument();
    xml.Load(new StringReader(xmlText));

    XML_Strings = new Hashtable();

    var doc_element = xml.DocumentElement[language]; //The element is always the language that we've selected, that's why we must insert the tag in the
                                                     //XML file.

    if (doc_element != null) //if the tag exists
    {
      var elemEnum = doc_element.GetEnumerator();

      while (elemEnum.MoveNext()) //While we find data
        XML_Strings.Add((elemEnum.Current as XmlElement).GetAttribute("name"), (elemEnum.Current as XmlElement).InnerText); //Save it on the hashtable
    }
    else
      Debug.LogError("Language does not exists: " + language);

  }

  ///Read a XML stored on the computer
  private static void SetLocalLanguage(string path, string language)
  {
    var xml = new XmlDocument();
    xml.Load(path);

    XML_Strings = new Hashtable();
    var element = xml.DocumentElement;
    if (element != null)
    {
      var elemEnum = element.GetEnumerator();

      while (elemEnum.MoveNext())
        XML_Strings.Add((elemEnum.Current as XmlElement).GetAttribute("name"), (elemEnum.Current as XmlElement).InnerText);
    }
    else
      Debug.LogError("The specified language does not exist: " + language);
  }


  /// <summary>
  /// This function allow us to open a XML file stored on the internet, we use a WWW class that returns a WWW.text, that will contains the XML text.
  /// </summary>
  private IEnumerator OpenWebXML(string Language)
  {
    //We're opening a file, so we reset all the states of this script
    UnityWebRequest wwwXML = null;

    wwwXML = new UnityWebRequest(languageURL + Language + ".xml");
    yield return wwwXML.SendWebRequest(); //we wait for the reading

    SetLanguageOnWeb(wwwXML.downloadHandler.text, currentLanguage);
    currentLanguage = Language;
    PlayerPrefs.SetInt(Language, 1);
    OnSetFinished(wwwXML.downloadHandler.isDone, wwwXML.responseCode);
  }


  /// <summary>
  /// This function allow us to open a XML file stored on the computer, in the GAMENAME_Data folder created by the unity build.
  /// </summary>
  private static void OpenLocalXML(string Language)
  {
    //We're opening a file, so we reset all the states of this script
    SetLocalLanguage(Path.Combine(Application.persistentDataPath, relativeFolderPath + Language + ".xml"), Language);
    currentLanguage = Language;
  }

}