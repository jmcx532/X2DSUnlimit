/* These functions originally used __aullshr() and Garment Grid data.
 * Adding Freelancer and Leblanc Goon adds non-vanilla values onto Garment Grid data.
 * The result of __aullshr() is often used for memory reads, and is on the stack.
 * These functions have been re-implemented to prevent crashes.
 */

namespace Fahrenheit.Mods.X2DSUnlimit;

public partial class X2DSUnlimitModule : FhModule
{
    public uint h_MsGetSavePlate(uint param_1)
    {
        return _MsGetSavePlate_handle.orig_fptr.Invoke(param_1);
    }

    // reads garment grid data, builds a list of unique IDs on grid, returns number of unique ds ids.
    // safety hook, this function originally used aullshr() with Garment Grid Data, adding FL/LG (ids: 32, 33) caused crashes
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
    // safety hook, this function originally used aullshr() with Garment Grid Data, adding FL/LG (ids: 32, 33) caused crashes
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
    // safety hook, this function reads game memory, messing with dressphere IDs (Freelancer/LG) in Garment Grid data can produce bad reads and crash the game
    public byte h_kyGetCursorPoint(int chr_id, int plate_id)
    {
        //byte original_result = _kyGetCursorPoint_handle.orig_fptr.Invoke(param_1, param_2);

        // should prevent crashes with GG slot in memory set to 32 (Freelancer)
        if (chr_id == 0x01000000) { return _kyGetCursorPoint_handle.orig_fptr.Invoke(0, plate_id); }

        //_logger.Info("kyGetCursorPoint param_1: " + chr_id.ToString());
        //_logger.Info("kyGetCursorPoint param_2: " + plate_id.ToString());

        byte slot = _kyGetCursorPoint_handle.orig_fptr.Invoke(chr_id, plate_id);

        //_logger.Info("kyGetCursorPoint return result: " + slot.ToString());
        return slot;
    }

    // safety hook, this function in the original calls _aullshr() which takes in Garment Grid data manipulates it and the game can use this for memory reads. With new dressphere IDs, it could cause issues.
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

                // lop that reads each byte of GGrid data (which is 32 bytes long), used to increment a var that tracks if a grid has spheres set
                for (int i = 0; i < 32; i++)
                {
                    int plate_data_base = FhUtil.get_at<int>(0x9f5fc0);
                    if (*(byte*)(plate_data_base + (plate_id * 32) + i) != 0)
                    {
                        num_plates_with_spheres++;
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
        } while (plate_id < 0x40);//0x40 is number of Grids

        //DAT_00df6d88 = iVar4; // number of Grids that have Dresspheres on them.
        FhUtil.set_at<int>(0x9f6d88, num_plates_with_spheres); // number of Grids that have Dresspheres on them.

        return iVar3; // return number of owned Garment Grids
    }

}
