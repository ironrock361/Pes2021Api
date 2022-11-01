using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public enum Position
    {
        GK = 0,
        CB = 1,
        LB = 2,
        RB = 3,
        DMF = 4,
        CMF = 5,
        LMF = 6,
        RMF = 7,
        AMF = 8,
        LWF = 9,
        RWF = 10,
        SS = 11,
        CF = 12
    }

    public enum StrongerFoot
    {
        Left = 1,
        Right = 0
    }

    public enum PlayingStyle
    {
        [Display(Name = "N/A")]
        NA = 0,

        [Display(Name = "Goal Poacher")]
        GoalPoacher = 1,

        [Display(Name = "Dummy Runner")]
        DummyRunner = 2,

        [Display(Name = "Fox in the Box")]
        FoxInTheBox = 3,

        [Display(Name = "Prolific Winger")]
        ProlificWinger = 4,

        [Display(Name = "Classic No. 10")]
        ClassicNo10 = 5,

        [Display(Name = "Hole Player")]
        HolePlayer = 6,

        [Display(Name = "Box-To-Box")]
        BoxToBox = 7,

        [Display(Name = "Anchor Man")]
        AnchorMan = 8,

        [Display(Name = "The Destroyer")]
        TheDestroyer = 9,

        [Display(Name = "Extra Front Man")]
        ExtraFrontMan = 10,

        [Display(Name = "Offensive Full Back")]
        OffensiveFullBack = 11,

        [Display(Name = "Defensive Full Back")]
        DefensiveFullBack = 12,

        [Display(Name = "Target Man")]
        TargetMan = 13,

        [Display(Name = "Creative Playmaker")]
        CreativePlaymaker = 14,

        [Display(Name = "Build Up")]
        BuildUp = 15,

        [Display(Name = "Offensive Goalkeeper")]
        OffensiveGoalkeeper = 16,

        [Display(Name = "Defensive Goalkeeper")]
        DefensiveGoalkeeper = 17
    }
}
