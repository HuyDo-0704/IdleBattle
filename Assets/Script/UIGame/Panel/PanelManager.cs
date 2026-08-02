using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }

    [SerializeField] private List<PanelReference> panels = new();

    private void Awake()
    {
        Instance = this;
    }

    public void OpenPanel(int panelType)
    {       
        foreach (var panel in panels)
        {
            panel.Root.SetActive(panel.PanelType == (TypePanel)panelType);
        }
    }
}

public enum TypePanel
{
    Lobby,
    WorldMap,
    Inventory,
    Forge
}

[System.Serializable]
public class PanelReference
{
    [field: SerializeField] public GameObject Root { get; private set; }
    [field: SerializeField] public TypePanel PanelType { get; private set; }
}