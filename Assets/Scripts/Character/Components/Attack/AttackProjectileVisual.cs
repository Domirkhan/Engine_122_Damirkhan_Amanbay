using System.Collections;
using UnityEngine;

/// <summary>
/// Короткая анимация «пули» от атакующего к цели (только визуал, урон уже нанесён в CharacterAttackComponent).
/// </summary>
public class AttackProjectileVisual : MonoBehaviour
{
    public static void Spawn(Vector3 from, Vector3 to, float flySpeed = 42f, float sphereScale = 0.18f)
    {
        var go = new GameObject("AttackProjectileVisual");
        var comp = go.AddComponent<AttackProjectileVisual>();
        comp.Run(from, to, flySpeed, sphereScale);
    }

    private void Run(Vector3 from, Vector3 to, float flySpeed, float sphereScale)
    {
        transform.position = from;

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.SetParent(transform, false);
        sphere.transform.localScale = Vector3.one * sphereScale;
        Object.Destroy(sphere.GetComponent<Collider>());

        var renderer = sphere.GetComponent<Renderer>();
        if (renderer != null)
        {
            Shader shader = Shader.Find("Unlit/Color");
            if (shader == null)
                shader = Shader.Find("Universal Render Pipeline/Unlit");
            if (shader == null)
                shader = Shader.Find("Sprites/Default");

            var mat = new Material(shader);
            if (mat.HasProperty("_Color"))
                mat.color = new Color(1f, 0.92f, 0.15f);
            else if (mat.HasProperty("_BaseColor"))
                mat.SetColor("_BaseColor", new Color(1f, 0.92f, 0.15f));
            renderer.material = mat;
        }

        StartCoroutine(Fly(to, flySpeed));
    }

    private IEnumerator Fly(Vector3 to, float speed)
    {
        const float hitSqr = 0.04f;
        while ((transform.position - to).sqrMagnitude > hitSqr)
        {
            transform.position = Vector3.MoveTowards(transform.position, to, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = to;
        HitBurstVfx.Spawn(to, new Color(1f, 0.55f, 0.1f));
        yield return new WaitForSeconds(0.02f);
        Destroy(gameObject);
    }
}
