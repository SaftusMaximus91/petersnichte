namespace WinFormsApp1;

public class WhiteoutSurvival : JobConfig
{
    public enum SequenceSteps
    {
        FromStartDialogToAllianzTab = 1,

        StartBeastJob,
        CollectExploration,
        FromExplorationBackToCity,
        FromCityToWorldIfNot,
        CollectIntels,
        WaitForPotentialMarching,
        IfStillInSearchWindowThanBackToCity,
        GoToCityFromWorld,
        CollectInfAndSetupQueue,
        CollectMMAndSetupQueue,
        CollectLancerAndSetupQueue,
        DoFCLabTasks,
        CollectIsland,
        CheckForOnlineRewardByMenu,
        CollectOnlineRewardOrStamina,
        CollectFreeRecrutement,
        SwitchAccWhenInMenuDialog,
        CloseMenuIfOpen,
        Wait,
        CollectDailyMission,
        ChooseAlliForschung,
        LeaveAlliTabIfStuck,
        DoMail,
        StartAutoClicker,
        CloseAds,
        BaumLow,
        PetBuffs,
        SammelnZurückholen,
        StartHeal,
        StartForschung,
        LeaveForschung,
        Mine,
        CheckIfResearchRunning,
        IsDunkleSchmiede,
        IsErdlabor,
        IsLandDerTapferen,
        HoehleDerMonster,
        Leuchstein
    }

    public readonly double factor = 0.90;
    public readonly string TemplateDirPath = @"C:\Users\schmi\Pictures\AutoClicker\WhiteoutSurvival_NEW\Templates";
    public bool collectStuff;
    public bool doAlliMob;
    public bool doBearHunt;
    public bool doBeastHuntPolarTerror;
    public bool doTroops;
    public bool doDailyStuff;
    public bool doErkundung;
    public bool doIntels;
    public bool doMails;
    public bool intel;
    public bool littleAccounts;

    public int offSetX;
    public int offSetY;
    public bool petAdventure;
    public SequenceSteps? sequenceStep = null;
    public bool troops;

    //   1920 x 1080


    public WhiteoutSurvival()
    {
        Name = "Whiteout Survival";
        /*    ScreenDefaultStart = new Point(2575, 0);
            ScreenDefaultEnd = new Point(3183, 1080); */
        ScreenDefaultStart = new Point(655, 0);
        ScreenDefaultEnd = new Point(1265, 1080);
        hasOptions = true;
    }

    private List<JobInfo> FillWaitJobs()
    {
        List<JobInfo> jobs = new();
        if (sequenceStep != null)
            return jobs;

        var path = new[] { "alliMob" };
        jobs.Add(new JobInfo("Truppen-Aufgabe 200%", GetTemplatePathWithSubDirectories("alliMob_Troop200.png", path), 0,
            false, false,
            0.965));
        jobs.Add(new JobInfo("Truppen-Aufgabe 120%", GetTemplatePathWithSubDirectories("alliMob_Troop120.png", path), 0,
            false, false,
            0.965));
        return jobs;
    }

    private List<JobInfo> FillLeftClickJobs()
    {
        List<JobInfo> jobs = new();
        offSetX = ScreenDefaultStart.X;
        offSetY = ScreenDefaultStart.Y;


        if (sequenceStep == SequenceSteps.DoMail)
        {
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailIcon.png", "Mail"), 50, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mail.png", "Mail"), 50, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("newMail.png", "Mail"), 50, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_1.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_2.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_3.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_4.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_5.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_6.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_7.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_8.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_9.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_10.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_11.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_12.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_13.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_14.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_15.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_16.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_17.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_18.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_19.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_20.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailresponse.png", "Mail"), 50,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailConfirm.png", "Mail"), 20, true,
                false, factor));
        }

        if (sequenceStep == SequenceSteps.CloseAds)
        {
            var path = new[] { "SequenceSteps", "CloseAds" };
            
            jobs.Add(CreateJobWithAdditionalClicks("2nd_anni_free", "2nd_anni_free.png", path, 1,
                new List<AdditonalClickAction>
                {
                    new(new Point(525, 170), 800)
                }));
            jobs.Add(new JobInfo("Talisman_AD",
                GetTemplatePathWithSubDirectories("Talisman_AD.png", path), 40, true,
                false, factor));
            jobs.Add(new JobInfo("Gouv_AD",
                GetTemplatePathWithSubDirectories("Gouv_AD.png", path), 40, true,
                false, factor));
            jobs.Add(new JobInfo("Ad_Fruehling",
                GetTemplatePathWithSubDirectories("Ad_Fruehling.png", path), 40, true,
                false, factor));
            jobs.Add(new JobInfo("2nd_anni_free",
                GetTemplatePathWithSubDirectories("2nd_anni_close.png", path), 10, true,
                false, factor));
            jobs.Add(new JobInfo("Basat Gratis",
                GetTemplatePathWithSubDirectories("Basar_Gratis.png", path), 1, true,
                false, factor));
            jobs.Add(new JobInfo("Allianz Tab öffnen",
                GetTemplatePathWithSubDirectories("Emporium_AD.png", path), 90, true,
                false, factor));
            jobs.Add(new JobInfo("AdEssenzsteine_leave",
                GetTemplatePathWithSubDirectories("AdEssenzsteine_leave.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Starterpack_ad_Leave", GetTemplatePathWithSubDirectories("StarterPack_AD.png", path),
                90, true, false, factor));
            jobs.Add(new JobInfo("Event_AD", GetTemplatePathWithSubDirectories("Event_AD.png", path), 3, true, false,
                factor, false, 523 + offSetX, 165 + offSetY));
            jobs.Add(new JobInfo("AdBasar_leave",
                GetTemplatePathWithSubDirectories("AdBasar_leave.png", path), 90, true,
                false, factor));            
            jobs.Add(new JobInfo("Ad_Wuerfel",
                GetTemplatePathWithSubDirectories("BrotherInArms_leave.png", path), 90, true,
                false, factor));
            jobs.Add(new JobInfo("AdBasar_leave",
                GetTemplatePathWithSubDirectories("BrotherInArms_leave.png", path), 90, true,
                false, factor));
            jobs.Add(new JobInfo("Willkommen Bildschirm bestätitgen",
                GetTemplatePathWithSubDirectories("Pet19_AD.png", path), 1, true,
                false, 0.88));
            jobs.Add(new JobInfo("Willkommen Bildschirm bestätitgen",
                GetTemplatePathWithSubDirectories("Button_WelcomeBack_Commit.png", path), 1, true,
                false, 0.88));
            jobs.Add(new JobInfo("Willkommen Bildschirm bestätitgen",
                GetTemplatePathWithSubDirectories("Button_WelcomeBack_Commit_2.png", path), 1, true,
                false, 0.88));
            jobs.Add(new JobInfo("Willkommen Bildschirm bestätitgen",
                GetTemplatePathWithSubDirectories("Button_WelcomeBack_Commit_3.png", path), 1, true,
                false, 0.88));
            jobs.Add(new JobInfo("Allianz inaktiv wegklicken",
                GetTemplatePathWithSubDirectories("Icon_Allianz_Inaktiv.png", path), 1, true,
                false, factor));
            jobs.Add(new JobInfo("Zur Stadt wenn Welt",
                GetTemplatePathWithSubDirectories("Icon_City_Menu.png", path), 30, true,
                false, factor));
            jobs.Add(new JobInfo("Zur Stadt wenn Welt",
                GetTemplatePathWithSubDirectories("Jasser_AD.png", path), 30, true,
                false, factor));
            jobs.Add(new JobInfo("LedgyShards_AD", GetTemplatePathWithSubDirectories("LedgyShards_AD.png", path), 3,
                true, false,
                factor, false, 517 + offSetX, 110 + offSetY));
            jobs.Add(new JobInfo("NatAd", GetTemplatePathWithSubDirectories("NatAd_leave.png", path), 3, true, false,
                factor, false, 537 + offSetX, 62 + offSetY));
            jobs.Add(new JobInfo("Close Valentins AD",
                GetTemplatePathWithSubDirectories("Valentins_AD.png", path), 30, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.FromStartDialogToAllianzTab)
        {
            var path = new[] { "SequenceSteps", "FromStartDialogToAllianzTab" };
            jobs.Add(new JobInfo("Allianz Tab öffnen",
                GetTemplatePathWithSubDirectories("Icon_Allianz_Menu.png", path), 90, true,
                false, factor));
            jobs.Add(new JobInfo("Allianz Tab öffnen",
                GetTemplatePathWithSubDirectories("Icon_Allianz_Menu_2.png", path), 90, true,
                false, factor));
        }

        if (sequenceStep == SequenceSteps.StartBeastJob)
        {
            var path = new[] { "SequenceSteps", "StartBeastJob" };
            jobs.Add(new JobInfo("Pet auswählen", GetTemplatePathWithSubDirectories("PetAdventure_chose_pet.png", path),
                7, true, false, factor));
            jobs.Add(new JobInfo("Pet auswählen",
                GetTemplatePathWithSubDirectories("PetAdventure_chose_pet_2.png", path),
                7, true, false, factor));
            jobs.Add(new JobInfo("Freunde Kiste Seite offen",
                GetTemplatePathWithSubDirectories("PetAdventure_collect_friends_return.png", path), 80, true, false,
                factor));
            jobs.Add(CreateJobWithAdditionalClicks("Keine Pet Versuche mehr verfügbar", "PetAdventure_missingPower.png",
                path,
                20, new List<Point> { new(560, 980) }, 0.8));

            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_epic_ready_3.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_epic_2.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_epic_3.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_epic_4.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_rare_3.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_rare_4.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_rare_5.png", path), 80, true, false, factor));


            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectories("PetAdventure_collectReward.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("offene Epic Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_epic.png", path), 80, true, false, 0.94));
            jobs.Add(new JobInfo("offene Ledgy Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_ledgy.png", path), 80, true, false, 0.97));
            jobs.Add(new JobInfo("offene Rare Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_rare_tilt.png", path), 75, true, false, 0.95));
            jobs.Add(new JobInfo("offene Rare Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_rare.png", path), 75, true, false, 0.95));
            jobs.Add(new JobInfo("offene Rare Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_rare_2.png", path), 75, true, false, 0.95));
            jobs.Add(new JobInfo("Mission Fenster verlassen",
                GetTemplatePathWithSubDirectories("PetAdventure_missionDialog_return.png", path), 80, true, false,
                factor));
            jobs.Add(CreateJobWithAdditionalClicks("Keine Pet Versuche mehr verfügbar", "PetAdventure_noTrys.png", path,
                20, new List<Point> { new(560, 190), new(540, 180) }));
            jobs.Add(CreateJobWithAdditionalClicks("Rare-Kiste fertig und nehmen", "PetAdventure_rare_ready.png", path,
                2, 0.97, new List<Point> { new(300, 671) }));
            jobs.Add(CreateJobWithAdditionalClicks("Epic-Kiste fertig und nehmen", "PetAdventure_epic_ready.png", path,
                2, 0.97, new List<Point> { new(300, 671) }));
            jobs.Add(CreateJobWithAdditionalClicks("Epic-Kiste fertig und nehmen", "PetAdventure_epic_ready_2.png",
                path, 2, 0.97, new List<Point> { new(300, 671) }));
            jobs.Add(CreateJobWithAdditionalClicks("Ledgy-Kiste fertig und nehmen", "PetAdventure_ledgy_ready.png",
                path, 2, 0.97, new List<Point> { new(300, 671) }));
            jobs.Add(new JobInfo("offene Rare Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_startFor10Energy.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("offene Rare Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_start_return.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("offene Rare Truhe gefunden",
                GetTemplatePathWithSubDirectories("PetAdventure_take.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("offene Rare Truhe gefunden",
                GetTemplatePathWithSubDirectories("Share.png", path), 5, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.FromCityToWorldIfNot)
        {
            var path = new[] { "SequenceSteps", "FromCityToWorldIfNot" };
            //     jobs.Add(new JobInfo("Öffne Intel Menue", GetTemplatePathWithSubDirectories("Icon_Intel.png", path), 80,true, false, 0.8));
            jobs.Add(new JobInfo("Gehe in Welt", GetTemplatePathWithSubDirectories("Icon_World.png", path), 80, true,
                false, factor));
        }

        if (sequenceStep == SequenceSteps.CollectIntels)
        {
            var path = new[] { "SequenceSteps", "CollectIntels" };
            jobs.Add(new JobInfo("Raus aus Fenster wenn keine Truppen",
                GetTemplatePathWithSubDirectories("Intel_noTroops.png", path), 99, true, false, factor, false,
                32 + offSetX, 34 + offSetY));
            jobs.Add(new JobInfo("Raus aus Fenster wenn keine Truppen",
                GetTemplatePathWithSubDirectories("Intel_noTroops_2.png", path), 99, true, false, factor, false,
                32 + offSetX, 34 + offSetY));
            jobs.Add(new JobInfo("Raus aus Fenster wenn keine Truppen",
                GetTemplatePathWithSubDirectories("Intel_noTroops_3.png", path), 99, true, false, factor, false,
                32 + offSetX, 34 + offSetY));


            jobs.Add(new JobInfo("Raus aus Fenster wenn kein Marsch",
                GetTemplatePathWithSubDirectories("Intel_noMarsch.png", path), 99, true, false, factor, false,
                537 + offSetX, 285 + offSetY));
            jobs.Add(new JobInfo("In Intel Fenster gehen", GetTemplatePathWithSubDirectories("Icon_Intel.png", path),
                99, true, false, factor));
            jobs.Add(new JobInfo("Keine Truppen trotzdem senden",
                GetTemplatePathWithSubDirectories("Intel_einsetzen_falscheTruppen.png", path), 99, true, false,
                factor));
            jobs.Add(new JobInfo("Upgraden", GetTemplatePathWithSubDirectories("Intel_Upgrade.png", path), 99, true,
                false, factor));
            jobs.Add(new JobInfo("In Intel Fenster nehmen",
                GetTemplatePathWithSubDirectories("Intel_take_button.png", path), 50, true, false, factor));
            var beastfactor = 0.97;
            //GREY
            jobs.Add(new JobInfo("Epic Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_grey_beast.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Epic Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_grey_beast_ready.png", path), 90, true, false, beastfactor));
            //GREEN
            jobs.Add(new JobInfo("Epic Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_green_beast.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Epic Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_green_beast_ready.png", path), 90, true, false, beastfactor));
            //EPIC BEAST
            jobs.Add(new JobInfo("Epic Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_purple_beast.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Epic Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_purple_beast_ready.png", path), 90, true, false, beastfactor));
            //BUE
            jobs.Add(new JobInfo("Epic Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_blue_beast.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Epic Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_blue_beast_ready.png", path), 90, true, false, beastfactor));
            //GREEN GATHER
            jobs.Add(new JobInfo("Epic Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_green_gather_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Epic Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_green_gather.png", path), 90, true, false, factor));
            //GREEN FIGHT
            jobs.Add(new JobInfo("Epic Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("intel_green_fight.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Epic Beast Intel fertig",
                GetTemplatePathWithSubDirectories("intel_green_fight_ready.png", path), 90, true, false, factor));


            jobs.Add(new JobInfo("Rare Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_rare_beast.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Rare Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_rare_beast_ready.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Epic Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_epic_beast.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Epic Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_epic_beast_ready.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Ledgy Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_ledgy_beast.png", path), 90, true, false, beastfactor));
            jobs.Add(new JobInfo("Ledgy Beast Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_ledgy_beast_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("SBeat Beast Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_ledgy_sbeast.png", path), 90, true, false,
                factor)); //TODO bei Event
            jobs.Add(new JobInfo("SBeat Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_ledgy_sbeast_ready.png", path), 90, true, false,
                factor)); //TODO bei Event
            jobs.Add(new JobInfo("Rare Fight Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_rare_fight.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Rare Fight Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_rare_fight_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Epic Fight Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_epic_fight.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Epic Fight Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_epic_fight_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Ledgy Fight Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_ledgy_fight.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Ledgy Fight Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_ledgy_fight_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Rare Fight Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_rare_gather.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Rare Fight Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_rare_gather_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Epic Fight Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_epic_gather.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Epic Fight Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_epic_gather_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Ledgy Fight Intel gefunden",
                GetTemplatePathWithSubDirectories("Intel_ledgy_gather.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Ledgy Fight Intel fertig",
                GetTemplatePathWithSubDirectories("Intel_ledgy_gather_ready.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Intel Belohnung nehmen",
                GetTemplatePathWithSubDirectories("Intel_gather_reward.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Intel Belohnung nehmen",
                GetTemplatePathWithSubDirectories("Intel_gather_reward_2.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Intel ansehen", GetTemplatePathWithSubDirectories("Intel_ansehen.png", path), 90,
                true, false, factor));
            jobs.Add(new JobInfo("Intel angreifen", GetTemplatePathWithSubDirectories("Intel_attack.png", path), 90,
                true, false, factor));
            jobs.Add(new JobInfo("Intel starten für 8 Ausdauer",
                GetTemplatePathWithSubDirectories("Intel_start_8stam.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Intel starten für 10 Ausdauer",
                GetTemplatePathWithSubDirectories("Intel_start_10stam.png", path), 90, true, false, factor));
            jobs.Add(new JobInfo("Intel retten für 12 Ausdauer",
                GetTemplatePathWithSubDirectories("Intel_rescue_12stam.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Intel erkundgen", GetTemplatePathWithSubDirectories("Intel_discover.png", path), 90,
                true, false, factor));
            jobs.Add(new JobInfo("Intel Sieg", GetTemplatePathWithSubDirectories("Intel_fight_victory.png", path), 90,
                true, false, factor));
            jobs.Add(new JobInfo("Intel kämpfen", GetTemplatePathWithSubDirectories("Intel_fight.png", path), 90, true,
                false, factor));
            jobs.Add(new JobInfo("Intel kämpfen", GetTemplatePathWithSubDirectories("Intel_fight_2.png", path), 90,
                true, false, factor));
            jobs.Add(new JobInfo("Intel kämpfen", GetTemplatePathWithSubDirectories("Intel_fight_3.png", path), 90,
                true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("Intel marschieren wegklicken", "Intel_marschieren.png", path, 10,
                new List<Point> { new(253, 958) }));
            jobs.Add(CreateJobWithAdditionalClicks("Intel marschieren wegklicken", "Intel_marschieren_2.png", path, 10,
                new List<Point> { new(253, 958) }));
        }

        if (sequenceStep == SequenceSteps.CollectExploration)
        {
            var path = new[] { "SequenceSteps", "CollectExploration" };
            jobs.Add(CreateJobWithAdditionalClicks("Erkundung öffnen", "Exploration_Icon.png", path, 80,
                new List<AdditonalClickAction>
                {
                    new(new Point(525, 780), 800), new(new Point(315, 780), 800), new(new Point(315, 780), 800),
                    new(new Point(30, 35), 800)
                }));
        }

        if (sequenceStep == SequenceSteps.FromExplorationBackToCity)
        {
            var path = new[] { "SequenceSteps", "FromExplorationBackToCity" };
            jobs.Add(new JobInfo("Exploration zurück", GetTemplatePathWithSubDirectories("Exploration_back.png", path),
                99, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.CollectInfAndSetupQueue)
        {
            var path = new[] { "SequenceSteps", "CollectInfAndSetupQueue" };
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_lvl3.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_lvl4.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_lvl5.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_lvl6.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_lvl7.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_lvl8.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_Big_FC2.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Inf_ready_Big_FC8.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Train_icon.png", path, 1,
                new List<Point> { new(402, 727), new(455, 1022), new(43, 26) }));
        }

        if (sequenceStep == SequenceSteps.CollectMMAndSetupQueue)
        {
            var path = new[] { "SequenceSteps", "CollectMMAndSetupQueue" };
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_lvl3.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_lvl4.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_lvl4_2.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_lvl5.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_lvl6.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_lvl7.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_lvl8.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_Big_FC2.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("MM fertig und abholen", "MM_ready_Big_FC7.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Train_icon.png", path, 1,
                new List<Point> { new(402, 727), new(455, 1022), new(43, 26) }));
        }

        if (sequenceStep == SequenceSteps.CollectLancerAndSetupQueue)
        {
            var path = new[] { "SequenceSteps", "CollectLancerAndSetupQueue" };
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_lvl3.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_lvl4.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_lvl5.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_lvl6.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_lvl7.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_lvl8.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_Big_FC2.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Lancer fertig und abholen", "Lancer_ready_Big_FC7.png", path, 1,
                new List<Point> { new(293, 577) }));
            jobs.Add(CreateJobWithAdditionalClicks("Inf fertig und abholen", "Train_icon.png", path, 1,
                new List<Point> { new(402, 727), new(455, 1022), new(43, 26) }));
        }

        if (sequenceStep == SequenceSteps.CheckForOnlineRewardByMenu)
        {
            var path = new[] { "SequenceSteps", "CheckForOnlineRewardByMenu" };
            jobs.Add(CreateJobWithAdditionalClicks("1. Onlinebelohnung holen.. oder nicht", "Menu_OnlineBelohnung.png",
                path, 1, new List<AdditonalClickAction> { new(new Point(117, 470), 2000) }));
            jobs.Add(CreateJobWithAdditionalClicks("1. Onlinebelohnung holen.. oder nicht",
                "Menu_OnlineBelohnung_2.png", path, 1,
                new List<AdditonalClickAction> { new(new Point(117, 470), 2000) }));
            jobs.Add(CreateJobWithAdditionalClicks("1. Onlinebelohnung holen.. oder nicht",
                "Menu_OnlineBelohnung_Lancer_9.png", path, 1,
                new List<AdditonalClickAction> { new(new Point(117, 470), 2000) }));
        }

        if (sequenceStep == SequenceSteps.DoFCLabTasks)
        {
            var path = new[] { "SequenceSteps", "DoFCLabTasks" };
            jobs.Add(new JobInfo("Super FC Ready",
                GetTemplatePathWithSubDirectories("FCLab_superCrytal_ready.png", path), 3, true, false, factor, false,
                550 + offSetX, 983 + offSetY));
            jobs.Add(new JobInfo("FC veredeln", GetTemplatePathWithSubDirectories("FCLab_refine.png", path), 80, true,
                false, factor));
            jobs.Add(new JobInfo("FC veredeln", GetTemplatePathWithSubDirectories("FCLab_refine_2.png", path), 80, true,
                false, factor));
            jobs.Add(new JobInfo("FC veredeln", GetTemplatePathWithSubDirectories("FCLab_refine_3.png", path), 80, true,
                false, factor));
        }

        if (sequenceStep == SequenceSteps.CollectOnlineRewardOrStamina)
        {
            var path = new[] { "SequenceSteps", "CollectOnlineRewardOrStamina" };
            jobs.Add(new JobInfo("Online reward verfügbar",
                GetTemplatePathWithSubDirectories("OnlineReward_ready_icon.png", path), 80, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("Online reward verfügbar",
                "OnlineReward_ready_icon.png", path, 80,
                new List<AdditonalClickAction> { new(new Point(300, 750), 2500) ,new(new Point(300, 750), 2500)}));
        
            jobs.Add(new JobInfo("Online Belohnung fenster schließen",
                GetTemplatePathWithSubDirectories("OnlineReward_return.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Online Belohnung fenster schließen",
                GetTemplatePathWithSubDirectories("OnlineReward_return_2.png", path), 79, true, false, factor));
            jobs.Add(new JobInfo("Online Belohnung fenster schließen",
                GetTemplatePathWithSubDirectories("OnlineReward_return_3.png", path), 79, true, false, factor));
            jobs.Add(new JobInfo("Ausdauer verfuegbar",
                GetTemplatePathWithSubDirectories("StaminaCollect_icon.png", path), 80, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("Ausdauer sammeln Fenster schließen",
                "StaminaCollect_take.png", path, 80,
                new List<AdditonalClickAction> { new(new Point(15, 590), 2000)}));
        }

        if (sequenceStep == SequenceSteps.PetBuffs)
        {
            var path = new[] { "SequenceSteps", "PetBuffs" };
            jobs.Add(CreateJobWithAdditionalClicks("Verwenden klicken",
                "wolf.png", path, 5,
                new List<AdditonalClickAction> { new(new Point(300, 930), 1500), new(new Point(300, 930), 1500), new(new Point(300, 930), 1500) }));
            jobs.Add(CreateJobWithAdditionalClicks("Verwenden klicken",
                "tapir.png", path, 5,
                new List<AdditonalClickAction> { new(new Point(300, 930), 1500), new(new Point(300, 930), 1500), new(new Point(300, 930), 1500) }));
            jobs.Add(CreateJobWithAdditionalClicks("Verwenden klicken",
                "ochse.png", path, 5,
                new List<AdditonalClickAction> { new(new Point(300, 930), 1500), new(new Point(300, 930), 1500), new(new Point(300, 930), 1500) }));
           
        }

        if (sequenceStep == SequenceSteps.SammelnZurückholen)
        {
            var path = new[] { "SequenceSteps", "SammelnZurückholen" };
            jobs.Add(CreateJobWithAdditionalClicks("Marsch zurück",
                "marsch_zurück.png", path, 5,
                new List<AdditonalClickAction> { new(new Point(450, 666), 1500)}));
            jobs.Add(new JobInfo("Marsch zurück bestätigen", GetTemplatePathWithSubDirectories("confirm.png", path), 80,true, false, factor));

        }

        if (sequenceStep == SequenceSteps.StartHeal)
        {
            var path = new[] { "SequenceSteps", "StartHeal" };
            jobs.Add(CreateJobWithAdditionalClicks("Marsch zurück",
                "Heal.png", path, 5,
                new List<AdditonalClickAction> { new(new Point(500, 1015), 1500),new(new Point(300, 483), 1500)}));
        }

        if (sequenceStep == SequenceSteps.CloseMenuIfOpen)
        {
            var path = new[] { "SequenceSteps", "CloseMenuIfOpen" };
            jobs.Add(new JobInfo("Rekrutierung zurück", GetTemplatePathWithSubDirectories("Menu_Back.png", path), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Rekrutierung zurück", GetTemplatePathWithSubDirectories("Menu_Back_2.png", path), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Rekrutierung zurück",
                GetTemplatePathWithSubDirectories("Menu_Back_Lancer_lvl3.png", path), 80, true, false, factor));
            jobs.Add(new JobInfo("Rekrutierung zurück",
                GetTemplatePathWithSubDirectories("Menu_Back_Lager_lvl1.png", path), 80, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.StartAutoClicker)
        {
            var path = new[] { "SequenceSteps", "StartAutoClicker" };
            jobs.Add(new JobInfo("Rekrutierung zurück",
                GetTemplatePathWithSubDirectories("Activate_SingleClick.png", path), 80,
                true, false, factor));
        }

        if (sequenceStep == SequenceSteps.CollectFreeRecrutement)
        {
            var path = new[] { "SequenceSteps", "CollectFreeRecrutement" };
            jobs.Add(new JobInfo("Rekrutierung zurück", GetTemplatePathWithSubDirectories("Menu_Back.png", path), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Rekrutierung zurück", GetTemplatePathWithSubDirectories("Menu_Back_2.png", path), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Erweiterte Rekrutierung ready",
                GetTemplatePathWithSubDirectories("Menu_AdvanceHeroReady.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Erweiterte Rekrutierung ready",
                GetTemplatePathWithSubDirectories("Menu_AdvanceHeroReady_2.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Rekrutierung ready", GetTemplatePathWithSubDirectories("Menu_HeroReady.png", path), 1,
                true, false, factor));
            jobs.Add(new JobInfo("Rekrutierung ready", GetTemplatePathWithSubDirectories("Menu_HeroReady_2.png", path),
                1, true, false, factor));
            jobs.Add(new JobInfo("Rekrutierung zurück wenn Held",
                GetTemplatePathWithSubDirectories("Recrutement_rare_heroExists.png", path), 10, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("Rekrutierung frei Button", "Recrute_free_button.png", path, 80,
                new List<AdditonalClickAction>
                {
                    new(new Point(70, 120), 1000), new(new Point(70, 120), 1000), new(new Point(70, 120), 1000),
                    new(new Point(70, 120), 3000)
                }));

            jobs.Add(CreateJobWithAdditionalClicks("Rekrutierung normal nehmen", "Recrute_normal_take.png", path, 90,
                new List<AdditonalClickAction>
                    { new(new Point(50, 343), 1000), new(new Point(50, 343), 1000), new(new Point(50, 343), 1000) }));
            jobs.Add(CreateJobWithAdditionalClicks("Rekrutierung normal nehmen", "Recrute_normal_take_2.png", path, 90,
                new List<AdditonalClickAction>
                    { new(new Point(50, 343), 1000), new(new Point(50, 343), 1000), new(new Point(50, 343), 1000) }));
            jobs.Add(CreateJobWithAdditionalClicks("Rekrutierung normal nehmen", "Recrute_advanced_take.png", path, 90,
                new List<AdditonalClickAction>
                    { new(new Point(50, 343), 1000), new(new Point(50, 343), 1000), new(new Point(50, 343), 1000) }));

            jobs.Add(new JobInfo("Erweiterte Rekrutierung nehmen",
                GetTemplatePathWithSubDirectories("Recrute_back_icon.png", path), 99, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("1. Onlinebelohnung holen.. oder nicht", "Menu_OnlineBelohnung.png",
                path, 1, new List<AdditonalClickAction> { new(new Point(117, 470), 2000) }));
            jobs.Add(CreateJobWithAdditionalClicks("1. Onlinebelohnung holen.. oder nicht",
                "Menu_OnlineBelohnung_2.png", path, 1,
                new List<AdditonalClickAction> { new(new Point(117, 470), 2000) }));
            jobs.Add(CreateJobWithAdditionalClicks("1. Onlinebelohnung holen.. oder nicht",
                "Menu_OnlineBelohnung_Lancer_9.png", path, 1,
                new List<AdditonalClickAction> { new(new Point(117, 470), 2000) }));
        }

        if (sequenceStep == SequenceSteps.LeaveAlliTabIfStuck)
        {
            var path = new[] { "SequenceSteps", "LeaveAlliTabIfStuck" };
            jobs.Add(new JobInfo("Intel zurück wenn noch offen",
                GetTemplatePathWithSubDirectories("Leave.png", path), 1, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.CheckIfResearchRunning)
        {
            var path = new[] { "SequenceSteps", "CheckIfResearchRunning" };
            
            jobs.Add(new JobInfo("Beschleunigen_Detail", GetTemplatePathWithSubDirectories("Beschleunigen_Detail.png", path), 3, true, false,
                factor, false, 200 + offSetX, 35 + offSetY)); 
            
            jobs.Add(new JobInfo("Beschleunigen_Haupt", GetTemplatePathWithSubDirectories("Beschleunigen_Haupt.png", path), 3, true, false,
                factor, false, 40 + offSetX, 40 + offSetY));            
        }


        if (sequenceStep == SequenceSteps.Mine)
        {
            var path = new[] { "SequenceSteps", "Mine" };
            jobs.Add(CreateJobWithAdditionalClicks("Herausfordern",
                "Herausfordern.png", path, 2,
                new List<AdditonalClickAction>
                {
                    new(new Point(500, 150), 2500)
                }));   
            jobs.Add(CreateJobWithAdditionalClicks("NichtHerausfordern",
                "NichtHerausfordern.png", path, 1,
                new List<AdditonalClickAction>
                {
                    new(new Point(40, 30), 2500)
                })); 
            jobs.Add(CreateJobWithAdditionalClicks("Erneut",
                "Erneut.png", path, 2,
                new List<AdditonalClickAction>
                {
                    new(new Point(500, 150), 2500)
                }));    
            jobs.Add(CreateJobWithAdditionalClicks("Erneut",
                "Erneut2.png", path, 2,
                new List<AdditonalClickAction>
                {
                    new(new Point(500, 150), 2500)
                }));  
            jobs.Add(CreateJobWithAdditionalClicks("SchnelleHerausforderung",
                "SchnelleHerausforderung.png", path, 2,
                new List<AdditonalClickAction>
                {
                    new(new Point(120, 700), 2500),
                    new(new Point(550, 990), 2500)
                }));    
            jobs.Add(CreateJobWithAdditionalClicks("SchnelleHerausforderung2",
                "SchnelleHerausforderung2.png", path, 2,
                new List<AdditonalClickAction>
                {
                    new(new Point(120, 700), 2500),
                    new(new Point(550, 990), 2500)
                })); 
            
            jobs.Add(CreateJobWithAdditionalClicks("Weiter",
                "Weiter.png", path, 2,
                new List<AdditonalClickAction>
                {
                    new(new Point(500, 150), 2500)
                }));   
            
            jobs.Add(new JobInfo("Menu_Mine_ready ready clicken",GetTemplatePathWithSubDirectories("Menu_Mine_ready.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("Menu_Mine_ready_2 ready clicken",GetTemplatePathWithSubDirectories("Menu_Mine_ready_2.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("Kapitel ready clicken",GetTemplatePathWithSubDirectories("Kapitel.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("Erdlabor offen 0",GetTemplatePathWithSubDirectories("Einfordern.png", path), 20, true, false, factor));
            jobs.Add(new JobInfo("Erdlabor offen 0",GetTemplatePathWithSubDirectories("Ueberfall.png", path), 20, true, false, factor));
            jobs.Add(new JobInfo("Einsetze offen 1",GetTemplatePathWithSubDirectories("Einsetze.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("Einsetze offen 1",GetTemplatePathWithSubDirectories("Einsetzen.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("Einsetze offen 1",GetTemplatePathWithSubDirectories("SkipIntro.png", path), 1, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("SchnellerEinsatz",
                "SchnellerEinsatz.png", path, 1,
                new List<AdditonalClickAction>
                {
                    new(new Point(450, 1000), 1000)
                })); 
            
            
            jobs.Add(new JobInfo("NeueVersuche offen 2",GetTemplatePathWithSubDirectories("NeueVersuche.png", path), 1, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("NeueVersuche2",
                "NeueVersuche2.png", path, 2,
                new List<AdditonalClickAction>
                {
                    new(new Point(30, 35), 2500)
                })); 
        }
        
        if (sequenceStep == SequenceSteps.Leuchstein)
        {
            var path = new[] { "SequenceSteps", "Mine" };
            jobs.Add(new JobInfo("LandDerTapferen offen 0",GetTemplatePathWithSubDirectories("Leuchstein_Halb.png", path), 2, true, false, factor));
        }
        
        if (sequenceStep == SequenceSteps.HoehleDerMonster)
        {
            var path = new[] { "SequenceSteps", "Mine" };
            jobs.Add(new JobInfo("LandDerTapferen offen 0",GetTemplatePathWithSubDirectories("Monster_Halb.png", path), 2, true, false, factor));
        }
        
        if (sequenceStep == SequenceSteps.IsLandDerTapferen)
        {
            var path = new[] { "SequenceSteps", "Mine" };
            jobs.Add(new JobInfo("LandDerTapferen offen 0",GetTemplatePathWithSubDirectories("LandDerTapferen_Halb.png", path), 2, true, false, factor));
        }
        
        if (sequenceStep == SequenceSteps.IsErdlabor)
        {
            var path = new[] { "SequenceSteps", "Mine" };
            jobs.Add(new JobInfo("Erdlabor offen 0",GetTemplatePathWithSubDirectories("Erdlabor.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("Erdlabor offen 0",GetTemplatePathWithSubDirectories("Erdlabor_Halb.png", path), 2, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.IsDunkleSchmiede)
        {
            var path = new[] { "SequenceSteps", "Mine" };
            jobs.Add(new JobInfo("DunkleSchmiede offen 2",GetTemplatePathWithSubDirectories("DunkleSchmiede.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("DunkleSchmiede_New offen 2",GetTemplatePathWithSubDirectories("DunkleSchmiede_New.png", path), 2, true, false, factor));
            jobs.Add(new JobInfo("DunkleSchmiede_Halb offen 2",GetTemplatePathWithSubDirectories("DunkleSchmiede_Halb.png", path), 2, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.StartForschung)
        {
            var path = new[] { "SequenceSteps", "StartForschung" };
            jobs.Add(new JobInfo("Zu Forschung wechseln",GetTemplatePathWithSubDirectories("Forschung_ready.png", path), 2, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("Zu Forschung wechseln",
                "Menu_Forschung_ready.png", path, 1,
                new List<AdditonalClickAction>
                {
                    new(new Point(290, 840), 2000)
                }));              
            jobs.Add(new JobInfo("Forschung ready clicken",GetTemplatePathWithSubDirectories("Hilfe.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Forschung ready clicken",GetTemplatePathWithSubDirectories("Forschung.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Forschung offen 0",GetTemplatePathWithSubDirectories("forschung_0.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Forschung offen 1",GetTemplatePathWithSubDirectories("forschung_1.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Forschung offen 2",GetTemplatePathWithSubDirectories("forschung_2.png", path), 1, true, false, factor));
        }
        
        if (sequenceStep == SequenceSteps.LeaveForschung)
        {
            var path = new[] { "SequenceSteps", "LeaveForschung" };
            jobs.Add(new JobInfo("Forschung verlassen",GetTemplatePathWithSubDirectories("leave.png", path), 1, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.BaumLow)
        {
            var path = new[] { "SequenceSteps", "BaumLow" };
            jobs.Add(CreateJobWithAdditionalClicks("1. Onlinebelohnung holen.. oder nicht",
                "Menu_Baum_ready.png", path, 1,
                new List<AdditonalClickAction>
                {
                    new(new Point(300, 414), 4000),
                    new(new Point(500, 375), 1000),
                    new(new Point(400, 260), 1000),
                    new(new Point(30, 28), 1000)
                }));
        }

        if (sequenceStep == SequenceSteps.ChooseAlliForschung)
        {
            var path = new[] { "SequenceSteps", "ChooseAlliForschung" };
            jobs.Add(new JobInfo("Intel zurück wenn noch offen",
                GetTemplatePathWithSubDirectories("AlliTabTechnologie.png", path), 1, true, false, factor));

            jobs.Add(CreateJobWithAdditionalClicks("GreenThumb",
                "GreenThumb.png", path, 1,
                new List<AdditonalClickAction>
                {
                    new(new Point(400, 860), 1000),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(400, 860), 250),
                    new(new Point(560, 170), 250),
                    new(new Point(30, 30), 250),
                    new(new Point(30, 30), 250),
                }));
        }

        if (sequenceStep == SequenceSteps.WaitForPotentialMarching)
        {
            var path = new[] { "SequenceSteps", "WaitForPotentialMarching" };
            jobs.Add(new JobInfo("Intel zurück wenn noch offen",
                GetTemplatePathWithSubDirectories("Intel_leave.png", path), 99, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.Wait)
        {
            var path = new[] { "SequenceSteps", "Wait" };
        }

        if (sequenceStep == SequenceSteps.CollectIsland)
        {
            var path = new[] { "SequenceSteps", "CollectIsland" };
            jobs.Add(new JobInfo("Menu Baum voll", GetTemplatePathWithSubDirectories("Menu_baum_voll.png", path), 80,
                true, false, 0.95));
            jobs.Add(CreateJobWithAdditionalClicks("Baum Platzhalter Klick dann Sammel-Klicks", "Baum_Platzhalter.png",
                path, 80, new List<Point> { new(312, 304), new(100, 138), new(36, 24) }));
        }

        if (sequenceStep == SequenceSteps.SwitchAccWhenInMenuDialog)
        {
            var path = new[] { "SequenceSteps", "SwitchAccWhenInMenuDialog" };
            jobs.Add(new JobInfo("Acc wechsel bestätigen",
                GetTemplatePathWithSubDirectories("Button_Switch_Acc.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Wechseln zu Feta", GetTemplatePathWithSubDirectories("Switch_Acc_ToFeta.png", path),
                1, true, false, factor));
            jobs.Add(new JobInfo("Wechseln zu PaketenReter",
                GetTemplatePathWithSubDirectories("Switch_Acc_ToPaketenReter.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Wechseln zu PaketenReter",
                GetTemplatePathWithSubDirectories("Switch_Acc_ToPaketenReter_2.png", path), 1, true, false, factor));
            jobs.Add(new JobInfo("Wechseln zu PaketenReter", GetTemplatePathWithSubDirectories("Frau.png", path), 81,
                true, false, factor));
            jobs.Add(new JobInfo("Wechseln zu PaketenReter", GetTemplatePathWithSubDirectories("Bahiti.png", path), 81,
                true, false, factor));

            jobs.Add(new JobInfo("Super FC Ready", GetTemplatePathWithSubDirectories("SwitchAcc.png", path), 3, true,
                false, factor, false, 500 + offSetX, 450 + offSetY));
            jobs.Add(new JobInfo("Super FC Ready", GetTemplatePathWithSubDirectories("SwitchAcc_1.png", path), 3, true,
                false, factor, false, 500 + offSetX, 580 + offSetY));
        }

        if (sequenceStep == SequenceSteps.GoToCityFromWorld)
        {
            var path = new[] { "SequenceSteps", "GoToCityFromWorld" };
            jobs.Add(new JobInfo("Intel zurück wenn noch offen",
                GetTemplatePathWithSubDirectories("Icon_City_Menu.png", path), 80, true, false, factor));
        }

        if (sequenceStep == SequenceSteps.IfStillInSearchWindowThanBackToCity)
        {
            var path = new[] { "SequenceSteps", "IfStillInSearchWindowThanBackToCity" };
            jobs.Add(new JobInfo("In Stadt wenn Welt", GetTemplatePathWithSubDirectories("Icon_City_Menu.png", path), 4,
                true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("Suchfenster schließen für RSS",
                "SearchWindow_rss_search_button.png", path, 3, new List<Point> { new(355, 586) }));
        }


        if (sequenceStep == SequenceSteps.CollectDailyMission)
        {
            var path = new[] { "SequenceSteps", "CollectDailyMission" };
            jobs.Add(new JobInfo("DailyMission nehmen klein", GetTemplatePathWithSubDirectories("take_small.png", path),
                70, true, false, factor));
            jobs.Add(new JobInfo("DailyMission nehmen klein", GetTemplatePathWithSubDirectories("rewards.png", path),
                70, true, false, factor));
            jobs.Add(CreateJobWithAdditionalClicks("DailyMission nehmen groß", "take_large.png", path, 1,
                new List<Point> { new(100, 100), new(100, 100), new(100, 100) }));
        }

        if (sequenceStep.HasValue) return jobs;


        if (doAlliMob) RefreshAllyMobForTroopTask(jobs);

        if (doBearHunt)
        {
            //Loopen
            var job2 = new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory(Path.Combine("Player", "0.png"), "Bear"), 20,
                true,
                true, factor);
            var clickActions2 = new List<AdditonalClickAction>();
            clickActions2.Add(new AdditonalClickAction(new Point(3050, 434)));
            clickActions2.Add(new AdditonalClickAction(new Point(3040, 131))); //Trupp 8
            clickActions2.Add(new AdditonalClickAction(new Point(3020, 975)));
            clickActions2.Add(new AdditonalClickAction(new Point(2610, 65)));
            job2.ClickActions = clickActions2;
            jobs.Add(job2);


            /*
            jobs.Add(new JobInfo("Bär Event", GetTemplatePath("bear_icon.png"), 90, true, false, factor));
            jobs.Add(new JobInfo("Rally starten gegen Bär", GetTemplatePath("bear_rally.png"), 90, true, false,
                factor));
            jobs.Add(new JobInfo("Rally starten gegen Bär bestaetigen", GetTemplatePath("bear_rallyStarten.png"),
                90, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePath("bear_einsetzen.png"), 90, true,
                false, factor));
                */
        }

        if (doIntels)
        {
            jobs.Add(new JobInfo("Bär Event", GetTemplatePathWithSubDirectory("intel_noTroops.png", "intel"), 99, true,
                false, factor, false, 2612, 64));

            jobs.Add(new JobInfo("Bär Event", GetTemplatePathWithSubDirectory("intel_icon.png", "intel"), 99, true,
                false, factor));

            jobs.Add(new JobInfo("Rally starten gegen Bär",
                GetTemplatePathWithSubDirectory("intel_epic_beast.png", "intel"), 90, true,
                false, factor));

            jobs.Add(new JobInfo("Rally starten gegen Bär bestaetigen",
                GetTemplatePathWithSubDirectory("intel_epic_fight.png", "intel"),
                90, true, false, factor));
            jobs.Add(new JobInfo("Rally starten gegen Bär bestaetigen",
                GetTemplatePathWithSubDirectory("intel_ledgy_fight.png", "intel"),
                90, true, false, factor));
            jobs.Add(new JobInfo("Rally starten gegen Bär bestaetigen",
                GetTemplatePathWithSubDirectory("intel_ledgy_fight_ready.png", "intel"), 90, true, false, factor));
            jobs.Add(new JobInfo("Rally starten gegen Bär bestaetigen",
                GetTemplatePathWithSubDirectory("intel_ledgy_gather.png", "intel"),
                90, true, false, factor));
            jobs.Add(new JobInfo("Rally starten gegen Bär bestaetigen",
                GetTemplatePathWithSubDirectory("intel_ledgy_beast_ready.png", "intel"),
                90, true, false, factor));
            jobs.Add(new JobInfo("Rally starten gegen Bär bestaetigen",
                GetTemplatePathWithSubDirectory("intel_ledgy_sbeast.png", "intel"),
                90, true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_ansehen.png", "intel"), 90, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_attack.png", "intel"), 90, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_start_8stam.png", "intel"), 90,
                true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_start_10stam.png", "intel"), 90,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_discover.png", "intel"), 90,
                true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_fight_victory.png", "intel"), 90,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_fight.png", "intel"), 90, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_rescue_12stam.png", "intel"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_gather_reward.png", "intel"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("intel_nehmen.png", "intel"), 80,
                true, false, factor));
        }

        if (doTroops)
        {
            //UPGRADE X zu XI
            /* 
            var speedUpTroop = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("X.png", "doTroops"),
                99, true,
                false,  factor,false);
            
            var speedUpTroopActions = new List<AdditonalClickAction>();
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(1212, 550)));
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(1105, 756),400));
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(1100, 1016),541));
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(990, 754),612));
            speedUpTroop.ClickActions = speedUpTroopActions;
            jobs.Add(speedUpTroop);  
            */
            //AUSBILDEN XI
            /* */
            var speedUpTroop = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("XI.png", "doTroops"),
                99, true,
                false,  factor,false);
            
            var speedUpTroopActions = new List<AdditonalClickAction>();
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(1100, 1016)));
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(1100, 1016)));
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(990, 754)));
            speedUpTroop.ClickActions = speedUpTroopActions;
            jobs.Add(speedUpTroop);  
            
            //AUSBILDEN X
            /*  
            var speedUpTroop = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("X.png", "doTroops"),
                99, true,
                false,  factor,false);

            var speedUpTroopActions = new List<AdditonalClickAction>();
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(1100, 1016)));
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(1100, 1016),541));
            speedUpTroopActions.Add(new AdditonalClickAction(new Point(990, 754),612));
            speedUpTroop.ClickActions = speedUpTroopActions;
            jobs.Add(speedUpTroop);
             */
         
        }

        if (doBeastHuntPolarTerror)
        {
            var checkRallyGestartet = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("rallygestartet.png", "BeastHunt"),
                1, true,
                false,  factor,false);


            var checkRallyGestartetActions = new List<AdditonalClickAction>();
            checkRallyGestartetActions.Add(new AdditonalClickAction(new Point(695, 35)));
            checkRallyGestartetActions.Add(new AdditonalClickAction(new Point(695, 35)));
            checkRallyGestartet.ClickActions = checkRallyGestartetActions;
            jobs.Add(checkRallyGestartet);  
            
            
            
            var checkMarschvoll = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("search_icon_2.png", "BeastHunt"),
                1, true,
                false,  0.97,false);
             var checkMarschvoll2 = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("search_icon_marsch5.png", "BeastHunt"),
                            1, true,
                            false,  0.97,false);


            var checkMarschvollActions = new List<AdditonalClickAction>();
            checkMarschvollActions.Add(new AdditonalClickAction(new Point(100, 683)));
        //    checkMarschvoll.ClickActions = checkMarschvollActions;
            jobs.Add(checkMarschvoll);           
            jobs.Add(checkMarschvoll2);           
                
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("search.png", "BeastHunt"), 80,
                true, false, factor));

            var startRally2 = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("rally_cost.png", "BeastHunt"),
                50, true,
                false, factor);


            var clickActions2 = new List<AdditonalClickAction>();
            clickActions2.Add(new AdditonalClickAction(new Point(955, 683)));
            startRally2.ClickActions = clickActions2;
            jobs.Add(startRally2);

            var startRally = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("check.png", "BeastHunt"), 99,
                true,
                false, factor);


            var clickActions = new List<AdditonalClickAction>();

            clickActions.Add(new AdditonalClickAction(new Point(1140, 133)));
            clickActions.Add(new AdditonalClickAction(new Point(1125, 986)));
            startRally.ClickActions = clickActions;
            jobs.Add(startRally);
            
            
            var startRallyByQueue = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("search_icon.png", "BeastHunt"), 90,
                true,
                false, factor,true);

            var clickActionsyByQueue = new List<AdditonalClickAction>();

            clickActionsyByQueue.Add(new AdditonalClickAction(new Point(871, 770)));
            startRallyByQueue.ClickActions = clickActionsyByQueue;
            jobs.Add(startRallyByQueue);
            
            var chooseTroop = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("Troop_1.png", "BeastHunt"), 90,
                true,
                false, factor,false);

            var clickActionsTroop = new List<AdditonalClickAction>();
       //Starten der Rally mit nur 1 Einheit
            clickActionsTroop.Add(new AdditonalClickAction(new Point(715, 1000)));
            clickActionsTroop.Add(new AdditonalClickAction(new Point(1210, 600)));
            clickActionsTroop.Add(new AdditonalClickAction(new Point(1130, 1030)));
            chooseTroop.ClickActions = clickActionsTroop;
            jobs.Add(chooseTroop);
            
        }

        if (doMails)
        {
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailIcon.png", "Mail"), 50, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("newMail.png", "Mail"), 50, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_1.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_2.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_3.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_4.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_5.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_6.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_7.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_8.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_9.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_10.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_11.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_12.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_13.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_14.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_15.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_16.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_17.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_18.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_19.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailAnswer_20.png", "Mail"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailresponse.png", "Mail"), 50,
                true, false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory("mailConfirm.png", "Mail"), 20, true,
                false, factor));
        }

        //Erkundung
        if (doErkundung)
        {
            var job = new JobInfo("erkundung_icon", GetTemplatePath("erkundung_icon.png"), 80, true,
                false, factor);
            var clickActions = new List<AdditonalClickAction>();
            clickActions.Add(new AdditonalClickAction(new Point(3068, 687)));
            job.ClickActions = clickActions;
            jobs.Add(job);
            jobs.Add(new JobInfo("erkundung_nehmen", GetTemplatePath("erkundung_nehmen.png"), 80, true,
                false, 90));
            jobs.Add(new JobInfo("erkundung_nehmen_2", GetTemplatePath("erkundung_nehmen_2.png"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("erkundung_nehmen_3", GetTemplatePath("erkundung_nehmen_3.png"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("erkundung_zurueck", GetTemplatePath("erkundung_zurueck.png"), 99, true,
                false, factor));
            jobs.Add(new JobInfo("erkundung_zurueck_2", GetTemplatePath("erkundung_zurueck_2.png"), 99, true,
                false, factor));
        }

        if (collectStuff)
        {
            //CollectStuff
            //Online 
            jobs.Add(new JobInfo("Collect online reward", GetTemplatePath("online_reward_ready_icon.png"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Collect online reward", GetTemplatePath("online_reward_ready_icon_2.png"), 80,
                true, false, factor));
            jobs.Add(new JobInfo("Collect online reward", GetTemplatePath("online_reward_ready_icon_maxout.png"),
                80, true, false, factor));
            jobs.Add(new JobInfo("Collect online return", GetTemplatePath("online_reward_return.png"), 80, true,
                false, factor));
            //Stamina
            jobs.Add(new JobInfo("Collect online return", GetTemplatePath("stamina_collect_icon.png"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Collect online return", GetTemplatePath("stamina_collect_take.png"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePath("help_request.png"), 90, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter", GetTemplatePath("help_request_2.png"), 90, true,
                false, factor));
        }

        if (petAdventure)
        {
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_chose_pet.png", "PetAdventure"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_collect_friends_return.png", "PetAdventure"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_collectReward.png", "PetAdventure"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_epic.png", "PetAdventure"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_ledgy.png", "PetAdventure"), 80, true,
                false, factor));


            jobs.AddRange(GetPetAdventureReadyJob());


            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_rare.png", "PetAdventure"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_return.png", "PetAdventure"), 80, true,
                false, factor));
            var collectRewardJob = new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward.png", "PetAdventure"), 20, true,
                false, 80);


            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward (1).png", "PetAdventure"), 20, true,
                false, 80));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward (2).png", "PetAdventure"), 20, true,
                false, 80));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward (3).png", "PetAdventure"), 20, true,
                false, 80));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward (4).png", "PetAdventure"), 20, true,
                false, 80));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward (5).png", "PetAdventure"), 20, true,
                false, 80));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward (6).png", "PetAdventure"), 20, true,
                false, 80));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_reward (7).png", "PetAdventure"), 20, true,
                false, 80));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_rewards_2.png", "PetAdventure"), 20, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_start.png", "PetAdventure"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_start_return.png", "PetAdventure"), 80, true,
                false, factor));
            jobs.Add(new JobInfo("Platzhalter",
                GetTemplatePathWithSubDirectory("pet_adventure_take.png", "PetAdventure"), 80, true,
                false, factor));
        }

        return jobs;
    }

    private JobInfo CreateJobWithAdditionalClicks(string description, string template, string[] paths, int prio,
        List<AdditonalClickAction> additionalClickActions)
    {
        var job = new JobInfo(description, GetTemplatePathWithSubDirectories(template, paths), prio, true, false,
            factor);
        var clickActions = new List<AdditonalClickAction>();

        foreach (var action in additionalClickActions)
        {
            var point = action.Position;
            action.Position = new Point(point.X + offSetX, point.Y + offSetY);
            job.ClickActions.Add(action);
        }

        return job;
    }

    private JobInfo CreateJobWithAdditionalClicks(string description, string template, string[] paths, int prio,
        double factor, List<Point> additionalClickPoints)
    {
        var job = new JobInfo(description, GetTemplatePathWithSubDirectories(template, paths), prio, true, false,
            factor);
        var clickActions = new List<AdditonalClickAction>();
        foreach (var point in additionalClickPoints)
        {
            var pointWithOffSet = new Point(point.X + offSetX, point.Y + offSetY);
            clickActions.Add(new AdditonalClickAction(pointWithOffSet));
        }

        job.ClickActions = clickActions;
        return job;
    }

    private JobInfo CreateJobWithAdditionalClicks(string description, string template, string[] paths, int prio,
        List<Point> additionalClickPoints, double? nFactor = null)
    {
        var job = new JobInfo(description, GetTemplatePathWithSubDirectories(template, paths), prio, true, false,
            nFactor.HasValue ? nFactor.Value : factor);
        var clickActions = new List<AdditonalClickAction>();
        foreach (var point in additionalClickPoints)
        {
            var pointWithOffSet = new Point(point.X + offSetX, point.Y + offSetY);
            clickActions.Add(new AdditonalClickAction(pointWithOffSet));
        }

        job.ClickActions = clickActions;
        return job;
    }

    private void RefreshAllyMobForTroopTask(List<JobInfo> jobs)
    {
        var path = new[] { "alliMob" };

        jobs.Add(new JobInfo("Exklusive ALli Aufgabe", GetTemplatePathWithSubDirectories("alliMob_exklusiv.png", path),
            90, true, false, factor));
        jobs.Add(new JobInfo("Exklusive ALli Aufgabe",
            GetTemplatePathWithSubDirectories("alliMob_exklusiv_2.png", path), 90, true, false, factor));
        jobs.Add(new JobInfo("Exklusive ALli Aufgabe",
            GetTemplatePathWithSubDirectories("alliMob_exklusiv_3.png", path), 90, true, false, factor));
        jobs.Add(new JobInfo("Alli Aufgabe aktualisieren",
            GetTemplatePathWithSubDirectories("alliMob_refresh.png", path), 99, true,
            false, factor));
        jobs.Add(new JobInfo("Alli Aufgabe aktualisieren bestätigen",
            GetTemplatePathWithSubDirectories("alliMob_refresh_commit.png", path), 99, true, false, factor));
    }

    private List<JobInfo> GetPetAdventureReadyJob()
    {
        var imagePaths = new List<string>
            { "pet_adventure_epic_ready.png", "pet_adventure_rare_ready.png", "pet_adventure_ledgy_ready.png" };
        var res = new List<JobInfo>();
        foreach (var imagePath in imagePaths)
        {
            var epicReady = new JobInfo("Platzhalter", GetTemplatePathWithSubDirectory(imagePath, "PetAdventure"), 2,
                true,
                false, factor);
            var clickActions = new List<AdditonalClickAction>();
            clickActions.Add(new AdditonalClickAction(new Point(2871, 671)));
            epicReady.ClickActions = clickActions;
            res.Add(epicReady);
        }

        return res;
    }

    private string GetTemplatePath(string fileName)
    {
        return Path.Combine(TemplateDirPath, fileName);
    }

    private string GetTemplatePathWithSubDirectory(string fileName, string subdirectory)
    {
        return Path.Combine(TemplateDirPath, subdirectory, fileName);
    }

    private string GetTemplatePathWithSubDirectories(string fileName, string[] subdirectories)
    {
        var path = TemplateDirPath;
        foreach (var subdirectory in subdirectories) path = Path.Combine(path, subdirectory);

        return Path.Combine(path, fileName);
    }

    public override List<JobInfo> GetLeftclickJobs()
    {
        return FillLeftClickJobs();
    }

    public override List<JobInfo> GetWaitJobs()
    {
        return FillWaitJobs();
    }
}