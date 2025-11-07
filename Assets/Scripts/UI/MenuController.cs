using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;

    public void ToggleMenu()
    {
        if (menuPanel != null)
            menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
