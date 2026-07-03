using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TabButton : MonoBehaviour
{
   [SerializeField] private Image icon;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;
    void Start()
    {
        Deselect();
    }

    public virtual  void Select()
    {
        icon.color = selectedColor;
    }

    public virtual void Deselect()
    {
        icon.color = normalColor;
    }
}