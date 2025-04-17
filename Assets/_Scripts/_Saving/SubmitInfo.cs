using UnityEngine;
using TMPro;


public class SubmitInfo : MonoBehaviour
{
    public TMP_InputField userInput;
    public TMP_InputField passInput;

    public void NewGame()
    {
        Global.username = userInput.text;
        Global.password = passInput.text;
    }
}
