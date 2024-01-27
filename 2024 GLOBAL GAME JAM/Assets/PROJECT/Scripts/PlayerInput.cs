using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Key[] keys;
    [SerializeField] GameObject keyPrefab;
    [SerializeField] Image fillBar;
    [SerializeField] Sprite wrongSprite;

    [SerializeField] Transform centerSpawnPosition;
    [SerializeField] float spriteOffset;
    
    [SerializeField] float baseInputWindow = 1f;
    [SerializeField] float speedChangeModifier = 0.1f;
    [SerializeField] float speedModifier = 1f;

    [SerializeField] int minQueue = 3;
    [SerializeField] int maxQueue = 3;
    List<GameKey> keyQueue = new();
    List<KeyCode> keyList = new();
    List<GameKey> closedKeyList = new();
    GameKey currentKey;

    private void Start()
    {
        foreach(var key in keys)
        {
            keyList.Add(key.keybind);
        }
        keyQueue.Clear();
        StartCoroutine(InputSequence());
    }

    void AddToQueue()
    {
        int queueSize = Random.Range(minQueue, maxQueue);
        keyQueue.Clear();
        for (int i = 0; i < queueSize; i++)
        {
            GameKey newKey = new(keys[Random.Range(0, keys.Length)]);
            keyQueue.Add(newKey);
        }
    }

    void SpawnKeys()
    {
        float centerAlignmentOffset = -(keyQueue.Count * spriteOffset) / 2;
        for (int i = 0; i < keyQueue.Count; i++)
        {
            keyQueue[i].gameObject = Instantiate(keyPrefab, centerSpawnPosition);
            keyQueue[i].gameObject.transform.position += new Vector3(centerAlignmentOffset + (i * spriteOffset) + (keyQueue[i].gameObject.GetComponent<RectTransform>().rect.width / 2), 0f, 0f);
            keyQueue[i].gameObject.GetComponent<Image>().sprite = keyQueue[i].baseSprite;
        }
    }

    IEnumerator InputSequence()
    {
        float timer = 0;
        while (true) // Change this to an actual condition later pls
        {
            if (keyQueue.Count == 0)
            {
                AddToQueue();
                SpawnKeys();
            }

            if (currentKey == null)
            {
                currentKey = keyQueue[0];
            }

            timer = CheckInput(timer);

            if ((baseInputWindow / speedModifier) < timer || keyQueue.Count == 0)
            {
                timer = 0;
                foreach (var key in closedKeyList)
                {
                    Destroy(key.gameObject); // Animation later?
                }
                closedKeyList.Clear();
                // Fail code here
            }

            timer += Time.deltaTime;
            fillBar.fillAmount = 1 - (timer / (baseInputWindow / speedModifier));
            yield return null;
        }
    }

    private float CheckInput(float timer)
    {
        if (currentKey != null && Input.GetKeyDown(currentKey.keybind))
        {
            keyQueue[0].gameObject.GetComponent<Image>().sprite = keyQueue[0].activatedSprite;
            Dequeue();
            currentKey = null;
            speedModifier += speedModifier * speedChangeModifier;
            if (keyQueue.Count == 0)
            {
                timer = 0;
                // Success code here
            }
        }
        else if (currentKey != null && OtherInputsPressed())
        {
            keyQueue[0].gameObject.GetComponent<Image>().sprite = wrongSprite;
            Dequeue();
            currentKey = null;
            speedModifier -= speedModifier * speedChangeModifier;
        }

        return timer;
    }

    private void Dequeue()
    {
        closedKeyList.Add(keyQueue[0]);
        keyQueue.RemoveAt(0);
    }

    bool OtherInputsPressed()
    {
        foreach(var keycode in keyList)
        {
            if (Input.GetKeyDown(keycode))
            {
                return true;
            }
        }
        return false;
    }
}