using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Key[] keys;
    [SerializeField] GameObject keyPrefab;
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float baseInputWindow = 1f;
    [SerializeField] int minQueue = 3;
    [SerializeField] int maxQueue = 3;
    float speedModifier = 1f;
    Queue<GameKey> keyQueue = new();
    GameKey currentKey;

    void AddToQueue()
    {
        int queueSize = Random.Range(minQueue, maxQueue);
        for (int i = 0; i < queueSize; i++)
        {
            GameKey newKey = new(keys[Random.Range(0, keys.Length)]);
            keyQueue.Enqueue(newKey);
        }
    }

    void SpawnKeys()
    {
        foreach (var key in keyQueue)
        {
            
        }
    }

    IEnumerator InputSequence()
    {
        float timer = 0;
        while (true)
        {
            if (keyQueue.Count == 0)
            {
                AddToQueue();
                timer = 0;
            }

            if (currentKey == null)
            {
                currentKey = keyQueue.Dequeue();
            }

            if (currentKey != null && Input.GetKeyDown(currentKey.keybind))
            {
                try { currentKey = keyQueue.Dequeue(); } 
                catch 
                { 
                    currentKey = null;
                    // Success code here
                }
            }

            if ((baseInputWindow * speedModifier) > timer)
            {
                // Fail code here
            }

            timer += Time.deltaTime;

            yield return null;
        }
    }
}