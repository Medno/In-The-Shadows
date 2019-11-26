using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimateClicked : MonoBehaviour
{
	private Animator animator;
    public UnityEvent   functionToExecute;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator Animate()
    {
        animator.SetTrigger("Clicked");
		yield return new WaitForSeconds(0.4f);
        if (functionToExecute.GetPersistentEventCount() > 0)
            functionToExecute.Invoke();
    }
}
