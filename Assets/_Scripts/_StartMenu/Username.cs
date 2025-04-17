using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Username : MonoBehaviour
{
    // Main Menu save Player ID
    private TMP_InputField input;
    EventSystem system;

    void Start()
    {
        input = gameObject.GetComponent<TMP_InputField>();
        input.onEndEdit.AddListener(UpdateUser);
        input.text = Global.username;
        system = EventSystem.current;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
        }
    }

    private void UpdateUser(string arg)
    {
        Global.username = arg;
    }

}
