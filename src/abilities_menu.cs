// SPDX-License-Identifier: MIT

namespace Fahrenheit.Mods.X2DSUnlimit;

[FhLoad(FhGameId.FFX2)]
public class X2DSUnlimitAbilitiesMenu : FhModule {
    int addr_offset = 0x400000;



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
    private MsGetRomJob _MsGetRomJob = FhUtil.get_fptr<MsGetRomJob>(0x21deb0);

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


    // Main delegate 2
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuMakeJobAbilityList(uint param_1, uint param_2);
    private readonly FhMethodHandle<TOMenuMakeJobAbilityList> _TOMenuMakeJobAbilityList_handle;//7788d0

    //sub-function delegates
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveAp(uint p1, uint p2);//60c2e0
    private MsGetSaveAp _MsGetSaveAp = FhUtil.get_fptr<MsGetSaveAp>(0x20c2e0);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveNeedAp(byte p1, uint p2);//60cb20
    private MsGetSaveNeedAp _MsGetSaveNeedAp = FhUtil.get_fptr<MsGetSaveNeedAp>(0x20cb20);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort MsGetSaveLearn(uint p1, uint p2); // 60ca70
    private MsGetSaveLearn _MsGetSaveLearn = FhUtil.get_fptr<MsGetSaveLearn>(0x20ca70);

    //Main Delegate 3
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TOMenuStartJobAbilityWindow(uint param_1, uint param_2);
    private readonly FhMethodHandle<TOMenuStartJobAbilityWindow> _TOMenuStartJobAbilityWindow_handle;//778f70

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

    public unsafe X2DSUnlimitAbilitiesMenu()
    {
        _MsGetJobAbilityList_handle = new FhMethodHandle<MsGetJobAbilityList>(this, "FFX-2.exe", 0x629af0 - addr_offset, h_MsGetJobAbilityList);
        _TOMenuMakeJobAbilityList_handle = new FhMethodHandle<TOMenuMakeJobAbilityList>(this, "FFX-2.exe", 0x7788d0 - addr_offset, h_TOMenuMakeJobAbilityList);
        _TOMenuStartJobAbilityWindow_handle = new FhMethodHandle<TOMenuStartJobAbilityWindow>(this, "FFX-2.exe", 0x778f70 - addr_offset, h_TOMenuStartJobAbilityWindow);
        _FUN_778160_handle = new FhMethodHandle<FUN_778160>(this, "FFX-2.exe", 0x778160 - addr_offset, h_FUN_778160);
    }


    //Freelancer/Leblanc Goon ID's appended to mod copt of DAT_00D63e78 table (ffx-2.exe + 0x963e78)
    //TOMsJobAbilityWindow+
    private static readonly ushort[] CustomTOMSJAW_DS_Table =
{
    0x5000, 0x5001, 0x5002, 0x5003, 0x5004, 0x5005,
    0x5006, 0x5007, 0x5008, 0x5009, 0x500A,
    0x500B, 0x500C, 0x500D, 0x500E, 0x5018,
    0x5019, 0x501a, 0x501b, 0x501d, 0x501e,
    0x501f, 0x501c, 0x500f, 0x5010, 0x5011,
    0x5012, 0x5013, 0x5014, 0x5015, 0x5016,
    0x5017, 0x5020, 0x5021
};

    // handles rendering of 16 ability boxes (Abilites->DS->this menu)
    // a lot of floating point / FPU guff?
    public unsafe void h_FUN_778160(int param_1, int param_2, int param_3, int param_4)
    {

        //_logger.Info("Param_1 is: " + param_1.ToString("X"));// memory address
        //_logger.Info("Param_2 is: " + param_2.ToString());// hard coded 0x2e or 0x10a
        //_logger.Info("Param_3 is: " + param_3.ToString());// 0xce + some*0x16
        //_logger.Info("Param_4 is: " + param_4.ToString());// iterator?

        _FUN_778160_handle.orig_fptr.Invoke(param_1, param_2, param_3, param_4);
        //return;
        
        /*
        
        //undefined* puVar1;
        int puVar1; // is a memory address

        uint uVar2;
        uint uVar3;
        int iVar4;
        int iVar5;

        //undefined4 uVar6;
        uint uVar6;

        byte* pcVar7;
        //float10 fVar7;
        double fVar7;

        //float local_28[4];
        float[] local_28 = new float[4];
        //byte local_18[16];
        byte[] local_18 = new byte[24];

        
        local_28[0] = 0.0f;
        local_28[1] = 0.0f;
        local_28[2] = 0.0f;
        local_28[3] = 0.0f;
        uVar2 = (uint)(param_4 + *(short*)(param_1 + 0x32) * 2);
        if (uVar2 < 0x10)
        {
            uVar3 = uVar2 & 0x8000000f;
            if ((int)uVar3 < 0)
            {
                uVar3 = (uVar3 - 1 | 0xfffffff0) + 1;
            }

            byte* DAT_016c0278 = FhUtil.ptr_at<byte>(0x12c0278);
            
            //puVar1 = &DAT_016c0278 + uVar3 * 0x10;
            puVar1 = (int)((int)(DAT_016c0278) + uVar3 * 0x10);

            iVar4 = _FUN_007730e0(puVar1);
            iVar5 = _FUN_007730c0(puVar1);
            if (iVar5 / 2 + 0x800 < 0x800)
            {
                iVar5 = _FUN_007730c0(puVar1);
                local_28[0] = (float)((float)(iVar5 / 2) * 3.1415927);
            }
            else
            {
                iVar5 = _FUN_007730c0(puVar1);
                local_28[0] = (float)((float)(0x800 - iVar5 / 2) * -3.1415927);
            }
            local_28[0] = (float)(local_28[0] * 0.00048828125);
            if (*(char*)(param_1 + 0x42) == '\0')
            {
                param_3 = iVar4;
            }

            //iVar5 = (uint)(byte)(&DAT_00d63e78)[DAT_016c0266 * 2] * 0x110;
            byte ds = FhUtil.get_at<byte>(0x12c0266);//what is this? Presuming ds_id or similar
            iVar5 = ds * 0x110; // Is this correct? Not likely for LG/FL

            iVar4 = _FUN_007730c0(puVar1);
            if (iVar4 != 0)
            {
                _FUN_00779dc0(param_2, param_3, local_28, 6, 0, ((int)uVar2 / 2) * 0x199, 0);
            }

            byte* DAT_011bb210 = FhUtil.ptr_at<byte>(0xdbb210); // is this type correct?
            int DAT_011bb210_addr = (int)DAT_011bb210;
            if (((DAT_011bb210[uVar2 * 0x10 + iVar5] == 1)&&(-1.5707964 < local_28[0])) &&(local_28[0] < 1.5707964))
            //if ((((&DAT_011bb210)[uVar2 * 0x10 + iVar5] == '\x01') && (-1.5707964 < local_28[0])) && (local_28[0] < 1.5707964))
            {
                //if ((*(char*)(iVar5 + 0x11bb213 + uVar2 * 0x10) == 1) && (iVar4 = _FUN_007730c0(puVar1), iVar4 == 0x1000))
                iVar4 = _FUN_007730c0(puVar1);
                if ((*(byte*)(iVar5 + 0x11bb213 + uVar2 * 0x10) == 1) && (iVar4 == 0x1000)){ //verify the + 0x11bb213 works
                    _FUN_00776910();
                    
                    /*
                    _offsetAdjust_Y(0x3b, 3);
                    uVar6 = _FUN_0087e0d0();
                    _offsetAdjust_X(0x2d8, uVar6);
                    uVar6 = _FUN_0087e0d0();
                    _offsetAdjust_Y(7, uVar6);
                    uVar6 = _FUN_0087e0d0();
                    _offsetAdjust_X(9, uVar6);
                    uVar6 = _FUN_0087e0d0();
                    _FUN_007b0f50(uVar6);
                    


                    _TOKickPacket();
                }
                _FUN_00776910();
                _FUN_007ae7e0(6);
                _FFX2_Set_UI_Scale(0x3f55f15f, 0x3f313b14);
                _TOGetFFXLang();

                /*
                _offsetAdjust_Y(0xf, 0);
                uVar6 = _FUN_0087e0d0();
                
                double test_double = _offsetAdjust_Y(0xf);
                int test_double_as_int = (int)(test_double);
                //uVar6 = _FUN_0087e0d0();


                int DAT_011bb214_addr = (int)FhUtil.ptr_at<byte>(0xdbb214);

                _TOMkpComIconNameClut(*(uint*)(DAT_011bb214_addr + uVar2 * 0x10 + iVar5), param_2 + 6, test_double_as_int, 0);// added 4th param, likely wrong
                //_TOMkpComIconNameClut(*(undefined4*)(&DAT_011bb214 + uVar2 * 0x10 + iVar5), param_2 + 6, uVar6);


                _FFX2_Reset_UI_Scale();

                byte* DAT_011bb212 = FhUtil.ptr_at<byte>(0xdbb212);

                if ((DAT_011bb212)[uVar2 * 0x10 + iVar5] == 1)
                //if ((&DAT_011bb212)[uVar2 * 0x10 + iVar5] == 1)
                {
                    _FFX2_Set_UI_Scale(0x3f092492, 0x3f333333);

                    //DAT_016f0ac0 = 1;
                    FhUtil.set_at<byte>(0x12f0ac0, 1);

                    uVar6 = _FUN_00764680();
                    _offsetAdjust_Y(0x14, 8, uVar6);
                    uVar6 = _FUN_0087e0d0();
                    _FUN_007b1250(param_2 + 0xa2, uVar6);

                    //DAT_016f0ac0 = 0;
                    FhUtil.set_at<byte>(0x12f0ac0, 1);
                }
                else
                {
                    int DAT_011bb218_addr = (int)FhUtil.ptr_at<byte>(0xdbb218);
                    int DAT_011bb21c_addr = (int)FhUtil.ptr_at<byte>(0xdbb21c);
                    _sprintf((byte*)local_18, "%3d/%3d", *(uint*)(DAT_011bb218_addr + uVar2 * 0x10 + iVar5), *(uint*)(DAT_011bb21c_addr + uVar2 * 0x10 + iVar5));
                    //_sprintf((byte*)local_18, "%3d/%3d", *(undefined4*)(&DAT_011bb218 + uVar2 * 0x10 + iVar5), *(undefined4*)(&DAT_011bb21c + uVar2 * 0x10 + iVar5));
                    iVar4 = _TOGetFFXLang();
                    if (iVar4 == 0)
                    {
                        _FFX2_Set_UI_Scale(0x3eec4ec5, 0x3f19999a);
                        _offsetAdjust_Y(0x16, 0x80, 0x80, 0x80, 0x80);
                        uVar6 = _FUN_0087e0d0();
                        _FUN_007ae430(local_18, param_2 + 0xc3, uVar6);
                    }
                    else
                    {
                        if (local_18[0] != 0)
                        {
                            pcVar7 = local_18;
                            do
                            {
                                if (*pcVar7 == 0x20)
                                {
                                    *pcVar7 = 0x3a;
                                }
                                else if (*pcVar7 == 0x2f)
                                {
                                    *pcVar7 = 0x49;
                                }
                                pcVar7 = pcVar7 + 1;
                            } while (*pcVar7 != 0);
                        }
                        _FFX2_Set_UI_Scale(0x3f55f15f, 0x3f313b14);

                        //fVar7 = (float10)_offsetAdjust_Y(10);
                        fVar7 = (double)_offsetAdjust_Y(10);

                        //_FUN_007aeda0(local_18, (float)(param_2 + 0xc3), (float)(fVar7 + (float10)param_3));
                        _FUN_007aeda0(local_18, (float)(param_2 + 0xc3), (float)(fVar7 + (double)param_3));
                    }
                }
                _FFX2_Reset_UI_Scale();
                _FUN_007b0a60();
                _FUN_0077aa20(param_2, param_3, local_28, 6, 0);
                _TOKickPacket();
                
                return;
            }
        }
        return;
        */
    }

    // p1 is DAT_00df6d80 -> chr_id of who is looked at in the menu YRP -> 0,1,2, p2 is ds_id from kyGetJobIndex (currently equipped ds_id?)
    public unsafe void h_TOMenuStartJobAbilityWindow(uint param_1, uint param_2)
    {
        int iVar1;

        //DAT_016c0270 = param_1;
        FhUtil.set_at<uint>(0x12c0270, param_1);
        //DAT_016c0274 = param_2 | 0x5000;
        FhUtil.set_at<uint>(0x12c0274, param_2 | 0x5000);

        iVar1 = 0;
        do
        {
        
        /*
         * DAT_011bb204 is something? It gains avalue of 255 when ability menu is opened (abilties? after char select? After DS Select?)
         * DAT_00d63e79 is a table of Dressphere IDs e.g 0x5001... (Doesn't contain Freelancer/LeblancGoon by default)
         * 
         * original Ghidra decomp:
        if ((param_2 | 0x5000) == (&DAT_011bb204)[(uint)(byte)(&DAT_00d63e78)[iVar1 * 2] * 0x44])
        {
            //DAT_016c0266 = (undefined1)iVar1;
            FhUtil.set_at<byte>(0x12c0266, (byte)iVar1);
        }
        iVar1 = iVar1 + 1;
        */

        
            uint* D_11bb204 = FhUtil.ptr_at<uint>(0xdbb204);
            int D_11bb204_addr = (int)(D_11bb204);

            /*
            // some reference table filled with Dressphere ID's (0x5001, 0x5002)
            byte* D_D63e78 = FhUtil.ptr_at<byte>(0x963e78);// change this to reference custom table. Or:
            int D_D63e78_addr = (int)(D_D63e78);// change this to reference custom table.

            byte checking_ds_id = *(byte*)(D_D63e78_addr + (iVar1 * 2));
            */

            ushort ds_full_id = CustomTOMSJAW_DS_Table[iVar1];
            byte checking_ds_id = (byte)(ds_full_id & 0xfff);

            //ushort check_val = *(ushort*)(D_11bb204_addr + (checking_ds_id * 0x44));
            ushort check_val = *(ushort*)(D_11bb204_addr + (checking_ds_id * 0x110)); //mul by 4 to work correctly (vanilla....)

            //if ((param_2 | 0x5000) == (&DAT_011bb204)[(uint)(byte)(&DAT_00d63e78)[iVar1 * 2] * 0x44])
            if ((param_2 | 0x5000) == check_val){
            //DAT_016c0266 = (undefined1)iVar1;
            FhUtil.set_at<byte>(0x12c0266, (byte)iVar1); // should write the correct DS ID (ID Only, not 0x50xx)
        }
        iVar1 = iVar1 + 1;

            //} while (iVar1 < 0x20);
        } while (iVar1 < CustomTOMSJAW_DS_Table.Length);// CHanged to ref new table length to include FL and LG

            FhUtil.set_at<uint>(0x963ed4, 7);// used for menu progression, tells game now entered ds ability list with 16 moves showing, ap, mastery etc.
    }

    public unsafe void h_TOMenuMakeJobAbilityList(uint param_1, uint param_2)
    {
        //undefined4 uVar1;
        uint uVar1;
        int iVar2;
        //undefined4 uVar3;
        uint uVar3;
        int iVar4;
        int iVar5;
        byte* pbVar6;
        uint uVar7;
        uint* puVar8;
        byte* pbVar9;

        //undefined1 local_1c[4];
        //byte* local_1c;//MsGetRomJob -- is param_3 out_data_end?

        int local_18;
        ushort* local_14;
        //undefined4* local_10;
        uint* local_10;
        int local_c;
        //undefined4 local_8;
        uint local_8;

        switch (param_2)
        {
            case 0x5010:
                param_1 = 3;
                break;
            case 0x5011:
                param_1 = 4;
                break;
            default:
                break;
            case 0x5013:
                param_1 = 5;
                break;
            case 0x5014:
                param_1 = 6;
                break;
            case 0x5016:
                param_1 = 7;
                break;
            case 0x5017:
                param_1 = 8;
                break;
        }

        uint* DAT_11bb200 = FhUtil.ptr_at<uint>(0xdbb200);
        nint DAT_11bb200_addr = (nint)DAT_11bb200;

        //local_10 = &DAT_011bb200 + (param_2 & 0xfff) * 0x44;
        local_10 = (uint*)DAT_11bb200_addr + (param_2 & 0xfff) * 0x44;
        local_8 = param_1;

        byte* local_1c = stackalloc byte[64];

        iVar2 = _MsGetRomJob(param_1, param_2, local_1c);
        uVar1 = local_8;
        local_14 = (ushort*)(iVar2 + 0x3e); // start of job.bin dressphere abilities

        byte* D_dbb213 = FhUtil.ptr_at<byte>(0xdbb213);

        //iVar2 = (int)((param_2 & 0xfff) * 0x110 + 0x11bb213);
        iVar2 = (int)((param_2 & 0xfff) * 0x110 + (int)D_dbb213);
        local_18 = 0x10;
        do
        {
            uVar7 = (uint)*local_14;
            //*(undefined1*)(iVar2 + -3) = 0;
            //*(undefined2*)(iVar2 + -1) = 0;
            *(byte*)(iVar2 + -3) = 0;
            *(ushort*)(iVar2 + -1) = 0;
            *(uint*)(iVar2 + 1) = uVar7;
            uVar3 = _MsGetSaveAp(uVar1, uVar7);
            *(uint*)(iVar2 + 5) = uVar3;
            iVar4 = _MsGetSaveNeedAp((byte)uVar1, uVar7);
            uVar3 = local_8;
            *(int*)(iVar2 + 9) = iVar4;
            if (iVar4 < *(int*)(iVar2 + 5))
            {
                *(int*)(iVar2 + 5) = iVar4;
            }
            local_14 = local_14 + 2;
            iVar2 = iVar2 + 0x10;
            local_18 = local_18 + -1;
        } while (local_18 != 0);

        local_18 = h_MsGetJobAbilityList((int)local_8, (int)param_2, &local_c, 0);
        puVar8 = local_10 + 5;
        iVar4 = 0x10;
        iVar2 = local_c;
        do
        {
            iVar5 = 0;
            if (0 < iVar2)
            {
                do
                {
                    if (*puVar8 == (uint)*(ushort*)(local_18 + iVar5 * 2))
                    {
                        *(byte*)(puVar8 + -1) = 1;
                        uVar7 = _MsGetSaveLearn(uVar3, param_2);
                        iVar2 = local_c;
                        if (uVar7 == *puVar8)
                        {
                            *(byte*)((int)puVar8 + -1) = 1;
                        }
                        break;
                    }
                    iVar5 = iVar5 + 1;
                } while (iVar5 < iVar2);
            }
            puVar8 = puVar8 + 4;
            iVar4 = iVar4 + -1;
        } while (iVar4 != 0);
        iVar4 = h_MsGetJobAbilityList((int)uVar3, (int)param_2, &local_c, 1);
        pbVar9 = (byte*)((int)local_10 + 0x12);
        pbVar6 = pbVar9;
        iVar2 = 0x10;
        do
        {
            iVar5 = iVar2;
            iVar2 = 0;
            if (0 < local_c)
            {
                do
                {
                    if (*(uint*)(pbVar6 + 2) == (uint)*(ushort*)(iVar4 + iVar2 * 2))
                    {
                        pbVar6[-2] = 1;
                        pbVar6[0] = 1;
                        pbVar6[1] = 0;
                        break;
                    }
                    iVar2 = iVar2 + 1;
                } while (iVar2 < local_c);
            }
            pbVar6 = pbVar6 + 0x10;
            iVar2 = iVar5 + -1;
            if (iVar2 == 0)
            {
                iVar4 = 0;
                iVar5 = iVar5 + 0xf;
                iVar2 = 0;
                do
                {
                    iVar4 = iVar4 + *(int*)(pbVar9 + 10);
                    if (*pbVar9 == 1)
                    {
                        iVar2 = iVar2 + *(int*)(pbVar9 + 6);
                    }
                    pbVar9 = pbVar9 + 0x10;
                    iVar5 = iVar5 + -1;
                } while (iVar5 != 0);
                if ((iVar2 == 0) || (iVar4 == 0))
                {
                    local_10[2] = 0;
                }
                else if (local_10[1] == 0x5002)
                {
                    local_10[2] = (uint)((iVar2 * 0x54) / iVar4);


                    uint* DAT_11bd420 = FhUtil.ptr_at<uint>(0xdbd420);


                    //pbVar6 = &DAT_011bd420;
                
                    do
                    {
                        if (pbVar6[-0x20] == 1)
                        {
                            local_10[2] = local_10[2] + 1;
                        }
                        if (pbVar6[-0x10] == 1)
                        {
                            local_10[2] = local_10[2] + 1;
                        }
                        if (*pbVar6 == 1)
                        {
                            local_10[2] = local_10[2] + 1;
                        }
                        if (pbVar6[0x10] == 1)
                        {
                            local_10[2] = local_10[2] + 1;
                        }
                        pbVar6 = pbVar6 + 0x40;
                    } while ((int)pbVar6 < 0x11bd520);
                }
                else
                {
                    local_10[2] = (uint)((iVar2 * 100) / iVar4);
                }

                /*
                if (99 < (int)local_10[2])
                {
                    achievementUnlockAchievement(0xe);
                }*/
                return;
            }
        } while (true);

        //_TOMenuMakeJobAbilityList_handle.orig_fptr.Invoke(param_1, param_2);
    }

    public unsafe int h_MsGetJobAbilityList(int param_1, int param_2, int* param_3, int param_4)
    {
        short sVar1;
        short sVar2;
        uint uVar3;
        uint uVar4;
        int iVar5;
        int iVar6;
        int iVar7;
        int iVar8;
        int iVar9;
        int iVar10;
        int iVar11;
        uint uVar12;
        int iVar13;
        int iVar14;
        uint* puVar15;

        ushort* DAT_00df9258 = FhUtil.ptr_at<ushort>(0x9f9258); // contains auto-abilities and commands to add from accessories
        nint DAT_00df9258_addr = (nint)(DAT_00df9258);

        iVar13 = 0;
        uVar3 = _MsGetChrNum((uint)param_1);
        uVar4 = (uint)_MsCalcChrLevel((byte)uVar3);
        //iVar5 = _MsGetRomJob(uVar3, (uint)param_2, 0);
        iVar5 = (int)_MsGetRomJob(uVar3, (uint)param_2, null);
        if (iVar5 != 0)
        {
            iVar6 = (int)_MsBtlMonsterSaveNumCheck(uVar3);
            if (iVar6 == 0)
            {
                iVar7 = 0x10;
                iVar5 = iVar5 + 0x3c;//job.bin ds abilities table
            }
            else
            {
                iVar7 = 2;
                iVar5 = iVar5 + 0xb0;
            }
            /*
            if ((iVar5 != 0) && (iVar14 = 0, iVar7 != 0))
            {
                do
                {
                    sVar1 = *(short*)(iVar5 + 2 + iVar14 * 4);
                    sVar2 = *(short*)(iVar5 + iVar14 * 4);
                    if (sVar1 != 0)
                    {
                        iVar8 = MsCheckAbility(uVar3, sVar2, uVar4);
                        /* Excel/MsGetRomAbility related
                            */
                        /*
                        iVar9 = FUN_006294f0(sVar1, &DAT_00df9258, iVar13);
                        iVar10 = MsCheckLearnCommand(uVar3, sVar1);
                        if (((((iVar6 != 0) || (param_4 == 0)) || (sVar2 == 0)) || (iVar11 = MsGetSaveCommand(uVar3, sVar1), iVar11 != 0)) && (((iVar8 != 0 && (iVar9 != 0)) && ((iVar10 != 0 && (iVar13 < 0x10))))))
                        {
                            *(short*)((int)&DAT_00df9258 + iVar13 * 2) = sVar1;
                            iVar13 = iVar13 + 1;
                        }
                    }
                    iVar14 = iVar14 + 1;
                } while (iVar14 < iVar7);
                if (0xf < iVar13) goto LAB_00629c2d;
            }*/

            if (iVar5 != 0)
            {
                for (iVar14 = 0; iVar14 < iVar7; iVar14++)
                {
                    sVar1 = *(short*)(iVar5 + 2 + iVar14 * 4);
                    sVar2 = *(short*)(iVar5 + iVar14 * 4);
                    if (sVar1 != 0)
                    {
                        iVar8 = (int)_MsCheckAbility(uVar3, sVar2, (int)uVar4);
                        /* Excel/MsGetRomAbility related
                            */
                        iVar9 = (int)_FUN_6294f0((uint)sVar1, (int)DAT_00df9258_addr, iVar13);
                        iVar10 = _MsCheckLearnCommand((byte)uVar3, sVar1);

                        iVar11 = (int)_MsGetSaveCommand(uVar3, (uint)sVar1);

                        bool left_condition = iVar6 != 0 || param_4 == 0 || sVar2 == 0 || iVar11 !=0;
                        bool right_condition = (iVar8 != 0 && iVar9 != 0) && (iVar10 != 0 && (iVar13 < 10));
                        /*
                        if (((((iVar6 != 0) || (param_4 == 0)) || (sVar2 == 0)) || (iVar11 = _MsGetSaveCommand(uVar3, sVar1), iVar11 != 0)) && (((iVar8 != 0 && (iVar9 != 0)) && ((iVar10 != 0 && (iVar13 < 0x10))))))
                        {
                            *(short*)((int)&DAT_00df9258 + iVar13 * 2) = sVar1;
                            iVar13 = iVar13 + 1;
                        }*/

                        if (left_condition && right_condition)
                        {
                            //*(short*)((int)&DAT_00df9258 + iVar13 * 2) = sVar1;
                            DAT_00df9258[iVar13] = (ushort)sVar1;
                            iVar13 = iVar13 + 1;
                        }

                    }
                }

                if (iVar13 > 0xF)
                {
                    goto LAB_00629c2d;
                }
            }


        }

        ushort* DAT_00ff00ff = FhUtil.ptr_at<ushort>(0xbf00ff); // contains auto-abilities and commands to add from accessories
        nint DAT_00ff00ff_addr = (nint)(DAT_00ff00ff);

        /*
        puVar15 = (undefined4*)((int)&DAT_00df9258 + iVar13 * 2);
        for (uVar12 = 0x10U - iVar13 >> 1; uVar12 != 0; uVar12 = uVar12 - 1)
        {
            *puVar15 = (uint)DAT_00ff00ff_addr;
            puVar15 = puVar15 + 1;
        }
        for (uVar12 = (uint)((0x10U - iVar13 & 1) != 0); uVar12 != 0; uVar12 = uVar12 - 1)
        {
            *(ushort*)puVar15 = 0xff;
            puVar15 = (undefined4*)((int)puVar15 + 2);
        }
        */
        for (int i = iVar13; i < 16; i++)
        {
            DAT_00df9258[i] = 0x00FF;
        }

    LAB_00629c2d:
        /*
        if (param_3 != (undefined4*)0x0)
        {
            *param_3 = 0x10;
        }*/

        if (param_3 != null)
        {
            *param_3 = 0x10;
        }
        /* Returns Added abilities? From Accessories? */
        //return &DAT_00df9258;
        return (int)DAT_00df9258_addr;

        //return _MsGetJobAbilityList_handle.orig_fptr.Invoke(param_1, param_2, param_3, param_4);
    }


    public override void render_imgui()
    {
        //base.render_imgui();
        uint show_job_ability_list = FhUtil.get_at<uint>(0x12c0260); // flag which is 1 when viewing a dressphere's abilities in the Abilities Menu
        byte job_number = FhUtil.get_at<byte>(0x12c0266);// the dressphere ID number which is being viewed / was last viewed.

        if (show_job_ability_list == 1)
        {
            if (31 < job_number)
            {
                ImGui.Begin("Dressphere Abilities Window");

                ImGui.Text("Ability");

                ImGui.End();
            }
        }

    }



    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return _MsGetJobAbilityList_handle.hook()
            && _TOMenuMakeJobAbilityList_handle.hook()
            && _TOMenuStartJobAbilityWindow_handle.hook()
            && _FUN_778160_handle.hook();
            
    }

    public override void load_local_state(FileStream? local_state_file, FhLocalStateInfo local_state_info) { }
    public override void save_local_state(FileStream  local_state_file)                                    { }
}
