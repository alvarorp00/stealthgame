using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Tasks
{
    public enum TaskType { ObjectDelivery, ObjectTake, PointReached, DialogFinished, PathBlock };

    public class ObjectDeliveryTask : Task
    {
        public ObjectDeliveryTask(string Name, string Description) : base(Name, Description)
        {
        }
    }

    public class ObjectTakeTask : Task
    {
        public ObjectTakeTask(string Name, string Description) : base(Name, Description)
        {
        }
    }

    public class PointReachedTask : Task
    {
        public PointReachedTask(string Name, string Description) : base(Name, Description)
        {
        }
    }

    public class DialogFinishedTask : Task
    {
        public DialogFinishedTask(string Name, string Description) : base(Name, Description)
        {
        }
    }

    public class PathBlockTask : Task
    {
        public PathBlockTask(string Name, string Description) : base(Name, Description)
        {
        }
    }
}
