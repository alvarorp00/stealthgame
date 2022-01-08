using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.Scripts.Managers
{
    internal class UIItemManager : MonoBehaviour
    {
        //public GameObject UIMap mapUI;
        public GameObject UIMap;
        public GameObject UINote;

        public static UIItemManager Instance;

        public enum UIItemShowState { Showing, NotShowing };

        public Action<UIItemShowState> OnUIItemShow;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void ShowNote(Note note)
        {
            TextMeshProUGUI text = UINote.GetComponentInChildren<TextMeshProUGUI>();
            text?.SetText(note.message);

            Show(UINote);
        }

        public void EnableMap(Map map)
        {
            GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>().enabled = true;
            Show(UIMap);

            // todo: player.menu.enableMap
        }

        /* PRIVATE */

        private void Show(GameObject obj)
        {
            obj?.SetActive(true);
            OnUIItemShow?.Invoke(UIItemShowState.Showing);
        }

        public void StopShow(GameObject obj)
        {
            obj?.SetActive(false);
            OnUIItemShow?.Invoke(UIItemShowState.NotShowing);
        }

        public void StopMapCamera() => GameObject.FindGameObjectWithTag("MapCamera").GetComponent<Camera>().enabled = false;

    }
}
