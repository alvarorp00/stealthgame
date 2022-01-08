using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] public AudioClip c_sound;

        public enum TeleportMove { Scoped, Scene };

        public string targetName;
        public GameManager.RegionSelector region;
        public TeleportMove typeOfTeleport;

        private Transform animator;

        private void Awake()
        {
            animator = transform.Find("Animator");
            //DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (this.animator != null)
                this.animator.Rotate(0f, 3.5f, 0f);
        }

        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == Player.Instance.tag && targetName != null)
            {
                Transform target = GameObject.Find(targetName).transform;

                if (typeOfTeleport == TeleportMove.Scoped)
                    GameManager.Instance.TeleportPlayer(target, c_sound);
                else
                    GameManager.Instance.SwitchScene(region, target, c_sound);
            }
        }
    }
}
