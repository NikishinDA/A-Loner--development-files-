using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodRayController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private ParticleSystem zone;

    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
    }

    private void OnGameStart(GameStartEvent obj)
    {
        StartCoroutine(SlowDisable(3f));
    }

    private IEnumerator SlowDisable(float time)
    {
        Color spriteRendererColor;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            spriteRendererColor = spriteRenderer.color;
            spriteRendererColor.a = Mathf.Lerp(1, 0, t / time);
            spriteRenderer.color = spriteRendererColor;
            yield return null;
        }
        spriteRendererColor = spriteRenderer.color;
        spriteRendererColor.a = 0f;
        spriteRenderer.color = spriteRendererColor;
        dust.Stop();
        zone.Stop();
    }
}
