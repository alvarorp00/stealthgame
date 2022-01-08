using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tasks;
using Assets.Scripts.Tasks.Controllers;

namespace Assets.Scripts.Managers
{
    public class TaskManager : MonoBehaviour
    {
        public static TaskManager Instance;

        public Transform TaskContainer;

        private List<TaskController> taskControllers;

        // duplicates here so'll be removing resources associated with previous tasks
        public HashSet<Task> PendingTasks { get; private set; }
        public HashSet<Task> AvailableTasks { get; private set; }
        public HashSet<Task> CompletedTasks { get; private set; }

        private void Awake() // Singleton pattern
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        void Start()
        {
            if (TaskContainer == null)
            {
                Debug.LogWarning("No tasks will be executed. No task container was supplied.");
            }
            else
            {
                taskControllers = new List<TaskController>();

                PendingTasks = new HashSet<Task>();
                AvailableTasks = new HashSet<Task>();
                CompletedTasks = new HashSet<Task>();

                foreach (Transform taskControllerObj in TaskContainer)
                {
                    if (!taskControllerObj.gameObject.activeSelf)
                        continue; // skip inactive tasks
                    TaskController tc = taskControllerObj.GetComponent<TaskController>();
                    if (tc != null)
                    {
                        taskControllers.Add(tc);
                        PendingTasks.Add(tc.Task);
                        tc.Task.OnTaskStateChange += TaskStateChange;
                    }
                }
            }
        }

        private void TaskStateChange(Task task, TaskState previousSt, TaskState nextSt)
        {
            //if (nextSt == TaskState.Finished)
            //{
            //    TaskController tc = taskControllers.Find(tc => tc.Task == task);
            //    //task = tc?.Task; // clone
            //}

            if (task != null)
            {
                RemoveTaskFromContainer(task, previousSt);
                InsertTaskIntoContainer(task, nextSt);
            }

            PrintInfo();
        }

        private bool RemoveTaskFromContainer(Task task, TaskState state)
        {
            return state switch
            {
                TaskState.NotStarted => PendingTasks.Remove(task),
                TaskState.Started => AvailableTasks.Remove(task),
                _ => false
            };
        }

        private bool InsertTaskIntoContainer(Task task, TaskState state)
        {
            return state switch
            {
                TaskState.Started => AvailableTasks.Add(task),
                TaskState.Finished => CompletedTasks.Add(task),
                _ => false
            };
        }

        private void PrintInfo()
        {
            string pending = "Pending: ";
            string available = "Available: ";
            string completed = "Completed: ";

            foreach (Task p in PendingTasks)
                pending += $" {p.Name} ";
            foreach (Task a in AvailableTasks)
                available += $" {a.Name} ";
            foreach (Task c in CompletedTasks)
                completed += $" {c.Name} ";

            Debug.Log($"-- {pending} -- {available} -- {completed}");
        }
    }
}
