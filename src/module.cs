// SPDX-License-Identifier: MIT

using Fahrenheit.FFX2;
using System;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using TerraFX.Interop.WinRT;
using static Fahrenheit.Mods.X2DSUnlimit.X2DSUnlimitModule;

namespace Fahrenheit.Mods.X2DSUnlimit;

[FhLoad(FhGameId.FFX2)]
public class X2DSUnlimitModule : FhModule {

    //delegates
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte MsGetSaveDressUpCount(int param_1, uint param_2);
    private readonly FhMethodHandle<MsGetSaveDressUpCount> _MsGetSaveDressUpCount_handle;//60c730

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int kySetDefPlateWindow(short param_1, short param_2, int param_3, int param_4, int param_5);
    private readonly FhMethodHandle<kySetDefPlateWindow> _kySetDefPlateWindow_handle;//5e5550

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
    public delegate uint MsGetSavePlate(uint param_1);
    private readonly FhMethodHandle<MsGetSavePlate> _MsGetSavePlate_handle;//60cc00

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsAddSaveDreSphere(uint ds_id, int param_2);
    private readonly FhMethodHandle<MsAddSaveDreSphere> _MsAddSaveDreSphere_handle;//60b260

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int kySetDefJobWindow(ushort param_1, ushort param_2, int param_3, int param_4, int param_5, int param_6);
    private readonly FhMethodHandle<kySetDefJobWindow> _kySetDefJobWindow_handle;//5e5470

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
    public delegate uint MsGetSaveConfigChangeEffect();
    private readonly FhMethodHandle<MsGetSaveConfigChangeEffect> _MsGetSaveConfigChangeEffect_handle;//60c650

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint MsGetRamConfigChangeEffect();
    private readonly FhMethodHandle<MsGetRamConfigChangeEffect> _MsGetRamConfigChangeEffect_handle;//625c90

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsCheckRange(int number, int lower_bound, int upper_bound);//624cd0
    private readonly FhMethodHandle<MsCheckRange> _MsCheckRange_handle;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MsGetSaveDreSphere(uint ds_id);
    private readonly FhMethodHandle<MsGetSaveDreSphere> _MsGetSaveDreSphere_handle;//60c710

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte kyGetCursorPoint(int param_1, int param_2);
    private readonly FhMethodHandle<kyGetCursorPoint> _kyGetCursorPoint_handle;//5ea770

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void kyEquipStart(int param_1);
    private readonly FhMethodHandle<kyEquipStart> _kyEquipStart_handle;// b5b900


    //test hook of a void fx(void) thst has been reduced mostly to _aullshr()
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint kyIsUsedPoint(uint param_1, uint param_2);
    private readonly FhMethodHandle<kyIsUsedPoint> _kyIsUsedPoint_handle;// 5eb9e0


    public X2DSUnlimitModule() {
        int addr_offset = 0x400000;

        _MsAddSaveDreSphere_handle = new FhMethodHandle<MsAddSaveDreSphere>(this, "FFX-2.exe", 0x60b260 - addr_offset, h_MsAddSaveDreSphere);
        _MsCheckRange_handle = new FhMethodHandle<MsCheckRange>(this, "FFX-2.exe", 0x624cd0 - addr_offset, h_MsCheckRange);
        _MsGetSaveDreSphere_handle = new FhMethodHandle<MsGetSaveDreSphere>(this, "FFX-2.exe", 0x60c710 - addr_offset, h_MsGetSaveDreSphere);
        _kyEquipStart_handle = new FhMethodHandle<kyEquipStart>(this, "FFX-2.exe", 0xb5b900 - addr_offset, h_kyEquipStart);
        _kySetDefJobWindow_handle = new FhMethodHandle<kySetDefJobWindow>(this, "FFX-2.exe", 0x5e5470 - addr_offset, h_kySetDefJobWindow);
        _kyGetJobNum_handle = new FhMethodHandle<kyGetJobNum>(this, "FFX-2.exe", 0x5ea7b0 - addr_offset, h_kyGetJobNum);
        _kyGetJobNum2_handle = new FhMethodHandle<kyGetJobNum2>(this, "FFX-2.exe", 0x5ea810 - addr_offset, h_kyGetJobNum2);
        _kyGetJobNum3_handle = new FhMethodHandle<kyGetJobNum3>(this, "FFX-2.exe", 0x5ea8b0 - addr_offset, h_kyGetJobNum3);

        _kyGetCursorPoint_handle = new FhMethodHandle<kyGetCursorPoint>(this, "FFX-2.exe", 0x5ea770 - addr_offset, h_kyGetCursorPoint);
        _kyIsUsedPoint_handle = new FhMethodHandle<kyIsUsedPoint>(this, "FFX-2.exe", 0x5eb9e0 - addr_offset, h_kyIsUsedPoint);
        _kyGetResultPlateNum_handle = new FhMethodHandle<kyGetResultPlateNum>(this, "FFX-2.exe", 0x5eb2e0 - addr_offset, h_kyGetResultPlateNum);
        _MsGetSavePlate_handle = new FhMethodHandle<MsGetSavePlate>(this, "FFX-2.exe", 0x60cc00 - addr_offset, h_MsGetSavePlate);
        _kyGetUsedPoint_handle = new FhMethodHandle<kyGetUsedPoint>(this, "FFX-2.exe", 0x5eb480 - addr_offset, h_kyGetUsedPoint);

        _MsGetSaveDressUpCount_handle = new FhMethodHandle<MsGetSaveDressUpCount>(this, "FFX-2.exe", 0x60c730 - addr_offset, h_MsGetSaveDressUpCount);
        _F791610_handle = new FhMethodHandle<F791610>(this, "FFX-2.exe", 0x791610 - addr_offset, h_F791610);
        _kySetDefPlateWindow_handle = new FhMethodHandle<kySetDefPlateWindow>(this, "FFX-2.exe", 0x5e5550 - addr_offset, h_kySetDefPlateWindow);

        _MsGetSaveConfigChangeEffect_handle = new FhMethodHandle<MsGetSaveConfigChangeEffect>(this, "FFX-2.exe", 0x60c650 - addr_offset, h_MsGetSaveConfigChangeEffect);
        _MsGetRamConfigChangeEffect_handle = new FhMethodHandle<MsGetRamConfigChangeEffect>(this, "FFX-2.exe", 0x625c90 - addr_offset, h_MsGetRamConfigChangeEffect);
    }

    public int h_kySetDefPlateWindow(short param_1, short param_2, int param_3, int param_4, int param_5)
    {
        //_logger.Info("Param_1 is: " + param_1.ToString());
        //_logger.Info("Param_2 is: " + param_2.ToString());
        //_logger.Info("Param_3 is: " + param_3.ToString());
        //_logger.Info("Param_4 is: " + param_4.ToString("X"));
        //_logger.Info("Param_5 is: " + param_5.ToString("X"));
        return _kySetDefPlateWindow_handle.orig_fptr.Invoke(param_1, param_2, param_3, param_4, param_5);
    }

    // This function is hooked to try and prevent crashes when switching to Freelancer in battle. (not just a lack of animation thing)
    // Leblanc Goon seems fine.
    // This function is called when the Triangle menu is opened, or spherechange is executed.
    //Param 1 is 0x25 on spherechange (790c90), 0x12 on Tri/Y/V Menu open (71003fc90_761300)
    // P2 is a TOFaceIndexResult (790c90) - 
    // P3 is a large number, dont think it's a memory address (chr or party data?) (Seems to point after voicemapper if it is?)
    public void h_F791610(int param_1, int param_2, int param_3)
    {
        _logger.Info("Param_1 is: " + param_1.ToString("X"));
        _logger.Info("Param_2 is: " + param_2.ToString());
        _logger.Info("Param_3 is: " + param_3.ToString());

        if (param_1 == 0x25) // on spherechange
        {
            if (param_2 == 34)
            {
                _logger.Info("Y enters Freelancer, Overriding param_2");
                _F791610_handle.orig_fptr.Invoke(param_1, 17, param_3);
                return;
            }

            if (param_2 == 56)
            {
                _logger.Info("R enters Freelancer, Overriding param_2");
                _F791610_handle.orig_fptr.Invoke(param_1, 49, param_3);
                return;
            }

            if (param_2 == 78)
            {
                _logger.Info("P enters Freelancer, Overriding param_2");
                _F791610_handle.orig_fptr.Invoke(param_1, 64, param_3);
                return;
            }

        }

        _F791610_handle.orig_fptr.Invoke(param_1, param_2, param_3);
        return;
    }

    // this function is hooked so the dressphere entered/exited magic animations are registered as having been seen already.
    public byte h_MsGetSaveDressUpCount(int param_1, uint param_2)
    {

        _logger.Info("Param_1 is: " + param_1.ToString());
        _logger.Info("Param_2 is: " + param_2.ToString());
        byte original_result = _MsGetSaveDressUpCount_handle.orig_fptr.Invoke(param_1, param_2);
        _logger.Info("Return result is: " + original_result.ToString());

        if (original_result < 2)
        {
            _logger.Info("Overiding return");
            return 77;
        }
        else
        {
            return original_result;
        }

        
    }

    public uint h_MsGetSaveConfigChangeEffect()
    {
        uint original_result = _MsGetSaveConfigChangeEffect_handle.orig_fptr.Invoke();
        _logger.Info("Return result is: " + original_result.ToString());
        return original_result;
    }

    public uint h_MsGetRamConfigChangeEffect()
    {
        uint original_result = _MsGetRamConfigChangeEffect_handle.orig_fptr.Invoke();
        _logger.Info("Return result is: " + original_result.ToString());
        return original_result;
    }

    public int h_MsCheckRange(int number, int lower_bound, int upper_bound)
    {
        return _MsCheckRange_handle.orig_fptr.Invoke(number, lower_bound, upper_bound);
    }

    public void h_kyEquipStart(int param_1)
    {
        //_logger.Info("kyEquipStart param_1: " + param_1.ToString("X"));
        _kyEquipStart_handle.orig_fptr.Invoke(param_1);
    }

    // reads garment grid data, builds a list of unique IDs on grid, returns number of unique ds ids.
    public unsafe int h_kyGetUsedPoint()
    {

        int plate_data_base = FhUtil.get_at<int>(0x9f5fc0);
        byte current_plate_id = FhUtil.get_at<byte>(0x9f6d78);

        int current_plate_base = plate_data_base + (current_plate_id * 32);

        byte[] dses_on_grid = new byte[0x7f]; //

        // cycle over grid data slots, construct array of ds_ids on grid
        for (int i = 0; i < 32; i++)
        {
            byte ds_id = *(byte*)(current_plate_base + i);
            if (ds_id != 0)
            {
                //set array index of ds_id to ds_id, preserves ordering, handles duplicates
                dses_on_grid[ds_id] = ds_id;
            }
        }

        byte* unique_ds_on_grid_list = FhUtil.ptr_at<byte>(0x9f5fc4);
        byte count = 0;
        // Write list to memory
        for (int i = 0; i < dses_on_grid.Length; i++)
        {
            if (dses_on_grid[i] != 0)
            {
                unique_ds_on_grid_list[count] = dses_on_grid[i];
                count++;
            }

        }

        return count;
    }

    // takes a plate id and slot number, and returns the dressphere ID.
    public uint h_kyIsUsedPoint(uint plate_id, uint gg_slot)
    {
        // uint original_result = _kyIsUsedPoint_handle.orig_fptr.Invoke(param_1, param_2);

        //_logger.Info("kyIsUsedPoint param_1: " + plate_id.ToString());
        //_logger.Info("kyIsUsedPoint param_2: " + gg_slot.ToString());

        uint ds_id = _kyIsUsedPoint_handle.orig_fptr.Invoke(plate_id, gg_slot);
        //_logger.Info("kyIsUsedPoint returning: " + ds_id.ToString());
        return ds_id;
    }

    // takes a chr_id and a Garment Grid ID, and returns which slot their equipped/highlighted? dressphere is in.
    public byte h_kyGetCursorPoint(int chr_id, int plate_id)
    {
        //byte original_result = _kyGetCursorPoint_handle.orig_fptr.Invoke(param_1, param_2);

        // should MAYBE prevent crashes with 32, but surely no?
        if (chr_id == 0x01000000) { return _kyGetCursorPoint_handle.orig_fptr.Invoke(0, plate_id);}

        //_logger.Info("kyGetCursorPoint param_1: " + chr_id.ToString());
        //_logger.Info("kyGetCursorPoint param_2: " + plate_id.ToString());

        byte slot = _kyGetCursorPoint_handle.orig_fptr.Invoke(chr_id, plate_id);

        //_logger.Info("kyGetCursorPoint return result: " + slot.ToString());
        return slot;
    }

    public uint h_MsGetSavePlate(uint param_1)
    {
        return _MsGetSavePlate_handle.orig_fptr.Invoke(param_1);
    }

    //returns the number of Garment Grids owned. Populates the GG Inventory list, keeps a record of plates with spheres on them etc.
    public unsafe int h_kyGetResultPlateNum()
    {
        byte cVar1;
        int plateOwned;
        int iVar3;
        int iVar4;
        int plate_id;
        int local_c;
        int local_8;
        int num_plates_with_spheres;


        iVar3 = 0;
        iVar4 = 0;
        plate_id = 0;
        num_plates_with_spheres = 0;
        local_c = 0;
        local_8 = 0;
        do
        {
            plateOwned = (int)h_MsGetSavePlate((uint)plate_id);
            if (0 < plateOwned)
            {   

                for (int i = 0; i < 32; i++)
                {
                    int plate_data_base = FhUtil.get_at<int>(0x9f5fc0);
                    //byte* plateData = FhUtil.ptr_at<byte>(0x9f5fc0);

                    if ( *(byte*)(plate_data_base + (plate_id * 32) + i) != 0){
                        num_plates_with_spheres ++;
                        break;
                    }
                }


                /* DAT_00DF5fd8 is a list of which Garment Grids have been obtained.(Menu oriented, which ones to display in the list. */
                //(&DAT_00df5fd8)[local_c] = (char)local_10;
                byte* gg_obtain_list = FhUtil.ptr_at<byte>(0x9f5fd8);
                *(byte*)(gg_obtain_list + local_c) = (byte)plate_id;

                iVar3 = local_c + 1;
                iVar4 = local_8;
                local_c = iVar3;
            }
            plate_id = plate_id + 1;
        } while (plate_id < 0x40);

        //DAT_00df6d88 = iVar4; // number of Grids that have Dresspheres on them.
        FhUtil.set_at<int>(0x9f6d88, num_plates_with_spheres); // number of Grids that have Dresspheres on them.

        return iVar3; // return number of owned Garment Grids
    }

    public unsafe int h_MsGetSaveDreSphere(uint ds_id)

    {
        //if ((param_1 & 0xfff) < 0x1e)
        if ((ds_id & 0xfff) < 0x28) // increased limit
        {
            byte* DAT_00E00D1C = FhUtil.ptr_at<byte>(0xa00d1c);
            return (int)*(byte*)(DAT_00E00D1C + (ds_id & 0xfff));
        }
        return 0;
    }

    //returns the number of owned dresspheres. And.....
    public int h_kySetDefJobWindow(ushort param_1, ushort param_2, int param_3, int param_4, int param_5, int param_6)
    {
        _logger.Info("Param_1: " + param_1.ToString());
        _logger.Info("Param_2: " + param_2.ToString());
        _logger.Info("Param_3: " + param_3.ToString());
        _logger.Info("Param_4: " + param_4.ToString("X"));
        _logger.Info("Param_5: " + param_5.ToString("X"));
        _logger.Info("Param_6: " + param_6.ToString()); // GG Menu: 0, DS Menu: 1, Abilities Menu: 2

        int original_result = _kySetDefJobWindow_handle.orig_fptr.Invoke(param_1, param_2, param_3, param_4, param_5, param_6);
        _logger.Info("Return result: " + original_result.ToString());
        return original_result;
    }

    private static readonly ushort[] CustomDsLookupTable =
{
    0x0001, 0x0002, 0x0003, 0x0004, 0x0005,
    0x0006, 0x0007, 0x0008, 0x0009, 0x000A,
    0x000B, 0x000C, 0x000D, 0x000E, 0x001c,
    0x001d, 0x001f, 0x0020, 0x0021, 0x0022
};

    // Used in Garment Grid Menu
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

    // Used for Dressphere List Menu
    public uint h_kyGetJobNum2()
    {
        uint original_result = _kyGetJobNum2_handle.orig_fptr.Invoke();
        _logger.Info("Return result: " + original_result.ToString());
        return original_result;
    }

    // Used for Abilities Menu
    public unsafe uint h_kyGetJobNum3()
    {
        byte bVar1;
        int iVar2;
        int iVar3;
        uint number_of_elements;
        byte* unique_ds_on_grid_list = FhUtil.ptr_at<byte>(0x9f5fc4);

        byte* ds_amount =  FhUtil.ptr_at<byte>(0x9f6018); //this is speculative

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

        //uint original_result = _kyGetJobNum3_handle.orig_fptr.Invoke();
        //_logger.Info("Return result: " + original_result.ToString());
        //return original_result;
    }

    public unsafe int h_MsAddSaveDreSphere(uint param_1, int param_2)
    {
        int iVar1;

        //int* DAT_00e00d1c = FhUtil.ptr_at<int>(0xa00d1c);
        byte* DAT_00E00D1C = FhUtil.ptr_at<byte>(0xa00d1c);

        param_1 = param_1 & 0xfff;
        if (param_1 < 0x22) // increase limit
        {
            iVar1 = h_MsCheckRange(*(byte*)(DAT_00E00D1C + param_1) + param_2, 0, 99);
            
            *(byte*)(DAT_00E00D1C + param_1) = (byte)iVar1;

            //(&DAT_00e00d1c)[param_1] = (byte)iVar1;

            return iVar1;
        }
        return 0;
    }


    public override bool init(FhModContext mod_context, FileStream global_state_file) {
        return _MsAddSaveDreSphere_handle.hook()
            && _MsCheckRange_handle.hook()
            && _MsGetSaveDreSphere_handle.hook()
            && _kyGetCursorPoint_handle.hook()
            && _kyEquipStart_handle.hook()
            && _kyIsUsedPoint_handle.hook()
            && _MsGetSaveConfigChangeEffect_handle.hook()
            && _MsGetRamConfigChangeEffect_handle.hook()
            && _MsGetSaveDressUpCount_handle.hook()
            && _kySetDefJobWindow_handle.hook()
            && _kyGetJobNum_handle.hook()
            && _kyGetJobNum2_handle.hook()
            && _kyGetJobNum3_handle.hook()
            && _F791610_handle.hook()
            && _MsGetSavePlate_handle.hook()
            && _kyGetResultPlateNum_handle.hook()
            && _kyGetUsedPoint_handle.hook();
            //&& _kySetDefPlateWindow_handle.hook();
    }

    public override void load_local_state(FileStream? local_state_file, FhLocalStateInfo local_state_info) { }
    public override void save_local_state(FileStream  local_state_file)                                    { }
}
