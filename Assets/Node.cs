using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Text textObj;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            Debug.Log("Collider is " + other.name);
            if (textObj)
            {
                textObj.gameObject.SetActive(true);
                textObj.text = text;
            }
                
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collider is " + other.name);
            if (textObj)
                textObj.gameObject.SetActive(false);
        }
    }
}
