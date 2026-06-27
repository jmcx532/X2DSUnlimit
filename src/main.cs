
namespace Fahrenheit.Mods.X2DSUnlimit;

[FhLoad(FhGameId.FFX2)]
public partial class X2DSUnlimitModule : FhModule {

    // extra Job definitions
    private unsafe Job* rikku_freelancer_ptr;
    private unsafe Job* rikku_leblancgoon_ptr;

    private unsafe Job* paine_freelancer_ptr;
    private unsafe Job* paine_leblancgoon_ptr;

    // Local state / save state
    public static byte   freelancer_quantity = 0;
    public static byte   leblanc_goon_quantity = 0;
    public static ushort freelancer_ability_learning = 0;
    public static ushort leblancgoon_ability_learning = 0;

    private class X2DSUnlimitState
    {
        public byte   freelancer_quantity          { get; set; }
        public ushort freelancer_ability_learning  { get; set; }
        public byte   leblanc_goon_quantity        { get; set; }
        public ushort leblancgoon_ability_learning { get; set; }

        public X2DSUnlimitState()
        {
            this.freelancer_quantity = X2DSUnlimitModule.freelancer_quantity;
            this.leblanc_goon_quantity = X2DSUnlimitModule.leblanc_goon_quantity;

            this.freelancer_ability_learning  = X2DSUnlimitModule.freelancer_ability_learning;
            this.leblancgoon_ability_learning = X2DSUnlimitModule.leblancgoon_ability_learning;
        }
    }

    /// <summary>
    /// Table replacements
    /// Freelancer/Leblanc Goon ID's appended to mod copt of DAT_00D63e78 table (ffx-2.exe + 0x963e78)
    /// TOMsJobAbilityWindow+ and other functions (many)
    /// </summary>
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

    //replaces lookup table at: ffx-2.exe + cc36cc, used by the kyGetJobNum series of functions and kyGetUsedPoint ONLY.
    private static readonly ushort[] CustomDsLookupTable =
{
    0x0001, 0x0002, 0x0003, 0x0004, 0x0005,
    0x0006, 0x0007, 0x0008, 0x0009, 0x000A,
    0x000B, 0x000C, 0x000D, 0x000E, 0x001c,
    0x001d, 0x001f, 0x0020, 0x0021, 0x0022
};


    public unsafe X2DSUnlimitModule() {
        int addr_offset = 0x400000;

        #region // MethodHandles
        _MsCheckRange_handle = new FhMethodHandle<MsCheckRange>(this, "FFX-2.exe", 0x624cd0 - addr_offset, h_MsCheckRange);

        // rework in future to extend job.bin
        _MsGetRomJob_handle = new FhMethodHandle<MsGetRomJob>(this, "FFX-2.exe", 0x61deb0 - addr_offset, h_MsGetRomJob);

        //dressphere quantities
        _MsAddSaveDreSphere_handle = new FhMethodHandle<MsAddSaveDreSphere>(this, "FFX-2.exe", 0x60b260 - addr_offset, h_MsAddSaveDreSphere);
        _MsGetSaveDreSphere_handle = new FhMethodHandle<MsGetSaveDreSphere>(this, "FFX-2.exe", 0x60c710 - addr_offset, h_MsGetSaveDreSphere);
        
        // garment grid menu, job list
        _kyGetJobNum_handle = new FhMethodHandle<kyGetJobNum>(this, "FFX-2.exe", 0x5ea7b0 - addr_offset, h_kyGetJobNum);

        // garment grid icons
        _FUN_5E7580_handle = new FhMethodHandle<FUN_5E7580>(this, "FFX-2.exe", 0x5e7580 - addr_offset, h_FUN_5E7580);

        //__aullshr() functions
        _MsGetSavePlate_handle = new FhMethodHandle<MsGetSavePlate>(this, "FFX-2.exe", 0x60cc00 - addr_offset, h_MsGetSavePlate);
        _kyGetUsedPoint_handle = new FhMethodHandle<kyGetUsedPoint>(this, "FFX-2.exe", 0x5eb480 - addr_offset, h_kyGetUsedPoint);
        _kyIsUsedPoint_handle = new FhMethodHandle<kyIsUsedPoint>(this, "FFX-2.exe", 0x5eb9e0 - addr_offset, h_kyIsUsedPoint);
        _kyGetCursorPoint_handle = new FhMethodHandle<kyGetCursorPoint>(this, "FFX-2.exe", 0x5ea770 - addr_offset, h_kyGetCursorPoint);
        _kyGetResultPlateNum_handle = new FhMethodHandle<kyGetResultPlateNum>(this, "FFX-2.exe", 0x5eb2e0 - addr_offset, h_kyGetResultPlateNum);
        
        // spherechange
        _MsGetSaveDressUpCount_handle = new FhMethodHandle<MsGetSaveDressUpCount>(this, "FFX-2.exe", 0x60c730 - addr_offset, h_MsGetSaveDressUpCount);
        _F791610_handle = new FhMethodHandle<F791610>(this, "FFX-2.exe", 0x791610 - addr_offset, h_F791610);
        _MsGetSaveConfigChangeEffect_handle = new FhMethodHandle<MsGetSaveConfigChangeEffect>(this, "FFX-2.exe", 0x60c650 - addr_offset, h_MsGetSaveConfigChangeEffect);
        _MsGetRamConfigChangeEffect_handle = new FhMethodHandle<MsGetRamConfigChangeEffect>(this, "FFX-2.exe", 0x625c90 - addr_offset, h_MsGetRamConfigChangeEffect);

        //command menu preview and auto ability list
        _MsGetJobAbilityList_handle = new FhMethodHandle<MsGetJobAbilityList>(this, "FFX-2.exe", 0x629af0 - addr_offset, h_MsGetJobAbilityList);

        //abilites menu, ability list
        _kyGetJobNum3_handle = new FhMethodHandle<kyGetJobNum3>(this, "FFX-2.exe", 0x5ea8b0 - addr_offset, h_kyGetJobNum3);
        _TOMenuMakeJobList_handle = new FhMethodHandle<TOMenuMakeJobList>(this, "FFX-2.exe", 0x778b00 - addr_offset, h_TOMenuMakeJobList);
        _memset_handle = new FhMethodHandle<memset>(this, "FFX-2.exe", 0x87dd24 - addr_offset, h_memset);
        _TOMenuMakeJobAbilityList_handle = new FhMethodHandle<TOMenuMakeJobAbilityList>(this, "FFX-2.exe", 0x7788d0 - addr_offset, h_TOMenuMakeJobAbilityList);
        _TOMenuStartJobAbilityWindow_handle = new FhMethodHandle<TOMenuStartJobAbilityWindow>(this, "FFX-2.exe", 0x778f70 - addr_offset, h_TOMenuStartJobAbilityWindow);

        //_FUN_778160_handle = new FhMethodHandle<FUN_778160>(this, "FFX-2.exe", 0x778160 - addr_offset, h_FUN_778160);// ability, ap rendering

        //custom ability menu handles
        _MsGetSaveLearn_handle = new FhMethodHandle<MsGetSaveLearn>(this, "FFX-2.exe", 0x60ca70 - addr_offset, h_MsGetSaveLearn);
        _MsSetSaveLearn_handle = new FhMethodHandle<MsSetSaveLearn>(this, "FFX-2.exe", 0x60E270 - addr_offset, h_MsSetSaveLearn);

        _MsGetChrNum_handle = new FhMethodHandle<MsGetChrNum>(this, "FFX-2.exe", 0x60c1a0 - addr_offset, h_MsGetChrNum);
        _MsCalcChrLevel_handle = new FhMethodHandle<MsCalcChrLevel>(this, "FFX-2.exe", 0x617140 - addr_offset, h_MsCalcChrLevel);

        _MsGetComData_handle = new FhMethodHandle<MsGetComData>(this, "FFX-2.exe", 0x625160 - addr_offset, h_MsGetComData);
        _MsGetRomAbility_handle = new FhMethodHandle<MsGetRomAbility>(this, "FFX-2.exe", 0x61de50 - addr_offset, h_MsGetRomAbility);
        _MsBtlMonsterSaveNumCheck_handle = new FhMethodHandle<MsBtlMonsterSaveNumCheck>(this, "FFX-2.exe", 0x610440 - addr_offset, h_MsBtlMonsterSaveNumCheck);
        _MsCheckAbility_handle = new FhMethodHandle<MsCheckAbility>(this, "FFX-2.exe", 0x629280 - addr_offset, h_MsCheckAbility);
        _FUN_6294f0_handle = new FhMethodHandle<FUN_6294f0>(this, "FFX-2.exe", 0x6294f0 - addr_offset, h_FUN_6294f0);
        _MsCheckLearnCommand_handle = new FhMethodHandle<MsCheckLearnCommand>(this, "FFX-2.exe", 0x635790 - addr_offset, h_MsCheckLearnCommand);
        _MsGetSaveCommand_handle = new FhMethodHandle<MsGetSaveCommand>(this, "FFX-2.exe", 0x60c500 - addr_offset, h_MsGetSaveCommand);

        _MsGetSaveAp_handle = new FhMethodHandle<MsGetSaveAp>(this, "FFX-2.exe", 0x60c2e0 - addr_offset, h_MsGetSaveAp);
        _MsGetSaveNeedAp_handle = new FhMethodHandle<MsGetSaveNeedAp>(this, "FFX-2.exe", 0x60cb20 - addr_offset, h_MsGetSaveNeedAp);
        _MsGetJobNumBasic_handle = new FhMethodHandle<MsGetJobNumBasic>(this, "FFX-2.exe", 0x61de30 - addr_offset, h_MsGetJobNumBasic);
        _MsBtlPlayerSaveNumCheck_handle = new FhMethodHandle<MsBtlPlayerSaveNumCheck>(this, "FFX-2.exe", 0x610460 - addr_offset, h_MsBtlPlayerSaveNumCheck);

        // for enabling name/desc string reading
        _TOGetSaveJobName_handle = new FhMethodHandle<TOGetSaveJobName>(this, "FFX-2.exe", 0x794600 - addr_offset, h_TOGetSaveJobName);
        _MsGetSaveJob_handle = new FhMethodHandle<MsGetSaveJob>(this, "FFX-2.exe", 0x60c950 - addr_offset, h_MsGetSaveJob);
        //help string - stop crashes with C# defined Jobs
        _FUN_5E59B0_handle = new FhMethodHandle<FUN_5E59B0>(this, "FFX-2.exe", 0x5e59b0 - addr_offset, h_FUN_5E59B0);
        _TOMenuSetHelpMes_handle = new FhMethodHandle<TOMenuSetHelpMes>(this, "FFX-2.exe", 0x763970 - addr_offset, h_TOMenuSetHelpMes);

        //AltChr handles
        _MsGetChrID_handle = new FhMethodHandle<MsGetChrID>(this, "FFX-2.exe", 0x624f90 - addr_offset, h_MsGetChrID);
        _MsSetRamMotionChrData_handle = new FhMethodHandle<MsSetRamMotionChrData>(this, "FFX-2.exe", 0x627a20 - addr_offset, h_MsSetRamMotionChrData);
        _beta_fx_handle = new FhMethodHandle<beta_fx>(this, "FFX-2.exe", 0x62ab30 - addr_offset, h_beta_fx);
        _charlie_fx_handle = new FhMethodHandle<charlie_fx>(this, "FFX-2.exe", 0x534a70 - addr_offset, h_charlie_fx);
        _MsGetChr_handle = new FhMethodHandle<MsGetChr>(this, "FFX-2.exe", 0x611450 - addr_offset, h_MsGetChr);

        //for reading/writing character names
        _MsGetSaveChrName_handle = new FhMethodHandle<MsGetSaveChrName>(this, "FFX-2.exe", 0x60c4a0 - addr_offset, h_MsGetSaveChrName);

        //write Excel bin pointers
        _FUN_6083B0_handle =  new FhMethodHandle<FUN_6083B0>(this, "FFX-2.exe", 0x6083b0 - addr_offset, h_FUN_6083B0);

        #endregion MethodHanldles

        // malloc for C# defined jobs and initialisation
        rikku_freelancer_ptr = (Job*)NativeMemory.AllocZeroed((nuint)sizeof(Job));
        rikku_leblancgoon_ptr = (Job*)NativeMemory.AllocZeroed((nuint)sizeof(Job));
        paine_freelancer_ptr = (Job*)NativeMemory.AllocZeroed((nuint)sizeof(Job));
        paine_leblancgoon_ptr = (Job*)NativeMemory.AllocZeroed((nuint)sizeof(Job));
        InitNewJobs();

    }



    public int h_MsGetChr(uint chr_id) {
        return _MsGetChr_handle.orig_fptr.Invoke(chr_id);
    }

    public uint h_MsGetSaveJob(uint chr_id) {
        return _MsGetSaveJob_handle.orig_fptr.Invoke(chr_id);
    }

    public int h_MsBtlPlayerSaveNumCheck(byte p1)
    {
        return _MsBtlPlayerSaveNumCheck_handle.orig_fptr.Invoke(p1);
    }

    public ushort h_MsGetJobNumBasic(uint p1)
    {
        return _MsGetJobNumBasic_handle.orig_fptr.Invoke(p1);
    }

    public ushort h_MsGetSaveAp(uint p1, uint p2)
    {
        return _MsGetSaveAp_handle.orig_fptr.Invoke(p1, p2);
    }

    public ushort h_MsGetSaveNeedAp(byte p1, uint p2)
    {
        return _MsGetSaveNeedAp_handle.orig_fptr.Invoke(p1, p2);
    }

    public uint h_MsGetSaveCommand(uint p1, uint p2) {
        return _MsGetSaveCommand_handle.orig_fptr.Invoke(p1, p2);
    }

    public byte h_MsCheckLearnCommand(byte p1, int p2) {
        return _MsCheckLearnCommand_handle.orig_fptr.Invoke(p1, p2);
    }

    public uint h_FUN_6294f0(uint p1, int p2, int p3) {
        return _FUN_6294f0_handle.orig_fptr.Invoke(p1, p2, p3);
    }

    public uint h_MsCheckAbility(uint p1, int p2, int p3) {
        return _MsCheckAbility_handle.orig_fptr.Invoke(p1, p2, p3);
    }

    public bool h_MsBtlMonsterSaveNumCheck(uint param_1) {
        return _MsBtlMonsterSaveNumCheck_handle.orig_fptr.Invoke(param_1);
    }

    public unsafe int h_MsGetRomAbility(uint id, byte* out_data_end) {
        return _MsGetRomAbility_handle.orig_fptr.Invoke(id, out_data_end);
    }

    public unsafe  int h_MsGetComData(uint id, byte* out_data_end) {
        return _MsGetComData_handle.orig_fptr.Invoke(id, out_data_end);
    }

    public int h_MsCalcChrLevel(byte p1) {
        return _MsCalcChrLevel_handle.orig_fptr.Invoke(p1);
    }

    public uint h_MsGetChrNum(uint param_1) {
        return _MsGetChrNum_handle.orig_fptr.Invoke(param_1);
    }

    public unsafe byte* h_MsGetSaveChrName(int chr_id) {
        return _MsGetSaveChrName_handle.orig_fptr.Invoke(chr_id);
    }

    // reimplementation so job name string is read correctly from job.bin for C# defined jobs
    // Returns a memory address at which a null-terminated string is located.
    public unsafe uint h_TOGetSaveJobName(uint chr_id) {

        uint job_id;

        int job_bin_base = FhUtil.get_at<int>(0x9f9188); // memory address of job.bin
        int string_start = *(int*)(job_bin_base + 0x18); // read from excel header
        int string_table_base = job_bin_base + 0x20 + string_start; //base, skip header, jump to start of string table


        job_id = h_MsGetSaveJob(chr_id);
        byte* local_8 = null;
        ushort name_string_pointer = *(ushort*)(h_MsGetRomJob(chr_id, job_id, local_8));

        //custom name handling
        HandleCustomCharacterName(chr_id, job_id);

        return (uint)(string_table_base + name_string_pointer); // memory address, start of null terminated byte string

    }

    // reimplementation so job help string is read correctly from job.bin for C# defined jobs
    // runs TOMenuSetHelpMes which takes a memory address of a null terminated byte string as a parameter.
    public unsafe void h_FUN_5E59B0(uint job_id) {
        if (0x5020 <= job_id) {

            int job_bin_base = FhUtil.get_at<int>(0x9f9188); // memory address of job.bin
            int string_start = *(int*)(job_bin_base + 0x18); // read from excel header
            int string_table_base = job_bin_base + 0x20 + string_start; //base, skip header, jump to start of string table

            int chr_id = FhUtil.get_at<int>(0x9f6d80);
            byte* out_data_end = null;

            Job considered_job = *(Job*)h_MsGetRomJob((uint)chr_id, job_id, out_data_end);

            ushort help_string_offset = considered_job.help_offset.text_offset;
            ushort cre_help_string_offset = considered_job.creature_data.help_text.text_offset;

            //ushort help_string_offset = *(ushort*)(h_MsGetRomJob((uint)chr_id, job_id, out_data_end) + 0x4); // use this to get help string offset
            //ushort cre_help_string_offset = *(ushort*)(h_MsGetRomJob((uint)chr_id, job_id, out_data_end) + 0xAC);

            // cre help
            if (chr_id > 2) {
                h_TOMenuSetHelpMes(string_table_base + cre_help_string_offset);
            }
            h_TOMenuSetHelpMes(string_table_base + help_string_offset);
        }
        else {
            _FUN_5E59B0_handle.orig_fptr.Invoke(job_id);
        }
    }

    public void h_TOMenuSetHelpMes(int addr_of_txt_bytes) {
        _TOMenuSetHelpMes_handle.orig_fptr.Invoke(addr_of_txt_bytes);
    }

    // handle FL/LG getting ability to be learned
    public unsafe ushort h_MsGetSaveLearn(uint param_1, uint param_2)
    {

        uint chr_num;
        int is_plyChr;

        chr_num = h_MsGetChrNum(param_1);
        is_plyChr = h_MsBtlPlayerSaveNumCheck((byte)chr_num);
        if (is_plyChr != 0)
        {
            int job_num = h_MsGetJobNumBasic(param_2);
            if (job_num < 0x1e) // Vanilla
            {
                ushort* DAT_00e05de0 = FhUtil.ptr_at<ushort>(0xa05de0);

                return *(ushort*)((int)(DAT_00e05de0) + (job_num + chr_num * 0x1e) * 2);
            }
            else // FL/LG
            {
                if (job_num == 0x20)
                {
                    return freelancer_ability_learning;
                }

                if (job_num == 0x21)
                {
                    return leblancgoon_ability_learning;
                }
            }
        }
        return 0;
    }
       
    // handle FL/LG setting ability to be learned
    public unsafe int h_MsSetSaveLearn(uint chr_id, uint job_id, ushort ability_id)
    {
        uint chr_num;
        int is_plyChr;


        chr_num = h_MsGetChrNum(chr_id);
        is_plyChr = h_MsBtlPlayerSaveNumCheck((byte)chr_num);
        if (is_plyChr != 0)
        {
            int job_num = h_MsGetJobNumBasic(job_id);
            if (job_num< 0x1e) // vanilla
            {
                ushort* DAT_00e05de0 = FhUtil.ptr_at<ushort>(0xa05de0);

                *(ushort*)((int)(DAT_00e05de0) + (job_num + chr_num * 0x1e) * 2) = ability_id;
                //return 0xffffffff;
                return -1;
            }
            else // FL/LG
            {
                if (job_num == 0x20)
                {
                    freelancer_ability_learning = ability_id;
                    return -1;
                }

                if (job_num == 0x21)
                {
                    leblancgoon_ability_learning = ability_id;
                    return -1;
                }
            }
        }
        return 0;
    }


    // util
    public int h_MsCheckRange(int number, int lower_bound, int upper_bound)
    {
        return _MsCheckRange_handle.orig_fptr.Invoke(number, lower_bound, upper_bound);
    }

    /* job id is 0x50xx form
     * returns the address where a X-2 Job is located.
     * TOGetSaveJobName also uses this to get the Jobs string table pointer
     * 
     * Adds extra Job data for Rikku/Paine to allow for different stats/abilities
     * 
     */
    public unsafe Job* h_MsGetRomJob(uint chr_id, uint job_id, byte* out_data_end)
    {
        
        
        if(job_id == 0x5020) {
            if (chr_id == 1) {
                return rikku_freelancer_ptr;
            }
            if (chr_id == 2) {
                return paine_freelancer_ptr;
            }
        }

        if (job_id == 0x5021) {
            if (chr_id == 1) {
                return rikku_leblancgoon_ptr;
            }
            if (chr_id == 2) {
                return paine_leblancgoon_ptr;
            }
        }

        //if not Freelance/Leblanc Goon
        return _MsGetRomJob_handle.orig_fptr.Invoke(chr_id, job_id, out_data_end);
    }


    // param_1 and 2 tell where GG DS icon is drawn (special game co-ord system - not based on resolution)
    // fix LG/FL showing blank icon on Garment Grid
    public void h_FUN_5E7580(int grid_x, int grid_y, int icon, uint param_4)
    {
        /* Icon map (param_3_
     * 1,2 -> Red/Green Gate
     * 3,4
     * 5,6 -> ??,, Shiny Grey orb (1: gate background orb, 2:  used as bg for some Dressspheres) Use standalone for LG/FL?
     * 7,14 ->, Gunner/Songstress
     * 8, -> gun mage
     * 21, 22 -> Psychic/Festivalist
     * 23, 24, 25 -> Floral Fallal, Machina Maw, Full Throttle
     * 26, 27 -> greyORBluish flash/pulsing, Yellow flash/pulsing
     * 28,29,30 -> nope
     * 99 -> Full Throttle
     */

        if (icon == 38) // FL or LG
        {
            _FUN_5E7580_handle.orig_fptr.Invoke(grid_x, grid_y, 6, param_4);
            _FUN_5E7580_handle.orig_fptr.Invoke(grid_x, grid_y, 26, param_4);
            return;
        }

        if (icon == 39) // FL or LG
        {
            _FUN_5E7580_handle.orig_fptr.Invoke(grid_x, grid_y, 6, param_4);
            _FUN_5E7580_handle.orig_fptr.Invoke(grid_x, grid_y, 27, param_4);
            return;
        }


        _FUN_5E7580_handle.orig_fptr.Invoke(grid_x, grid_y, icon, param_4);
    }

    // Used in Garment Grid Menu, necessary for Freelancer/Leblanc Goon to show up in Dressphere List
    public unsafe uint h_kyGetJobNum()
    {
        int iVar1;
        uint number_of_elements;
        ushort* puVar2;
        byte* unique_ds_on_grid_list = FhUtil.ptr_at<byte>(0x9f5fc4);

        number_of_elements = 0;

        for (int i = 0; i < CustomDsLookupTable.Length; i++)
        {
            iVar1 = h_MsGetSaveDreSphere(CustomDsLookupTable[i]);
            if (0 < iVar1)
            {
                unique_ds_on_grid_list[number_of_elements] = (byte)CustomDsLookupTable[i];
                number_of_elements++;
            }

        }

        FhUtil.set_at<byte>(0x9f602c, (byte)number_of_elements);
        _logger.Info("Return result: " + number_of_elements.ToString());
        return number_of_elements;

        //uint original_result = _kyGetJobNum_handle.orig_fptr.Invoke();
        //_logger.Info("Return result: " + original_result.ToString());
        //return original_result;
    }



    //returns the owned quantity of a given dressphere
    public unsafe int h_MsGetSaveDreSphere(uint ds_id)

    {
        //Freelancer, Leblanc Goon +
        if ((ds_id & 0xfff) > 0x1e)
        {
            switch(ds_id & 0xfff)
            {
                case 0x20:
                    return freelancer_quantity;
                case 0x21:
                    return leblanc_goon_quantity;
                default:
                    return 0;
            }
        }

        // Vanilla dresspheres
        if ((ds_id & 0xfff) < 0x1e)
        {
            byte* DAT_00E00D1C = FhUtil.ptr_at<byte>(0xa00d1c);
            return (int)*(byte*)(DAT_00E00D1C + (ds_id & 0xfff));
        }

        return 0;
    }

    // makes obtaining LG/Freelancer implement dressphere quantity.
    public unsafe int h_MsAddSaveDreSphere(uint ds_id, int param_2)
    {
        int quantity;

        //int* DAT_00e00d1c = FhUtil.ptr_at<int>(0xa00d1c);
        byte* DAT_00E00D1C = FhUtil.ptr_at<byte>(0xa00d1c);
        ds_id = ds_id & 0xfff;

        // Freelance, Leblanc Goon +
        if (ds_id > 0x1e)
        {
            switch (ds_id)
            {
                case 0x20:
                    freelancer_quantity = (byte)Math.Min(freelancer_quantity + 1, 99);
                    return freelancer_quantity;
                case 0x21:
                    leblanc_goon_quantity = (byte)Math.Min(leblanc_goon_quantity + 1, 99);
                    return leblanc_goon_quantity;
            }
        }

        //vanilla
        if (ds_id < 0x1e)
        {
            quantity = h_MsCheckRange(*(byte*)(DAT_00E00D1C + ds_id) + param_2, 0, 99);
            
            *(byte*)(DAT_00E00D1C + ds_id) = (byte)quantity;

            //(&DAT_00e00d1c)[param_1] = (byte)iVar1;

            return quantity;
        }
        return 0;
    }


    public override bool init(FhModContext mod_context, FileStream global_state_file) {

        return _FUN_6083B0_handle.hook()
            && _MsGetRomJob_handle.hook()
            && _MsAddSaveDreSphere_handle.hook()
            && _MsCheckRange_handle.hook()
            && _MsGetSaveDreSphere_handle.hook()
            && _kyGetCursorPoint_handle.hook()
            && _kyIsUsedPoint_handle.hook()
            && _MsGetSaveConfigChangeEffect_handle.hook()
            && _MsGetRamConfigChangeEffect_handle.hook()
            && _MsGetSaveDressUpCount_handle.hook()
            && _kyGetJobNum_handle.hook()
            && _kyGetJobNum3_handle.hook()
            && _TOMenuMakeJobList_handle.hook()
            && _memset_handle.hook()
            && _F791610_handle.hook()
            && _MsGetSavePlate_handle.hook()
            && _kyGetUsedPoint_handle.hook()
            && _FUN_5E7580_handle.hook()
            && _MsGetJobAbilityList_handle.hook()
            && _TOMenuMakeJobAbilityList_handle.hook()
            && _TOMenuStartJobAbilityWindow_handle.hook()
            && _MsGetSaveLearn_handle.hook()
            && _MsSetSaveLearn_handle.hook()


            && _MsGetChrNum_handle.hook()
            && _MsCalcChrLevel_handle.hook()
            && _MsGetComData_handle.hook()
            && _MsGetRomAbility_handle.hook()
            && _MsBtlMonsterSaveNumCheck_handle.hook()
            && _MsCheckAbility_handle.hook()
            && _FUN_6294f0_handle.hook()
            && _MsCheckLearnCommand_handle.hook()
            && _MsGetSaveCommand_handle.hook()
            && _MsGetSaveAp_handle.hook()
            && _MsGetSaveNeedAp_handle.hook()
            && _MsGetJobNumBasic_handle.hook()
            && _MsBtlPlayerSaveNumCheck_handle.hook()

            && _TOGetSaveJobName_handle.hook()
            && _MsGetSaveJob_handle.hook()
            && _FUN_5E59B0_handle.hook()
            && _TOMenuSetHelpMes_handle.hook()

            && _MsGetChrID_handle.hook()
            && _MsSetRamMotionChrData_handle.hook()
            && _MsGetChr_handle.hook()
            && _beta_fx_handle.hook()
            && _charlie_fx_handle.hook()
            && _MsGetSaveChrName_handle.hook();
            
            //&& _FUN_778160_handle.hook();
    }

    public override void load_local_state(FileStream? local_state_file, FhLocalStateInfo local_state_info) 
    {
        var loaded_state = JsonSerializer.Deserialize<X2DSUnlimitState>(local_state_file);

        if (loaded_state != null)
        {
            freelancer_quantity = loaded_state.freelancer_quantity;
            freelancer_ability_learning = loaded_state.freelancer_ability_learning;

            leblanc_goon_quantity = loaded_state.leblanc_goon_quantity;
            leblancgoon_ability_learning = loaded_state.leblancgoon_ability_learning;
        }
    }
    public override void save_local_state(FileStream  local_state_file)
    {
        X2DSUnlimitState  state = new();
        JsonSerializer.Serialize(local_state_file, state);
        local_state_file.SetLength(local_state_file.Position);
    }
}
