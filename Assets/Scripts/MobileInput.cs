using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MobileInput 
{

    public MyButton LeftButton;
    public MyButton RightButton;
    public MyButton JumpButton;
    public MyButton AttackButton;

    public int Horizzontal;

    public bool AnyInput() => LeftButton.IsPressed || RightButton.IsPressed || JumpButton.IsPressed;
}

