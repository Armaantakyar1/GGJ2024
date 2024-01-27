using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "KeyBind", menuName ="Keys")]
public class Key : ScriptableObject
{
    public KeyCode keybind;
    public Image keybindSprite;
}