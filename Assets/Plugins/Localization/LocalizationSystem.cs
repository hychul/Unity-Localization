using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    [XmlRoot(ElementName = "resource")]
    public class LocaleResource
    {
        [XmlElement(ElementName = "string")]
        public List<LocaleString> StringList { get; set; }
    }

    [XmlRoot(ElementName = "string")]
    public class LocaleString
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    public delegate void OnLocalChanged();

    private const string RESOURCE_DIR = "Localization/string_{0}";

    private static Dictionary<string, string> _stringById;
    private static OnLocalChanged onLocalChanged;

    static LocalizationSystem()
    {
        _stringById = new Dictionary<string, string>();
    }

    public static void Load(string locale)
    {
        var textAsset = Resources.Load(string.Format(RESOURCE_DIR, locale)) as TextAsset;

        if (textAsset == null)
        {
            Debug.LogErrorFormat("[LocalizationSystem] There is no resource in : {0}", string.Format("Assets/Resource/{0}", string.Format(RESOURCE_DIR, locale)));
            return;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LocaleResource));
        try
        {
            Dictionary<string, string> stringById = new Dictionary<string, string>();
            LocaleResource localeResource = xmlSerializer.Deserialize(new StringReader(textAsset.text)) as LocaleResource;

            foreach (LocaleString localeString in localeResource.StringList)
            {
                if (stringById.ContainsKey(localeString.Name))
                    continue;

                stringById.Add(localeString.Name, localeString.Text);
            }

            _stringById = stringById;
        }
        catch (Exception ex)
        {
            Debug.LogErrorFormat("[LocalizationSystem] There is syntax error in : {0}", string.Format("Assets/Resource/{0}", string.Format(RESOURCE_DIR, locale)));
        }

        onLocalChanged?.Invoke();
    }

    public static void Subscribe(OnLocalChanged subscriber)
    {
        onLocalChanged += subscriber;
    }

    public static void Unsubscribe(OnLocalChanged subscriber)
    {
        onLocalChanged -= subscriber;
    }

    public static string Find(string id)
    {
        if (!_stringById.ContainsKey(id))
            return id;

        return _stringById[id];
    }
}