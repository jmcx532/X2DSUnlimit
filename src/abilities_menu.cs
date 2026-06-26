namespace Fahrenheit.Mods.X2DSUnlimit;

public partial class X2DSUnlimitModule : FhModule {

    // Used for Abilities Menu, necessary for Freelancer and Leblanc Goon to show up in job list
    public unsafe uint h_kyGetJobNum3()
    {
        byte bVar1;
        int iVar2;
        int iVar3;
        uint number_of_elements;
        byte* unique_ds_on_grid_list = FhUtil.ptr_at<byte>(0x9f5fc4);// list of dressphere IDs (byte), unique ones on current grid, 

        byte* ds_amount = FhUtil.ptr_at<byte>(0x9f6018); //this is speculative

        iVar3 = 0;
        number_of_elements = 0;
        for (int i = 0; i < CustomDsLookupTable.Length; i++)
        {
            iVar2 = h_MsGetSaveDreSphere(CustomDsLookupTable[i]);
            if (0 < iVar2)
            {
                unique_ds_on_grid_list[number_of_elements] = (byte)CustomDsLookupTable[i];
                number_of_elements++;
            }

            //(&DAT_00df6018)[iVar3] = (byte)iVar2;
            ds_amount[iVar3] = (byte)iVar2;
            iVar3 = iVar3 + 1;
        }

        //ints with special dressphere ids, + chr_id menu character, 8 4
        //iVar3 = MsGetSaveDreSphere(*(undefined4*)(&DAT_00d48a90 + DAT_00df6d80 * 4));
        int* special_ds_id_records = FhUtil.ptr_at<int>(0x948A90);
        byte menu_chr_id = FhUtil.get_at<byte>(0x9f6d80); // in certain Tri/Y/V Menus, is the chr_id of the character who's being looked at.
        int tgt_special_ds_id = special_ds_id_records[menu_chr_id];

        iVar3 = h_MsGetSaveDreSphere((uint)tgt_special_ds_id);
        if (0 < iVar3)
        {
            FhUtil.set_at<ushort>(0x9f6028, 0x101);

            bVar1 = (byte)special_ds_id_records[menu_chr_id];
            unique_ds_on_grid_list[number_of_elements] = bVar1;
            FhUtil.set_at<byte>(0x9f602a, 1);
            unique_ds_on_grid_list[number_of_elements + 1] = (byte)(bVar1 + 1);
            unique_ds_on_grid_list[number_of_elements + 2] = (byte)(bVar1 + 2);
            number_of_elements = number_of_elements + 3;
        }


        FhUtil.set_at<byte>(0x9f602c, (byte)number_of_elements);
        _logger.Info("Return result: " + number_of_elements.ToString());
        return number_of_elements;

    }

    public unsafe void* h_memset(void* _Dst, int _Val, uint _Size)
    {
        return _memset_handle.orig_fptr.Invoke(_Dst, _Val, _Size);
    }

    //adds Freelancer/Leblanc Goon to abilities menu job list
    public unsafe void h_TOMenuMakeJobList(int chr_id)
    {
        //int iVar1;
        uint job_idNum_toCheck;
        //undefined4* puVar3;
        uint* puVar3 = FhUtil.ptr_at<uint>(0xdbb208);
        nint puVar3_addr = (nint)puVar3;
        //uint* puVar4;
        uint job_id;


        // memory overwrite loop
        for (int i = 0; i < 32; i++)
        {
            puVar3[-2] = 0;
            puVar3[-1] = 0xff;
            puVar3[0] = 0;
            puVar3[1] = 0;

            h_memset(puVar3 + 2, 0, 0x100);

            puVar3 += 0x44;
        }


        FhUtil.set_at<byte>(0x12c0265, 0);
        FhUtil.set_at<byte>(0x12c0266, 0);// abilities menu: dressphere id viewing/last viewed

        job_idNum_toCheck = 0;
        uint* puVar4 = FhUtil.ptr_at<uint>(0xdbb204);

        // entry < 34 -> 34 is the nww max jobs to look through, added LG and Freelancer
        for (int entry = 0; entry < 34; entry++)
        {
            job_id = job_idNum_toCheck | 0x5000;

            bool validJob = false;

            switch (job_id)
            {
                case 0x5001:
                case 0x5002:
                case 0x5003:
                case 0x5004:
                case 0x5005:
                case 0x5006:
                case 0x5007:
                case 0x5008:
                case 0x5009:
                case 0x500A:
                case 0x500B:
                case 0x500C:
                case 0x500D:
                case 0x500E:
                case 0x501C:
                case 0x501D:
                case 0x5020: // Freelancer
                case 0x5021: // Leblanc Goon

                    if (h_MsGetSaveDreSphere(job_id) != 0)// if dressphere obtained/in inventory
                    {
                        validJob = true;
                        h_TOMenuMakeJobAbilityList((uint)chr_id, job_id);
                    }
                    break;

                case 0x500F:
                case 0x5010:
                case 0x5011:

                    if (chr_id == 0 &&
                        h_MsGetSaveDreSphere(0x500F) != 0)
                    {
                        validJob = true;
                        h_TOMenuMakeJobAbilityList(0, job_id);
                    }
                    break;

                case 0x5012:
                case 0x5013:
                case 0x5014:

                    if (chr_id == 1 &&
                        h_MsGetSaveDreSphere(0x5012) != 0)
                    {
                        validJob = true;
                        h_TOMenuMakeJobAbilityList(1, job_id);
                    }
                    break;

                case 0x5015:
                case 0x5016:
                case 0x5017:

                    if (chr_id == 2 &&
                        h_MsGetSaveDreSphere(0x5015) != 0)
                    {
                        validJob = true;
                        h_TOMenuMakeJobAbilityList(2, job_id);
                    }
                    break;
            }

            if (validJob)
            {
                puVar4[-1] = 1;
                puVar4[0] = job_id;

                byte count = FhUtil.get_at<byte>(0x12c0265);
                FhUtil.set_at<byte>(0x12c0265, (byte)(count + 1));
            }
            else
            {
                puVar4[-1] = 0;
                puVar4[0] = 0xff;
                puVar4[1] = 0;
                puVar4[2] = 0;
            }

            puVar4 += 0x44;
            job_idNum_toCheck++;
        }

        //_TOMenuMakeJobList_handle.orig_fptr.Invoke(param_1);
    }

    // p1 is DAT_00df6d80 -> chr_id of who is looked at in the menu YRP -> 0,1,2, p2 is ds_id from kyGetJobIndex (currently equipped ds_id?)
    public unsafe void h_TOMenuStartJobAbilityWindow(uint param_1, uint param_2)
    {
        int iVar1;

        FhUtil.set_at<uint>(0x12c0270, param_1);
        FhUtil.set_at<uint>(0x12c0274, param_2 | 0x5000);

        iVar1 = 0;
        do
        {
            // base of some dressphere related data
            uint* D_11bb204 = FhUtil.ptr_at<uint>(0xdbb204);
            int D_11bb204_addr = (int)(D_11bb204);

            // get dresspher ID
            ushort ds_full_id = CustomTOMSJAW_DS_Table[iVar1];
            byte checking_ds_id = (byte)(ds_full_id & 0xfff);

            //ushort check_val = *(ushort*)(D_11bb204_addr + (checking_ds_id * 0x44));
            ushort check_val = *(ushort*)(D_11bb204_addr + (checking_ds_id * 0x110)); //mul by 4 to work correctly (vanilla....)

            
            if ((param_2 | 0x5000) == check_val)
            {
            FhUtil.set_at<byte>(0x12c0266, (byte)iVar1); // should write the correct DS ID (ID Only, not 0x50xx)
            
            }
            iVar1 = iVar1 + 1;
        } while (iVar1 < CustomTOMSJAW_DS_Table.Length);// CHanged to ref new table length to include FL and LG

            FhUtil.set_at<uint>(0x963ed4, 7);// used for menu progression, tells game now entered ds ability list with 16 moves showing, ap, mastery etc.
    }

    public unsafe void h_TOMenuMakeJobAbilityList(uint chr_id, uint job_id)
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

        switch (job_id)
        {
            case 0x5010:
                chr_id = 3;
                break;
            case 0x5011:
                chr_id = 4;
                break;
            default:
                break;
            case 0x5013:
                chr_id = 5;
                break;
            case 0x5014:
                chr_id = 6;
                break;
            case 0x5016:
                chr_id = 7;
                break;
            case 0x5017:
                chr_id = 8;
                break;
        }

        uint* DAT_11bb200 = FhUtil.ptr_at<uint>(0xdbb200);
        nint DAT_11bb200_addr = (nint)DAT_11bb200;

        //local_10 = &DAT_011bb200 + (param_2 & 0xfff) * 0x44;
        local_10 = (uint*)DAT_11bb200_addr + (job_id & 0xfff) * 0x44;
        local_8 = chr_id;

        byte* local_1c = stackalloc byte[64];

        iVar2 = (int)h_MsGetRomJob(chr_id, job_id, local_1c);
        uVar1 = local_8;
        local_14 = (ushort*)(iVar2 + 0x3e); // start of job.bin dressphere abilities

        byte* D_dbb213 = FhUtil.ptr_at<byte>(0xdbb213);

        //iVar2 = (int)((param_2 & 0xfff) * 0x110 + 0x11bb213);
        iVar2 = (int)((job_id & 0xfff) * 0x110 + (int)D_dbb213);
        local_18 = 0x10;
        do
        {
            uVar7 = (uint)*local_14;
            //*(undefined1*)(iVar2 + -3) = 0;
            //*(undefined2*)(iVar2 + -1) = 0;
            *(byte*)(iVar2 + -3) = 0;
            *(ushort*)(iVar2 + -1) = 0; // zeroes is_selected bool
            *(uint*)(iVar2 + 1) = uVar7;
            uVar3 = h_MsGetSaveAp(uVar1, uVar7);
            *(uint*)(iVar2 + 5) = uVar3;
            iVar4 = h_MsGetSaveNeedAp((byte)uVar1, uVar7);
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

        local_18 = h_MsGetJobAbilityList((int)local_8, (int)job_id, &local_c, 0);
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
                        uVar7 = h_MsGetSaveLearn(uVar3, job_id);
                        iVar2 = local_c;
                        if (uVar7 == *puVar8)
                        {
                            *(byte*)((int)puVar8 + -1) = 1;// updates is_selected bool
                        }
                        break;
                    }
                    iVar5 = iVar5 + 1;
                } while (iVar5 < iVar2);
            }
            puVar8 = puVar8 + 4;
            iVar4 = iVar4 + -1;
        } while (iVar4 != 0);
        iVar4 = h_MsGetJobAbilityList((int)uVar3, (int)job_id, &local_c, 1);
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

    // In Abilities Menu, job list - handles command window preview and auto abilities
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
        uVar3 = h_MsGetChrNum((uint)param_1);
        uVar4 = (uint)h_MsCalcChrLevel((byte)uVar3);

        //iVar5 = _MsGetRomJob(uVar3, (uint)param_2, 0);
        iVar5 = (int)h_MsGetRomJob(uVar3, (uint)param_2, null);
        if (iVar5 != 0)
        {

            iVar6 = (int)h_MsBtlMonsterSaveNumCheck(uVar3);
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
            

            if (iVar5 != 0)
            {
                for (iVar14 = 0; iVar14 < iVar7; iVar14++)
                {
                    sVar1 = *(short*)(iVar5 + 2 + iVar14 * 4);
                    sVar2 = *(short*)(iVar5 + iVar14 * 4);
                    if (sVar1 != 0)
                    {
                        iVar8 = (int)h_MsCheckAbility(uVar3, sVar2, (int)uVar4);
                        iVar9 = (int)h_FUN_6294f0((uint)sVar1, (int)DAT_00df9258_addr, iVar13);/* Excel/MsGetRomAbility related */
                        iVar10 = h_MsCheckLearnCommand((byte)uVar3, sVar1);
                        iVar11 = (int)h_MsGetSaveCommand(uVar3, (uint)sVar1);

                        bool left_condition = iVar6 != 0 || param_4 == 0 || sVar2 == 0 || iVar11 !=0;
                        bool right_condition = (iVar8 != 0 && iVar9 != 0) && (iVar10 != 0 && (iVar13 < 10));
                        
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

        for (int i = iVar13; i < 16; i++)
        {
            DAT_00df9258[i] = 0x00FF;
        }

    LAB_00629c2d:

        if (param_3 != null)
        {
            *param_3 = 0x10;
        }

        // Returns mem location of added command/abilities From Accessories
        return (int)DAT_00df9258_addr;

        //return _MsGetJobAbilityList_handle.orig_fptr.Invoke(param_1, param_2, param_3, param_4);
    }




    // might reimplement this and other functions in future, instead of custom ImGui menu implemented in custom_menu_ability_list.cs
    // handles rendering of 16 ability boxes (Abilites->DS->this menu)
    // a lot of floating point / FPU guff?
    /*
    public unsafe void h_FUN_778160(int param_1, int param_2, int param_3, int param_4)
    {

        //_logger.Info("Param_1 is: " + param_1.ToString("X"));// memory address
        //_logger.Info("Param_2 is: " + param_2.ToString());// hard coded 0x2e or 0x10a
        //_logger.Info("Param_3 is: " + param_3.ToString());// 0xce + some*0x16
        //_logger.Info("Param_4 is: " + param_4.ToString());// iterator?

        _FUN_778160_handle.orig_fptr.Invoke(param_1, param_2, param_3, param_4);
        //return;



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
            if (((DAT_011bb210[uVar2 * 0x10 + iVar5] == 1) && (-1.5707964 < local_28[0])) && (local_28[0] < 1.5707964))
            //if ((((&DAT_011bb210)[uVar2 * 0x10 + iVar5] == '\x01') && (-1.5707964 < local_28[0])) && (local_28[0] < 1.5707964))
            {
                //if ((*(char*)(iVar5 + 0x11bb213 + uVar2 * 0x10) == 1) && (iVar4 = _FUN_007730c0(puVar1), iVar4 == 0x1000))
                iVar4 = _FUN_007730c0(puVar1);
                if ((*(byte*)(iVar5 + 0x11bb213 + uVar2 * 0x10) == 1) && (iVar4 == 0x1000))
                { //verify the + 0x11bb213 works
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


                //_offsetAdjust_Y(0xf, 0);
                //uVar6 = _FUN_0087e0d0();

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

                    //uVar6 = (uint)_FUN_00764680();
                    //_offsetAdjust_Y(0x14, 8, uVar6);
                    //uVar6 = _FUN_0087e0d0();
                    //_FUN_007b1250(param_2 + 0xa2, uVar6);

                    //DAT_016f0ac0 = 0;
                    FhUtil.set_at<byte>(0x12f0ac0, 1);
                }
                else
                {
                    int DAT_011bb218_addr = (int)FhUtil.ptr_at<byte>(0xdbb218);
                    int DAT_011bb21c_addr = (int)FhUtil.ptr_at<byte>(0xdbb21c);
                    _sprintf((byte*)local_18, "%3d/%3d", *(uint*)(DAT_011bb218_addr + uVar2 * 0x10 + iVar5), *(uint*)(DAT_011bb21c_addr + uVar2 * 0x10 + iVar5));
                    //_sprintf((byte*)local_18, "%3d/%3d", *(undefined4*)(&DAT_011bb218 + uVar2 * 0x10 + iVar5), *(undefined4*)(&DAT_011bb21c + uVar2 * 0x10 + iVar5));
                    iVar4 = (int)_TOGetFFXLang();
                    if (iVar4 == 0)
                    {
                        _FFX2_Set_UI_Scale(0x3eec4ec5, 0x3f19999a);
                        //_offsetAdjust_Y(0x16, 0x80, 0x80, 0x80, 0x80);
                        //uVar6 = _FUN_0087e0d0();
                        //_FUN_007ae430(local_18, param_2 + 0xc3, uVar6);
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

    }*/

}


