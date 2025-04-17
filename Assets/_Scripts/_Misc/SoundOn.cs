using UnityEngine;

public class SoundOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Global.muted)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
