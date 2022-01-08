using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tasks.Controllers
{
    public class ObjectTakeTaskController : TaskController
    {
        public Item objectToTake;

        private void Awake()
        {
            Task = new ObjectTakeTask(Name, Description);
            taskType = TaskType.ObjectTake;
            if (objectToTake == null)
                throw new Exception("Object delivery requires an object!");
        }

        protected override void SetUp()
        {
            Player.Instance.OnItemTaken += (item) =>
            {
                if (Task.State == TaskState.Started && item == objectToTake.GetComponent<Item>())
                {
                    Debug.Log($"Item taken. Task {Task.Name} finished.");
                    //Task.UpdateTaskState(TaskState.Finished);
                    ForceFinish();
                }
            };

            Task.OnTaskStateChange += (task, prev, nxt) =>
            {
                if (nxt >= TaskState.Started && Player.Instance.HasItem(objectToTake))
                    ForceFinish();
            };
        }

        private void OnTriggerEnter(Collider other) => Activate(other);
        //private void OnTriggerStay(Collider other) => Activate(other);
        //private void OnTriggerExit(Collider other) => Activate(other);

        private void Activate(Collider other)
        {
            if (other.tag == Player.Instance.tag && Task.State == TaskState.NotStarted)
            {
                TryStart();
            }
        }
    }
}
