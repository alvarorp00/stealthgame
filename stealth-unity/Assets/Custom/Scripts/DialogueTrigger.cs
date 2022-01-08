using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
	public class DialogueTrigger : MonoBehaviour
	{
		public Action<DialogueTrigger, DialogueState> OnDialogueStateChange;

		public Dialogue dialogue;

        private void Awake()
        {
			UpdateDialogueState(DialogueState.NotStarted);
		}

        public void TriggerDialogue()
		{
			GameManager.Instance.dialogueManager.StartDialogue(dialogue);
			UpdateDialogueState(DialogueState.Finished);
		}
		public void UpdateDialogueState(DialogueState newState)
		{
			OnDialogueStateChange?.Invoke(this, newState);
		}
	}

	public enum DialogueState { NotStarted, Finished }; // we just skip the Started part as it's unusable
}