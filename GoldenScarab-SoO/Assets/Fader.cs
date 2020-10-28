using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader current;
    public Animator animator;
    public Image bg;
    public Canvas canvas;

    private void Awake()
    {
        current = this;
    }

    public void FadeIn()
    {
        StartCoroutine(StartFadeIn());
    }

    IEnumerator StartFadeIn()
    {
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            string param = animator.parameters[i].name;
            animator.SetBool(param, "fadeIn".Equals(param));
        }
        yield return new WaitUntil(() => bg.color.a == 0);
        canvas.sortingOrder = -1;
    }

    public void FadeOut()
    {
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeOut()
    {
        canvas.sortingOrder = 1;
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            string param = animator.parameters[i].name;
            animator.SetBool(param, "fadeOut".Equals(param));
        }
        yield return new WaitUntil(() => bg.color.a == 1);
    }
}
