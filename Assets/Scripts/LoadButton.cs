using System;
using UnityEngine;

public class LoadButton : MonoBehaviour
{
    [SerializeField]
    private string locale;

    public void OnClick() {
        LocalizationSystem.Load(locale);
    }
}