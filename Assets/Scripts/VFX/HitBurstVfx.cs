using System.Collections;
using UnityEngine;

/// <summary>
/// Короткая вспышка в точке попадания без префаба (сфера растёт и исчезает).
/// </summary>
public class HitBurstVfx : MonoBehaviour
{
    public static void Spawn(Vector3 position, Color color, float duration = 0.18f, float maxScale = 0.45f)
    {
        var go = new GameObject("HitBurstVfx");
        go.transform.position = position;
        var fx = go.AddComponent<HitBurstVfx>();
        fx.Run(color, duration, maxScale);
    }

    private void Run(Color color, float duration, float maxScale)
    {
        StartCoroutine(Animate(color, duration, maxScale));
    }

    private IEnumerator Animate(Color color, float duration, float maxScale)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.SetParent(transform, false);
        sphere.transform.localScale = Vector3.one * 0.08f;
        Destroy(sphere.GetComponent<Collider>());

        var renderer = sphere.GetComponent<Renderer>();
        Material mat = null;
        if (renderer != null)
        {
            Shader shader = Shader.Find("Unlit/Color");
            if (shader == null)
                shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null)
                shader = Shader.Find("Sprites/Default");

            mat = new Material(shader);
            if (mat.HasProperty("_Color"))
                mat.color = color;
            else if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", color);
            renderer.material = mat;
        }

        float t = 0f;
        Vector3 start = Vector3.one * 0.08f;
        Vector3 end = Vector3.one * maxScale;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / duration);
            sphere.transform.localScale = Vector3.Lerp(start, end, k);
            if (mat != null)
            {
                Color c = color;
                c.a = 1f - k;
                if (mat.HasProperty("_Color"))
                    mat.color = c;
                else if (mat.HasProperty("_BaseColor"))
                    mat.SetColor("_BaseColor", c);
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}
