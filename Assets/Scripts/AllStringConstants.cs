using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllStringConstants
{
    #region Loading

    #endregion

    #region MainMenu

    #region Choose Mode 

    public const string STORY_MODE_STATUS = "StoryModeComplete";


    #endregion

    #region Map

    public const string HAS_PLAYER_WATCHED_STORY = "StorySeen";

    #endregion

    #region Option

    public const string VIBRATION_STATUS = "IsVibrationOn";

    public const string MASTER_VOLUME_MIXER = "volume";
    public const string SFX_VOLUME_MIXER = "sfxVolume";

    public const string RATE_US_INFO_ANDROID = "market://details?id=com.company.productname";
    public const string RATE_US_INFO_IOS = "itms-apps://itunes.apple.com/app/idYOUR_ID";

    #endregion

    #region Shop

    public const string COINS_AVAILABILITY_STATUS = "Coins";
    public const string CRYSTALS_AVAILABILITY_STATUS = "Crystals";

    public const string GOODS1_COINS = "CoinPurchase1";
    public const string GOODS2_COINS = "CoinPurchase2";
    public const string GOODS3_CRYSTALS = "CrystalPurchase1";
    public const string GOODS4_CRYSTALS = "CrystalPurchase2";

    //with these tag extract their damage data and lvl from gamedatamanager. 
    public const string HOMINGMISSILE_SHIP1 = "Ship1HM";
    public const string HOMINGMISSILE_SHIP2 = "Ship2HM";
    public const string HOMINGMISSILE_SHIP3 = "Ship3HM";

    public const string lASER_SHIP1 = "Ship1Laser";
    public const string lASER_SHIP2 = "Ship2Laser";
    public const string lASER_SHIP3 = "Ship3Laser";

    public const string PROTECTION_SHIP1 = "Ship1Protection";
    public const string PROTECTION_SHIP2 = "Ship2Protection";
    public const string PROTECTION_SHIP3 = "Ship3Protection";

    public const string IT_IS_COIN = "ItsCoin";
    public const string IT_IS_CRYSTAL = "ItsCrystal";

    public static readonly string[] SHIPS_AVAILABILITY_STATUS = { 
                                                                    "Ship1Button", 
                                                                    "Ship2Button",
                                                                    "Ship3Button" 
                                                                };

    public static readonly string[][] SHIPS_CARDS_AVAILABILITY = {
                                                                    new string[] 
                                                                    {
                                                                        "Ship1Card1",
                                                                        "Ship1Card2",
                                                                        "Ship1Card3",
                                                                        "Ship1Card4",
                                                                        "Ship1Card5",
                                                                        "Ship1Card6",
                                                                        "Ship1Card7",
                                                                        "Ship1Card8",
                                                                        "Ship1Card9",
                                                                        "Ship1Card10"
                                                                    },

                                                                    new string[]
                                                                    {
                                                                        "Ship2Card1",
                                                                        "Ship2Card2",
                                                                        "Ship2Card3",
                                                                        "Ship2Card4",
                                                                        "Ship2Card5",
                                                                        "Ship2Card6",
                                                                        "Ship2Card7",
                                                                        "Ship2Card8",
                                                                        "Ship2Card9",
                                                                        "Ship2Card10"
                                                                     },
                                                                    new string[] 
                                                                    {
                                                                        "Ship3Card1",
                                                                        "Ship3Card2",
                                                                        "Ship3Card3",
                                                                        "Ship3Card4",
                                                                        "Ship3Card5",
                                                                        "Ship3Card6",
                                                                        "Ship3Card7",
                                                                        "Ship3Card8"
                                                                    }

    };



    #endregion

    #endregion

    #region storyMenu

    public const string CROSSFADE_BLACK = "fade";
    public const string CROSSFADE_WHITE = "fadeInWhite";
    public const string SPIKE_TRANSITION_START = "cover";
    public const string CONVERSATION_ANIM = "story conversation entry";

    #endregion



    #region Player

    public const string OPEN_DARK_PANEL_ANIM = "DarkenPanel";
    public const string OPEN_IDLE_ANIM = "Idle";
    

    #endregion

    #region Enemy

    #endregion

    #region Audios

    public const string CONGRATULATIONS_SOUND = "congratulations";
    public const string ERROR_SOUND = "error";
    public const string BUTTONCLICK_SOUND = "click";
    public const string BACKTOMAINMENU_SOUND = "backtomainmenu";
    public const string TOGGLE_SOUND = "toggle";
    public const string PLAY_SOUND = "play";
    public const string PROGRESS_SOUND = "progress bar";
    public const string PURCHASE_SOUND = "purchase";
    public const string SHIPLEVELUNLOCK_SOUND = "shiplevelunlocked";

    #endregion



}
