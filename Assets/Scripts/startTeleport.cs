using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTeleport : MonoBehaviour
{
    public GameObject startPosition;
    public GameObject playerController;

    public void goToStart()
    {
        //transform.position = L2.transform.position;
        playerController.GetComponent<CAVE2WandNavigator>().SetPosition(startPosition.transform.position);
        playerController.GetComponent<CAVE2WandNavigator>().SetRotation(startPosition.transform.rotation);
    }

}
