using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Tasks.Controllers
{
    public class PointReachedTaskController : TaskController
    {
        private void Awake()
        {
            Task = new PointReachedTask(Name, Description);
            autoStart = true;
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            if (Player.Instance.CompareTag(other.gameObject.tag))
                ForceFinish();
        }

        protected override void SetUp()
        {
            // pass
        }
    }
}
