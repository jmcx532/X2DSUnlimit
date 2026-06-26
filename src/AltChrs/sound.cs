namespace Fahrenheit.Mods.X2DSUnlimit;


public partial class X2DSUnlimitModule : FhModule {

    //62ab30 - btsSoundStreamNormal - replacing SFX
    public unsafe uint h_beta_fx(uint chr_id, uint sound_id) {
        uint original_result = _beta_fx_handle.orig_fptr.Invoke(chr_id, sound_id);
        int chr_base_addr = h_MsGetChr(chr_id);
        int chr_model = *(int*)(chr_base_addr + 4);

        if (chr_id == 0) {
            _logger.Info("Chr_id) is: " + chr_id);
            _logger.Info("Sfx id / atk/hurt etc.) is: " + sound_id);
            //use for printing int* and other pointer types _logger.Info($" 62ae40 param_2 is: 0x{(ulong)param_2:X}");
            _logger.Info("Return result is: " + original_result);
        }

        //if player character model has been replaced with Kimahri and requested sfx is 11 (is sfx_num,not a VL integer), play his hurt sound (Requires his bank 1313 to be loaded - by changing wep/w___.bin) (create w0019 and w0020.bin for Fl/LG, can copy and rename an existing one)
        if (chr_id < 3 && sound_id == 11 && chr_model == 0x1139) {
            //_logger.Info("62ab30 return result is: 22313");
            return 22313;
        }

        return original_result;
    }

    //534a70 - plays character IOP sounds - using this to mute YRPs
    public unsafe void h_charlie_fx(int* param_1, int voice_integer, int param_3, int param_4,/*FMODCHANNELINDEX*/ int param_5, int param_6, int* param_7, int* param_8, int* param_9) {
        //_logger.Info($" 534a70 param_1 is: 0x{(ulong)param_1:X}");//Pointer to a pointer, to a pointer to fmodex.dll
        _logger.Info($" 534a70 p2 (voice integer) is:" + voice_integer);//Voice integer - E.g 8, lookup in VoiceIDMapper.txt, check the string pointer next to it, go to that address in VIDMapper, the string is sample name in voice_btl FEV
        //_logger.Info($" 534a70 param_3 is:" + param_3);
        //_logger.Info($" 534a70 param_4 is:" + param_4);
        //_logger.Info($" 534a70 param_7 is: 0x{(ulong)param_5:X}");//Pointer to a pointer to fmodex.dll stuff
        //_logger.Info($" 534a70 param_6 is:" + param_6);
        //_logger.Info($" 534a70 param_7 is: 0x{(ulong)param_7:X}");
        //_logger.Info($" 534a70 param_8 is: 0x{(ulong)param_8:X}");
        _logger.Info($" 534a70 param_9 is: 0x{(ulong)param_9:X}");//pointer to start of VoiceIDMapper.txt

        int y_base_addr = h_MsGetChr(0);
        int r_base_addr = h_MsGetChr(1);
        int p_base_addr = h_MsGetChr(2);
        bool y_in_new_ds = (*(short*)(y_base_addr + 0x86a) != 0x5020 && *(short*)(y_base_addr + 0x86a) != 0x5021);
        bool r_in_new_ds = (*(short*)(r_base_addr + 0x86a) != 0x5020 && *(short*)(r_base_addr + 0x86a) != 0x5021);
        bool p_in_new_ds = (*(short*)(p_base_addr + 0x86a) != 0x5020 && *(short*)(p_base_addr + 0x86a) != 0x5021);

        //switch statement to stop battle_iop lines playing when character model has been swapped - hurt sound, attack hya
        switch (voice_integer) {
            
            //Yuna's IOP lines
            case < 11: //0A 00 00 00 is Yuna's last IOP voiceline integer
                //if Yuna is in Freelancer/LeblancGoon, break, return early to mute her IOP Sounds
                if (y_in_new_ds){
                    break;
                }
                else {
                    return;
                }

            case < 21:
                if (r_in_new_ds) {
                    break;
                }
                else {
                    return;
                }

            case < 32:
                if (p_in_new_ds) {
                    break;
                }
                else {
                    return;
                }
            case 0x21: 
            case 0x24:
                    if (y_in_new_ds) {
                    break;
                }
                else {
                    return;
                }
            case 0x22:
            case 0x25:
                if (r_in_new_ds) {
                    break;
                }
                else {
                    return;
                }
            case 0x23:
            case 0x26:
                if (r_in_new_ds) {
                    break;
                }
                else {
                    return;
                }

        }

        _charlie_fx_handle.orig_fptr.Invoke(param_1, voice_integer, param_3, param_4,/*FMODCHANNELINDEX*/ param_5, param_6, param_7, param_8, param_9);
    }

}
