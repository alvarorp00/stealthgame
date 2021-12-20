using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actions : MonoBehaviour {

  public void PlayGame() {
        GameManager.Instance.SpawnPlayer();
  }
  
}

