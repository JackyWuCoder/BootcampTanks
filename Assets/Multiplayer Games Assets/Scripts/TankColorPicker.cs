using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankColorPicker : NetworkBehaviour
{
    public TMP_Dropdown colorDropdown;
    public Renderer tankRenderer;

    public NetworkVariable<Color> tankColor = new NetworkVariable<Color>();

    private void Start()
    {
        // Initialize the tank color
        if (IsLocalPlayer)
        {
            ChangeColor(); // Call local method to change color
        }
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
    }
}