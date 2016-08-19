using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public GameObject menuObj;
	
    void Start()
    {
        menuObj.SetActive(false);
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            menuObj.SetActive(!menuObj.activeInHierarchy);
	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
