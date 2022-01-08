using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Managers;


namespace Assets.Scripts
{
    public class DialogueManager : MonoBehaviour
    {

        public TMP_Text nameText;

        public TMP_Text dialogueText;

        public Animator animator;

        private Queue<string> sentences;

        private void Awake()
        {
            sentences = new Queue<string>();
        }

        public bool Active()
        {
            return sentences.ToArray().Length > 0;
        }

        public void StartDialogue(Dialogue dialogue)
        {

            sentences.Clear();
            animator.SetBool("IsOpen", true);

            GameManager.Instance.UpdateGameState(GameState.OnConversation);

            nameText.text = dialogue.name;

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            GameManager.Instance.UpdateGameState(GameState.OnConversation);

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {

            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            StopAllCoroutines();

            string sentence = sentences.Dequeue();

            StartCoroutine(TypeSentence(sentence));

        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            animator.SetBool("IsOpen", false);
            GameManager.Instance.UpdateGameState(GameState.OnPlay);
        }

    }
}