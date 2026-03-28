using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text healthText;

    [Tooltip("Если включено, в левом верхнем углу экрана рисуется HP (видно даже без привязки Slider/Text).")]
    [SerializeField] private bool drawCornerHpOverlay = true;

    private ILiveComponent bound;
    private GUIStyle cornerStyle;

    private void OnEnable()
    {
        SubscribeToGameManager();
    }

    private void Start()
    {
        SubscribeToGameManager();
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnPlayerLiveReady -= BindToPlayer;
        Unbind();
    }

    private void SubscribeToGameManager()
    {
        if (GameManager.Instance == null)
            return;

        GameManager.Instance.OnPlayerLiveReady -= BindToPlayer;
        GameManager.Instance.OnPlayerLiveReady += BindToPlayer;
    }

    private void BindToPlayer(ILiveComponent live)
    {
        Unbind();
        bound = live;
        if (bound == null)
            return;

        bound.OnHealthChanged += OnHealthChanged;
        bound.OnCharacterDeath += OnPlayerDeath;
        Refresh(bound.Health, bound.MaxHealth);
    }

    private void OnPlayerDeath(Character _)
    {
        Unbind();
    }

    private void Unbind()
    {
        if (bound == null)
            return;

        bound.OnHealthChanged -= OnHealthChanged;
        bound.OnCharacterDeath -= OnPlayerDeath;
        bound = null;
    }

    private void OnHealthChanged(float current, int max)
    {
        Refresh(current, max);
    }

    private void Refresh(float current, int max)
    {
        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = max;
            healthSlider.value = current;
        }

        if (healthText != null)
            healthText.text = $"{Mathf.CeilToInt(current)} / {max}";
    }

    private void OnGUI()
    {
        if (!drawCornerHpOverlay || bound == null)
            return;

        if (cornerStyle == null)
        {
            cornerStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperLeft
            };
            cornerStyle.normal.textColor = Color.white;
        }

        string line = $"HP {Mathf.CeilToInt(bound.Health)} / {bound.MaxHealth}";
        GUI.Label(new Rect(14f, 10f, 320f, 36f), line, cornerStyle);
    }
}
