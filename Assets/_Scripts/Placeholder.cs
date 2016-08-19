using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Placeholder : MonoBehaviour {
    public TextMesh objectLabel;
    public Vector3 wantedScale = Vector3.one * 0.009f;

    void Update()
    {
        if(objectLabel.transform.localScale != wantedScale)
            objectLabel.transform.localScale = wantedScale;

        if (objectLabel.text != gameObject.name)
            objectLabel.text = gameObject.name;
    }
}
