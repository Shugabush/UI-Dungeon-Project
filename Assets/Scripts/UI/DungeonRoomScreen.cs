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
    [SerializeField] Button continueButton;
    [SerializeField] TMP_Text continueButtonText;
    [SerializeField] LootLayout loot;

    static DungeonDifficulty[] Difficulties => instance.difficulties;
    public static Canvas ScreenCanvas => instance.screenCanvas;
    static Image CombatReport => instance.combatReport;
    static TMP_Text DifficultyAndSuccessText => instance.difficultyAndSuccessText;
    static TMP_Text SuccessPercentText => instance.successPercentText;
    static Button ContinueButton => instance.continueButton;
    static TMP_Text ContinueButtonText => instance.continueButtonText;
    static LootLayout Loot => instance.loot;

    static DungeonRoomScreen instance;

    DungeonDifficulty currentDifficulty;
    public static DungeonDifficulty CurrentDifficulty
    {
        get
        {
            return instance.currentDifficulty;
        }
        private set
        {
            instance.currentDifficulty = value;
        }
    }

    void Awake()
    {
        instance = this;
        continueButton.onClick.AddListener(DisableCombatReport);
    }

    public static void EnableCombatReport(DungeonRoom room)
    {
        ActiveCanvasManager.SetCanvasActive(ScreenCanvas);

        CurrentDifficulty = GetDifficulty(room.DifficultyIndex);

        CombatReport.enabled = true;

        int successChance = CurrentDifficulty.baseFightSuccess + PlayerStatsScreen.GetAdditionalFightSuccess();
        string successChanceString = successChance.ToString("00") + "%";

        int successPercent = Random.Range(0, 100);
        string successString = string.Empty;

        if (successPercent > successChance)
        {
            // The player failed
            successString += "\nFAIL!";
            Loot.gameObject.SetActive(false);
        }
        else
        {
            // The player succeeded
            successString += "\nSUCCESS!";
            Loot.gameObject.SetActive(true);

            Loot.GenerateLoot();
        }

        // The player completes the room regardless of whether they succeeded or not
        room.Completed = true;

        GameManager.Health -= Random.Range(CurrentDifficulty.minHealthLost, CurrentDifficulty.maxHealthLost);

        DifficultyAndSuccessText.text = $"Difficulty: {room.DifficultyIndex + 1}\nSuccess Chance: {successChanceString}";
        SuccessPercentText.text = successString;
        ContinueButton.image.enabled = true;
        ContinueButtonText.text = "Continue";
    }

    public static void DisableCombatReport()
    {
        ActiveCanvasManager.SetCanvasActive(DungeonMap.Screen);

        CombatReport.enabled = false;
        DifficultyAndSuccessText.text = string.Empty;
        SuccessPercentText.text = string.Empty;
        ContinueButton.image.enabled = false;
        ContinueButtonText.text = string.Empty;
    }

    public static DungeonDifficulty GetDifficulty(int index)
    {
        return Difficulties[index];
    }
}
