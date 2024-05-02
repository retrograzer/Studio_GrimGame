using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_AudioManager : MonoBehaviour
{
    public AudioClip OWTheme;
    public AudioClip BattleTheme;
    public AudioClip ShopTheme;
    public AudioClip mmTheme;
    public EM_Manager em;
    public ShopBehavior sb;

    Transform playerPos;
    float[] rampUpValues;
    float safeZone;
    AudioSource src;
    [HideInInspector] public bool inShop = false;
    bool isFading = false;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        rampUpValues = em.enemyRampUp;
        src = GetComponent<AudioSource>();

        src.volume = 0;
        FadeIn(2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead)
            return;
        CheckDistanceAndUpdateClip();
    }

    void CheckDistanceAndUpdateClip()
    {
        // Calculate the distance from the target position to the origin (0,0

        float distance = 0f;
        if (playerPos)
            distance = Vector2.Distance(new Vector2(playerPos.position.x, playerPos.position.y), Vector2.zero);
        inShop = sb.menuActive;

        // Check if the distance is within the specified radius
        if (!inShop)
        {
            if (distance <= rampUpValues[1])
            {
                // Change to the new clip if it's not already set
                if (src.clip != OWTheme)
                {
                    CrossfadeToNextClip(OWTheme, 2f);
                }
            }
            else
            {
                // Revert to the original clip if out of radius and it's not already set
                if (src.clip != BattleTheme)
                {
                    CrossfadeToNextClip(BattleTheme, 2f);
                }
            }
        }
        else
        {
            if (src.clip != ShopTheme)
            {
                CrossfadeToNextClip(ShopTheme, .5f);
            }
        }

    }

    public void FadeToMM (float fadeDuration)
    {
        isDead = true;
        CrossfadeToNextClip(mmTheme, fadeDuration);
    }

    public void FadeIn(float fadeDuration)
    {
        StartCoroutine(FadeAudio(0, 1, fadeDuration)); // Fade from 0 (mute) to 1 (full volume)
    }

    // Method to start fading out
    public void FadeOut(float fadeDuration)
    {
        StartCoroutine(FadeAudio(1, 0, fadeDuration)); // Fade from 1 (full volume) to 0 (mute)
    }

    private IEnumerator FadeAudio(float startLevel, float endLevel, float duration)
    {
        float currentTime = 0;

        // Initially set the volume to the start level
        src.volume = startLevel;

        // Gradually change the volume
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            src.volume = Mathf.Lerp(startLevel, endLevel, currentTime / duration);
            yield return null;
        }

        // Ensure the final volume is set accurately
        src.volume = endLevel;
    }

    public void CrossfadeToNextClip(AudioClip newClip, float fadeTime)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAudio(newClip, fadeTime));
        }
    }

    private IEnumerator FadeAudio(AudioClip newClip, float duration)
    {
        isFading = true;
        float currentTime = 0;
        float startVolume = src.volume;

        // Fade out the current clip
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            src.volume = Mathf.Lerp(startVolume, 0, currentTime / duration);
            yield return null;
        }

        // Change clip and fade in
        src.clip = newClip;
        src.Play();

        currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            src.volume = Mathf.Lerp(0, startVolume, currentTime / duration);
            yield return null;
        }

        isFading = false;
    }
}
