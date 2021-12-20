using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertMessage : MonoBehaviour
{
    public bool doOnce;

    private bool run;

    private void Awake()
    {
        run = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Player.Instance.tag)
        {
            if (doOnce && !run)
            {
                GetComponent<DialogueTrigger>().TriggerDialogue();
                run = true;
            }
        }
    }
}
