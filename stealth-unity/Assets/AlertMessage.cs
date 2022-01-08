using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AlertMessage : MonoBehaviour
    {
        public bool doOnce;

        private bool run;

        public DialogueState DialogueState { get; private set; }

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
                else if (!doOnce)
                {
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                    run = true;
                }
            }
        }
    }

    

}
