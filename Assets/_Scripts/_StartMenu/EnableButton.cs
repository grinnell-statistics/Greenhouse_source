using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnableButton : MonoBehaviour
{
    //Enable buttons in Main Menu Scene only if Player ID and Group ID are entered
    //Bad word filter
    public TMP_InputField groupID;
    public TMP_InputField playerID;


    [SerializeField]
    public TextAsset badWordsFile;

    private string[] badWords;

    void Awake()
    {
        badWords = badWordsFile.text.Split(',');
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().interactable = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (playerID.text != "" && groupID.text != "" && !IsBadWord(playerID.text, groupID.text))
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
    }


    private bool IsBadWord(string player, string group)
    {
        player = player.ToLower();
        group = group.ToLower();

        foreach (string badword in badWords)
        {
            string newbadword = badword.Substring(1);

            if (player.Contains(newbadword))
            {
                return true;
            }
            if (group.Contains(newbadword))
            {
                return true;
            }
        }

        return false;
    }


}
