using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spells
{
    SPELL_1,
    SPELL_2,
    SPELL_3,
    SPELL_4,
    SPELL_5,
    BLANK
}

public static class SpellTypes
{

    public static string GetDescription(this Spells spell)
    {
        return spell switch
        {
            Spells.SPELL_1 => "Spell for quickly growing yeast within a dish of starter, for baking.",
            Spells.SPELL_2     => "Spell for removing all bones from fish meat.",
            Spells.SPELL_3 => "Spell for protecting the eyes from onions, peppers, and other irritants",
            Spells.SPELL_4  => "Complex in its ubiquity, essentially microwaves food",
            Spells.SPELL_5  => "Blender spell.",
            Spells.BLANK  => "",
            _                    => string.Empty,
        };
    }

    // Can access runes/translation and correct slot location from the value returned
    public static WordData GetWord(this Spells spell)
    {
        // return item switch
        // {
        //     Spells.SPELL_1 => ,
        //     // Spells.SPELL_2     => "Spell for removing all bones from fish meat.",
        //     // Spells.SPELL_3 => "Spell for protecting the eyes from onions, peppers, and other irritants",
        //     // Spells.SPELL_4  => "Complex in its ubiquity, essentially microwaves food",
        //     // Spells.SPELL_5  => "Blender spell.",
        //     _                    => null,
        // };
        return null;
    }

    // Return the idx of rune # to use
    public static IReadOnlyCollection<int> GetSprite(this Spells spell)
    {
        return spell switch
        {
            Spells.SPELL_1 => new int[] {0, 5, 10},
            Spells.SPELL_2 => new int[] {1, 6, 11},
            Spells.SPELL_3 => new int[] {2, 7, 12},
            Spells.SPELL_4  => new int[] {3, 8, 13},
            Spells.SPELL_5  => new int[] {4, 9, 14},
            Spells.BLANK  => new int[] {15, 15, 15}, // no runes on empty page
            _                    => null,
        };
    }

    // Return the translation of rune symbols to use
    public static IReadOnlyCollection<string> GetTranslation(this Spells spell)
    {
        return spell switch
        {
            Spells.SPELL_1 => new string[] {"fungus", "effect", "target"},
            Spells.SPELL_2 => new string[] {"bones", "exercise", "dead fish"},
            Spells.SPELL_3 => new string[] {"eyes", "protect", "self"},
            Spells.SPELL_4  => new string[] {"water", "heat", "food"},
            Spells.SPELL_5  => new string[] {"whisk", "spin", "bowl"},
            Spells.BLANK  => new string[] {"", "", ""},
            _                    => null,
        };
    }
}