 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;

 // this toggles a component (usually an Image or Renderer) on and off for an interval to simulate a blinking effect
 public class Blink : MonoBehaviour {

     // this is the UI.Text or other UI element you want to toggle
     public MaskableGraphic imageToToggle;

     public float intervalDefault = 1f;
     private float interval;
     public float startDelay = 0.5f;
     public bool currentState = true;
     public bool defaultState = true;
     public bool isBlinking = false;

     void Start()
     {
         imageToToggle.enabled = defaultState;
     }

     public void StartBlink(float _interval = 0)
     {

         // do not invoke the blink twice - needed if you need to start the blink from an external object
         if(_interval != 0 ){
             interval = _interval;
         } else {
             interval = intervalDefault;
         }
         if (isBlinking)
             return;

         if (imageToToggle !=null)
         {
             isBlinking = true;
             InvokeRepeating("ToggleState", startDelay, interval);
         }
     }

     public void StopBlinking(){
         isBlinking = false;
         imageToToggle.enabled = defaultState;
         CancelInvoke("ToggleState");
     }

     public void ToggleState()
     {
         imageToToggle.enabled = !imageToToggle.enabled;
     }

     public void SetColorRed(){
         imageToToggle.color = Color.red;
     }
     public void SetColorWhite(){
         imageToToggle.color = Color.white;
     }
 }