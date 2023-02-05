using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    static GameObject instance = null;
    [SerializeField] Animator balckImage;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
        } else
        {
            Destroy(gameObject);
        }    
    }

    private void Start()
    {      
        DontDestroyOnLoad(gameObject);
    }

    public void LoadBadEnd()
    {
        StartCoroutine(LoadBadEndAfter());
    }

    public void LoadGoodEnd()
    {
        StartCoroutine(LoadGoodEndAfter());
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadLevelAfter(index));
    }

    IEnumerator LoadLevelAfter(int index)
    {
        balckImage.SetTrigger("Fade");
        yield return new WaitForSeconds(2.25f);
        SceneManager.LoadScene(index);
    }

    IEnumerator LoadBadEndAfter()
    {
        LoadLevel(2);
        yield return new WaitForSeconds(10f);
        LoadLevel(0);
    }

    IEnumerator LoadGoodEndAfter()
    {
        LoadLevel(3);
        yield return new WaitForSeconds(10f);
        LoadLevel(0);
    }
}
