using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerHealthDisplayer : MonoBehaviour
{
    [field:SerializeField]
    private PlayerHealthHandler playerHealthHandler;
    private TMP_Text textLabel;
    void Start()
    {
        textLabel = GetComponent<TMP_Text>();
    }
    void Update()
    {
        textLabel.text = $"{playerHealthHandler.Health}/{playerHealthHandler.MaxHealth}";
    }
}
