using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public string enemy_name;
        public float damage_rate; // seconds to wait on each attack
        public ushort damage_done; // damage done on each attack

        private void Awake()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            InvokeRepeating("DoDamage", damage_rate, damage_rate);
        }

        private void OnTriggerStay(Collider other)
        {
            // pass
        }

        private void OnTriggerExit(Collider other)
        {
            CancelInvoke();
        }

        protected void DoDamage()
        {
            Player.Instance.Damage(damage_done);
            Debug.Log($"Damage {damage_done} done by {enemy_name}");
        }

    }
}
