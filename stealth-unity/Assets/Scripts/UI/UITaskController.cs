using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Tasks;
using Assets.Scripts.Managers;
using UnityEngine.Events;

namespace Assets.Scripts.UI
{
    public class UITaskController : MonoBehaviour
    {
        [SerializeField] private Transform scroll_panel_parent;
        [SerializeField] private Transform quest_button_sample; // instantiate this
        [SerializeField] private GameObject quest_panel_info; // where info is drawed

        //private List<RectTransform> instances;
        private Dictionary<Task, RectTransform> taskList;

        private void Awake()
        {
            taskList = new Dictionary<Task, RectTransform>();

            foreach (Task task in TaskManager.Instance.AvailableTasks)
                InstantiateTask(task, true);

            foreach (Task task in TaskManager.Instance.CompletedTasks)
                InstantiateTask(task, true);

            foreach (Task task in TaskManager.Instance.PendingTasks)
                InstantiateTask(task, false);
        }

        private void SetUp()
        {
            
        }

        private void Start()
        {

            Debug.Log("Tasks: " + taskList.Count);
            foreach (Task task in taskList.Keys)
                Debug.Log(task.Name);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            quest_panel_info.SetActive(false);
        }

        private void AddInfo(Task task)
        {
            quest_panel_info.SetActive(true);
            quest_panel_info.transform.GetChild(0).GetComponent<Text>().text = task.Name;
            quest_panel_info.transform.GetChild(1).GetComponent<Text>().text = task.Description;
            quest_panel_info.transform.GetChild(2).GetComponentInChildren<Text>().text = task.State.ToString();
        }

        private void OnTaskStateUpdate(Task task, TaskState oldSt, TaskState newSt)
        {
            if (oldSt == TaskState.NotStarted && newSt != TaskState.NotStarted)
            {
                taskList.TryGetValue(task, out RectTransform rectTransform);
                rectTransform?.gameObject.SetActive(true);
            }
        }

        private void InstantiateTask(Task task, bool enable)
        {
            if (taskList.ContainsKey(task))
                return; // be careful!
            RectTransform rectTransform = Instantiate(quest_button_sample, scroll_panel_parent).GetComponent<RectTransform>();
            rectTransform.Find("QuestName").GetComponent<Text>().text = task.Name;
            rectTransform.gameObject.SetActive(true);

            UnityAction partializedAction = () => AddInfo(task);

            rectTransform.GetComponent<Button>().onClick.AddListener(partializedAction);
            taskList.Add(task, rectTransform);

            task.OnTaskStateChange += OnTaskStateUpdate;
        }
    }
}