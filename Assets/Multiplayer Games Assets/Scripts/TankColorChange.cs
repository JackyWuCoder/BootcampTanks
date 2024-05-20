using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TankColorChange : NetworkBehaviour
{
    private GameObject colorDropDownGO;
    private TMP_Dropdown colorDropdown;

    // Network variable to synchronize color
    // [SyncVar(hook = nameof(OnColorChanged))]
    private Color tankColor = Color.white;

    // Reference to the tank renderer
    private Renderer tankRenderer;

    private void Start()
    {
        // Find the child GameObject by name among the children of the Canvas
        Transform canvasTransform = GameObject.Find("Canvas").transform; // Replace "Canvas" with the name of your Canvas GameObject
        if (canvasTransform != null)
        {
            colorDropDownGO = canvasTransform.Find("Color Dropdown").gameObject; // Replace "ChildObjectName" with the name of the child GameObject you want to find
            if (colorDropDownGO != null)
            {
                Debug.Log("Found child GameObject: " + colorDropDownGO.name);
            }
            else
            {
                Debug.LogError("Child GameObject not found!");
            }
        }
        else
        {
            Debug.LogError("Canvas GameObject not found!");
        }
        tankRenderer = GetComponent<Renderer>();
    }

    public void OnColorChanged(Color newColor)
    {
        tankColor = newColor;
        tankRenderer.material.color = newColor;
    }

    public void ChangeColor()
    {
        // Get the selected color from the dropdown
        Color newColor = GetSelectedColor();

        // Change the color locally and sync across the network
        tankColor = newColor;
        tankRenderer.material.color = newColor;

        // If using UNET, CmdChangeColor method should be called to sync color across network
    }

    private Color GetSelectedColor()
    {
        // Get the selected color from the dropdown
        string colorName = colorDropdown.options[colorDropdown.value].text;
        return GetColorFromString(colorName);
    }

    private Color GetColorFromString(string colorName)
    {
        // Map color names to Color values
        switch (colorName)
        {
            case "Red":
                return Color.red;
            case "Green":
                return Color.green;
            case "Blue":
                return Color.blue;
            default:
                return Color.white;
        }
    }
}
