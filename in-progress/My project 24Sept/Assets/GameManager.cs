using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject dominoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // create dominos by instantiate using prefab of a domino
        // GameObject domino = Instantiate(dominoPrefab, transform.position, Quaternion.identity);
        // Vector3 inFrontofDomino1 = domino.transform.position + domino.transform.forward;

        Vector3 startPosition = transform.position;


        Instantiate(dominoPrefab, inFrontofDomino1, Quaternion.identity);
        for(int i=0; i<1000; i++) {
            Vector3 position = startPosition + transform.forward * i;
            GameObject domino = Instantiate(dominoPrefab, transform.position, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
