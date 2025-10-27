using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour {
    

    [SerializeField] private float displayTime = 10.0f;
    
    public static UIHandler instance { get; private set; }
    private VisualElement m_Healthbar;
    private VisualElement m_NonPlayerdialogue;
    private float m_TimerDisplay;

    private void Awake() {
        instance = this;
    }
            

    void Start() {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_NonPlayerdialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerdialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    private void Update() {
        if (m_TimerDisplay > 0.0f) {
            m_TimerDisplay -= Time.deltaTime;
        }

        if (m_TimerDisplay <= 0.0f) {
            m_NonPlayerdialogue.style.display = DisplayStyle.None;
        }
    }

    public void SetHealthValue(float percentage) {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    public void ShowNonPlayerDialogue() {
        m_NonPlayerdialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    
}
