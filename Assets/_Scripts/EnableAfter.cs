using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class EnableAfter : MonoBehaviour {
    public float afterSeconds = 3.0f;
    public UnityEvent onFinish;
    private bool once = true;
    public void EnableTargets()
    {
        if (once)
        {
            once = false;
            StartCoroutine(EnableTargetsCoroutine());
        }
    }

	IEnumerator EnableTargetsCoroutine () {
        yield return new WaitForSeconds(afterSeconds);
        
        onFinish.Invoke();
	}
	
	
}
