using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tasks.Controllers
{
    public class DialogFinishedTaskController : TaskController
    {
        public DialogueTrigger MessageToWait;

        private void Awake()
        {
            taskType = TaskType.DialogFinished;
            if (MessageToWait == null)
                throw new Exception("AlertMessage can't be null inside DialogTask");
            Task = new DialogFinishedTask(Name, Description);
        }

        protected override void SetUp()
        {
            MessageToWait.OnDialogueStateChange += (sender, state) =>
            {
                Debug.Log("Dialogue detected. Status: " + state.ToString());
                if (state == DialogueState.Finished)
                {
                    ForceFinish();
                }
            };
        }
    }
}
