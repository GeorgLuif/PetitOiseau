using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]
public class Touchable : MonoBehaviour {

    public bool once = false;
    public UnityEvent onClick;

    public float maxDistance = 4f;

    private bool notUsed = true;

    private float clickCoolDown = 0.1f;
    private float timerCoolDown = -0.1f;

    private float hoverTimer = -0.1f;

    private bool highlighted = false;
    private Renderer[] highlightAbles;
    public bool aktiv = false;

    public bool clickCallsEvents = true;

    void Start()
    {
        highlightAbles = gameObject.GetComponentsInChildren<Renderer>();

        if(highlightAbles == null)
        {
            if (GetComponent<Renderer>() != null)
            {
                highlightAbles = new Renderer[1];
                highlightAbles[0] = GetComponent<Renderer>();
            }
                

        }

        InvokeRepeating("CheckDistance", 2.0f, 0.5f);
    }

    void CheckDistance()
    {
        if(aktiv && Vector3.Distance(transform.position,Character.instance.transform.position) > maxDistance)
        {
            aktiv = false;
        }
        if(!aktiv && Vector3.Distance(transform.position, Character.instance.transform.position) < maxDistance)
        {
            aktiv = true;
        }
    }

    public void Hover()
    {
        if (!notUsed|| !aktiv)
            return;

        hoverTimer = 0.2f;
        Highlight(true);
    }

    public IEnumerator Click()
    {

        if (timerCoolDown > 0)
            yield break;

        if (!notUsed || !aktiv)
            yield break;

        timerCoolDown = clickCoolDown;

        if (once)
            notUsed = false;

        // IF EVENTS - NO COMMENT
        if (clickCallsEvents)
        {
            print(gameObject.name + " calls interesting object library.");
            GameMaster.instance.ClickedOnInterestingObject(gameObject.name);
            
        }

        print(gameObject.name + " calls touchable events.");
        onClick.Invoke();

        if (GetComponent<Readable>() != null)
            GetComponent<Readable>().Read();

        if (GetComponent<Drug>() != null)
            GetComponent<Drug>().Use();

        if (GetComponent<Phone>() != null)
            GetComponent<Phone>().Phonecall();


        yield return new WaitForSeconds(0.1f);
    }


    void Update()
    {
        if (timerCoolDown > 0)
            timerCoolDown -= Time.deltaTime;

        if (hoverTimer > 0)
            hoverTimer -= Time.deltaTime;

        if (hoverTimer < 0)
            Highlight(false);

    }

    void Highlight(bool yesno)
    {
        if (yesno)
        {
            if (!highlighted)
            {
                highlighted = true;
                StopAllCoroutines();
                StartCoroutine(HighlightSmoothly(true));
            }
            
        }

        else
        {
            if (highlighted)
            {
                highlighted = false;
                StopAllCoroutines();
                StartCoroutine(HighlightSmoothly(false));
            }
        }
    }

    IEnumerator HighlightSmoothly(bool yesno)
    {

        //print("Highlighting " + gameObject.name + " = " + yesno);
       float duration = 0.3f;

        if (highlightAbles == null)
            yield break;

        while(duration > 0)
          {
            duration -= Time.deltaTime;

            foreach (Renderer r in highlightAbles)
            {
                foreach (Material m in r.materials)
                {
                    if (m.color == null)
                    {
                        break;
                    }

                    else
                    {
                        if (yesno)
                        {
                            if(m.color != null)
                                m.color += new Color(0.05f, 0.05f, 0.05f, 0f);
                        }


                        if (!yesno)
                        {
                            if (m.color != null)
                                m.color -= new Color(0.05f, 0.05f, 0.05f, 0f);
                        }
                    }
        
                }
            }
            yield return null;
         }

       
    }
}
