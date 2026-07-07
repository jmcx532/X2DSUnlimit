namespace Fahrenheit.Mods.X2DSUnlimit;
public partial class X2DSUnlimitModule : FhModule {

    public int h_TOGetFaceIndex2(int chr_id, uint job_id) {

        /* // for logging purposes
        int original_result = _TOGetFaceIndex2_handle.orig_fptr.Invoke(chr_id, job_id);
        _logger.Info("Param_1 is: " + chr_id.ToString());
        _logger.Info("Param_2 is: " + job_id.ToString());
        _logger.Info("Return result: " + original_result.ToString());
        return original_result;
        */

        // Freelancer Replacements
        if (chr_id == 0 && job_id == 0x5020) {
            return 261; // Kimahri
        }

        if (chr_id == 1 && job_id == 0x5020) {
            return 263; // Tidus
        }

        if (chr_id == 2 && job_id == 0x5020) {
            return 264; // Seymour
        }

        // Leblanc Goon Replacements
        if (chr_id == 0 && job_id == 0x5021) {
            return 261; // Kimahri
        }

        if (chr_id == 1 && job_id == 0x5021) {
            return 263; // Tidus
        }

        if (chr_id == 2 && job_id == 0x5021) {
            return 264; // Seymour
        }

        return _TOGetFaceIndex2_handle.orig_fptr.Invoke(chr_id, job_id); // forces return of 0x52 which removes all portrait images
    }
}

