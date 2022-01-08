using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;

namespace Assets.Scripts.Classes
{
    internal class HealthSystem : MonoBehaviour
    {
        [SerializeField] private UIHealthController healthController;

        public short Health { get; private set; }
        public short MaxHealth { get; private set; }

        private void Awake()
        {
            Health = healthController.Health;
            MaxHealth = healthController.MaxHealth;
        }

        public void Damage(ushort damage)
        {
            healthController.Damage(damage);
            Health = healthController.Health;
            MaxHealth = healthController.MaxHealth;
        }

        public void Heal(ushort heal)
        {
            healthController.Heal(heal);
            Health = healthController.Health;
            MaxHealth = healthController.MaxHealth;
        }

    }
}
