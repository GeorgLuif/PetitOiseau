using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject prefab;
    public int amountX = 2;
    public int amountY = 2;
    public float offsetX = 2f;
    public float offsetY = 2f;

	void Start () {

        for (int i = 0; i < amountX; i++)
        {
            

            for (int e = 0; e < amountY; e++)
            {
                GameObject clone = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
                clone.transform.position += new Vector3(offsetX * i,offsetY*e, 0);
            }

        }

	 
	}

}
