using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace Assets.Scripts.Tasks.Controllers
{
    public class ObjectDeliveryTaskController : TaskController
    {
        public Item objectToDeliver;

        private void Awake()
        {
            Task = new ObjectDeliveryTask(Name, Description);
            taskType = TaskType.ObjectDelivery;
            if (objectToDeliver == null)
                throw new NullReferenceException("Object to deliver can't be null");
        }

        protected override void SetUp()
        {
            // pass
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Player.Instance.tag)
            {
                Debug.Log($"Task {Task.Name} collider hit.");
                Debug.Log($"Player has object: {Player.Instance.HasItem(objectToDeliver)}");
                Debug.Log($"Task status: {Task.State}");
            }
            if (other.tag == Player.Instance.tag
                && Player.Instance.HasItem(objectToDeliver)
                   && Task.State == TaskState.Started)
            {
                ForceFinish();
                Player.Instance.RemoveItem(objectToDeliver);
            }
        }

    }
}
