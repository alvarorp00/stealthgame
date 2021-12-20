using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICursor : MonoBehaviour {

  // Start is called before the first frame update
  public Texture2D cursorTexture;
  
  void Start() {
    Cursor.SetCursor(cursorTexture, new Vector2(20,20), CursorMode.Auto);
  }

  // Update is called once per frame
  void Update() {
    
  }
  
}

