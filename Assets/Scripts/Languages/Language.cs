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

    static Hashtable XML_Strings;
    private static readonly string relativeFolderPath = "Lang/";
    private static readonly string languageURL = "http://yourdomainname.dx.am/MLS_Languages/";
    private static string currentLanguage = "Arabic";
    private readonly string defaultLanguage = "English";
    public static bool arabic = false;
    private static bool defaultLang = false;


    private Action<bool,
    long> OnSetFinished;

    public static Language language; 

    public bool Arabic
    {
        get => arabic;
        set => arabic = value;
    }
    public static bool DefaultLang {
        get => defaultLang;
        set => defaultLang = value; 
    }

    public void Awake()
    {
        if (language == null)
        {
            language = this;
        }

        var currlang = Cache.GetLanguage();
        if (currlang.Equals("Arabic"))
        {
            arabic = true;
            OpenLocalXML(currlang);
        }

        if (currlang == string.Empty) 
            currlang = defaultLanguage;
            defaultLang = true;
    }

    public string GetString(string _name)
    {
        if (arabic == false)
        {
            return _name;
        }
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
            OnSetFinished(true,
          default);
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

    private static void SetLanguageOnWeb(string xmlText, string language)
    {
        var xml = new XmlDocument();
        xml.Load(new StringReader(xmlText));

        XML_Strings = new Hashtable();

        var doc_element = xml.DocumentElement[language]; 

        if (doc_element != null) 
        {
            if (arabic == true)
            {
                XML_Strings = CreateFixedHash(xml);

            }
            else
            {
                var elemEnum = doc_element.GetEnumerator();
                while (elemEnum.MoveNext()) 
                XML_Strings.Add((elemEnum.Current as XmlElement).GetAttribute("name"), (elemEnum.Current as XmlElement).InnerText);
            }
        }
        else Debug.LogError("Language does not exists: " + language);

    }

    private static void SetLocalLanguage(string path, string language)
    {
        var xml = new XmlDocument();
        xml.Load(path);
        var element = xml.DocumentElement;
        XML_Strings = new Hashtable();
        if (element != null)
        {
            if (arabic == true)
            {
                XML_Strings = CreateFixedHash(xml);
            }
            else
            {
                var elemEnum = element.GetEnumerator();
                while (elemEnum.MoveNext())
                    XML_Strings.Add((elemEnum.Current as XmlElement).GetAttribute("name"), (elemEnum.Current as XmlElement).InnerText);
            }
        }
        else Debug.LogError("The specified language does not exist: " + language);
    }
    private static Hashtable CreateFixedHash(XmlDocument xml)
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

    private IEnumerator OpenWebXML(string Language)
    {
        UnityWebRequest wwwXML = null;

        wwwXML = new UnityWebRequest(languageURL + Language + ".xml");
        yield
        return wwwXML.SendWebRequest();

        SetLanguageOnWeb(wwwXML.downloadHandler.text, currentLanguage);
        currentLanguage = Language;
        PlayerPrefs.SetInt(Language, 1);
        OnSetFinished(wwwXML.downloadHandler.isDone, wwwXML.responseCode);
    }

    private static void OpenLocalXML(string Language)
    {
        SetLocalLanguage(Path.Combine(Application.persistentDataPath, relativeFolderPath + Language + ".xml"), Language);
        currentLanguage = Language;
    }

}