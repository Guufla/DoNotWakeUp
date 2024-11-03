using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : Interactable
{

    bool pickedUp = false;

    Transform playerPickupPoint;

    // Start is called before the first frame update
    public override void StartEvents()
    {
        base.StartEvents();

        playerPickupPoint = GameManager.instance.player.transform.GetChild(1).transform;
    }

    // Update is called once per frame
    public override void UpdateEvents()
    {
        
        base.UpdateEvents();
        if(pickedUp == true){
            gameObject.transform.position = playerPickupPoint.position;
            gameObject.transform.rotation = playerPickupPoint.rotation;
        }
    }

    public void PickUp(){

        Debug.Log("Picked Up");
        pickedUp = true;
    }

    public void Drop(){
        Debug.Log("Dropped");

        pickedUp = false;
    }   
}
