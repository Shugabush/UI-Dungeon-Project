using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRoomScreen : MonoBehaviour
{
    [SerializeField] Image combatReport;
    [SerializeField] Canvas screenCanvas;
    [SerializeField] TMP_Text difficultyAndSuccessText;
    [SerializeField] TMP_Text successPercentText;
    [SerializeField] Button retreatButton;
    [SerializeField] TMP_Text retreatButtonText;

    public static Canvas ScreenCanvas => instance.screenCanvas;
    static Image CombatReport => instance.combatReport;
    static TMP_Text DifficultyAndSuccessText => instance.difficultyAndSuccessText;
    static TMP_Text SuccessPercentText => instance.successPercentText;
    static Button RetreatButton => instance.retreatButton;
    static TMP_Text RetreatButtonText => instance.retreatButtonText;

    static DungeonRoomScreen instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DisableCombatReport();
    }

    public static void EnableCombatReport()
    {
        CombatReport.enabled = true;
        DifficultyAndSuccessText.text = string.Empty;
        SuccessPercentText.text = string.Empty;
        RetreatButton.image.enabled = true;
        RetreatButton.interactable = false;
        RetreatButtonText.text = "Retreat to the Surface";

        StorageScreen.Instance.gameObject.SetActive(false);
        StoreScreen.Instance.gameObject.SetActive(false);
    }

    public static void DisableCombatReport()
    {
        CombatReport.enabled = false;
        DifficultyAndSuccessText.text = string.Empty;
        SuccessPercentText.text = string.Empty;
        RetreatButton.image.enabled = false;
        RetreatButton.interactable = false;
        RetreatButtonText.text = string.Empty;

        StorageScreen.Instance.gameObject.SetActive(true);
        StoreScreen.Instance.gameObject.SetActive(true);
    }
}
