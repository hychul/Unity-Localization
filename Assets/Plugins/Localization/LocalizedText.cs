using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string id;

    private Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        LocalizationSystem.Subscribe(OnLocalChanged);
        text.text = LocalizationSystem.Find(id);
    }

    void OnLocalChanged()
    {
        text.text = LocalizationSystem.Find(id);
    }

    void OnDisable()
    {
        LocalizationSystem.Unsubscribe(OnLocalChanged);
    }
}
