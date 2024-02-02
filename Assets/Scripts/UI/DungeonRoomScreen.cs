using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRoomScreen : MonoBehaviour
{
    [SerializeField] DungeonDifficulty[] difficulties = new DungeonDifficulty[0];

    [SerializeField] Canvas screenCanvas;
    [SerializeField] Image combatReport;
    [SerializeField] TMP_Text difficultyAndSuccessText;
    [SerializeField] TMP_Text successPercentText;
    [SerializeField] Button retreatButton;
    [SerializeField] TMP_Text retreatButtonText;

    static DungeonDifficulty[] Difficulties => instance.difficulties;
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
        retreatButton.onClick.AddListener(DisableCombatReport);
    }

    public static void EnableCombatReport(DungeonRoom room)
    {
        ActiveCanvasManager.SetCanvasActive(ScreenCanvas);

        DungeonDifficulty difficulty = GetDifficulty(room.DifficultyIndex);

        CombatReport.enabled = true;

        int successChance = difficulty.baseFightSuccess + PlayerStatsScreen.GetAdditionalFightSuccess();
        string successChanceString = successChance.ToString("00") + "%";

        int successPercent = Random.Range(0, 100);
        string successString = successPercent.ToString("00") + "%";

        if (successPercent > successChance)
        {
            // The player failed
            successString += "\nFAIL!";
        }
        else
        {
            // The player succeeded
            successString += "\nSUCCESS!";
            room.Completed = true;
        }

        DifficultyAndSuccessText.text = $"Difficulty: {room.DifficultyIndex + 1}\nSuccess Chance: {successChanceString}";
        SuccessPercentText.text = successString;
        RetreatButton.image.enabled = true;
        RetreatButtonText.text = "Retreat to the Surface";

        StorageScreen.Instance.gameObject.SetActive(false);
        StoreScreen.Instance.gameObject.SetActive(false);
    }

    public static void DisableCombatReport()
    {
        ActiveCanvasManager.SetCanvasActive(DungeonMap.MapCanvas);

        CombatReport.enabled = false;
        DifficultyAndSuccessText.text = string.Empty;
        SuccessPercentText.text = string.Empty;
        RetreatButton.image.enabled = false;
        RetreatButtonText.text = string.Empty;

        StorageScreen.Instance.gameObject.SetActive(true);
        StoreScreen.Instance.gameObject.SetActive(true);
    }

    public static DungeonDifficulty GetDifficulty(int index)
    {
        return Difficulties[index];
    }
}
