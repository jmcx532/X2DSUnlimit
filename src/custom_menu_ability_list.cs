

namespace Fahrenheit.Mods.X2DSUnlimit;

public partial class X2DSUnlimitModule : FhModule
{
    /* FFX-2.exe + A02240 = Start of character's command AP earned records (MsGetSaveAP) 0x350 per character
     * FFX-2.exe + A026e0 = Start of character's auto-ability AP earned records (MsGetSaveAP) 0x350 per character
     * 
     * 
     */


    public unsafe string ReadAbilityName(uint ability_id)
    {
        if (ability_id == 0)
        {
            return "Attack";
        }

        int string_table_start = 0;
        int command_bin_start = FhUtil.get_at<int>(0x9f9164);
        int a_ability_bin_start = FhUtil.get_at<int>(0x9f9174);
        int ability_base_addr = h_MsGetComData(0x302c, null);

        if (ability_id >> 0xc == 3)
        {
            byte* p = stackalloc byte[40];
            string_table_start = command_bin_start + 0x12f18;
            ability_base_addr = h_MsGetComData(ability_id, p);
        }
        if (ability_id >> 0xc == 8)
        {
            byte* p = stackalloc byte[40];
            string_table_start = a_ability_bin_start + 0x6f80;
            ability_base_addr = h_MsGetRomAbility(ability_id, p);
        }

        
        

        ushort ability_name_string_pointer_val = *(ushort*)(ability_base_addr);

        byte* ability_name_bytes = null;
        if (string_table_start != 0)
        {
           ability_name_bytes = (byte*)(string_table_start + ability_name_string_pointer_val);
        }

        if (ability_name_bytes == null)
        {
            return "Attack";
        }


        Span<byte> buf = stackalloc byte[40];

        // read ability bytes into buffer
        int len = 0;
        for (int i = 0; i < 16; i++)
        {
            byte b = ability_name_bytes[i];
            if (b == 0) break;
            buf[len++] = b;
        }

        // create Span for Fh decoded string
        Span<byte> decoded_string = stackalloc byte[40];
        // Decode the bytes from FFXX-2 encoding to normal ASCII
        FhEncoding.decode(buf, decoded_string, FhLangId.English, FhGameId.FFX2);

        return System.Text.Encoding.ASCII.GetString(decoded_string.Slice(0, len));
    }

    uint GetButtonColor(bool type)
    {
        return type switch
        {
            false => ImGui.ColorConvertFloat4ToU32(new Vector4(0.0f, 0.0f, 0.85f, 0.33f)), // Blue, normal
            true => ImGui.ColorConvertFloat4ToU32(new Vector4(1.0f, 0.63f, 0.04f, 0.33f)), // yellow - being learned
            //_ => ImGui.ColorConvertFloat4ToU32(new Vector4(0.0f, 0.0f, 0.85f, 1f)), // Blue, normal

        };
    }

    public unsafe override void render_imgui()
    {
        /*

        //base.render_imgui();
        uint show_job_ability_list = FhUtil.get_at<uint>(0x12c0260); // flag which is 1 when viewing a dressphere's abilities in the Abilities Menu
        byte job_number = FhUtil.get_at<byte>(0x12c0266);// the dressphere ID number which is being viewed / was last viewed.
        //byte menu_chr_id = FhUtil.get_at<byte>(0x9f6d80);

        DSAbilityListData* ability_list_base = FhUtil.ptr_at<DSAbilityListData>(0xdbb200);

        DSAbilityListData current_ability_list = ability_list_base[job_number];

        if (show_job_ability_list == 1)
        {
            if (31 < job_number)
            {
                uint menu_chr_id = FhUtil.get_at<uint>(0x12c0270);
                uint menu_job_id = FhUtil.get_at<uint>(0x12c0274);

                //ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0.18f, 0.28f, 0.15f, 0.60f)); // Green
                ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(4, 2));
                ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);


                ImGui.SetNextWindowSize(new Vector2(1380, 450), ImGuiCond.Always);
                ImGui.Begin(
                    "Dressphere Abilites Window",
                    ImGuiWindowFlags.NoTitleBar |
                    ImGuiWindowFlags.NoResize |
                    ImGuiWindowFlags.NoScrollbar |
                    ImGuiWindowFlags.NoScrollWithMouse |
                    ImGuiWindowFlags.NoFocusOnAppearing |
                    ImGuiWindowFlags.NoCollapse
                );

                if (ImGui.BeginTable("MyTable", 2))
                {
                    // Optional headers
                    ImGui.TableSetupColumn("Abilities");
                    ImGui.TableSetupColumn("Abilities");
                    ImGui.TableHeadersRow();

                    for (int row = 1; row < 17; row = row + 2)
                    {
                        ImGui.TableNextRow();

                        float width = ImGui.GetContentRegionAvail().X;
                        //string mastered_string = "( M )";
                        bool abilityMastered = false;

                        ImGui.TableSetColumnIndex(0);

                        //string visible_text_odd = current_ability_list.Abilities[row - 1].ability_id.ToString("X");
                        string vt = ReadAbilityName(current_ability_list.Abilities[row - 1].ability_id);
                        string ap = " (" + current_ability_list.Abilities[row - 1].ap_current.ToString() + " / " + current_ability_list.Abilities[row - 1].ap_needed.ToString();
                        
                        if(current_ability_list.Abilities[row - 1].ap_current >= current_ability_list.Abilities[row - 1].ap_needed
                            || current_ability_list.Abilities[row-1].is_mastered
                          )
                        {
                            ap = " ( M )";
                            abilityMastered = true;
                        }

                        if (!abilityMastered)
                        {
                            if (current_ability_list.Abilities[row - 1].is_visible)
                            {
                                ImGui.PushStyleColor(ImGuiCol.Button, GetButtonColor(current_ability_list.Abilities[row - 1].is_selected));
                                if (ImGui.Button($"{vt + ap}##{row - 1}", new Vector2(width, 0)))
                                {
                                    h_MsSetSaveLearn(menu_chr_id, menu_job_id, (ushort)current_ability_list.Abilities[row - 1].ability_id);
                                    h_TOMenuMakeJobAbilityList(menu_chr_id, menu_job_id);
                                }
                                ImGui.PopStyleColor();
                            }
                            else
                            {
                                ImGui.Text(" ");
                            }
                        }
                        else
                        {
                            if (current_ability_list.Abilities[row - 1].is_visible)
                            {
                                ImGui.Text(vt + ap);
                            }
                            else
                            {
                                ImGui.Text("  ");
                            }

                        }
                        
                        ImGui.TableSetColumnIndex(1);
                        abilityMastered = false;
                        string vt2 = ReadAbilityName(current_ability_list.Abilities[row].ability_id);
                        //string visible_text_even = current_ability_list.Abilities[row].ability_id.ToString("X");

                        if (current_ability_list.Abilities[row].ap_current >= current_ability_list.Abilities[row].ap_needed
                            || current_ability_list.Abilities[row].is_mastered
                           )
                        {
                            ap = " ( M )";
                            abilityMastered = true;
                        }
                        else
                        {
                            ap = " (" + current_ability_list.Abilities[row].ap_current.ToString() + " / " + current_ability_list.Abilities[row].ap_needed.ToString();
                        }


                        if (!abilityMastered)
                        {
                            if (current_ability_list.Abilities[row].is_visible)
                            {
                                ImGui.PushStyleColor(ImGuiCol.Button, GetButtonColor(current_ability_list.Abilities[row].is_selected));
                                if (ImGui.Button($"{vt2 + ap}##{row}", new Vector2(width, 0)))
                                {
                                    h_MsSetSaveLearn(menu_chr_id, menu_job_id, (ushort)current_ability_list.Abilities[row].ability_id);
                                    h_TOMenuMakeJobAbilityList(menu_chr_id, menu_job_id);
                                }
                                ImGui.PopStyleColor();
                            }
                            else
                            {
                                ImGui.Text("   ");
                            }
                        }
                        else
                        {
                            if (current_ability_list.Abilities[row ].is_visible)
                            {
                                ImGui.Text(vt2 + ap);
                            }
                            else
                            {
                                ImGui.Text("    ");
                            }
                        }

                        
                    }

                    ImGui.EndTable();
                }


                ImGui.End();
                ImGui.PopStyleVar(2);
                //ImGui.PopStyleColor(1);
            }
        }
        */
    }

}
