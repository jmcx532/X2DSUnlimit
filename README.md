# Final Fantasy X-2 - Extra Dresspheres mod

A mod for Final Fantasy X-2 that adds 2 extra dresspheres to the main game.

# Customization

Customizing the dressphere can currently be done via editing files in `src/AltChrs`  and using the External File loader to modify certain files.

A full guide can be found in this repo's wiki.

# EFL Files required:

### Obtaining the dresspheres

** Some method for obtaining these dresspheres ** - e.g edit monster file, item drops

### Dressphere name and help strings

new_uspc/battle/kernel/job.bin - string table edits

### Weapons - SFX and weapon afterimages

jppc/battle/wep/w0019.bin - Yuna Freelancer weapon, edited SeSep sections for SFX
jppc/battle/wep/w0020.bin - Yuna Leblanc Goon weapon, edited SeSep sections for SFX

jppc/battle/wep/w0119.bin - Same for Rikku
jppc/battle/wep/w0120.bin 
jppc/battle/wep/w0219.bin - Same for Paine
jppc/battle/wep/w0220.bin

### Command changes

"" - command.bin, monmagic.bin - edit commands as required.
