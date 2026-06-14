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
    private memset _memset = FhUtil.get_fptr<memset>(0x47dd24);

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
    private MsGetChrNum _MsGetChrNum = FhUtil.get_fptr<MsGetChrNum>(0x20c1a0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsCalcChrLevel(byte p1);//617140
    private MsCalcChrLevel _MsCalcChrLevel = FhUtil.get_fptr<MsCalcChrLevel>(0x217140);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int MsGetRomJob(uint p1, uint p2_jobid_0x5001, byte* out_data_end);//61deb0
    //private MsGetRomJob _MsGetRomJob = FhUtil.get_fptr<MsGetRomJob>(0x21deb0);
    private readonly FhMethodHandle<MsGetRomJob> _MsGetRomJob_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int MsGetComData(uint id, byte* out_data_end);//625160
    private readonly MsGetComData _MsGetComData = FhUtil.get_fptr<MsGetComData>(0x225160);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int MsGetRomAbility(uint id, byte* out_data_end);//61de50
    private readonly MsGetRomAbility _MsGetRomAbility = FhUtil.get_fptr<MsGetRomAbility>(0x21de50);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsBtlMonsterSaveNumCheck(uint p1); //610440
    private MsBtlMonsterSaveNumCheck _MsBtlMonsterSaveNumCheck = FhUtil.get_fptr<MsBtlMonsterSaveNumCheck>(0x210440);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsCheckAbility(uint p1, int p2, int p3); // 629280
    private MsCheckAbility _MsCheckAbility = FhUtil.get_fptr<MsCheckAbility>(0x229280);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint FUN_6294f0(uint p1, int p2, int p3); //6294f0
    private FUN_6294f0 _FUN_6294f0 = FhUtil.get_fptr<FUN_6294f0>(0x2294f0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte MsCheckLearnCommand(byte p1, int p2); // 635790
    private MsCheckLearnCommand _MsCheckLearnCommand = FhUtil.get_fptr<MsCheckLearnCommand>(0x235790);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetSaveCommand(uint p1, uint p2); // 60c500
    private MsGetSaveCommand _MsGetSaveCommand = FhUtil.get_fptr<MsGetSaveCommand>(0x20c500);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_778160(int param_1, int param_2, int param_3, int param_4);
    private readonly FhMethodHandle<FUN_778160> _FUN_778160_handle;// 778160


    //sub-function delegates
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveAp(uint p1, uint p2);//60c2e0
    private MsGetSaveAp _MsGetSaveAp = FhUtil.get_fptr<MsGetSaveAp>(0x20c2e0);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveNeedAp(byte p1, uint p2);//60cb20
    private MsGetSaveNeedAp _MsGetSaveNeedAp = FhUtil.get_fptr<MsGetSaveNeedAp>(0x20cb20);

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
    private MsGetJobNumBasic _MsGetJobNumBasic = FhUtil.get_fptr<MsGetJobNumBasic>(0x21de30);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsBtlPlayerSaveNumCheck(byte param_1);//610460
    private MsBtlPlayerSaveNumCheck _MsBtlPlayerSaveNumCheck = FhUtil.get_fptr<MsBtlPlayerSaveNumCheck>(0x210460);

    //Main Delegate 3
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuStartJobAbilityWindow(uint param_1, uint param_2);
    private readonly FhMethodHandle<TOMenuStartJobAbilityWindow> _TOMenuStartJobAbilityWindow_handle;//778f70

    



    /*
    // Main Delegate 4
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_778160(int param_1, int param_2, int param_3, int param_4);
    private readonly FhMethodHandle<FUN_778160> _FUN_778160_handle;// 778160
    
    // thunk at 87d984
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public unsafe delegate int sprintf(byte* _Dest, byte* _Format);
    private sprintf _sprintf = FhUtil.get_fptr<sprintf>(0x47d984);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_007730e0(int param_1);
    private FUN_007730e0 _FUN_007730e0 = FhUtil.get_fptr<FUN_007730e0>(0x3730e0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_007730c0(int param_1);
    private FUN_007730c0 _FUN_007730c0 = FhUtil.get_fptr<FUN_007730c0>(0x3730c0);

    //params need types verifying perhaps (were undefined4)
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FUN_00779dc0(int param_1, int param_2, int param_3, int param_4, int param_5, int param_6, int param_7);
    private FUN_00779dc0 _FUN_00779dc0 = FhUtil.get_fptr<FUN_00779dc0>(0x3730c0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_00776910();
    private FUN_00776910 _FUN_00776910 = FhUtil.get_fptr<FUN_00776910>(0x376910);

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
    public delegate void FUN_007ae7e0(int param_1);
    private FUN_007ae7e0 _FUN_007ae7e0 = FhUtil.get_fptr<FUN_007ae7e0>(0x3ae7e0);

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
    public delegate byte FUN_00764680();// 764680
    private FUN_00764680 _FUN_00764680 = FhUtil.get_fptr<FUN_00764680>(0x364680);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_007aeda0(int param_1, int param_2, int param_3);// 7aeda0
    private FUN_007aeda0 _FUN_007aeda0 = FhUtil.get_fptr<FUN_007aeda0>(0x3aeda0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_007b0a60();// 7b0a60
    private FUN_007b0a60 _FUN_007b0a60 = FhUtil.get_fptr<FUN_007b0a60>(0x3b0a60);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FUN_0077aa20(int param_1, int param_2, int param_3, int param_4, int param_5);// 77aa20
    private FUN_0077aa20 _FUN_0077aa20 = FhUtil.get_fptr<FUN_0077aa20>(0x37aa20);
    */
}