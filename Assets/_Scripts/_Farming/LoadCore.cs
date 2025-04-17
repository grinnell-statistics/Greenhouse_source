using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Global.loadCore)
        {
            StartCoroutine(CheckCore());
        }
        else
        {
            SceneManager.LoadScene("Core", LoadSceneMode.Additive);
        }
    }

    private IEnumerator CheckCore()
    {
        yield return new WaitForSeconds(1);
        if (!SceneManager.GetSceneByName("Core").isLoaded)
        {
            SceneManager.LoadScene("Core", LoadSceneMode.Additive);
        }
    }

}
