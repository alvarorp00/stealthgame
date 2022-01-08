using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal class UIHealthController : MonoBehaviour
    {
        [SerializeField] private short maxHealth;
        [SerializeField] private short initialHealth;
        private short currentHealth;

        private Transform[] healthy_hearts;
        private Transform[] damaged_hearts;

        private void Awake()
        {
            healthy_hearts = new Transform[maxHealth];
            damaged_hearts = new Transform[maxHealth];

            currentHealth = initialHealth;

            short i = 0;
            foreach (Transform ht in transform.Find("Hearts").transform)
                healthy_hearts[i++] = ht;
            i = 0;
            foreach (Transform dt in transform.Find("DamagedHearts").transform)
                damaged_hearts[i++] = dt;

            Repaint();
        }

        public void Damage(ushort damage)
        {
            short i;
            if (currentHealth == 0 || damage == 0)
                return;
            for (i = currentHealth; i > (currentHealth - damage) && i >= 0;)
            {
                healthy_hearts[--i].gameObject.SetActive(false); // i do --i here bcs indexoutofbounds possibility
                damaged_hearts[i].gameObject.SetActive(true);
            }
            currentHealth = i;
            Debug.Log($"Added {damage} damage");
            Repaint();
        }

        public void Heal(ushort heal)
        {
            short i;
            if (currentHealth == maxHealth || heal == 0)
                return;
            for (i = currentHealth; i < maxHealth && i < (currentHealth + heal); i++)
            {
                healthy_hearts[i].gameObject.SetActive(false);
                damaged_hearts[i].gameObject.SetActive(true);
            }
            currentHealth = i;
            Debug.Log($"Added {heal} heal");
            Repaint();
        }

        private void Repaint()
        {
            short i;
            for (i = 0; i < currentHealth; i++)
            {
                healthy_hearts[i].gameObject.SetActive(true);
                damaged_hearts[i].gameObject.SetActive(false);
            }

            for (; i < maxHealth; i++)
            {
                healthy_hearts[i].gameObject.SetActive(false);
                damaged_hearts[i].gameObject.SetActive(true);
            }
        }

        public short Health => currentHealth;
        public short MaxHealth => maxHealth;
    }
}
