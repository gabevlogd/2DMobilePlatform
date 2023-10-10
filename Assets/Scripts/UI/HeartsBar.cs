using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsBar : MonoBehaviour
{
    [SerializeField]
    private int heartsNumber;
    [SerializeField]
    private List<Sprite> barStates;
    private Image image;

    private void Awake() => image = GetComponent<Image>();

    public void UpdateBarState(int spriteIndex)
    {
        if (spriteIndex < 0 || spriteIndex >= barStates.Count) return;

        image.sprite = barStates[spriteIndex];
        heartsNumber = spriteIndex + 1;
    }

    public int GetLife() => heartsNumber;


}
