using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{


    // Start is called before the first frame update
    public override void StartEvents()
    {
        base.StartEvents();
    }

    // Update is called once per frame
    public override void UpdateEvents()
    {
        base.UpdateEvents();
    }

    public void PickUp(){
        Debug.Log("Picked Up");
    }

    public void Drop(){
        Debug.Log("Dropped");
    }   
}
