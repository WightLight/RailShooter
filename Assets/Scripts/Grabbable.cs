using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] public Vector3 holdPosition;
    [SerializeField] public Vector3 holdRotation;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public bool CanGrab()
    {
        return GetComponent<Rigidbody>() != null;
    }
}
