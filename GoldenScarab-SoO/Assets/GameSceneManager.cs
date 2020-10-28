using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameSceneManager current;
    public float fadeInspeed;
    public Image bg;
    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        Fader.current.animator.speed = fadeInspeed;
        Fader.current.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeScene(int index, float speed)
    {
        Fader.current.animator.speed = speed;
        Fader.current.FadeOut();
        yield return new WaitUntil(() => bg.color.a == 1);
        SceneManager.LoadScene(index);
        //break;
    }

    public void LoadScene(int index, float speed)
    {
        StartCoroutine(FadeScene(index, speed));
    }
}
