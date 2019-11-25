using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateClicked : MonoBehaviour
{
	private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator Animate()
    {
        animator.SetTrigger("Clicked");
		yield return new WaitForSeconds(0.4f);
    }
}
