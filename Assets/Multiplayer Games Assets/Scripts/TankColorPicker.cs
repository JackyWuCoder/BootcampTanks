using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankColorPicker : NetworkBehaviour
{
    public TMP_Dropdown colorDropdown;
    public Renderer tankRenderer;
    public Button changeColorButton;

    // public NetworkVariable<Color> tankColor = new NetworkVariable<Color>();

    public string dropdownTag = "TankColorDropdown"; // tag of dropdown

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
        if (IsOwner && IsOwner)
        {
            ChangeColor();
        }
        else
        {
            Debug.LogWarning("Only the local player can change the color.");
        }
    }

    private void OnButtonClicked()
    {
        // Change color of the tank when the button is clicked locally
        ChangeColor();
    }

    public void ChangeColor()
    {
        // Change the color of the tank locally
        Color selectedColor = GetSelectedColor();

        ChangeColorServerRpc(selectedColor);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeColorServerRpc(Color newColor)
    {
        // Update the material color of the tank's renderer
        tankRenderer.material.color = newColor;

        // Broadcast the color change to all clients
        ChangeColorClientRpc(newColor);
    }

    [ClientRpc]
    private void ChangeColorClientRpc(Color newColor)
    {
        // Update the material color of the tank's renderer
        tankRenderer.material.color = newColor;
    }

    private Color GetSelectedColor()
    {
        // Retrieve the selected color from the dropdown
        string colorHex = colorDropdown.options[colorDropdown.value].text;
        ColorUtility.TryParseHtmlString(colorHex, out Color selectedColor);
        return selectedColor;
    }
}