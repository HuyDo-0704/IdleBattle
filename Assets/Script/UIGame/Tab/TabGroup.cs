using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private TabButton[] buttons;

    private TabButton current;
     void OnEnable()
    {
        Select(buttons[0]);
    }

    public void Select(TabButton button)
    {
        if (current == button)
            return;

        if (current != null)
            current.Deselect();

        current = button;
        current.Select();
    }
}