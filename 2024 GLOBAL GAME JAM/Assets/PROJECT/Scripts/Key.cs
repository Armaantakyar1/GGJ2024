using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "KeyBind", menuName ="Keys")]
public class Key : ScriptableObject
{
    public KeyCode keybind;
    public Sprite baseSprite;
    public Sprite activatedSprite;
}

public class GameKey
{
    public KeyCode keybind;
    public Sprite baseSprite;
    public Sprite activatedSprite;
    public GameObject gameObject;

    public GameKey(Key key)
    {
        this.keybind = key.keybind;
        this.baseSprite = key.baseSprite;
        this.activatedSprite = key.activatedSprite;
    }
}