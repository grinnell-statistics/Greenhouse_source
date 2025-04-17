using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

[RequireComponent(typeof(Button))]
public class DisableButtonOnEmptyStrings : MonoBehaviour
{
    private Button button;

    public TMP_InputField nameInput;
    public TMP_InputField farmInput;

    private void Awake()
    {
        button = this.GetComponent<Button>();
        button.interactable = false;
    }

    public void Update()
    {
        if(nameInput.text.CompareTo("") != 0 && farmInput.text.CompareTo("") != 0)
        {
            button.interactable = true;
        }
      }
}
