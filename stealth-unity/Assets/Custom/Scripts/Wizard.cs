using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour {

  private Animator anim; 
  // Start is called before the first frame update
  void Start() {
    anim = GetComponent<Animator>();
  }

  void OnTriggerEnter( Collider other ) {

    anim.SetBool("talking", true );

    GetComponent<DialogueTrigger>().TriggerDialogue();

    // Debug.Log( GetComponent<DialogueTrigger>() );

    // KeyHelper.Create( gameObject, "to onteract with the wizard" );
  }

  // Update is called once per frame
  void Update() {
    
  }
  
}

