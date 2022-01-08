using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
    public class Actions : MonoBehaviour
    {

        public void PlayGame()
        {
            GameManager.Instance.SpawnPlayer();
        }

    }
}

