using Fahrenheit.FFX2;

namespace Fahrenheit.Mods.X2DSUnlimit;

public partial class X2DSUnlimitModule : FhModule
{
    //from main.cs

    //delegates - main hooks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte MsGetSaveDressUpCount(int param_1, uint param_2);
    private readonly FhMethodHandle<MsGetSaveDressUpCount> _MsGetSaveDressUpCount_handle;//60c730

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsAddSaveDreSphere(uint ds_id, int param_2);
    private readonly FhMethodHandle<MsAddSaveDreSphere> _MsAddSaveDreSphere_handle;//60b260

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetSaveDreSphere(uint ds_id);
    private readonly FhMethodHandle<MsGetSaveDreSphere> _MsGetSaveDreSphere_handle;//60c710

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void F791610(int param_1, int param_2, int param_3);
    private readonly FhMethodHandle<F791610> _F791610_handle;//791610

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int kyGetResultPlateNum();
    private readonly FhMethodHandle<kyGetResultPlateNum> _kyGetResultPlateNum_handle;//5eb2e0

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int kyGetUsedPoint();
    private readonly FhMethodHandle<kyGetUsedPoint> _kyGetUsedPoint_handle;//5eb480

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int kySetDefJobWindow(ushort param_1, ushort param_2, int param_3, int param_4, int param_5, int param_6);
    private readonly FhMethodHandle<kySetDefJobWindow> _kySetDefJobWindow_handle;//5e5470

    // these 3 kyGetJobNum are used in Menus (at least)
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint kyGetJobNum();
    private readonly FhMethodHandle<kyGetJobNum> _kyGetJobNum_handle;//5ea7b0

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint kyGetJobNum2();
    private readonly FhMethodHandle<kyGetJobNum2> _kyGetJobNum2_handle;//5ea810

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint kyGetJobNum3();
    private readonly FhMethodHandle<kyGetJobNum3> _kyGetJobNum3_handle;//5ea8b0

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte kyGetCursorPoint(int param_1, int param_2);
    private readonly FhMethodHandle<kyGetCursorPoint> _kyGetCursorPoint_handle;//5ea770

    // delegates for utility and sub-functions
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsCheckRange(int number, int lower_bound, int upper_bound);//624cd0
    private readonly FhMethodHandle<MsCheckRange> _MsCheckRange_handle;


    // these functions were hooked for debug/RE purposes - 08.06.26
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetSavePlate(uint param_1);
    private readonly FhMethodHandle<MsGetSavePlate> _MsGetSavePlate_handle;//60cc00

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetSaveConfigChangeEffect();
    private readonly FhMethodHandle<MsGetSaveConfigChangeEffect> _MsGetSaveConfigChangeEffect_handle;//60c650

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetRamConfigChangeEffect();
    private readonly FhMethodHandle<MsGetRamConfigChangeEffect> _MsGetRamConfigChangeEffect_handle;//625c90

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void kyEquipStart(int param_1);
    private readonly FhMethodHandle<kyEquipStart> _kyEquipStart_handle;// b5b900

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int kySetDefPlateWindow(short param_1, short param_2, int param_3, int param_4, int param_5);
    private readonly FhMethodHandle<kySetDefPlateWindow> _kySetDefPlateWindow_handle;//5e5550

    //test hook of a void fx(void) thst has been reduced mostly to _aullshr()
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint kyIsUsedPoint(uint param_1, uint param_2);
    private readonly FhMethodHandle<kyIsUsedPoint> _kyIsUsedPoint_handle;// 5eb9e0



    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuMakeJobList(int param_1);
    private readonly FhMethodHandle<TOMenuMakeJobList> _TOMenuMakeJobList_handle; //778b00

    // thunk at 87dd24
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void* memset(void* _Dst, int _Val, uint _Size);
    //private memset _memset = FhUtil.get_fptr<memset>(0x47dd24);
    private readonly FhMethodHandle<memset> _memset_handle; //87dd24

    /*
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuMakeJobAbilityList(uint param_1, uint param_2);
    private TOMenuMakeJobAbilityList _TOMenuMakeJobAbilityList = FhUtil.get_fptr<TOMenuMakeJobAbilityList>(0x3788d0);//7788d0

    */
    // Main delegate 2
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuMakeJobAbilityList(uint param_1, uint param_2);
    private readonly FhMethodHandle<TOMenuMakeJobAbilityList> _TOMenuMakeJobAbilityList_handle;//7788d0

    // GG Icon fix for LG/Freelancer
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_5E7580(int param_1, int param_2, int icon, uint param_4);
    private readonly FhMethodHandle<FUN_5E7580> _FUN_5E7580_handle; //5e7580


    // from abilities_menu.cs
    // Main delegate 1
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int MsGetJobAbilityList(int param_1, int param_2, int* param_3, int param_4);
    private readonly FhMethodHandle<MsGetJobAbilityList> _MsGetJobAbilityList_handle;//629af0

    //sub-function delegates
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetChrNum(uint param_1); //60c1a0
    private readonly FhMethodHandle<MsGetChrNum> _MsGetChrNum_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsCalcChrLevel(byte p1);//617140
    private readonly FhMethodHandle<MsCalcChrLevel> _MsCalcChrLevel_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate Job* MsGetRomJob(uint chr_id, uint job_id, byte* out_data_end);//61deb0
    private readonly FhMethodHandle<MsGetRomJob> _MsGetRomJob_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int MsGetComData(uint id, byte* out_data_end);//625160
    private readonly FhMethodHandle<MsGetComData> _MsGetComData_handle;//625160

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int MsGetRomAbility(uint id, byte* out_data_end);//61de50
    private readonly FhMethodHandle<MsGetRomAbility> _MsGetRomAbility_handle;//61de50

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool MsBtlMonsterSaveNumCheck(uint p1); //610440
    private readonly FhMethodHandle<MsBtlMonsterSaveNumCheck> _MsBtlMonsterSaveNumCheck_handle;//610440

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsCheckAbility(uint p1, int p2, int p3); //629280
    private readonly FhMethodHandle<MsCheckAbility> _MsCheckAbility_handle;//629280

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_6294f0(uint p1, int p2, int p3); //6294f0
    private readonly FhMethodHandle<FUN_6294f0> _FUN_6294f0_handle;//6294f0

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte MsCheckLearnCommand(byte p1, int p2); //635790
    private readonly FhMethodHandle<MsCheckLearnCommand> _MsCheckLearnCommand_handle;//635790

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetSaveCommand(uint p1, uint p2); //60c500
    private readonly FhMethodHandle<MsGetSaveCommand> _MsGetSaveCommand_handle;//60c500


    //sub-function delegates
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveAp(uint p1, uint p2);//60c2e0
    //private MsGetSaveAp _MsGetSaveAp = FhUtil.get_fptr<MsGetSaveAp>(0x20c2e0);
    private readonly FhMethodHandle<MsGetSaveAp> _MsGetSaveAp_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveNeedAp(byte p1, uint p2);//60cb20
    private readonly FhMethodHandle<MsGetSaveNeedAp> _MsGetSaveNeedAp_handle;

    // also used in custom ability menus
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveLearn(uint param_1, uint param_2); // 60ca70
    //private MsGetSaveLearn _MsGetSaveLearn = FhUtil.get_fptr<MsGetSaveLearn>(0x20ca70);
    private readonly FhMethodHandle<MsGetSaveLearn> _MsGetSaveLearn_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsSetSaveLearn(uint param_1, uint param_2, ushort param_3); // 60e270
    private readonly FhMethodHandle<MsSetSaveLearn> _MsSetSaveLearn_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetJobNumBasic(uint param_1);//61de30
    //private MsGetJobNumBasic _MsGetJobNumBasic = FhUtil.get_fptr<MsGetJobNumBasic>(0x21de30);
    private readonly FhMethodHandle<MsGetJobNumBasic> _MsGetJobNumBasic_handle;


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsBtlPlayerSaveNumCheck(byte param_1);//610460
    //private MsBtlPlayerSaveNumCheck _MsBtlPlayerSaveNumCheck = FhUtil.get_fptr<MsBtlPlayerSaveNumCheck>(0x210460);
    private readonly FhMethodHandle<MsBtlPlayerSaveNumCheck> _MsBtlPlayerSaveNumCheck_handle;

    //Main Delegate 3
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuStartJobAbilityWindow(uint param_1, uint param_2);
    private readonly FhMethodHandle<TOMenuStartJobAbilityWindow> _TOMenuStartJobAbilityWindow_handle;//778f70

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint TOGetSaveJobName(uint param_1);
    private readonly FhMethodHandle<TOGetSaveJobName> _TOGetSaveJobName_handle;//794600

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetSaveJob(uint chr_id);
    private readonly FhMethodHandle< MsGetSaveJob> _MsGetSaveJob_handle;//60c950


    // AltChr delegates
    //main function
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetChrID(uint chr_id);
    private readonly FhMethodHandle<MsGetChrID> _MsGetChrID_handle;//624f90

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsSetRamMotionChrData(int chr_id, uint job_id);//627a20
    private readonly FhMethodHandle<MsSetRamMotionChrData> _MsSetRamMotionChrData_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //62ab30 -  FUN_710026cc90? - btlSoundStreamNormal? -- using this to force alts's hurt SFX to play
    public unsafe delegate uint FUN_62AB30(uint chr_id, uint sound_id);
    private readonly FhMethodHandle<FUN_62AB30> _FUN_62AB30_handle;

    [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
    //534a70 - this function accessed the VoiceIDMapper.txt integer/string pointer for Yuna's hurt sound. Might use this to silence it?
    public unsafe delegate void FUN_534A70(int* param_1, int param_2, int param_3, int param_4,/*FMODCHANNELINDEX*/ int param_5, int param_6, int* param_7, int* param_8, int* param_9);
    private readonly FhMethodHandle<FUN_534A70> _FUN_534A70_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetChr(uint chr_id);
    private readonly FhMethodHandle<MsGetChr> _MsGetChr_handle;//611450


    // added C# job name string implementation
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate byte* MsGetSaveChrName(int chr_id);
    private readonly FhMethodHandle<MsGetSaveChrName> _MsGetSaveChrName_handle;//60c4a0

    // C# defined job help string crash prevention
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_5E59B0(uint job_id);
    private readonly FhMethodHandle<FUN_5E59B0> _FUN_5E59B0_handle;//5E59B0

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuSetHelpMes(int addr_of_txt_bytes);
    private readonly FhMethodHandle<TOMenuSetHelpMes> _TOMenuSetHelpMes_handle;// 763970

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_6083B0(uint param_1);
    private readonly FhMethodHandle<FUN_6083B0> _FUN_6083B0_handle;// 6083B0

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TOGetFaceIndex2(int param_1, uint param_2);
    private readonly FhMethodHandle<TOGetFaceIndex2> _TOGetFaceIndex2_handle;// 793190

    // hooked to get VoiceIDMapper pointer -> AltChrs -> sound.cs
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MsBtlChrGetMem();
    private readonly FhMethodHandle<MsBtlChrGetMem> _MsBtlChrGetMem_handle;// 60fe10

    // overwrite voiceline integers on spherechange
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOCtrlATBChr();
    private readonly FhMethodHandle<TOCtrlATBChr> _TOCtrlATBChr_handle;// 75e2c0

    /*
    // Main Delegate 4
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_778160(int param_1, int param_2, int param_3, int param_4);
    private readonly FhMethodHandle<FUN_778160> _FUN_778160_handle;// 778160*/


    // Menu functionality - handle Ability Menu case  0x77762f - so correct ability name is displayed.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_777270(uint param_1);
    private readonly FhMethodHandle<FUN_777270> _FUN_777270_handle;//777270

    // Hopefully Abilities menu 16x ability list functionality - add Freelancer and Leblanc Goon support
    // We're interested in it's call from FFX-2.exe + 3773CE (FUN_00777270_7100424240)
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool FUN_776EC0(uint param_1, int ability_slot);
    private readonly FhMethodHandle<FUN_776EC0> _FUN_776EC0_handle;//776EC0


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuSetSaveLearn(byte param_1, uint param_2, int param_3);
    private readonly FhMethodHandle<TOMenuSetSaveLearn> _TOMenuSetSaveLearn_handle;//778f40

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuSetMacroCommandType(int param_1, int param_2, byte param_3);
    private readonly FhMethodHandle<TOMenuSetMacroCommandType> _TOMenuSetMacroCommandType_handle; //796330

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TOBtlGetComName(uint param_1);
    private readonly FhMethodHandle<TOBtlGetComName> _TOBtlGetComName_handle; //759fd0

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuSetMacroCommandValue(int param_1, int param_2, uint param_3);
    private readonly FhMethodHandle<TOMenuSetMacroCommandValue> _TOMenuSetMacroCommandValue_handle; //796360

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint TOGetMenuText(uint param_1);
    private readonly FhMethodHandle<TOGetMenuText> _TOGetMenuText_handle; //779250

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint SndSepPlaySimple(uint param_1);
    private readonly FhMethodHandle<SndSepPlaySimple> _SndSepPlaySimple_handle; //744760

    // Abilities menu 16x ability list rendering function
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_778160(int param_1, int param_2, int param_3, int param_4);
    private readonly FhMethodHandle<FUN_778160> _FUN_778160_handle;//778160

    // thunk at 87d984
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int sprintf(byte* _Dest, byte* _Format);
    private sprintf _sprintf = FhUtil.get_fptr<sprintf>(0x47d984);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TOGetRtcValue(int param_1);
    private TOGetRtcValue _TOGetRtcValue = FhUtil.get_fptr<TOGetRtcValue>(0x3730e0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TOGetRtcRatio(int param_1);
    private TOGetRtcRatio _TOGetRtcRatio = FhUtil.get_fptr<TOGetRtcRatio>(0x3730c0);

    //params need types verifying perhaps (were undefined4)
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int TOMenuDrawRotPlate(int param_1, int param_2, int param_3, int param_4, int param_5, int param_6, int param_7);
    private TOMenuDrawRotPlate _TOMenuDrawRotPlate = FhUtil.get_fptr<TOMenuDrawRotPlate>(0x379dc0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuOpenPkt();
    private TOMenuOpenPkt _TOMenuOpenPkt = FhUtil.get_fptr<TOMenuOpenPkt>(0x376910);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_007B0F50(int param_1, int param_2, int param_3, int param_4, int colour);
    private FUN_007B0F50 _FUN_007B0F50 = FhUtil.get_fptr<FUN_007B0F50>(0x3B0F50);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpShape2dMenu(int param_1, int param_2, int param_3, int param_4);
    private TOMkpShape2dMenu _TOMkpShape2dMenu = FhUtil.get_fptr<TOMkpShape2dMenu>(0x3B1250);


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate void FUN_007AE430(byte* param_1, int param_2, int param_3, int param_4, int param_5, int param_6, int param_7);
    private FUN_007AE430 _FUN_007AE430 = FhUtil.get_fptr<FUN_007AE430>(0x3AE430);


    //was float10 return type
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double offsetAdjust_Y(int param_1);//7764e0
    private offsetAdjust_Y _offsetAdjust_Y = FhUtil.get_fptr<offsetAdjust_Y>(0x3764e0);

    //was float10 return type
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double offsetAdjust_X(int param_1);//7764c0
    private offsetAdjust_X _offsetAdjust_X = FhUtil.get_fptr<offsetAdjust_X>(0x3764c0);

    //function just returns
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOKickPacket();// 7add10
    private TOKickPacket _TOKickPacket = FhUtil.get_fptr<TOKickPacket>(0x3add10);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuChangeFrameAccPlate(int param_1);
    private TOMenuChangeFrameAccPlate _TOMenuChangeFrameAccPlate = FhUtil.get_fptr<TOMenuChangeFrameAccPlate>(0x3ae7e0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFX2_Set_UI_Scale(int param_1, int param_2);// 7a0060
    private FFX2_Set_UI_Scale _FFX2_Set_UI_Scale = FhUtil.get_fptr<FFX2_Set_UI_Scale>(0x3a0060);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint TOGetFFXLang();// 793010
    private TOGetFFXLang _TOGetFFXLang = FhUtil.get_fptr<TOGetFFXLang>(0x393010);

    // param count mismatch, it's supposed to have 4 parameters. Called in 778160 with 3 args
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpComIconNameClut(uint param_1, int param_2, int param_3, int param_4);// 7ae9b0
    private TOMkpComIconNameClut _TOMkpComIconNameClut = FhUtil.get_fptr<TOMkpComIconNameClut>(0x3ae9b0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFX2_Reset_UI_Scale();// 7a0030
    private FFX2_Reset_UI_Scale _FFX2_Reset_UI_Scale = FhUtil.get_fptr<FFX2_Reset_UI_Scale>(0x3a0030);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte TkMenuGetTimer();// 764680
    private TkMenuGetTimer _TkMenuGetTimer = FhUtil.get_fptr<TkMenuGetTimer>(0x364680);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_007aeda0(int param_1, int param_2, int param_3);// 7aeda0
    private FUN_007aeda0 _FUN_007aeda0 = FhUtil.get_fptr<FUN_007aeda0>(0x3aeda0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpResetFrameAcc();// 7b0a60
    private TOMkpResetFrameAcc _TOMkpResetFrameAcc = FhUtil.get_fptr<TOMkpResetFrameAcc>(0x3b0a60);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMkpExPlateParam(int param_1, int param_2, int param_3, int param_4, int param_5);// 77aa20
    private TOMkpExPlateParam _TOMkpExPlateParam = FhUtil.get_fptr<TOMkpExPlateParam>(0x37aa20);
   
}
