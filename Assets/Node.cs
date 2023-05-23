using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Text textObj;
    public string text;
    public bool usable = true;

    bool isInVolume = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInVolume)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E KEY " + transform.name);

                Transform parent = transform.parent;
                if(parent)
                {
                    Node node = parent.GetComponent<Node>();
                    if (node)
                    {
                        foreach (Transform child in parent)
                        {
                            if (child != transform)
                            {
                                child.gameObject.SetActive(false);
                                child.GetComponent<Node>().usable = false;
                            }
                                
                        }
                    }
                }
                
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            isInVolume = true;

            Debug.Log("Collider is " + other.name);
            if (textObj)
            {
                textObj.gameObject.SetActive(true);
                textObj.text = text;                
            }

            foreach (Transform child in transform)
            {
                if(child.GetComponent<Node>().usable)
                    child.gameObject.SetActive(true);
            }

            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInVolume = false;

            Debug.Log("Collider is " + other.name);
            if (textObj)
                textObj.gameObject.SetActive(false);
        }
    }
}
