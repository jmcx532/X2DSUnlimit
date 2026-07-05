namespace Fahrenheit.Mods.X2DSUnlimit;

public partial class X2DSUnlimitModule : FhModule
{

    // This function is hooked to try and prevent crashes when switching to Freelancer in battle. (not just a lack of animation thing)
    // Leblanc Goon seems fine.
    // This function is called when the Triangle menu is opened, or spherechange is executed.
    // Param 1 is 0x25 on spherechange (790c90), 0x12 on Tri/Y/V Menu open (71003fc90_761300)
    // P2 is a TOFaceIndexResult (790c90) - 
    // P3 is a large number, dont think it's a memory address (chr or party data?) (Seems to point after voicemapper if it is?)
    public void h_F791610(int param_1, int param_2, int param_3)
    {

        if (param_1 == 0x25) // on spherechange
        {
            if (param_2 == 34)
            {
                //_logger.Info("Y enters Freelancer, Overriding param_2");
                _F791610_handle.orig_fptr.Invoke(param_1, 17, param_3);
                return;
            }

            if (param_2 == 56)
            {
                //_logger.Info("R enters Freelancer, Overriding param_2");
                _F791610_handle.orig_fptr.Invoke(param_1, 49, param_3);
                return;
            }

            if (param_2 == 78)
            {
                //_logger.Info("P enters Freelancer, Overriding param_2");
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

        //_logger.Info("Param_1 is: " + param_1.ToString());
        //_logger.Info("Param_2 is: " + param_2.ToString());
        byte original_result = _MsGetSaveDressUpCount_handle.orig_fptr.Invoke(param_1, param_2);
        //_logger.Info("Return result is: " + original_result.ToString());

        if (original_result < 2)
        {
            //_logger.Info("Overiding return");
            return 77;
        }
        else
        {
            return original_result;
        }
    }

    // read config dressphere animations setting
    public uint h_MsGetSaveConfigChangeEffect()
    {
        uint original_result = _MsGetSaveConfigChangeEffect_handle.orig_fptr.Invoke();
        //_logger.Info("Return result is: " + original_result.ToString());
        return original_result;
    }
    // read config dressphere animations setting
    public uint h_MsGetRamConfigChangeEffect()
    {
        uint original_result = _MsGetRamConfigChangeEffect_handle.orig_fptr.Invoke();
        //_logger.Info("Return result is: " + original_result.ToString());
        return original_result;
    }

}
