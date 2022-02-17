using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite _buttonOn;
    [SerializeField] private Sprite _buttonOff;

    public void SwitchButtonSprite()
    {
        Image image = gameObject.GetComponent<Image>();
        image.sprite = image.sprite == _buttonOn ? _buttonOff : _buttonOn;
    }
}
