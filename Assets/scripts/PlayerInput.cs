using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    
    [SerializeField] private DynamicJoystick HorizontalControls;
    [SerializeField] private DynamicJoystick VerticalControls;
    [SerializeField] private KeyCode JumpButton;
    [SerializeField] private KeyCode AttackButton;

    Controls controls = new Controls();

    public Controls GetInput()
    {
        controls.HorizontalMove =Mathf.Round( HorizontalControls.Horizontal);
        controls.VerticalMove = Mathf.Round(VerticalControls.Vertical);
        controls.JumpState = Input.GetKeyDown(JumpButton);
        controls.AttackState = Input.GetKeyDown(AttackButton);

        return controls;
    } 
}
