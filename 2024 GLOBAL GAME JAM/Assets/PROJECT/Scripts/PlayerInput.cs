using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;

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
    [SerializeField] float maxWidth = 600f;
    [SerializeField] string playerType;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip wrongSound;
    [SerializeField] float pitchMod = 0.2f;

    List<GameKey> keyQueue = new();
    List<KeyCode> keyList = new();
    List<GameKey> closedKeyList = new();
    GameKey currentKey;
    public static Action<string> FailedKeyPressed;

    private void Start()
    {
        foreach (var key in keys)
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
        HorizontalLayoutGroup group = centerSpawnPosition.GetComponent<HorizontalLayoutGroup>();
        group.padding.left = (int)((100 * keyQueue.Count) + (group.spacing * (keyQueue.Count - 1)));
        group.padding.left *= -1;
        group.padding.left /= 2;
        group.padding.left += 50;
        for (int i = 0; i < keyQueue.Count; i++)
        {
            keyQueue[i].gameObject = Instantiate(keyPrefab, centerSpawnPosition);
            keyQueue[i].gameObject.GetComponent<Image>().sprite = keyQueue[i].baseSprite;
        }
    }

    IEnumerator InputSequence()
    {
        float timer = 0;
        float successCounter = 0;
        while (true) // Change this to an actual condition later pls
        {
            if (keyQueue.Count == 0)
            {
                successCounter = 0;
                AddToQueue();
                SpawnKeys();
            }

            if (currentKey == null)
            {
                currentKey = keyQueue[0];
            }

            timer = CheckInput(timer, ref successCounter);

            if ((baseInputWindow / speedModifier) < timer || keyQueue.Count == 0)
            {
                timer = 0;
                foreach (var key in keyQueue)
                {
                    Destroy(key.gameObject);
                }
                foreach (var key in closedKeyList)
                {
                    Destroy(key.gameObject); // Animation later?
                }
                closedKeyList.Clear();
                keyQueue.Clear();
                audioSource.PlayOneShot(wrongSound);
                // Fail code here
                FailedKeyPressed?.Invoke(playerType);
            }

            timer += Time.deltaTime;
            fillBar.fillAmount = 1 - (timer / (baseInputWindow / speedModifier));
            yield return null;
        }
    }

    private float CheckInput(float timer, ref float successCounter)
    {
        if (currentKey != null && Input.GetKeyDown(currentKey.keybind))
        {
            float pitch = audioSource.pitch;
            audioSource.pitch += successCounter * pitchMod;
            successCounter++;
            audioSource.clip = correctSound;
            audioSource.PlayOneShot(correctSound);
            audioSource.pitch = pitch;
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
            audioSource.PlayOneShot(wrongSound);
            Dequeue();
            FailedKeyPressed?.Invoke(playerType);
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