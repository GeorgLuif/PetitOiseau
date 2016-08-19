using UnityEngine;
using System.Collections;

public class CharacterInteractions : MonoBehaviour {

    public bool debug = false;
    public bool cursorEnabled = true;
    public bool aktiv = true;

    void Start()
    {
        if (Application.isEditor)
        {
            Cursor.visible = cursorEnabled;
        }
        else
        {
            Cursor.visible = false;
        } 


    }
	void FixedUpdate () {

        if (!aktiv)
            return;

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            if (debug)
                print(objectHit.gameObject.name);

            if (objectHit.GetComponent<Touchable>() != null && objectHit.GetComponent<Touchable>().enabled)
            {
                objectHit.GetComponent<Touchable>().Hover();
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(objectHit.GetComponent<Touchable>().Click());
                }
            }
                    



        }
    }

    // SINGLETON
    public static CharacterInteractions instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }
}
