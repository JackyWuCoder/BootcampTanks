using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankColorPicker : NetworkBehaviour
{
    public TMP_Dropdown colorDropdown;
    public Renderer tankRenderer;

    public NetworkVariable<Color> tankColor = new NetworkVariable<Color>();

    public string dropdownTag = "TankColorDropdown"; // tag of dropdown
    public Button changeColorButton;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Find TMP_Dropdown component by tag
        GameObject dropdownObject = GameObject.FindWithTag(dropdownTag);
        if (dropdownObject != null)
        {
            TMP_Dropdown tempColorDropdown = dropdownObject.GetComponent<TMP_Dropdown>();
            if (tempColorDropdown != null)
            {
                colorDropdown = tempColorDropdown;
            }
            else
            {
                Debug.LogError("TMP_Dropdown component not found on object with tag: " + dropdownTag);
            }
            // Find the ColorSubmit button game object
            Transform changeColorButtonTransform = dropdownObject.transform.Find("ColorSubmitButton");
            if (changeColorButtonTransform != null)
            {
                changeColorButton = changeColorButtonTransform.gameObject.GetComponent<Button>();
                changeColorButton.onClick.AddListener(OnChangeColorButtonClicked); // Add listener for client button click
            }
        }
        else
        {
            Debug.LogError("Object with tag: " + dropdownTag + " not found in the scene.");
        }
    }

    private void OnChangeColorButtonClicked()
    {
        // Check if the client clicking the button is the local player
        if (IsLocalPlayer)
        {
            ChangeColor();
        }
        else
        {
            Debug.LogWarning("Only the local player can change the color.");
        }
    }

    private void Start()
    {
        // Initialize the tank color
        if (IsLocalPlayer)
        {
            ChangeColor(); // Call local method to change color
        }

        // Add listener to the button click event
        if (changeColorButton != null)
        {
            changeColorButton.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        // Change color of the tank when the button is clicked
        ChangeColor();
    }

    public void ChangeColor()
    {
        // Change the color of the tank locally
        Color selectedColor = GetSelectedColor();
        tankColor.Value = selectedColor;

        // Synchronize the color change across the network
        RpcChangeColorClientRpc(selectedColor);
    }

    private Color GetSelectedColor()
    {
        // Retrieve the selected color from the dropdown
        string colorHex = colorDropdown.options[colorDropdown.value].text;
        ColorUtility.TryParseHtmlString(colorHex, out Color selectedColor);
        return selectedColor;
    }

    [ClientRpc]
    private void RpcChangeColorClientRpc(Color newColor)
    {
        // Update the tank color on all clients
        tankColor.Value = newColor;

        // Update the tank's visual appearance
        UpdateTankColorVisual(newColor);
    }

    private void UpdateTankColorVisual(Color newColor)
    {
        // Update the material color of the tank's renderer
        tankRenderer.material.color = newColor;
    }
}