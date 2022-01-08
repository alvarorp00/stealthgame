using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
    public abstract class NPCController : MonoBehaviour
    {
        [SerializeField] public AudioClip[] talk_sound_effect;
        private uint talk_sound_effect_idx = 0;

        public Transform dialogParentSet;

        private Animator anim;
        private GameObject currentDialog;
        private Transform[] dialogArray;

        private uint dialogIndex;
        private uint dialogSize;

        // Start is called before the first frame update
        void Start()
        {
            if (dialogParentSet == null)
            {
                Debug.LogError($"NPC {gameObject.name} will not talk.");
                currentDialog = null; // no dialog
            }
            else
            {
                // we get childs excluding first one, which is the parent
                dialogArray = Array.FindAll(dialogParentSet.GetComponentsInChildren<Transform>(),
                        child => child != dialogParentSet.transform);
                dialogIndex = 0;
                dialogSize = (uint)dialogArray.Length;
                currentDialog = (dialogIndex < dialogSize) ? dialogArray[dialogIndex++].gameObject : null;
                anim = GetComponent<Animator>();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (Player.Instance.CompareTag(other.tag))
            {
                if (currentDialog == null)
                    return; // don't talk
                anim.SetBool("talking", true);
                Debug.Log($"Current dialog: {currentDialog.name}");
                GameManager.Instance.ReproduceSoundEffect(talk_sound_effect[talk_sound_effect_idx++]);
                currentDialog.GetComponent<DialogueTrigger>().TriggerDialogue();

                if (talk_sound_effect_idx == 3)
                    talk_sound_effect_idx = 0;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (Player.Instance.CompareTag(other.tag))
            {
                anim.SetBool("talking", false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        // public update, should be called from GameManager or NPCManager...

        public void NextDialog()
        {
            // TODO
            currentDialog = (dialogIndex < dialogSize) ? dialogArray[dialogIndex++].gameObject : null;
        }
    }
}

