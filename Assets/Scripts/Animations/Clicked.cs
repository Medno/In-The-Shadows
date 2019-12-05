using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class Clicked : MonoBehaviour
{
	private Animator animator;
    public UnityEvent   functionToExecute;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator Animate()
    {
        Debug.Log("Animated button has been clicked !");
        animator.SetTrigger("Clicked");
		yield return new WaitForSeconds(0.4f);
        if (functionToExecute.GetPersistentEventCount() > 0)
            functionToExecute.Invoke();
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void StartAnimation()
    {
        StartCoroutine(Animate());
    }
}
