using UnityEngine;
using UnityEngine.UI;

public class Key : ScriptableObject
{
    public KeyCode keybind;
    public Image keybindSprite;
}

public class GameKey
{
    public KeyCode keybind;
    public Image keybindSprite;

    public GameKey(Key key)
    {
        this.keybind = key.keybind;
        this.keybindSprite = key.keybindSprite;
    }
}