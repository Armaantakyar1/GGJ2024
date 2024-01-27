using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Key[] keys;
    [SerializeField] GameObject keyPrefab;

    [SerializeField] Transform centerSpawnPosition;
    [SerializeField] float spriteOffset;
    
    [SerializeField] float baseInputWindow = 1f;
    [SerializeField] float speedChangeModifier = 0.1f;
    float speedModifier = 1f;

    [SerializeField] int minQueue = 3;
    [SerializeField] int maxQueue = 3;
    List<GameKey> keyQueue = new();
    GameKey currentKey;

    void AddToQueue()
    {
        int queueSize = Random.Range(minQueue, maxQueue);
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
            keyQueue[i].gameObject.transform.position += new Vector3(centerAlignmentOffset + (i * spriteOffset), 0f, 0f);
            keyQueue[i].gameObject.GetComponent<Image>().sprite = keyQueue[i].keybindSprite;
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
            }

            if (currentKey == null)
            {
                currentKey = keyQueue[0];
            }

            timer = NewMethod(timer);

            if ((baseInputWindow * speedModifier) > timer)
            {
                timer = 0;
                foreach (var key in keyQueue)
                {
                    Destroy(key.gameObject); // Animation later?
                }
                keyQueue.Clear();
                // Fail code here
            }

            timer += Time.deltaTime;

            yield return null;
        }
    }

    private float NewMethod(float timer)
    {
        if (currentKey != null && Input.GetKeyDown(currentKey.keybind))
        {
            keyQueue.RemoveAt(0);
            currentKey = null;
            speedModifier += speedModifier * speedChangeModifier;
            if (keyQueue.Count == 0)
            {
                timer = 0;
                // Success code here
            }
        }
        else if (currentKey != null && Input.anyKey)
        {
            keyQueue.RemoveAt(0);
            currentKey = null;
            speedChangeModifier -= speedModifier * speedChangeModifier;
        }

        return timer;
    }
}