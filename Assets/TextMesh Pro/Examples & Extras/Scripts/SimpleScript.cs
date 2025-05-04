using UnityEngine;
using TMPro;
using NavKeypad;

public class KeypadUIPopup : MonoBehaviour
{
    public GameObject keypadPanel;
    public TMP_Text displayText;
    public Keypad keypadLogic;

    private string input = "";

    public void OpenPanel()
    {
        input = "";
        UpdateDisplay();
        keypadPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClosePanel()
    {
        keypadPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddDigit(string digit)
    {
        if (input.Length >= 9) return;
        input += digit;
        UpdateDisplay();
    }

    public void ClearInput()
    {
        input = "";
        UpdateDisplay();
    }

    public void Submit()
    {
        keypadLogic.FakeInputFromUI(input);
        ClosePanel();
    }

    private void UpdateDisplay()
    {
        displayText.text = input;
    }
}
