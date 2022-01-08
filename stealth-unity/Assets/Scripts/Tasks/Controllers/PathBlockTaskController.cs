using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tasks.Controllers
{

    public class PathBlockTaskController : TaskController
    {
        public GameObject blocking_object;
        public GameObject substitute_with; // if non-null then blocking will be sustituted by this

        public bool is_invisible; // render it or not

        private void Awake()
        {
            if (blocking_object == null)
                throw new ArgumentNullException("Blocking Object can't be null in this type of Tasks");
            if (substitute_with != null)
                substitute_with.SetActive(false);
            Task = new PathBlockTask(Name, Description);
            autoStart = true;
        }

        protected override void SetUp()
        {
            if (is_invisible && blocking_object.TryGetComponent(out Renderer renderer))
                renderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if  (Player.Instance.CompareTag(other.tag) && Task.State == TaskState.Started)
            {
                if (substitute_with != null)
                {
                    blocking_object.SetActive(false);
                    substitute_with.SetActive(true);
                }
                ForceFinish();
            }
        }

        //private void OnTriggerStay(Collider other)
        //{
        //    OnTriggerEnter(other); // don't reimplement things...
        //}
    }
}
