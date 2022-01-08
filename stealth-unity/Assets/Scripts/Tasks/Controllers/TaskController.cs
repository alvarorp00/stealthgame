using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tasks.Controllers
{
    public abstract class TaskController : MonoBehaviour
    {
        // activator & finisher pos are optional, for map purposes.
        public Transform activatorPos; // sets task to started
        public Transform finisherPos; // sets task to finished
        public string Name;
        public string Description;

        public NPCController NPCController;
        public bool next_dialog; // move to next dialog after being completed

        public bool autoStart; // start task just after required tasks where finished

        protected TaskType taskType;

        public Task Task { get; protected set; }

        public List<TaskController> tasksRequired;

        private void Start()
        {
            // detect when task is finished no matter where
            Task.OnTaskStateChange += (task, oldst, newst) =>
            {
                Debug.Log("Detected self task finish");
                if (newst == TaskState.Finished && next_dialog)
                    NPCController.NextDialog();
            };

            Debug.Log($"Task: {Task.Name} @@ list: {tasksRequired.Count}");
            foreach (TaskController task in tasksRequired)
            {
                Task.taskRequired.Add(task.Task); // set all tasks
                task.Task.OnTaskStateChange += TaskStateChangeController;
            }

            if (tasksRequired.Count == 0 && autoStart)
                TryStart(); // will start

            SetUp();
        }

        protected abstract void SetUp();

        private void TaskStateChangeController(Task task, TaskState oldState, TaskState newState)
        {
            if (newState == TaskState.Finished/* && Task.taskRequired.Contains(task)*/)
            {
                Debug.Log($"Completed required task {task.Name} for {Task.Name}. State: {Task.State}. Autostart: {autoStart}");
                Task.taskRequired.Remove(task);
                TryStart(autoStart && Task.taskRequired.Count == 0);
                task.OnTaskStateChange -= TaskStateChangeController;
            }
        }

        public bool IsAvailableToStart()
        {
            // no pending previous tasks and it has not started yet
            Debug.Log($"Task name: {Task.Name} @ Tasks pending: {Task.taskRequired.Count} @ Task state: {Task.State}");
            return Task.taskRequired.Count == 0 && Task.State == TaskState.NotStarted;
        }

        // this method is called from child classes, where
        // activation mode is defined specifically for the type
        // of the task
        protected void TryStart(bool force = false)
        {
            if (IsAvailableToStart() || force)
            {
                Task.UpdateTaskState(TaskState.Started);
                Debug.Log($"Task {Task.Name}: {Task.Description} has started");
            }
        }

        protected void ForceFinish()
        {
            if (Task.State != TaskState.Finished)
            {
                Task.UpdateTaskState(TaskState.Finished);
                Debug.Log($"Task {Task.Name} state forced to: {Task.State}");
            }
        }
    }
}
