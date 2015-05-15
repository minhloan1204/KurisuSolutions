﻿using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace Activator.Spells.Health
{
    class kalistarx : spell
    {
        internal override string Name
        {
            get { return "kalistarx"; }
        }

        internal override string DisplayName
        {
            get { return "Fate's Call | R"; }
        }

        internal override float Range
        {
            get { return 1200f; }
        }

        internal override MenuType[] Category
        {
            get { return new[] { MenuType.SelfLowHP }; }
        }

        internal override int DefaultHP
        {
            get { return 20; }
        }

        internal override int DefaultMP
        {
            get { return 0; }
        }

        public override void OnTick(EventArgs args)
        {
            if (!Menu.Item("use" + Name).GetValue<bool>())
                return;

            var cooptarget =
                ObjectManager.Get<Obj_AI_Hero>()
                    .FirstOrDefault(hero => hero.HasBuff("kalistacoopstrikeally", true));

            foreach (var hero in champion.Heroes)
            {
                if (cooptarget == null)
                    return;

                if (hero.Player.NetworkId != Player.NetworkId)
                    return;

                if (hero.Player.NetworkId == cooptarget.NetworkId)
                {
                    if (hero.Player.Distance(cooptarget.ServerPosition) <= Range &&
                        !cooptarget.HasBuffOfType(BuffType.Invulnerability))
                    
                        if (hero.Player.Health/hero.Player.MaxHealth <=
                            Menu.Item("SelfLowHP" + Name + "Pct").GetValue<Slider>().Value)
                        {
                            if (hero.IncomeDamage > 0)
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
