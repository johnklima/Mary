using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Text textObj;                //UI object to display text
    public string text;                 //the actual text to display
    public bool usable = true;          //has this one been selected, OR has it been rejected

    bool isInVolume = false;            //update flag to know player has entered volume.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //has the player entered the volume, a one shot trigger
        if(isInVolume)
        {
            if (Input.GetKeyDown(KeyCode.E))    //press E?
            {
                Debug.Log("E KEY " + transform.name);

                Transform parent = transform.parent;    //get the parent of this node that was listening for the E key
                
                //assuming it has a parent (the root node does not it is on the scene root)
                if(parent)
                {
                    Node node = parent.GetComponent<Node>();  //if I am a node, and I have parent, it must also be a node!
                    if (node) //but doouble check to make sure
                    {
                        //through the parent get my siblings
                        foreach (Transform child in parent)
                        {
                            if (child != transform) //if this child is not actually me
                            {
                                //disbale the geometry
                                child.gameObject.SetActive(false);
                                //and flag it as rejected.
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

    public void TellStory()
    {
        //starting from the root, traverse all enabled nodes in the hierarchy
        if(transform.gameObject.activeSelf)
        {
            //print story to screen...
            Debug.Log(text);

            //for each of children
            foreach(Transform child in transform)
            {
                //if it is active
                if (child.gameObject.activeSelf)
                {
                    //get its node
                    Node cnode = child.GetComponent<Node>();
                    
                    //and tell its story (will tell the stories of its children, etc... 
                    cnode.TellStory();
                }
            }
        }
            

    }
}
