using UnityEngine;
using System.Collections;
[RequireComponent(typeof(TextMesh))]
public class TextShadow : MonoBehaviour {

    public TextMesh textToCopy;
    private string previousText = "";

	void Start () {
        InvokeRepeating("Shadow",0f,0.5f);
	}
	
	// Update is called once per frame
	void Shadow () {
	
        if(textToCopy.text != previousText)
        {
            previousText = textToCopy.text;
            GetComponent<TextMesh>().text = textToCopy.text;
            GetComponent<TextMesh>().fontSize = textToCopy.fontSize;
            GetComponent<TextMesh>().font = textToCopy.font;
        }
	}
}
