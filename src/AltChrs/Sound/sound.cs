namespace Fahrenheit.Mods.X2DSUnlimit;

public struct MemoryWrite {
    public int Offset;
    public uint Value;
}

public partial class X2DSUnlimitModule : FhModule {

    nint system_01_addr;

    /// <summary>
    /// Retrieves the address of system_01.bin in memory.
    /// Will be used then to overwrite character voicelines
    /// </summary>
    public void h_MsBtlChrGetMem() {

        _MsBtlChrGetMem_handle.orig_fptr.Invoke();
        system_01_addr = FhUtil.get_at<nint>(0x9F882C); 

    }

    public unsafe void WriteInt(nint address, uint value) {
        *(uint*)(address) = value;
    }

    public void WriteVoicelines(uint chr_id, uint job_id) {

        MemoryWrite[] writes;
        /*
        if (chr_id == 0 && job_id == 0x501d) {
            writes = FreelancerVoicelines.FreelancerWrites_yn;
        }else if (chr_id == 0) {
            writes = VanillaVoicelines.VanillaVL_yn;
        }
        else {
            return;
        }*/

        if (job_id == 0x5020) {
            switch (chr_id) {
                case 0:
                    writes = FreelancerVoicelines.FreelancerWrites_yn;
                    break;
                case 1:
                    writes = FreelancerVoicelines.FreelancerWrites_rk;
                    break;
                case 2:
                    writes = FreelancerVoicelines.FreelancerWrites_pn;
                    break;
                default:
                    return;

            }
        }else if (job_id == 0x5021) {
            switch (chr_id) {
                case 0:
                    writes = LeblancGoonVoicelines.LeblancGoonVL_yn;
                    break;
                case 1:
                    writes = LeblancGoonVoicelines.LeblancGoonVL_rk;
                    break;
                case 2:
                    writes = LeblancGoonVoicelines.LeblancGoonVL_pn;
                    break;
                default:
                    return;

            }
        }
        else {
            // writes is null - return
            return;
        }

            foreach (var write in writes)
            WriteInt(system_01_addr + write.Offset, System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(write.Value));
    }

    // Usage: Overwrite voiceline integers is system_01.bin - subset of voicelines (Not dressphere entry lines...)
    public unsafe void h_TOCtrlATBChr() {

        _TOCtrlATBChr_handle.orig_fptr.Invoke();

        byte chr_window_open = FhUtil.get_at<byte>(0xdb747c);
        ushort dressphere = *(ushort*)(h_MsGetChr(chr_window_open) + 0x86e);

        WriteVoicelines(chr_window_open, dressphere);

    }


    //62ab30, 710026cc90 - btsSoundStreamNormal?
    // Usage: Play relevant character's SFX instead of YRP's
    public unsafe uint h_FUN_62AB30(uint chr_id, uint sound_id) {
        uint original_result = _FUN_62AB30_handle.orig_fptr.Invoke(chr_id, sound_id);
        int chr_base_addr = h_MsGetChr(chr_id);
        int chr_model = *(int*)(chr_base_addr + 4);

        //if player character model has been replaced and requested sfx is 11 (takes damage) (is sfx_num,not a VL integer), play hurt sound (Requires his bank 1313 to be loaded - by changing wep/w___.bin) (create w0019 and w0020.bin for Fl/LG, can copy and rename an existing one)
        if ( chr_id < 3 && sound_id == 11) {
            // Kimahri
            if (chr_model == 0x1139) {
                return 22313;
            }
            // Tidus
            if (chr_model == 0x113D) {
                return 22317;
            }
            // Seymour
            if (chr_model == 0x113F) {
                return 22319;
            }
        }

        return original_result;
    }

    //534a70 - plays character IOP sound
    // Usage: Mute YRPs SFX
    public unsafe void h_FUN_534A70(int* param_1, int voice_integer, int param_3, int param_4,/*FMODCHANNELINDEX*/ int param_5, int param_6, int* param_7, int* param_8, int* param_9) {
        //_logger.Info($" 534a70 param_1 is: 0x{(ulong)param_1:X}");//Pointer to a pointer, to a pointer to fmodex.dll
        //_logger.Info($" 534a70 p2 (voice integer) is:" + voice_integer);//Voice integer - E.g 8, lookup in VoiceIDMapper.txt, check the string pointer next to it, go to that address in VIDMapper, the string is sample name in voice_btl FEV
        //_logger.Info($" 534a70 param_3 is:" + param_3);
        //_logger.Info($" 534a70 param_4 is:" + param_4);
        //_logger.Info($" 534a70 param_7 is: 0x{(ulong)param_5:X}");//Pointer to a pointer to fmodex.dll stuff
        //_logger.Info($" 534a70 param_6 is:" + param_6);
        //_logger.Info($" 534a70 param_7 is: 0x{(ulong)param_7:X}");
        //_logger.Info($" 534a70 param_8 is: 0x{(ulong)param_8:X}");
        //_logger.Info($" 534a70 param_9 is: 0x{(ulong)param_9:X}");//pointer to start of VoiceIDMapper.txt

        int y_base_addr = h_MsGetChr(0);
        int r_base_addr = h_MsGetChr(1);
        int p_base_addr = h_MsGetChr(2);

        bool y_in_vanilla_ds = (*(short*)(y_base_addr + 0x86a) != 0x501D && *(short*)(y_base_addr + 0x86a) != 0x5020 && *(short*)(y_base_addr + 0x86a) != 0x5021);
        bool r_in_vanilla_ds = (*(short*)(r_base_addr + 0x86a) != 0x501E && *(short*)(r_base_addr + 0x86a) != 0x5020 && *(short*)(r_base_addr + 0x86a) != 0x5021);
        bool p_in_vanilla_ds = (*(short*)(p_base_addr + 0x86a) != 0x501F && *(short*)(p_base_addr + 0x86a) != 0x5020 && *(short*)(p_base_addr + 0x86a) != 0x5021);

        //switch statement to stop battle_iop lines playing when character model has been swapped - hurt sound, attack hya
        switch (voice_integer) {
            
            //Yuna's IOP lines
            case < 11: //0A 00 00 00 is Yuna's last IOP voiceline integer
                //if Yuna is in Freelancer/LeblancGoon, break, return early to mute her IOP Sounds
                if (y_in_vanilla_ds){
                    break;
                }
                else {
                    return;
                }

            case < 21:
                if (r_in_vanilla_ds) {
                    break;
                }
                else {
                    return;
                }

            case < 32:
                if (p_in_vanilla_ds) {
                    break;
                }
                else {
                    return;
                }
            case 0x21: 
            case 0x24:
                    if (y_in_vanilla_ds) {
                    break;
                }
                else {
                    return;
                }
            case 0x22:
            case 0x25:
                if (r_in_vanilla_ds) {
                    break;
                }
                else {
                    return;
                }
            case 0x23:
            case 0x26:
                if (p_in_vanilla_ds) {
                    break;
                }
                else {
                    return;
                }
            default:
                _logger.Info("Voice integer is: " + voice_integer.ToString("X"));
                break;

        }

        _FUN_534A70_handle.orig_fptr.Invoke(param_1, voice_integer, param_3, param_4,/*FMODCHANNELINDEX*/ param_5, param_6, param_7, param_8, param_9);
    }

}
