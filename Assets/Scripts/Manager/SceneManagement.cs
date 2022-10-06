using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public Animator transitionAnim;
    public int sceneNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChanger()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneNum);
        transitionAnim.SetTrigger("Start");
    }
}
