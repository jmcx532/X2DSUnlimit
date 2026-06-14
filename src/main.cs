using static Fahrenheit.Mods.X2DSUnlimit.X2DSUnlimitModule;

namespace Fahrenheit.Mods.X2DSUnlimit;

[FhLoad(FhGameId.FFX2)]
public partial class X2DSUnlimitModule : FhModule {

    // Local state / save state
    public static byte freelancer_quantity = 0;
    public static byte leblanc_goon_quantity = 0;
    public static ushort freelancer_ability_learning = 0;
    public static ushort leblancgoon_ability_learning = 0;

    private class X2DSUnlimitState
    {
        public byte freelancer_quantity { get; set; }
        public ushort freelancer_ability_learning { get; set; }
        public byte leblanc_goon_quantity { get; set; }
        public ushort leblancgoon_ability_learning { get; set; }


        public X2DSUnlimitState()
        {
            this.freelancer_quantity = X2DSUnlimitModule.freelancer_quantity;
            this.freelancer_ability_learning = X2DSUnlimitModule.freelancer_ability_learning;
            this.leblanc_goon_quantity = X2DSUnlimitModule.leblanc_goon_quantity;
            this.leblancgoon_ability_learning = X2DSUnlimitModule.leblancgoon_ability_learning;
        }
    }

    // table replacements
    //Freelancer/Leblanc Goon ID's appended to mod copt of DAT_00D63e78 table (ffx-2.exe + 0x963e78)
    //TOMsJobAbilityWindow+ and other functions (many)
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

    //replaces lookup table at: ffx-2.exe + cc36cc, used by the kyGetJobNum series of functions and kyGetUsedPoint only.
    private static readonly ushort[] CustomDsLookupTable =
{
    0x0001, 0x0002, 0x0003, 0x0004, 0x0005,
    0x0006, 0x0007, 0x0008, 0x0009, 0x000A,
    0x000B, 0x000C, 0x000D, 0x000E, 0x001c,
    0x001d, 0x001f, 0x0020, 0x0021, 0x0022
};

    public unsafe X2DSUnlimitModule() {
        int addr_offset = 0x400000;

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
        _TOMenuMakeJobAbilityList_handle = new FhMethodHandle<TOMenuMakeJobAbilityList>(this, "FFX-2.exe", 0x7788d0 - addr_offset, h_TOMenuMakeJobAbilityList);
        _TOMenuStartJobAbilityWindow_handle = new FhMethodHandle<TOMenuStartJobAbilityWindow>(this, "FFX-2.exe", 0x778f70 - addr_offset, h_TOMenuStartJobAbilityWindow);

        //_FUN_778160_handle = new FhMethodHandle<FUN_778160>(this, "FFX-2.exe", 0x778160 - addr_offset, h_FUN_778160);// ability, ap rendering

        //custom ability menu handles
        _MsGetSaveLearn_handle = new FhMethodHandle<MsGetSaveLearn>(this, "FFX-2.exe", 0x60ca70 - addr_offset, h_MsGetSaveLearn);
        _MsSetSaveLearn_handle = new FhMethodHandle<MsSetSaveLearn>(this, "FFX-2.exe", 0x60E270 - addr_offset, h_MsSetSaveLearn);

        
    }

    public unsafe ushort h_MsGetSaveLearn(uint param_1, uint param_2)
    {

        uint uVar1;
        int iVar2;

        uVar1 = _MsGetChrNum(param_1);
        iVar2 = _MsBtlPlayerSaveNumCheck((byte)uVar1);
        if (iVar2 != 0)
        {
            iVar2 = _MsGetJobNumBasic(param_2);
            if (iVar2 < 0x1e)
            {
                ushort* DAT_00e05de0 = FhUtil.ptr_at<ushort>(0xa05de0);

                return *(ushort*)((int)(DAT_00e05de0) + (iVar2 + uVar1 * 0x1e) * 2);
            }
            else
            {
                if (iVar2 == 0x20)
                {
                    return freelancer_ability_learning;
                }

                if (iVar2 == 0x21)
                {
                    return leblancgoon_ability_learning;
                }
            }
        }
        return 0;
    }
       

    public unsafe int h_MsSetSaveLearn(uint chr_id, uint job_id, ushort ability_id)
    {
        uint uVar1;
        int iVar2;


        uVar1 = _MsGetChrNum(chr_id);
        iVar2 = _MsBtlPlayerSaveNumCheck((byte)uVar1);
        if (iVar2 != 0)
        {
            iVar2 = _MsGetJobNumBasic(job_id);
            if (iVar2 < 0x1e)
            {
                ushort* DAT_00e05de0 = FhUtil.ptr_at<ushort>(0xa05de0);

                *(ushort*)((int)(DAT_00e05de0) + (iVar2 + uVar1 * 0x1e) * 2) = ability_id;
                //return 0xffffffff;
                return -1;
            }
            else
            {
                if (iVar2 == 0x20)
                {
                    freelancer_ability_learning = ability_id;
                    return -1;
                }

                if (iVar2 == 0x21)
                {
                    leblancgoon_ability_learning = ability_id;
                    return -1;
                }
            }
        }
        return 0;
    }

    public int h_MsCheckRange(int number, int lower_bound, int upper_bound)
    {
        return _MsCheckRange_handle.orig_fptr.Invoke(number, lower_bound, upper_bound);
    }

    public unsafe int h_MsGetRomJob(uint param_1, uint param_2, byte* out_data_end)
    {
        return _MsGetRomJob_handle.orig_fptr.Invoke(param_1, param_2, out_data_end);
    }


    // param_1 and 2 tell where it is drawn (special game co-ord system - not based on resolution)
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
        return _MsGetRomJob_handle.hook()
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
            && _F791610_handle.hook()
            && _MsGetSavePlate_handle.hook()
            && _kyGetUsedPoint_handle.hook()
            && _FUN_5E7580_handle.hook()
            && _MsGetJobAbilityList_handle.hook()
            && _TOMenuMakeJobAbilityList_handle.hook()
            && _TOMenuStartJobAbilityWindow_handle.hook()
            && _MsGetSaveLearn_handle.hook()
            && _MsSetSaveLearn_handle.hook();
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
