using System.Text;

namespace Fahrenheit.Mods.X2DSUnlimit;

//used for rendering - fetch data to fill, and update underlying data, not these directly?
// Populated by TOMenuMakeJobAbilityList
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x10)]
public struct DSAbilityListDataAbility {
    [FieldOffset(0x00)] public bool is_visible;
    [FieldOffset(0x01)] public bool b2;
    [FieldOffset(0x02)] public bool is_mastered;
    [FieldOffset(0x03)] public bool is_selected;
    [FieldOffset(0x04)] public uint ability_id;
    [FieldOffset(0x08)] public uint ap_current;
    [FieldOffset(0x0C)] public uint ap_needed;
}

[InlineArray(16)]
public struct DSAbilityListDataAbilityArray {
    private DSAbilityListDataAbility _element0;
}

// Populated by TOMenuMakeJobAbilityList, 0x110 per dressphere
// FFX-2.exe + DBB200 + (DS_ID * 0x110)
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 0x110)]
public struct DSAbilityListData {
    [FieldOffset(0x00)] public int i0;
    [FieldOffset(0x04)] public int ds_id;
    [FieldOffset(0x08)] public int percentage;
    [FieldOffset(0x0C)] public int i1;
    [FieldOffset(0x10)] public DSAbilityListDataAbilityArray Abilities;
}

public partial class X2DSUnlimitModule : FhModule {

    public void h_TOMenuSetSaveLearn(byte param_1, uint param_2, int param_3) {
        _TOMenuSetSaveLearn_handle.orig_fptr.Invoke(param_1, param_2, param_3);
        return;
    }

    public void h_TOMenuSetMacroCommandType(int param_1, int param_2, byte param_3) {
        _TOMenuSetMacroCommandType_handle.orig_fptr.Invoke(param_1, param_2, param_3);
        return;
    }

    public int h_TOBtlGetComName(uint param_1) {
        return _TOBtlGetComName_handle.orig_fptr.Invoke(param_1);
    }

    public void h_TOMenuSetMacroCommandValue(int param_1, int param_2, uint param_3) {
        _TOMenuSetMacroCommandValue_handle.orig_fptr.Invoke(param_1, param_2, param_3);
        return;
    }

    public uint h_TOGetMenuText(uint param_1) {
        return _TOGetMenuText_handle.orig_fptr.Invoke(param_1);
    }

    public uint h_SndSepPlaySimple(uint param_1) {
        return _SndSepPlaySimple_handle.orig_fptr.Invoke(param_1);
    }

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
        uint job_idNum_toCheck;
        uint job_id;


        DSAbilityListData* job_table = FhUtil.ptr_at<DSAbilityListData>(0xdbb200);

        for (int i = 0; i < 32; i++) {
            ref DSAbilityListData job = ref job_table[i];
            job.i0 = 0;
            job.ds_id = 0xff;
            job.percentage = 0;
            job.i1 = 0;

            Span<DSAbilityListDataAbility> abilities = job.Abilities;
            abilities.Clear();
        }

        FhUtil.set_at<byte>(0x12c0265, 0);
        FhUtil.set_at<byte>(0x12c0266, 0);// abilities menu: dressphere id viewing/last viewed

        job_idNum_toCheck = 0;

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

                    if (h_MsGetSaveDreSphere(job_id) != 0)// if dressphere obtained/in inventory
                    {
                        validJob = true;
                        h_TOMenuMakeJobAbilityList((uint)chr_id, job_id);
                    }
                    break;

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

            ref DSAbilityListData job_entry = ref job_table[entry];
            if (validJob) {
                job_entry.i0 = 1;
                job_entry.ds_id = (int)job_id;

                byte count = FhUtil.get_at<byte>(0x12c0265);
                FhUtil.set_at<byte>(0x12c0265, (byte)(count + 1));
            }
            else {
                job_entry.i0 = 0;
                job_entry.ds_id = 0xff;
                job_entry.percentage = 0;
                job_entry.i1 = 0;
            }

            job_idNum_toCheck++;
        }

        /*
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
        job_idNum_toCheck++;*/
    }



    // p1 is DAT_00df6d80 -> chr_id of who is viewed in the menu YRP -> 0,1,2, p2 is ds_id from kyGetJobIndex (currently equipped/viewed ds_id?)
    // Function updated to use custom table
    public unsafe void h_TOMenuStartJobAbilityWindow(uint param_1, uint param_2) {
        FhUtil.set_at<uint>(0x12c0270, param_1);
        FhUtil.set_at<uint>(0x12c0274, param_2 | 0x5000);

        DSAbilityListData* jobTable = FhUtil.ptr_at<DSAbilityListData>(0xdbb200);
        uint targetDsId = param_2 | 0x5000;

        int iVar1 = 0;
        do {
            ushort ds_full_id = CustomTOMenuStartJobAbilityWindow_DS_Table[iVar1];
            byte checking_ds_id = (byte)(ds_full_id & 0xff);

            if (jobTable[checking_ds_id].ds_id == targetDsId) {
                FhUtil.set_at<byte>(0x12c0266, (byte)iVar1);
            }

            iVar1++;
        } while (iVar1 < CustomTOMenuStartJobAbilityWindow_DS_Table.Length);

        FhUtil.set_at<uint>(0x963ed4, 7); // used for menu progression: tells game it's now entered the DS ability list with 16 moves showing, AP, mastery, etc.
    }

    // Writes abilities menu, dressphere 16x ability list data
    public unsafe void h_TOMenuMakeJobAbilityList(uint chr_id, uint job_id)
    {
        uint uVar1;
        int iVar2;
        uint uVar3;
        int iVar4;
        int iVar5;
        byte* pbVar6;
        uint uVar7;
        uint* puVar8;
        byte* pbVar9;

        int local_18;
        ushort* local_14;
        uint* local_10;
        int local_c;
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
        local_10 = (uint*)DAT_11bb200_addr + (job_id & 0xfff) * 0x44; // start of ability list data
        local_8 = chr_id;

        byte* local_1c = stackalloc byte[64];

        iVar2 = (int)h_MsGetRomJob(chr_id, job_id, local_1c);
        uVar1 = local_8;
        local_14 = (ushort*)(iVar2 + 0x3e); // start of job.bin dressphere abilities

        byte* D_dbb213 = FhUtil.ptr_at<byte>(0xdbb213);

        //iVar2 = (int)((job_id & 0xfff) * 0x110 + 0x11bb213);
        iVar2 = (int)((job_id & 0xfff) * 0x110 + (int)D_dbb213);
        local_18 = 0x10;
        do
        {
            uVar7 = (uint)*local_14; // command/a_ability id
            
            *(byte*)(iVar2 + -3) = 0; // zeroes is visible bool
            *(ushort*)(iVar2 + -1) = 0; // zeroes is mastered bool

            *(uint*)(iVar2 + 1) = uVar7; // command

            uVar3 = h_MsGetSaveAp(uVar1, uVar7); // current AP
            *(uint*)(iVar2 + 5) = uVar3;

            iVar4 = h_MsGetSaveNeedAp((byte)uVar1, uVar7); // Needed AP
            uVar3 = local_8;
            *(int*)(iVar2 + 9) = iVar4;

            if (iVar4 < *(int*)(iVar2 + 5)) // if current AP greater than needed AP
            {
                *(int*)(iVar2 + 5) = iVar4; // set current AP to needed AP Value (cap, stop increasing)
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
                        *(byte*)(puVar8 - 1) = 1; // updates is_selected bool
                        uVar7 = h_MsGetSaveLearn(uVar3, job_id);
                        iVar2 = local_c;
                        if (uVar7 == *puVar8)
                        {
                            *(byte*)((int)puVar8 - 1) = 1; // updates is_selected bool
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
        pbVar9 = (byte*)((int)local_10 + 0x12); // skip jobid header part of AbilityListData, centers on is_mastered
        pbVar6 = pbVar9;

        iVar2 = 0x10; // 16 abilities
        do
        {
            iVar5 = iVar2;
            iVar2 = 0;
            if (0 < local_c)
            {
                do
                {

                    /*
                    uint testedability_maybe = *(uint*)(pbVar6 + 2);
                    uint tested_against = (uint)*(ushort*)(iVar4 + iVar2 * 2); // from job_abilities
                    */

                    // if ability id? = ability to check against
                    if (*(uint*)(pbVar6 + 2) == (uint)*(ushort*)(iVar4 + iVar2 * 2)) 
                    {

                        pbVar6[-2] = 1; // is visible
                        pbVar6[0] = 1; // is mastered
                        pbVar6[1] = 0; // is selected
                        break;
                    }
                    iVar2 = iVar2 + 1;
                } while (iVar2 < local_c);
            }
            pbVar6 = pbVar6 + 0x10; //AbilityListDataAbility struct size
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

                    //uint* DAT_11bd420 = FhUtil.ptr_at<uint>(0xdbd420);
                    //pbVar6 = &DAT_011bd420;
                    pbVar6 = FhUtil.ptr_at<byte>(0xdbd420);


                    /*
                    byte* start_ptr = FhUtil.ptr_at<byte>(0xdbd420);
                    byte* end_ptr  = FhUtil.ptr_at<byte>(0xdbd520);

                    for (byte* pcVar5 = start_ptr; pcVar5 < end_ptr; pcVar5 += 0x40) {
                        if (pcVar5[-0x20] == 1) { local_10[2]++; }
                        if (pcVar5[-0x10] == 1) { local_10[2]++; }
                        if (pcVar5[0x00] == 1)  { local_10[2]++; }
                        if (pcVar5[0x10] == 1)  { local_10[2]++; }
                    }*/
                    
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

    // In Abilities Menu, job list - handles command window preview and auto abilities?
    // Also 16 x ability list
    public unsafe int h_MsGetJobAbilityList(int chr_id, int job_id, int* param_3, int param_4)
    {

       

        // working reimplementation - 
        ushort ability;
        ushort requirement;
        uint chr_num;
        uint chr_level;
        int iVar5;
        bool is_monster;
        int num_abilities_to_check;
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
        chr_num = h_MsGetChrNum((uint)chr_id);
        chr_level = (uint)h_MsCalcChrLevel((byte)chr_num);

        //iVar5 = _MsGetRomJob(uVar3, (uint)param_2, 0);
        iVar5 = (int)h_MsGetRomJob(chr_num, (uint)job_id, null);
        if (iVar5 != 0)
        {
            is_monster = h_MsBtlMonsterSaveNumCheck(chr_num); // Check if Player character or Creature
            if (!is_monster)
            {
                num_abilities_to_check = 0x10;
                iVar5 = iVar5 + 0x3c;//job.bin ds abilities table
            }
            else
            {
                num_abilities_to_check = 2;
                iVar5 = iVar5 + 0xb0; // creature data
            }
            

            if (iVar5 != 0)
            {
                for (iVar14 = 0; iVar14 < num_abilities_to_check; iVar14++)
                {
                    // iVar5 is the address of Job.dressphere_abilities here
                    ability = *(ushort*)(iVar5 + 2 + iVar14 * 4);
                    requirement = *(ushort*)(iVar5 + iVar14 * 4);

        
                    if (ability != 0)
                    {
                        iVar8 = (int)h_MsCheckAbility(chr_num, requirement, (int)chr_level); // does the character have the prereq
                        iVar9 = (int)h_FUN_6294f0((uint)ability, (int)DAT_00df9258_addr, iVar13);// Excel/MsGetRomAbility related 
                        iVar10 = h_MsCheckLearnCommand((byte)chr_num, ability);
                        iVar11 = (int)h_MsGetSaveCommand(chr_num, (uint)ability);

                        bool left_condition = is_monster || param_4 == 0 || requirement == 0 || iVar11 != 0;
                        bool right_condition = (iVar8 != 0 && iVar9 != 0) && (iVar10 != 0 && (iVar13 < 0x10));
                        
                        if (left_condition && right_condition)
                        {
                            //*(short*)((int)&DAT_00df9258 + iVar13 * 2) = sVar1;
                            DAT_00df9258[iVar13] = ability;
                            iVar13 = iVar13 + 1;
                        }
                    }

                }

                
                if (iVar13 > 0xF) // if reached maximum abilities return
                {
                    goto LAB_00629c2d;
                }
            }
        }

        for (int i = iVar13; i < 16; i++) // reset memory
        {
            DAT_00df9258[i] = 0x00FF;
        }

    LAB_00629c2d:

        if (param_3 != null)
        {
            *param_3 = 0x10;
        }

        return (int)DAT_00df9258_addr; // Returns mem location of added command/abilities From Accessories
        
    }


    public unsafe void h_FUN_777270(uint param_1) {

        uint uVar8 = *(uint*)(param_1 + 0x28);
        if (uVar8 < 0x14) {

            uint* switch_data_777864 = FhUtil.ptr_at<uint>(0x377864);

            if ((switch_data_777864[uVar8] & 0xFFFF) == 0x762F) {
                // Intercept switch case -> user selects command to learn ( not mastered )

                byte ds = FhUtil.get_at<byte>(0x12c0266);
                byte job_num = (byte)CustomTOMenuStartJobAbilityWindow_DS_Table[ds];

                uint menu_chr_id = FhUtil.get_at<uint>(0x12c0270);
                uint menu_job_id = FhUtil.get_at<uint>(0x12c0274);

                short offset_a = *(short*)(param_1 + 0x4c);
                int offset_b = *(int*)(param_1 + 0x5c);
                int slot = offset_b + offset_a * 2;

                h_TOMenuSetSaveLearn((byte)menu_chr_id, menu_job_id, slot);
                h_TOMenuMakeJobAbilityList(menu_chr_id, menu_job_id);
                h_TOMenuSetMacroCommandType(0, 1, 0);

                DSAbilityListData* abi_list_data = FhUtil.ptr_at<DSAbilityListData>(0xdbb200);
                uint abilityId = abi_list_data[job_num].Abilities[slot].ability_id;
                uint uVar9 = (uint)h_TOBtlGetComName(abilityId);

                h_TOMenuSetMacroCommandValue(0, 1, uVar9);
                uVar9 = h_TOGetMenuText(0x107a);
                *(uint*)(param_1 + 0x24) = uVar9;
                *(uint*)(param_1 + 0x28) = 9; // Menu progression state?
                return;
            }
            else if ((switch_data_777864[uVar8] & 0xFFFF) == 0x7587) {
                // Intercept switch case -> user selects command to learn (already learned)

                int slot = *(int*)(param_1 + 0x5c) + *(short*)(param_1 + 0x4c) * 2;

                byte ds = FhUtil.get_at<byte>(0x12c0266);
                byte job_num = (byte)CustomTOMenuStartJobAbilityWindow_DS_Table[ds];

                DSAbilityListData* abi_list_data = FhUtil.ptr_at<DSAbilityListData>(0xdbb200);
                ref DSAbilityListDataAbility ability = ref abi_list_data[job_num].Abilities[slot];

                if (ability.ability_id == 0x300a) {
                    h_SndSepPlaySimple(0x80000001);
                    *(ushort*)(param_1 + 0x2c) = *(ushort*)(param_1 + 0x4c);
                    *(byte*)(param_1 + 0x48) = (byte)(*(byte*)(param_1 + 0x48) + 1);
                    *(uint*)(param_1 + 0x28) = 0xd;
                    return;
                }

                if (!ability.is_mastered) {
                    h_SndSepPlaySimple(0x8000000a);
                    *(uint*)(param_1 + 0x80) = 1;
                    *(uint*)(param_1 + 0x28) = 7;
                    return;
                }

                h_SndSepPlaySimple(0x80000003);
                *(uint*)(param_1 + 0x80) = 0;
                *(uint*)(param_1 + 0x28) = 8;
                return;
            }
            else {
                _FUN_777270_handle.orig_fptr.Invoke(param_1);
            }
        }
    }

    public unsafe bool h_FUN_776EC0(uint param_1, int ability_slot) {
        DSAbilityListData* abi_list_data = FhUtil.ptr_at<DSAbilityListData>(0xdbb200);
        byte ds = FhUtil.get_at<byte>(0x12c0266);

        byte job_index = (byte)CustomTOMenuStartJobAbilityWindow_DS_Table[ds & 0xff];

        DSAbilityListData* job = &abi_list_data[job_index];

        return job->Abilities[ability_slot].is_visible;
    }

    /// <summary>
    /// Renders the dressphere ability list in the Abilities menu -> 16 dressphere abilities, icons, names, master icon and AP
    /// </summary>
    public unsafe void h_FUN_778160(int param_1, int param_2, int param_3, int param_4)
    {

        int puVar1; // is a memory address
        uint uVar2;
        uint uVar3;
        int iVar4;
        int iVar5;
        double fVar7;
        float[] local_28 = new float[4];
        byte[] local_18 = new byte[16];
        local_28[0] = 0.0f;
        local_28[1] = 0.0f;
        local_28[2] = 0.0f;
        local_28[3] = 0.0f;


        int local_30 = param_3;   // 00778199: initial copy
        uVar2 = (uint)(param_4 + *(short*)(param_1 + 0x32) * 2);

        if (uVar2 < 0x10)
        {
#region Unknown1
            uVar3 = uVar2 & 0x8000000f;
            if ((int)uVar3 < 0)
            {
                uVar3 = (uVar3 - 1 | 0xfffffff0) + 1;
            }

            byte* DAT_016c0278 = FhUtil.ptr_at<byte>(0x12c0278);
            puVar1 = (int)((int)(DAT_016c0278) + uVar3 * 0x10);

            iVar4 = _TOGetRtcValue(puVar1);
            iVar5 = _TOGetRtcRatio(puVar1);
            if (iVar5 / 2 + 0x800 < 0x800)
            {
                iVar5 = _TOGetRtcRatio(puVar1);
                local_28[0] = (float)((float)(iVar5 / 2) * 3.1415927);
            }
            else
            {
                iVar5 = _TOGetRtcRatio(puVar1);
                local_28[0] = (float)((float)(0x800 - iVar5 / 2) * -3.1415927);
            }

            local_28[0] = (float)(local_28[0] * 0.00048828125);
            if (*(byte*)(param_1 + 0x42) == 0)
            {
                param_3 = iVar4;
                local_30 = iVar4;      // Kept in sync
            }
            #endregion Unknown1

#region ListItemBB
            // Draw background rectangles under abilities
            iVar4 = _TOGetRtcRatio(puVar1);
            int barValue = ((int)uVar2 / 2) * 0x199;
            if (iVar4 != 0)
            {
                fixed (float* pAlpha = &local_28[0]) {
                    // Render backplates under Ability Icon, name, AP
                    _TOMenuDrawRotPlate(param_2, param_3, (int)pAlpha, 6, 0, barValue, 0);
                }
            }
#endregion ListItemBG


#region Ability Visibilty and Current Selection
            //Replaced table reference D63E78 with custom table
            byte ds = FhUtil.get_at<byte>(0x12c0266); // current dressphere ID
            byte job_index = (byte)(CustomTOMenuStartJobAbilityWindow_DS_Table[ds] & 0xFF); // Derived Job Index
            int job_offset = (int)job_index * 0x110;
            int ability_index = (int)uVar2;

            DSAbilityListData* abi_list_data = FhUtil.ptr_at<DSAbilityListData>(0xdbb200);
            DSAbilityListData* job = &abi_list_data[job_index];
            DSAbilityListDataAbility* ability = &job->Abilities[ability_index];


            if((ability->is_visible) && (-1.5707964 < local_28[0]) && (local_28[0] < 1.5707964))
            {
                iVar4 = _TOGetRtcRatio(puVar1);
                if ((ability->is_selected) && (iVar4 == 0x1000))
                {
                    _TOMenuOpenPkt();

                    double DAT_00c32010_value = FhUtil.get_at<double>(0x832010);
                    double valA = _offsetAdjust_Y(0x3b) - DAT_00c32010_value;
                    int fixedA = (int)valA;

                    // W = offsetAdjust_X(0x2d8)
                    double valB = _offsetAdjust_X(0x2d8);
                    int fixedB = (int)valB;

                    // Y = offsetAdjust_Y(7) + local_30 (savedCount / param_3)
                    double valC = _offsetAdjust_Y(7) + local_30;
                    int fixedC = (int)valC;

                    // X = offsetAdjust_X(9) + param_2
                    double valD = _offsetAdjust_X(9) + param_2;
                    int fixedD = (int)valD;

                    // Render Ability selection highlight, last parameter is colour
                    _FUN_007B0F50(fixedD, fixedC, fixedB, fixedA, 0x3); //TOMkpScrollWaveXYWH?

                    // Alternate colour
                    //_FUN_007B0F50(fixedD, fixedC, fixedB, fixedA, 0x11); //TOMkpScrollWaveXYWH?
                    _TOKickPacket();
                }
#endregion Ability Visibility and Current Selection


                _TOMenuOpenPkt();
                _TOMenuChangeFrameAccPlate(6);
                _FFX2_Set_UI_Scale(0x3f55f15f, 0x3f313b14);
                _TOGetFFXLang();

                double DAT_00cad868 = FhUtil.get_at<double>(0x8ad868);
                double test_double = _offsetAdjust_Y(0xf) + local_30 - DAT_00cad868;
                int test_double_as_int = (int)(test_double);

                int DAT_011bb214_addr = (int)FhUtil.ptr_at<byte>(0xdbb214);

                // Render Ability icon and Name
                _TOMkpComIconNameClut(*(uint*)(DAT_011bb214_addr + ability_index * 0x10 + job_offset), param_2 + 6, test_double_as_int, 0); 
                _FFX2_Reset_UI_Scale();


                // Handle AP / Needed AP vs Master icons

                //byte* DAT_011bb212 = FhUtil.ptr_at<byte>(0xdbb212);
                //if ((DAT_011bb212)[ability_index * 0x10 + job_offset] == 1)

                if (ability->is_mastered)
                {

                    _FFX2_Set_UI_Scale(0x3f092492, 0x3f333333);

                    FhUtil.set_at<byte>(0x12f0ac0, 1);

                    int timer_val = _TkMenuGetTimer();

                    double DAT_00cad890 = FhUtil.get_at<double>(0x8ad890);
                    double val = _offsetAdjust_Y(0x14) + local_30 - DAT_00cad890;
                    int fixedVal = (int)val;

                    // Render Ability Mastered icons
                    _TOMkpShape2dMenu(param_2 + 0xa2, fixedVal, 8, timer_val);

                    FhUtil.set_at<byte>(0x12f0ac0, 0);
                }
                else
                {
                    // AP / Needed AP Handling
                    //_sprintf((byte*)local_18, "%3d/%3d", *(uint*)(DAT_011bb218_addr + ability_index * 0x10 + job_offset), *(uint*)(DAT_011bb21c_addr + ability_index * 0x10 + job_offset));

                    uint current_ap = ability->ap_current;
                    uint required_ap = ability->ap_needed;

                    // prepare string
                    string s = $"{current_ap,3}/{required_ap,3}";
                    byte[] bytes = Encoding.ASCII.GetBytes(s);
                    int len = Math.Min(bytes.Length, local_18.Length - 1);
                    Array.Copy(bytes, local_18, len); // copy into buffer
                    local_18[len] = 0; // null terminate

                    iVar4 = (int)_TOGetFFXLang();
                    if (iVar4 == 0) {
                        _FFX2_Set_UI_Scale(0x3eec4ec5, 0x3f19999a);

                        double DAT_00cad890 = FhUtil.get_at<double>(0x8ad890);
                        double val = _offsetAdjust_Y(0x16) + local_30 - DAT_00cad890;
                        int fixedVal = (int)val;

                        // Render AP / Needed AP
                        fixed (byte* pText = local_18) {
                            _FUN_007AE430(pText, param_2 + 0xc3, fixedVal, 0x80, 0x80, 0x80, 0x80); // TOMkpEasyMesFontLRight?
                        }
                    }
                    else
                    {
                        // Non-Western text handling?
                        if (local_18[0] != 0)
                        {
                            int i = 0;
                            do {
                                if (local_18[i] == 0x20) {
                                    local_18[i] = 0x3A; // ':'
                                }
                                else if (local_18[i] == 0x2F) {
                                    local_18[i] = 0x49; // 'I'
                                }
                                i++;
                            } while (local_18[i] != 0);
                        }
                        _FFX2_Set_UI_Scale(0x3f55f15f, 0x3f313b14);

                        fVar7 = (double)_offsetAdjust_Y(10);
                        fixed (byte* pText = local_18) {
                            // TOAdpMesFontLXYZClutTypeRGBAChangeFontType caller (here) / TOMkpAscStrRightRGBA (switch ver)
                            _FUN_007aeda0((int)pText, (param_2 + 0xc3), (int)(fVar7 + param_3));
                        }
                    }
                }
                _FFX2_Reset_UI_Scale();
                _TOMkpResetFrameAcc();

                fixed (float* pAlpha = &local_28[0]) {
                    _TOMkpExPlateParam(param_2, param_3, (int)pAlpha, 6, 0);
                }

                _TOKickPacket();
                return;
            }
        }
        return;

    }
}


