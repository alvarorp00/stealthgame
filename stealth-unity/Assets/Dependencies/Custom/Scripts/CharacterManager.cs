using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

  void Start() {
    
  }

  void Update() {
  
  }

  private void OnApplicationFocus(bool focusStatus) {
    Cursor.lockState = focusStatus ? CursorLockMode.Locked : CursorLockMode.None;
  }
  
}

