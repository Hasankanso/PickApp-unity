using ArabicSupport;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

public class Language : MonoBehaviour
{

  private Hashtable XML_Strings;
  private static readonly string relativeFolderPath = "Lang/";
  private static string directory;
  private static readonly string languageURL = "https://backendlessappcontent.com/5FB0EA72-A363-4451-FFA5-A56F031D6600/C8502745-CB10-4F56-9FD5-3EFCE59F1926/files/languages/";

  public bool arabic = false;
  public bool english = false;

  public static Language defaultInstance;

  public bool Arabic
  {
    get => arabic;
  }

  public void Awake()
  {
    Destroy(defaultInstance);
    defaultInstance = this;
    Program.language = this;

    directory = Path.Combine(Application.persistentDataPath, relativeFolderPath);

    var currlang = Cache.GetLanguage();

    if (!LanguageExists(currlang))
    {
      currlang = "English";
      Cache.SetLanguage(currlang);
    }

    if (currlang.Equals("Arabic"))
    {
      arabic = true;
    }

    if (!currlang.Equals("English"))
    {
      LoadXml(currlang);
    }
    else
    {
      english = true;
    }

  }

  public string GetString(string _name)
  {

    if (!XML_Strings.ContainsKey(_name))
    {
      Debug.LogError("This string is not present in the XML file where you're reading: " + _name);

      return "";
    }

    return (string)XML_Strings[_name];
  }

  public static bool LanguageExists(string language)
  {
    if (language.Equals("English")) return true;

    var path = directory + language + ".xml";
    try
    {
      var xml = new XmlDocument();
      xml.Load(path);
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }

  public static IEnumerator DownloadXml(string language, Action<bool, string> OnDownloadComplete)
  {
    UnityWebRequest wwwXML = new UnityWebRequest(languageURL + language + ".xml");
    wwwXML.downloadHandler = new DownloadHandlerBuffer();
    print("start download");
    yield return wwwXML.SendWebRequest();
    print(languageURL + language + ".xml");
    if (wwwXML.isNetworkError || wwwXML.isHttpError)
    {
      OnDownloadComplete(false, language);
    }
    else
    {
      var xml = new XmlDocument();
      xml.Load(new StringReader(wwwXML.downloadHandler.text));
      try
      {
        if (!Directory.Exists(directory))
        {
          Directory.CreateDirectory(directory);
        }

        xml.Save(directory + language + ".xml");
        OnDownloadComplete(true, language);
      }
      catch (IOException)
      {
        OnDownloadComplete(false, language);
      }
    }
  }

  private static void LoadXml(string lang)
  {
    var path = directory + lang + ".xml";
    var xml = new XmlDocument();

    xml.Load(path);
    var element = xml.DocumentElement;
    defaultInstance.XML_Strings = new Hashtable();
    if (element != null)
    {
      if (defaultInstance.arabic == true)
      {
        defaultInstance.XML_Strings = FixArabicTable(xml);
      }
      else
      {
        var elemEnum = element.GetEnumerator();
        while (elemEnum.MoveNext())
          defaultInstance.XML_Strings.Add((elemEnum.Current as XmlElement).GetAttribute("name"), (elemEnum.Current as XmlElement).InnerText);
      }
      Cache.SetLanguage(lang);
      PlayerPrefs.SetInt(lang, 1);
    }
    else Debug.LogError("The specified language does not exist: " + lang);
  }
  private static Hashtable FixArabicTable(XmlDocument xml)
  {
    var fixedHash = new Hashtable();
    var element = xml.DocumentElement;
    if (element != null)
    {
      var elemEnum = element.GetEnumerator();

      while (elemEnum.MoveNext())
        fixedHash.Add((elemEnum.Current as XmlElement).GetAttribute("name"), ArabicFixer.Fix((elemEnum.Current as XmlElement).InnerText, true, true));

    }
    return fixedHash;
  }

}