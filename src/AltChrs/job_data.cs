namespace Fahrenheit.Mods.X2DSUnlimit;

/// <summary>
/// Customise dressphere data here!
/// 
/// This file implements: 
/// Overwrite Festivalist/Freelancer/Leblanc Goon job.bin entries in memory instead of needing to EFL them.
/// *Still need EFL at present for custom strings.*
/// 
/// Job data definitions for Freelancer and Leblanc Goon - one per character instead of the lone one in job.bin.
/// </summary>

public partial class X2DSUnlimitModule : FhModule {


    /// <summary>
    /// Usage: Overwrites Job entries in memory after job.bin has been loaded.
    /// </summary>
    bool FUN_6083B0_runOnce = false;
    //param_1 is a custom excel bin number, 8 is Job.bin
    public int h_FUN_6083B0(uint param_1) {
        int original_result = _FUN_6083B0_handle.orig_fptr.Invoke(param_1);

        if (param_1 == 8 && !FUN_6083B0_runOnce) {
            FUN_6083B0_runOnce = true;
            InitYunaFreelancer();
            InitYunaLeblancGoon();
        }
        return original_result;
    }

    // Initialise Rikku and Paine's own Freelancer and Leblanc Goon NativeAlloc job data
    public void InitNewJobs() {
        InitRikkuFreelancer();
        InitRikkuLeblancGoon();
        InitPaineFreelancer();
        InitPaineLeblancGoon();
    }

    /// <summary>
    /// Job data definitions
    /// 
    /// Note 1: Only Yuna's Freelance/Leblanc Goon data needs to be written back to job.bin in memory, Rikku/Paine's data is in NativeAlloc
    /// </summary>
    public unsafe void InitYunaFreelancer() {
        // read and get copy of Job struct
        Job y_freelancer = *(Job*)h_MsGetRomJob(0, 0x5020, null);

        y_freelancer.name_offset.text_offset = 2479;
        y_freelancer.help_offset.text_offset = 2357;
        y_freelancer.user = 0;
        y_freelancer.dressphere_menu_ordering = 15;
        y_freelancer.icon = 90;
        y_freelancer.berserk_action = 0x302D;

        // stat growth
        y_freelancer.growth_hp.linear_mult = 38;
        y_freelancer.growth_hp.quadratic_div = 133;
        y_freelancer.growth_hp.base_amount = 78;

        y_freelancer.growth_mp.linear_mult = 26;
        y_freelancer.growth_mp.quadratic_div = 180;
        y_freelancer.growth_mp.base_amount = 18;

        y_freelancer.growth_strength.linear_mult = 20;
        y_freelancer.growth_strength.linear_div = 10;
        y_freelancer.growth_strength.base_amount = 15;
        y_freelancer.growth_strength.quadratic_div_a = 12;
        y_freelancer.growth_strength.quadratic_div_b = 1;

        y_freelancer.growth_defense.linear_mult = 3;
        y_freelancer.growth_defense.linear_div = 200;
        y_freelancer.growth_defense.base_amount = 32;
        y_freelancer.growth_defense.quadratic_div_a = 200;
        y_freelancer.growth_defense.quadratic_div_b = 2;

        y_freelancer.growth_magic.linear_mult = 6;
        y_freelancer.growth_magic.linear_div = 4;
        y_freelancer.growth_magic.base_amount = 18;
        y_freelancer.growth_magic.quadratic_div_a = 200;
        y_freelancer.growth_magic.quadratic_div_b = 2;

        y_freelancer.growth_magic_defense.linear_mult = 3;
        y_freelancer.growth_magic_defense.linear_div = 16;
        y_freelancer.growth_magic_defense.base_amount = 38;
        y_freelancer.growth_magic_defense.quadratic_div_a = 200;
        y_freelancer.growth_magic_defense.quadratic_div_b = 2;

        y_freelancer.growth_agility.linear_mult = 0;
        y_freelancer.growth_agility.linear_div = 17;
        y_freelancer.growth_agility.base_amount = 54;
        y_freelancer.growth_agility.quadratic_div_a = 200;
        y_freelancer.growth_agility.quadratic_div_b = 4;

        y_freelancer.growth_evasion.linear_mult = 0;
        y_freelancer.growth_evasion.linear_div = 20;
        y_freelancer.growth_evasion.base_amount = 10;
        y_freelancer.growth_evasion.quadratic_div_a = 200;
        y_freelancer.growth_evasion.quadratic_div_b = 4;

        y_freelancer.growth_accuracy.linear_mult = 0;
        y_freelancer.growth_accuracy.linear_div = 17;
        y_freelancer.growth_accuracy.base_amount = 120;
        y_freelancer.growth_accuracy.quadratic_div_a = 200;
        y_freelancer.growth_accuracy.quadratic_div_b = 4;

        y_freelancer.growth_luck.linear_mult = 0;
        y_freelancer.growth_luck.linear_div = 22;
        y_freelancer.growth_luck.base_amount = 13;
        y_freelancer.growth_luck.quadratic_div_a = 200;
        y_freelancer.growth_luck.quadratic_div_b = 4;


        //weapon and armor
        y_freelancer.yuna_weapon_data[0].weapon_model = 4410;
        y_freelancer.yuna_weapon_data[0].weapon_position = 14;
        y_freelancer.yuna_weapon_data[1].weapon_model = 0;
        y_freelancer.yuna_weapon_data[1].weapon_position = 0;
        y_freelancer.yuna_weapon_data[2].weapon_model = 0;
        y_freelancer.yuna_weapon_data[2].weapon_position = 0;
        y_freelancer.yuna_weapon_data[3].weapon_model = 0;
        y_freelancer.yuna_weapon_data[3].weapon_position = 0;



        // abilities
        y_freelancer.dressphere_abilities[0].requirement = 0;
        y_freelancer.dressphere_abilities[0].ability = 0x302D; // Attack
        y_freelancer.dressphere_abilities[1].requirement = 0;
        y_freelancer.dressphere_abilities[1].ability = 0;
        y_freelancer.dressphere_abilities[2].requirement = 0;
        y_freelancer.dressphere_abilities[2].ability = 0;
        y_freelancer.dressphere_abilities[3].requirement = 0;
        y_freelancer.dressphere_abilities[3].ability = 0;
        y_freelancer.dressphere_abilities[4].requirement = 0;
        y_freelancer.dressphere_abilities[4].ability = 0;
        y_freelancer.dressphere_abilities[5].requirement = 0;
        y_freelancer.dressphere_abilities[5].ability = 0;
        y_freelancer.dressphere_abilities[6].requirement = 0;
        y_freelancer.dressphere_abilities[6].ability = 0;
        y_freelancer.dressphere_abilities[7].requirement = 0;
        y_freelancer.dressphere_abilities[7].ability = 0;
        y_freelancer.dressphere_abilities[8].requirement = 0;
        y_freelancer.dressphere_abilities[8].ability = 0;
        y_freelancer.dressphere_abilities[9].requirement = 0;
        y_freelancer.dressphere_abilities[9].ability = 0;
        y_freelancer.dressphere_abilities[10].requirement = 0;
        y_freelancer.dressphere_abilities[10].ability = 0;
        y_freelancer.dressphere_abilities[11].requirement = 0;
        y_freelancer.dressphere_abilities[11].ability = 0;
        y_freelancer.dressphere_abilities[12].requirement = 0;
        y_freelancer.dressphere_abilities[12].ability = 0;
        y_freelancer.dressphere_abilities[13].requirement = 0;
        y_freelancer.dressphere_abilities[13].ability = 0;
        y_freelancer.dressphere_abilities[14].requirement = 0;
        y_freelancer.dressphere_abilities[14].ability = 0;
        y_freelancer.dressphere_abilities[15].requirement = 0;
        y_freelancer.dressphere_abilities[15].ability = 0;

        // Write the changes to memory
        *(Job*)h_MsGetRomJob(0, 0x5020, null) = y_freelancer;
    }

    public unsafe void InitYunaLeblancGoon() {
        Job y_leblancgoon = *(Job*)h_MsGetRomJob(0, 0x5021, null);

        y_leblancgoon.name_offset.text_offset = 2492;
        y_leblancgoon.help_offset.text_offset = 2357;
        y_leblancgoon.user = 1;
        y_leblancgoon.dressphere_menu_ordering = 17;
        y_leblancgoon.icon = 84;
        y_leblancgoon.berserk_action = 0x302d;

        // stat growth
        y_leblancgoon.growth_hp.linear_mult = 38;
        y_leblancgoon.growth_hp.quadratic_div = 133;
        y_leblancgoon.growth_hp.base_amount = 78;

        y_leblancgoon.growth_mp.linear_mult = 26;
        y_leblancgoon.growth_mp.quadratic_div = 180;
        y_leblancgoon.growth_mp.base_amount = 18;

        y_leblancgoon.growth_strength.linear_mult = 20;
        y_leblancgoon.growth_strength.linear_div = 10;
        y_leblancgoon.growth_strength.base_amount = 15;
        y_leblancgoon.growth_strength.quadratic_div_a = 12;
        y_leblancgoon.growth_strength.quadratic_div_b = 1;

        y_leblancgoon.growth_defense.linear_mult = 3;
        y_leblancgoon.growth_defense.linear_div = 200;
        y_leblancgoon.growth_defense.base_amount = 32;
        y_leblancgoon.growth_defense.quadratic_div_a = 200;
        y_leblancgoon.growth_defense.quadratic_div_b = 2;

        y_leblancgoon.growth_magic.linear_mult = 6;
        y_leblancgoon.growth_magic.linear_div = 4;
        y_leblancgoon.growth_magic.base_amount = 18;
        y_leblancgoon.growth_magic.quadratic_div_a = 200;
        y_leblancgoon.growth_magic.quadratic_div_b = 2;

        y_leblancgoon.growth_magic_defense.linear_mult = 3;
        y_leblancgoon.growth_magic_defense.linear_div = 16;
        y_leblancgoon.growth_magic_defense.base_amount = 38;
        y_leblancgoon.growth_magic_defense.quadratic_div_a = 200;
        y_leblancgoon.growth_magic_defense.quadratic_div_b = 2;

        y_leblancgoon.growth_agility.linear_mult = 0;
        y_leblancgoon.growth_agility.linear_div = 17;
        y_leblancgoon.growth_agility.base_amount = 74;
        y_leblancgoon.growth_agility.quadratic_div_a = 200;
        y_leblancgoon.growth_agility.quadratic_div_b = 4;

        y_leblancgoon.growth_evasion.linear_mult = 0;
        y_leblancgoon.growth_evasion.linear_div = 20;
        y_leblancgoon.growth_evasion.base_amount = 10;
        y_leblancgoon.growth_evasion.quadratic_div_a = 200;
        y_leblancgoon.growth_evasion.quadratic_div_b = 4;

        y_leblancgoon.growth_accuracy.linear_mult = 0;
        y_leblancgoon.growth_accuracy.linear_div = 17;
        y_leblancgoon.growth_accuracy.base_amount = 120;
        y_leblancgoon.growth_accuracy.quadratic_div_a = 200;
        y_leblancgoon.growth_accuracy.quadratic_div_b = 4;

        y_leblancgoon.growth_luck.linear_mult = 0;
        y_leblancgoon.growth_luck.linear_div = 22;
        y_leblancgoon.growth_luck.base_amount = 13;
        y_leblancgoon.growth_luck.quadratic_div_a = 200;
        y_leblancgoon.growth_luck.quadratic_div_b = 4;


        //weapon and armor
        y_leblancgoon.yuna_weapon_data[0].weapon_model = 0x113E;
        y_leblancgoon.yuna_weapon_data[0].weapon_position = 5;
        y_leblancgoon.yuna_weapon_data[1].weapon_model = 0;
        y_leblancgoon.yuna_weapon_data[1].weapon_position = 0;
        y_leblancgoon.yuna_weapon_data[2].weapon_model = 0;
        y_leblancgoon.yuna_weapon_data[2].weapon_position = 0;
        y_leblancgoon.yuna_weapon_data[3].weapon_model = 0;
        y_leblancgoon.yuna_weapon_data[3].weapon_position = 0;


        // abilities
        y_leblancgoon.dressphere_abilities[0].requirement = 0;
        y_leblancgoon.dressphere_abilities[0].ability = 0x302D; // Attack
        y_leblancgoon.dressphere_abilities[1].requirement = 0;
        y_leblancgoon.dressphere_abilities[1].ability = 0;
        y_leblancgoon.dressphere_abilities[2].requirement = 0;
        y_leblancgoon.dressphere_abilities[2].ability = 0;
        y_leblancgoon.dressphere_abilities[3].requirement = 0;
        y_leblancgoon.dressphere_abilities[3].ability = 0;
        y_leblancgoon.dressphere_abilities[4].requirement = 0;
        y_leblancgoon.dressphere_abilities[4].ability = 0;
        y_leblancgoon.dressphere_abilities[5].requirement = 0;
        y_leblancgoon.dressphere_abilities[5].ability = 0;
        y_leblancgoon.dressphere_abilities[6].requirement = 0;
        y_leblancgoon.dressphere_abilities[6].ability = 0;
        y_leblancgoon.dressphere_abilities[7].requirement = 0;
        y_leblancgoon.dressphere_abilities[7].ability = 0;
        y_leblancgoon.dressphere_abilities[8].requirement = 0;
        y_leblancgoon.dressphere_abilities[8].ability = 0;
        y_leblancgoon.dressphere_abilities[9].requirement = 0;
        y_leblancgoon.dressphere_abilities[9].ability = 0;
        y_leblancgoon.dressphere_abilities[10].requirement = 0;
        y_leblancgoon.dressphere_abilities[10].ability = 0;
        y_leblancgoon.dressphere_abilities[11].requirement = 0;
        y_leblancgoon.dressphere_abilities[11].ability = 0;
        y_leblancgoon.dressphere_abilities[12].requirement = 0;
        y_leblancgoon.dressphere_abilities[12].ability = 0;
        y_leblancgoon.dressphere_abilities[13].requirement = 0;
        y_leblancgoon.dressphere_abilities[13].ability = 0;
        y_leblancgoon.dressphere_abilities[14].requirement = 0;
        y_leblancgoon.dressphere_abilities[14].ability = 0;
        y_leblancgoon.dressphere_abilities[15].requirement = 0;
        y_leblancgoon.dressphere_abilities[15].ability = 0;


        // Write the changes to memory
        *(Job*)h_MsGetRomJob(0, 0x5021, null) = y_leblancgoon;
    }

    public unsafe void InitRikkuFreelancer() {
        ref Job rikku_freelancer = ref *rikku_freelancer_ptr;

        // data
        //rikku_freelancer.name_offset = 2345;
        rikku_freelancer.name_offset.text_offset = 2479;
        rikku_freelancer.help_offset.text_offset = 2357;
        rikku_freelancer.user = 1;
        rikku_freelancer.dressphere_menu_ordering = 17;
        rikku_freelancer.icon = 90;
        rikku_freelancer.berserk_action = 0x302d;

        // stat growth
        rikku_freelancer.growth_hp.linear_mult = 38;
        rikku_freelancer.growth_hp.quadratic_div = 133;
        rikku_freelancer.growth_hp.base_amount = 78;

        rikku_freelancer.growth_mp.linear_mult = 26;
        rikku_freelancer.growth_mp.quadratic_div = 180;
        rikku_freelancer.growth_mp.base_amount = 18;

        rikku_freelancer.growth_strength.linear_mult = 20;
        rikku_freelancer.growth_strength.linear_div = 10;
        rikku_freelancer.growth_strength.base_amount = 15;
        rikku_freelancer.growth_strength.quadratic_div_a = 12;
        rikku_freelancer.growth_strength.quadratic_div_b = 1;

        rikku_freelancer.growth_defense.linear_mult = 3;
        rikku_freelancer.growth_defense.linear_div = 200;
        rikku_freelancer.growth_defense.base_amount = 32;
        rikku_freelancer.growth_defense.quadratic_div_a = 200;
        rikku_freelancer.growth_defense.quadratic_div_b = 2;

        rikku_freelancer.growth_magic.linear_mult = 6;
        rikku_freelancer.growth_magic.linear_div = 4;
        rikku_freelancer.growth_magic.base_amount = 18;
        rikku_freelancer.growth_magic.quadratic_div_a = 200;
        rikku_freelancer.growth_magic.quadratic_div_b = 2;

        rikku_freelancer.growth_magic_defense.linear_mult = 3;
        rikku_freelancer.growth_magic_defense.linear_div = 16;
        rikku_freelancer.growth_magic_defense.base_amount = 38;
        rikku_freelancer.growth_magic_defense.quadratic_div_a = 200;
        rikku_freelancer.growth_magic_defense.quadratic_div_b = 2;

        rikku_freelancer.growth_agility.linear_mult = 0;
        rikku_freelancer.growth_agility.linear_div = 17;
        rikku_freelancer.growth_agility.base_amount = 74;
        rikku_freelancer.growth_agility.quadratic_div_a = 200;
        rikku_freelancer.growth_agility.quadratic_div_b = 4;

        rikku_freelancer.growth_evasion.linear_mult = 0;
        rikku_freelancer.growth_evasion.linear_div = 20;
        rikku_freelancer.growth_evasion.base_amount = 10;
        rikku_freelancer.growth_evasion.quadratic_div_a = 200;
        rikku_freelancer.growth_evasion.quadratic_div_b = 4;

        rikku_freelancer.growth_accuracy.linear_mult = 0;
        rikku_freelancer.growth_accuracy.linear_div = 17;
        rikku_freelancer.growth_accuracy.base_amount = 120;
        rikku_freelancer.growth_accuracy.quadratic_div_a = 200;
        rikku_freelancer.growth_accuracy.quadratic_div_b = 4;

        rikku_freelancer.growth_luck.linear_mult = 0;
        rikku_freelancer.growth_luck.linear_div = 22;
        rikku_freelancer.growth_luck.base_amount = 13;
        rikku_freelancer.growth_luck.quadratic_div_a = 200;
        rikku_freelancer.growth_luck.quadratic_div_b = 4;


        //weapon and armor
        rikku_freelancer.rikku_weapon_data[0].weapon_model = 0x113E;
        rikku_freelancer.rikku_weapon_data[0].weapon_position = 5;
        rikku_freelancer.rikku_weapon_data[1].weapon_model = 0;
        rikku_freelancer.rikku_weapon_data[1].weapon_position = 0;
        rikku_freelancer.rikku_weapon_data[2].weapon_model = 0;
        rikku_freelancer.rikku_weapon_data[2].weapon_position = 0;
        rikku_freelancer.rikku_weapon_data[3].weapon_model = 0;
        rikku_freelancer.rikku_weapon_data[3].weapon_position = 0;

        // abilities
        rikku_freelancer.dressphere_abilities[0].requirement = 0;
        rikku_freelancer.dressphere_abilities[0].ability = 0x302D; // Attack
        rikku_freelancer.dressphere_abilities[1].requirement = 0;
        rikku_freelancer.dressphere_abilities[1].ability = 0;
        rikku_freelancer.dressphere_abilities[2].requirement = 0;
        rikku_freelancer.dressphere_abilities[2].ability = 0;
        rikku_freelancer.dressphere_abilities[3].requirement = 0;
        rikku_freelancer.dressphere_abilities[3].ability = 0;
        rikku_freelancer.dressphere_abilities[4].requirement = 0;
        rikku_freelancer.dressphere_abilities[4].ability = 0;
        rikku_freelancer.dressphere_abilities[5].requirement = 0;
        rikku_freelancer.dressphere_abilities[5].ability = 0;
        rikku_freelancer.dressphere_abilities[6].requirement = 0;
        rikku_freelancer.dressphere_abilities[6].ability = 0;
        rikku_freelancer.dressphere_abilities[7].requirement = 0;
        rikku_freelancer.dressphere_abilities[7].ability = 0;
        rikku_freelancer.dressphere_abilities[8].requirement = 0;
        rikku_freelancer.dressphere_abilities[8].ability = 0;
        rikku_freelancer.dressphere_abilities[9].requirement = 0;
        rikku_freelancer.dressphere_abilities[9].ability = 0;
        rikku_freelancer.dressphere_abilities[10].requirement = 0;
        rikku_freelancer.dressphere_abilities[10].ability = 0;
        rikku_freelancer.dressphere_abilities[11].requirement = 0;
        rikku_freelancer.dressphere_abilities[11].ability = 0;
        rikku_freelancer.dressphere_abilities[12].requirement = 0;
        rikku_freelancer.dressphere_abilities[12].ability = 0;
        rikku_freelancer.dressphere_abilities[13].requirement = 0;
        rikku_freelancer.dressphere_abilities[13].ability = 0;
        rikku_freelancer.dressphere_abilities[14].requirement = 0;
        rikku_freelancer.dressphere_abilities[14].ability = 0;
        rikku_freelancer.dressphere_abilities[15].requirement = 0;
        rikku_freelancer.dressphere_abilities[15].ability = 0;

        // Job Creature Data
    }

    public unsafe void InitRikkuLeblancGoon() {
        ref Job rikku_leblancgoon = ref *rikku_leblancgoon_ptr;

        // data
        //rikku_leblancgoon.name_offset = 2345;
        rikku_leblancgoon.name_offset.text_offset = 2492;
        rikku_leblancgoon.help_offset.text_offset = 2357;
        rikku_leblancgoon.user = 1;
        rikku_leblancgoon.dressphere_menu_ordering = 17;
        rikku_leblancgoon.icon = 84;
        rikku_leblancgoon.berserk_action = 0x302d;

        // stat growth
        rikku_leblancgoon.growth_hp.linear_mult = 38;
        rikku_leblancgoon.growth_hp.quadratic_div = 133;
        rikku_leblancgoon.growth_hp.base_amount = 78;

        rikku_leblancgoon.growth_mp.linear_mult = 26;
        rikku_leblancgoon.growth_mp.quadratic_div = 180;
        rikku_leblancgoon.growth_mp.base_amount = 18;

        rikku_leblancgoon.growth_strength.linear_mult = 20;
        rikku_leblancgoon.growth_strength.linear_div = 10;
        rikku_leblancgoon.growth_strength.base_amount = 15;
        rikku_leblancgoon.growth_strength.quadratic_div_a = 12;
        rikku_leblancgoon.growth_strength.quadratic_div_b = 1;

        rikku_leblancgoon.growth_defense.linear_mult = 3;
        rikku_leblancgoon.growth_defense.linear_div = 200;
        rikku_leblancgoon.growth_defense.base_amount = 32;
        rikku_leblancgoon.growth_defense.quadratic_div_a = 200;
        rikku_leblancgoon.growth_defense.quadratic_div_b = 2;

        rikku_leblancgoon.growth_magic.linear_mult = 6;
        rikku_leblancgoon.growth_magic.linear_div = 4;
        rikku_leblancgoon.growth_magic.base_amount = 18;
        rikku_leblancgoon.growth_magic.quadratic_div_a = 200;
        rikku_leblancgoon.growth_magic.quadratic_div_b = 2;

        rikku_leblancgoon.growth_magic_defense.linear_mult = 3;
        rikku_leblancgoon.growth_magic_defense.linear_div = 16;
        rikku_leblancgoon.growth_magic_defense.base_amount = 38;
        rikku_leblancgoon.growth_magic_defense.quadratic_div_a = 200;
        rikku_leblancgoon.growth_magic_defense.quadratic_div_b = 2;

        rikku_leblancgoon.growth_agility.linear_mult = 0;
        rikku_leblancgoon.growth_agility.linear_div = 17;
        rikku_leblancgoon.growth_agility.base_amount = 74;
        rikku_leblancgoon.growth_agility.quadratic_div_a = 200;
        rikku_leblancgoon.growth_agility.quadratic_div_b = 4;

        rikku_leblancgoon.growth_evasion.linear_mult = 0;
        rikku_leblancgoon.growth_evasion.linear_div = 20;
        rikku_leblancgoon.growth_evasion.base_amount = 10;
        rikku_leblancgoon.growth_evasion.quadratic_div_a = 200;
        rikku_leblancgoon.growth_evasion.quadratic_div_b = 4;

        rikku_leblancgoon.growth_accuracy.linear_mult = 0;
        rikku_leblancgoon.growth_accuracy.linear_div = 17;
        rikku_leblancgoon.growth_accuracy.base_amount = 120;
        rikku_leblancgoon.growth_accuracy.quadratic_div_a = 200;
        rikku_leblancgoon.growth_accuracy.quadratic_div_b = 4;

        rikku_leblancgoon.growth_luck.linear_mult = 0;
        rikku_leblancgoon.growth_luck.linear_div = 22;
        rikku_leblancgoon.growth_luck.base_amount = 13;
        rikku_leblancgoon.growth_luck.quadratic_div_a = 200;
        rikku_leblancgoon.growth_luck.quadratic_div_b = 4;

        //weapon and armor
        rikku_leblancgoon.rikku_weapon_data[0].weapon_model = 0x113E;
        rikku_leblancgoon.rikku_weapon_data[0].weapon_position = 5;
        rikku_leblancgoon.rikku_weapon_data[1].weapon_model = 0;
        rikku_leblancgoon.rikku_weapon_data[1].weapon_position = 0;
        rikku_leblancgoon.rikku_weapon_data[2].weapon_model = 0;
        rikku_leblancgoon.rikku_weapon_data[2].weapon_position = 0;
        rikku_leblancgoon.rikku_weapon_data[3].weapon_model = 0;
        rikku_leblancgoon.rikku_weapon_data[3].weapon_position = 0;

        // abilities
        rikku_leblancgoon.dressphere_abilities[0].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[0].ability = 0x302D; // Attack
        rikku_leblancgoon.dressphere_abilities[1].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[1].ability = 0;
        rikku_leblancgoon.dressphere_abilities[2].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[2].ability = 0;
        rikku_leblancgoon.dressphere_abilities[3].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[3].ability = 0;
        rikku_leblancgoon.dressphere_abilities[4].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[4].ability = 0;
        rikku_leblancgoon.dressphere_abilities[5].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[5].ability = 0;
        rikku_leblancgoon.dressphere_abilities[6].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[6].ability = 0;
        rikku_leblancgoon.dressphere_abilities[7].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[7].ability = 0;
        rikku_leblancgoon.dressphere_abilities[8].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[8].ability = 0;
        rikku_leblancgoon.dressphere_abilities[9].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[9].ability = 0;
        rikku_leblancgoon.dressphere_abilities[10].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[10].ability = 0;
        rikku_leblancgoon.dressphere_abilities[11].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[11].ability = 0;
        rikku_leblancgoon.dressphere_abilities[12].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[12].ability = 0;
        rikku_leblancgoon.dressphere_abilities[13].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[13].ability = 0;
        rikku_leblancgoon.dressphere_abilities[14].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[14].ability = 0;
        rikku_leblancgoon.dressphere_abilities[15].requirement = 0;
        rikku_leblancgoon.dressphere_abilities[15].ability = 0;

        // Job Creature Data
    }

    public unsafe void InitPaineFreelancer() {
        ref Job paine_freelancer = ref *paine_freelancer_ptr;

        // data
        //paine_freelancer.name_offset = 2345;
        paine_freelancer.name_offset.text_offset = 2479;
        paine_freelancer.help_offset.text_offset = 2357;
        paine_freelancer.user = 1;
        paine_freelancer.dressphere_menu_ordering = 17;
        paine_freelancer.icon = 90;
        paine_freelancer.berserk_action = 0x302d;

        // stat growth
        paine_freelancer.growth_hp.linear_mult = 38;
        paine_freelancer.growth_hp.quadratic_div = 133;
        paine_freelancer.growth_hp.base_amount = 78;

        paine_freelancer.growth_mp.linear_mult = 26;
        paine_freelancer.growth_mp.quadratic_div = 180;
        paine_freelancer.growth_mp.base_amount = 18;

        paine_freelancer.growth_strength.linear_mult = 20;
        paine_freelancer.growth_strength.linear_div = 10;
        paine_freelancer.growth_strength.base_amount = 15;
        paine_freelancer.growth_strength.quadratic_div_a = 12;
        paine_freelancer.growth_strength.quadratic_div_b = 1;

        paine_freelancer.growth_defense.linear_mult = 3;
        paine_freelancer.growth_defense.linear_div = 200;
        paine_freelancer.growth_defense.base_amount = 32;
        paine_freelancer.growth_defense.quadratic_div_a = 200;
        paine_freelancer.growth_defense.quadratic_div_b = 2;

        paine_freelancer.growth_magic.linear_mult = 6;
        paine_freelancer.growth_magic.linear_div = 4;
        paine_freelancer.growth_magic.base_amount = 18;
        paine_freelancer.growth_magic.quadratic_div_a = 200;
        paine_freelancer.growth_magic.quadratic_div_b = 2;

        paine_freelancer.growth_magic_defense.linear_mult = 3;
        paine_freelancer.growth_magic_defense.linear_div = 16;
        paine_freelancer.growth_magic_defense.base_amount = 38;
        paine_freelancer.growth_magic_defense.quadratic_div_a = 200;
        paine_freelancer.growth_magic_defense.quadratic_div_b = 2;

        paine_freelancer.growth_agility.linear_mult = 0;
        paine_freelancer.growth_agility.linear_div = 17;
        paine_freelancer.growth_agility.base_amount = 74;
        paine_freelancer.growth_agility.quadratic_div_a = 200;
        paine_freelancer.growth_agility.quadratic_div_b = 4;

        paine_freelancer.growth_evasion.linear_mult = 0;
        paine_freelancer.growth_evasion.linear_div = 20;
        paine_freelancer.growth_evasion.base_amount = 10;
        paine_freelancer.growth_evasion.quadratic_div_a = 200;
        paine_freelancer.growth_evasion.quadratic_div_b = 4;

        paine_freelancer.growth_accuracy.linear_mult = 0;
        paine_freelancer.growth_accuracy.linear_div = 17;
        paine_freelancer.growth_accuracy.base_amount = 120;
        paine_freelancer.growth_accuracy.quadratic_div_a = 200;
        paine_freelancer.growth_accuracy.quadratic_div_b = 4;

        paine_freelancer.growth_luck.linear_mult = 0;
        paine_freelancer.growth_luck.linear_div = 22;
        paine_freelancer.growth_luck.base_amount = 13;
        paine_freelancer.growth_luck.quadratic_div_a = 200;
        paine_freelancer.growth_luck.quadratic_div_b = 4;

        //weapon and armor
        paine_freelancer.paine_weapon_data[0].weapon_model = 0x1140;
        paine_freelancer.paine_weapon_data[0].weapon_position = 5;
        paine_freelancer.paine_weapon_data[1].weapon_model = 0;
        paine_freelancer.paine_weapon_data[1].weapon_position = 0;
        paine_freelancer.paine_weapon_data[2].weapon_model = 0;
        paine_freelancer.paine_weapon_data[2].weapon_position = 0;
        paine_freelancer.paine_weapon_data[3].weapon_model = 0;
        paine_freelancer.paine_weapon_data[3].weapon_position = 0;

        // abilities
        paine_freelancer.dressphere_abilities[0].requirement = 0;
        paine_freelancer.dressphere_abilities[0].ability = 0x302D; // Attack
        paine_freelancer.dressphere_abilities[1].requirement = 0;
        paine_freelancer.dressphere_abilities[1].ability = 0;
        paine_freelancer.dressphere_abilities[2].requirement = 0;
        paine_freelancer.dressphere_abilities[2].ability = 0;
        paine_freelancer.dressphere_abilities[3].requirement = 0;
        paine_freelancer.dressphere_abilities[3].ability = 0;
        paine_freelancer.dressphere_abilities[4].requirement = 0;
        paine_freelancer.dressphere_abilities[4].ability = 0;
        paine_freelancer.dressphere_abilities[5].requirement = 0;
        paine_freelancer.dressphere_abilities[5].ability = 0;
        paine_freelancer.dressphere_abilities[6].requirement = 0;
        paine_freelancer.dressphere_abilities[6].ability = 0;
        paine_freelancer.dressphere_abilities[7].requirement = 0;
        paine_freelancer.dressphere_abilities[7].ability = 0;
        paine_freelancer.dressphere_abilities[8].requirement = 0;
        paine_freelancer.dressphere_abilities[8].ability = 0;
        paine_freelancer.dressphere_abilities[9].requirement = 0;
        paine_freelancer.dressphere_abilities[9].ability = 0;
        paine_freelancer.dressphere_abilities[10].requirement = 0;
        paine_freelancer.dressphere_abilities[10].ability = 0;
        paine_freelancer.dressphere_abilities[11].requirement = 0;
        paine_freelancer.dressphere_abilities[11].ability = 0;
        paine_freelancer.dressphere_abilities[12].requirement = 0;
        paine_freelancer.dressphere_abilities[12].ability = 0;
        paine_freelancer.dressphere_abilities[13].requirement = 0;
        paine_freelancer.dressphere_abilities[13].ability = 0;
        paine_freelancer.dressphere_abilities[14].requirement = 0;
        paine_freelancer.dressphere_abilities[14].ability = 0;
        paine_freelancer.dressphere_abilities[15].requirement = 0;
        paine_freelancer.dressphere_abilities[15].ability = 0;

        // Job Creature Data
    }

    public unsafe void InitPaineLeblancGoon() {
        ref Job paine_leblancgoon = ref *paine_leblancgoon_ptr;

        // data
        //paine_leblancgoon.name_offset = 2345;
        paine_leblancgoon.name_offset.text_offset = 2492;
        paine_leblancgoon.help_offset.text_offset = 2357;
        paine_leblancgoon.user = 1;
        paine_leblancgoon.dressphere_menu_ordering = 17;
        paine_leblancgoon.icon = 84;
        paine_leblancgoon.berserk_action = 0x302d;

        // stat growth
        paine_leblancgoon.growth_hp.linear_mult = 38;
        paine_leblancgoon.growth_hp.quadratic_div = 133;
        paine_leblancgoon.growth_hp.base_amount = 78;

        paine_leblancgoon.growth_mp.linear_mult = 26;
        paine_leblancgoon.growth_mp.quadratic_div = 180;
        paine_leblancgoon.growth_mp.base_amount = 18;

        paine_leblancgoon.growth_strength.linear_mult = 20;
        paine_leblancgoon.growth_strength.linear_div = 10;
        paine_leblancgoon.growth_strength.base_amount = 15;
        paine_leblancgoon.growth_strength.quadratic_div_a = 12;
        paine_leblancgoon.growth_strength.quadratic_div_b = 1;

        paine_leblancgoon.growth_defense.linear_mult = 3;
        paine_leblancgoon.growth_defense.linear_div = 200;
        paine_leblancgoon.growth_defense.base_amount = 32;
        paine_leblancgoon.growth_defense.quadratic_div_a = 200;
        paine_leblancgoon.growth_defense.quadratic_div_b = 2;

        paine_leblancgoon.growth_magic.linear_mult = 6;
        paine_leblancgoon.growth_magic.linear_div = 4;
        paine_leblancgoon.growth_magic.base_amount = 18;
        paine_leblancgoon.growth_magic.quadratic_div_a = 200;
        paine_leblancgoon.growth_magic.quadratic_div_b = 2;

        paine_leblancgoon.growth_magic_defense.linear_mult = 3;
        paine_leblancgoon.growth_magic_defense.linear_div = 16;
        paine_leblancgoon.growth_magic_defense.base_amount = 38;
        paine_leblancgoon.growth_magic_defense.quadratic_div_a = 200;
        paine_leblancgoon.growth_magic_defense.quadratic_div_b = 2;

        paine_leblancgoon.growth_agility.linear_mult = 0;
        paine_leblancgoon.growth_agility.linear_div = 17;
        paine_leblancgoon.growth_agility.base_amount = 74;
        paine_leblancgoon.growth_agility.quadratic_div_a = 200;
        paine_leblancgoon.growth_agility.quadratic_div_b = 4;

        paine_leblancgoon.growth_evasion.linear_mult = 0;
        paine_leblancgoon.growth_evasion.linear_div = 20;
        paine_leblancgoon.growth_evasion.base_amount = 10;
        paine_leblancgoon.growth_evasion.quadratic_div_a = 200;
        paine_leblancgoon.growth_evasion.quadratic_div_b = 4;

        paine_leblancgoon.growth_accuracy.linear_mult = 0;
        paine_leblancgoon.growth_accuracy.linear_div = 17;
        paine_leblancgoon.growth_accuracy.base_amount = 120;
        paine_leblancgoon.growth_accuracy.quadratic_div_a = 200;
        paine_leblancgoon.growth_accuracy.quadratic_div_b = 4;

        paine_leblancgoon.growth_luck.linear_mult = 0;
        paine_leblancgoon.growth_luck.linear_div = 22;
        paine_leblancgoon.growth_luck.base_amount = 13;
        paine_leblancgoon.growth_luck.quadratic_div_a = 200;
        paine_leblancgoon.growth_luck.quadratic_div_b = 4;

        //weapon and armor
        paine_leblancgoon.paine_weapon_data[0].weapon_model = 0x1140;
        paine_leblancgoon.paine_weapon_data[0].weapon_position = 5;
        paine_leblancgoon.paine_weapon_data[1].weapon_model = 0;
        paine_leblancgoon.paine_weapon_data[1].weapon_position = 0;
        paine_leblancgoon.paine_weapon_data[2].weapon_model = 0;
        paine_leblancgoon.paine_weapon_data[2].weapon_position = 0;
        paine_leblancgoon.paine_weapon_data[3].weapon_model = 0;
        paine_leblancgoon.paine_weapon_data[3].weapon_position = 0;

        // abilities
        paine_leblancgoon.dressphere_abilities[0].requirement = 0;
        paine_leblancgoon.dressphere_abilities[0].ability = 0x302D; // Attack
        paine_leblancgoon.dressphere_abilities[1].requirement = 0;
        paine_leblancgoon.dressphere_abilities[1].ability = 0;
        paine_leblancgoon.dressphere_abilities[2].requirement = 0;
        paine_leblancgoon.dressphere_abilities[2].ability = 0;
        paine_leblancgoon.dressphere_abilities[3].requirement = 0;
        paine_leblancgoon.dressphere_abilities[3].ability = 0;
        paine_leblancgoon.dressphere_abilities[4].requirement = 0;
        paine_leblancgoon.dressphere_abilities[4].ability = 0;
        paine_leblancgoon.dressphere_abilities[5].requirement = 0;
        paine_leblancgoon.dressphere_abilities[5].ability = 0;
        paine_leblancgoon.dressphere_abilities[6].requirement = 0;
        paine_leblancgoon.dressphere_abilities[6].ability = 0;
        paine_leblancgoon.dressphere_abilities[7].requirement = 0;
        paine_leblancgoon.dressphere_abilities[7].ability = 0;
        paine_leblancgoon.dressphere_abilities[8].requirement = 0;
        paine_leblancgoon.dressphere_abilities[8].ability = 0;
        paine_leblancgoon.dressphere_abilities[9].requirement = 0;
        paine_leblancgoon.dressphere_abilities[9].ability = 0;
        paine_leblancgoon.dressphere_abilities[10].requirement = 0;
        paine_leblancgoon.dressphere_abilities[10].ability = 0;
        paine_leblancgoon.dressphere_abilities[11].requirement = 0;
        paine_leblancgoon.dressphere_abilities[11].ability = 0;
        paine_leblancgoon.dressphere_abilities[12].requirement = 0;
        paine_leblancgoon.dressphere_abilities[12].ability = 0;
        paine_leblancgoon.dressphere_abilities[13].requirement = 0;
        paine_leblancgoon.dressphere_abilities[13].ability = 0;
        paine_leblancgoon.dressphere_abilities[14].requirement = 0;
        paine_leblancgoon.dressphere_abilities[14].ability = 0;
        paine_leblancgoon.dressphere_abilities[15].requirement = 0;
        paine_leblancgoon.dressphere_abilities[15].ability = 0;
        // Job - Creature Data
    }

}
