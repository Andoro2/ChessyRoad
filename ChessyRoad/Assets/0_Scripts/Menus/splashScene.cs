using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splashScene : MonoBehaviour
{
    public float intro_length = 7f;
    void Start()
    {
        StartCoroutine(Intro_Scene());
    }

    IEnumerator Intro_Scene()
    {
        yield return new WaitForSeconds(intro_length);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
