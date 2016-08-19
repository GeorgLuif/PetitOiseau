using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[ExecuteInEditMode]
public class AnimatedObject : MonoBehaviour {

    public float animSpeed = 1f;

    public Vector3 pos1;
    public Vector3 pos2;
    public UnityEvent onFinish;
    private bool once = true;
    private bool b = true;
    
    public void Animate()
    {
        if (once)
        {
            once = false;
            StartCoroutine(Animation());

        }

    }

    IEnumerator Animation()
    {
        int safety = 400;

        while(transform.position != pos2 || safety > 0)
        {
            safety--;
            transform.position = Vector3.MoveTowards(transform.position, pos2, Time.deltaTime*animSpeed*0.1f);
            yield return 0;
        }

        onFinish.Invoke();
        yield break;
    }

    public void RecordPos1()
    {
        pos1 = transform.position;
    }

    public void RecordPos2()
    {
        pos2 = transform.position;
    }

    public void SwitchBetween()
    {
        b = !b;

        if (b)
            transform.position = pos1;

        if (!b)
            transform.position = pos2;
    }



}
