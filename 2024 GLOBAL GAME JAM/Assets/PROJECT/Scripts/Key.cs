using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "KeyBind", menuName ="Keys")]
public class Key : ScriptableObject
{
    public KeyCode keybind;
    public Sprite keybindSprite;
}

public class GameKey
{
    public KeyCode keybind;
    public Sprite keybindSprite;
    public GameObject gameObject;

    public GameKey(Key key)
    {
        this.keybind = key.keybind;
        this.keybindSprite = key.keybindSprite;
    }
}