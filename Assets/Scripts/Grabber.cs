using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grabber : MonoBehaviour
{
    public GameObject grabbedObject;
    float grabbedObjectSize;
	public Text promptText;
    //public Gun weapon;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		//If there's a grabbable item, prompt grabbing via prompt text
		if (GetMouseHoverObject(5) != null)
		{
			promptText.text = "[E]: Take " + GetMouseHoverObject(5).name;
		}
		else
		{
			promptText.text = null;
		}

		if(Input.GetKeyDown(KeyCode.E))
        {
            if(grabbedObject == null)
            {
                TryGrabObject(GetMouseHoverObject(5));
            }
            else
            {
                DropObject();
            }
        }

        if(grabbedObject != null)
        {
            grabbedObject.transform.SetParent(transform);
            Vector3 newPosition = grabbedObject.GetComponent<Grabbable>().holdPosition;
            Quaternion newRotation = Quaternion.Euler(grabbedObject.GetComponent<Grabbable>().holdRotation);
            grabbedObject.transform.localPosition = newPosition;
            grabbedObject.transform.localRotation = newRotation;
        }

        // SHOOT. THE GUN.
        if (Input.GetButtonDown ("Fire1") && (grabbedObject.GetComponent<Gun>() == true))
        {
            ShootWeapon();
        }
	}

    // Raycast to find and grab an object within grabby range
    Grabbable GetMouseHoverObject(float range)
    {
        // Set our position, target position, and a raycast hit
        Vector3 position = Camera.main.transform.position;
        Vector3 target = position + Camera.main.transform.forward * range;
        RaycastHit raycastHit;

        // Return what we hit in range if anything
		// also prompt text to pick it up
		if (Physics.Linecast(position, target, out raycastHit))
		{
			Grabbable grabCandidate = raycastHit.collider.gameObject.GetComponentInParent<Grabbable>();
			return grabCandidate;
		}

        return null;
    }

    void TryGrabObject(Grabbable grabbable)
    {
        // If there's no target or an ungrabbable target, don't grab
        if(grabbable == null || !grabbable.CanGrab())
        {
            return;
        }

        // Otherwise set this as the grabbed object and set its size
        grabbedObject = grabbable.gameObject;
        //grabbedObjectSize = grabbedObject.GetComponent<Renderer>().bounds.size.magnitude;﻿
    }

    void DropObject()
    {
        if(grabbedObject == null)
        {
            return;
        }

        if(grabbedObject.GetComponent<Rigidbody>() != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        grabbedObject = null;
    }

    void ShootWeapon()
    {
        grabbedObject.GetComponent<Gun>().Shoot();
    }
}
