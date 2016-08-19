using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class Door : MonoBehaviour {


    public bool openOnStart = false;
    public float startDelay = 3f;
    public float speed = 22f;
    public float duration = 4f;
    public Axis axis = Axis.X;
    private bool inverse = false;
    private float timer = 0f;
    public bool open = false;
    public UnityEvent onUse;
    private bool inUse = false;
    private IEnumerator Start()
    {
        timer = duration;
        if (openOnStart)
        {
            yield return new WaitForSeconds(startDelay);
            StartCoroutine(Open(true));
        }
    }


    public void OpenDoor()
    {
        if (inUse)
            return;

        if (!open)
        {
            StartCoroutine(Open(true));
        }

        else
        {
            StartCoroutine(Open(false));
        }
        
    }

    private IEnumerator Open(bool yesno)
    {
        inUse = true;

        Vector3 axisVec = Vector3.zero; 

        switch (axis)
        {
            case Axis.X:
                axisVec = Vector3.right;
                break;

            case Axis.Y:
                axisVec = Vector3.up;
                break;
            case Axis.Z:
                axisVec = Vector3.forward;
                break;
        }

        if(!yesno)
        axisVec *= -1;

        while (timer > 0)
        {
            transform.Rotate(axisVec * Time.deltaTime * speed);
            timer -= Time.deltaTime;
            yield return null;
        }

        if (timer < 0)
        {
            onUse.Invoke();
        }

        open = yesno;
        timer = duration;
        inUse = false;
    }
}

public enum Axis { X, Y, Z }

[System.Serializable]
public class MessageReceiver
{
    public GameObject obj;
    public string message;
}

