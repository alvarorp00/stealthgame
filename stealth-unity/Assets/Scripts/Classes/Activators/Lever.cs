using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes.Items.Activators
{
    internal class Lever : IActivators
    {
        public Transform LeverStick { get; set; } // will be rotated 90 degrees on
        public GameObject Receiver { get; set; }

        public enum RotateOver { X, Z };
        public RotateOver rotateOver;

        public ActionPerformed actionPerformed;

        public void Activate()
        {
            // TODO
        }
    }
}
