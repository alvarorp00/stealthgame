using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterFallDetector : MonoBehaviour
{
	public Transform respawn; // move character here

    void OnTriggerEnter(Collider collider)
    {
        GameManager.Instance.InvalidMoveThenRollback(this.respawn);
    }
}
