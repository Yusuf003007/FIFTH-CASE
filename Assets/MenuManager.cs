using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;

    public GameObject settingsPanel;

    public GameObject soundPanel;
    public GameObject graphicsPanel;
    public GameObject controlsPanel;

    // OPEN SETTINGS
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // BACK TO MAIN MENU
    public void BackToMain()
    {
        settingsPanel.SetActive(false);

        soundPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        controlsPanel.SetActive(false);

        mainMenu.SetActive(true);
    }

    // SOUND
    public void OpenSound()
    {
        settingsPanel.SetActive(false);
        soundPanel.SetActive(true);
    }

    public void BackFromSound()
    {
        soundPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // GRAPHICS
    public void OpenGraphics()
    {
        settingsPanel.SetActive(false);
        graphicsPanel.SetActive(true);
    }

    public void BackFromGraphics()
    {
        graphicsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // CONTROLS
    public void OpenControls()
    {
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void BackFromControls()
    {
        controlsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}