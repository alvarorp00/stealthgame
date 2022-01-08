using System;
using System.Collections.Generic;

namespace Assets.Scripts.Tasks
{
    public abstract class Task : ICloneable
    {
        private static uint TaskCounter = 0;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public uint Identifier { get; private set; }

        public TaskState State { get; private set; }
        public Action<Task, TaskState, TaskState> OnTaskStateChange;

        // required finished task to start this
        public List<Task> taskRequired;

        public Task(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
            State = TaskState.NotStarted; // by default they do not start
            Identifier = TaskCounter++;
            taskRequired = new List<Task>();
        }

        public void UpdateTaskState(TaskState newState)
        {
            TaskState oldState = State;
            State = newState;

            OnTaskStateChange?.Invoke(this, oldState, State);
        }

        public object Clone() => MemberwiseClone();

        public override int GetHashCode() => Name.GetHashCode() ^ Description.GetHashCode();

        //public override bool Equals(object obj) => obj.GetType() == typeof(Task) && ((Task)obj).Name.Equals(Name);
    }

    public enum TaskState { NotStarted, Started, Finished };
}
