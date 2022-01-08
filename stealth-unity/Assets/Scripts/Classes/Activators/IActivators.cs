using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Classes.Items.Activators
{
    public interface IActivators
    {
        GameObject Receiver { get; set; } // receiver of the action

        public void Activate();
    }

    public enum ActionPerformedType
    {
        None,
        ToggleObject, // for now, just this
        MoveObject
    }

    public class ActionPerformed
    {
        public static void ToggleObject(GameObject receiver) => receiver?.SetActive(receiver.activeSelf);

        public static void MoveObject(GameObject receiver, Transform dest) => receiver.transform.SetPositionAndRotation(new Vector3(dest.position.x, dest.position.y, dest.position.z), new Quaternion());
    }
}
