﻿using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace Activator.Spells.Shields
{
    class fiorariposte : spell
    {
        internal override string Name
        {
            get { return "fiorariposte"; }
        }

        internal override string DisplayName
        {
            get { return "Riposte | W"; }
        }

        internal override float Range
        {
            get { return float.MaxValue; }
        }

        internal override MenuType[] Category
        {
            get { return new[] { MenuType.SelfLowHP, MenuType.SelfMinMP }; }
        }

        internal override int DefaultHP
        {
            get { return 95; }
        }

        internal override int DefaultMP
        {
            get { return 55; }
        }

        public override void OnTick(EventArgs args)
        {
            if (!Menu.Item("use" + Name).GetValue<bool>() ||
                Player.GetSpell(Slot).State != SpellState.Ready)
                return;

            foreach (var hero in champion.Heroes)
            {
                if (hero.Player.NetworkId != Player.NetworkId)
                    return;

                if (hero.Player.Mana/hero.Player.MaxMana*100 >
                    Menu.Item("SelfMinMP" + Name + "Pct").GetValue<Slider>().Value)
                {
                    if (hero.Player.Health/hero.Player.MaxHealth*100 <=
                        Menu.Item("SelfLowHP" + Name + "Pct").GetValue<Slider>().Value)
                    {
                        if (hero.IncomeDamage > 0 && hero.HitTypes.Contains(HitType.AutoAttack))
                        {
                            UseSpell();
                            RemoveSpell();
                        }
                    }
                }
            }
        }
    }
}
