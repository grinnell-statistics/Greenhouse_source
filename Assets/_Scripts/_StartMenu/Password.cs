using UnityEngine;
using TMPro;

public class Password : MonoBehaviour
{
    // Main Menu save Group ID
    private TMP_InputField input;

    void Start()
    {
        input = gameObject.GetComponent<TMP_InputField>();
        input.onEndEdit.AddListener(UpdatePass);
        input.text = Global.password;
    }

    private void UpdatePass(string arg)
    {
        Global.password = arg;
    }

}
