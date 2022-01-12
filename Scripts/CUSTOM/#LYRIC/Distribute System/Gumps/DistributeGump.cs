using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
//using Server.Scripts;
using Server.Targeting;
using Server.Commands;

namespace Server.Gumps
{

    public class DistributeGump : Gump
    {
        private int m_Amount;

        public void RenderGump()
        {
            m_Amount = 1;
            RenderGump(100, 0, string.Empty);
        }

        public void RenderGump(int rule, int access, string type)
        {
            this.Closable = true;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 400, 461, 9260);
            this.AddImage(260, 40, 5536);
            this.AddLabel(80, 40, 1000, @"Item Distributer");
            this.AddButton(175, 415, 4023, 4024, (int)Buttons.GiveByTarget, GumpButtonType.Reply, 1);
            this.AddLabel(90, 305, 1000, @"Counselor");
            this.AddCheck(50, 305, 210, 211, (access & (int)Switches.Counselor) != 0, (int)Switches.Counselor);
            this.AddLabel(90, 275, 1000, @"GameMaster");
            this.AddCheck(50, 275, 210, 211, (access & (int)Switches.GameMaster) != 0, (int)Switches.GameMaster);
            this.AddLabel(90, 245, 1000, @"Seer");
            this.AddCheck(50, 245, 210, 211, (access & (int)Switches.Seer) != 0, (int)Switches.Seer);
            this.AddLabel(90, 215, 1000, @"Administrator");
            this.AddCheck(50, 215, 210, 211, (access & (int)Switches.Administrator) != 0, (int)Switches.Administrator);
            this.AddLabel(90, 185, 1000, @"Developer");
            this.AddCheck(50, 185, 210, 211, (access & (int)Switches.Developer) != 0, (int)Switches.Developer);
            this.AddLabel(90, 155, 1000, @"Owner");
            this.AddCheck(50, 155, 210, 211, (access & (int)Switches.Owner) != 0, (int)Switches.Owner);
            this.AddLabel(260, 275, 1000, @"Per Access Level");
            this.AddRadio(220, 275, 209, 208, rule == (int)Switches.GiveToAccessLevel, (int)Switches.GiveToAccessLevel);
            this.AddLabel(260, 245, 1000, @"Per Character");
            this.AddRadio(220, 245, 209, 208, rule == (int)Switches.GiveToCharacter, (int)Switches.GiveToCharacter);
            this.AddLabel(260, 215, 1000, @"Per Account");
            this.AddRadio(220, 215, 209, 208, rule == (int)Switches.GiveToAccount, (int)Switches.GiveToAccount);
            this.AddLabel(75, 385, 1000, @"Target the item you wish to distribute");
            this.AddButton(232, 305, 9764, 9765, (int)Buttons.DecAmount, GumpButtonType.Reply, -1);
            this.AddButton(220, 305, 9760, 9761, (int)Buttons.IncAmount, GumpButtonType.Reply, 1);
            this.AddLabel(320, 305, 1000, m_Amount.ToString());
            this.AddLabel(260, 305, 0, @"Amount:");
        }

        public DistributeGump()
            : base(50, 50)
        {
            RenderGump();
        }

        public DistributeGump(int GiveRule, int Access, string TypeName, int Amount)
            : base(50, 50)
        {
            m_Amount = Amount;
            RenderGump(GiveRule, Access, TypeName);
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            string TypeName = string.Empty;
            int GiveRule = 0;
            int Access = 0;

            foreach (int sw in info.Switches)
            {
                switch (sw)
                {
                    case (int)Switches.GiveToCharacter:
                        {
                            GiveRule = (int)Switches.GiveToCharacter;
                            break;
                        }
                    case (int)Switches.GiveToAccount:
                        {
                            GiveRule = (int)Switches.GiveToAccount;
                            break;
                        }
                    case (int)Switches.GiveToAccessLevel:
                        {
                            GiveRule = (int)Switches.GiveToAccessLevel;
                            break;
                        }
                    case (int)Switches.Owner:
                    case (int)Switches.Developer:
                    case (int)Switches.Administrator:
                    case (int)Switches.GameMaster:
                    case (int)Switches.Seer:
                    case (int)Switches.Counselor:
                        {
                            Access += sw;
                            break;
                        }
                }
            }
            if (GiveRule == 0)
            {
                from.SendMessage("You must select the audience rule to receive the item.");
                from.SendGump(new DistributeGump(GiveRule, Access, TypeName, m_Amount));
                return;
            }
            else if (GiveRule == (int)Switches.GiveToAccessLevel && Access == 0)
            {
                from.SendMessage("You must select the AccessLevel to receive the item.");
                from.SendGump(new DistributeGump(GiveRule, Access, TypeName, m_Amount));
                return;
            }

            switch (info.ButtonID)
            {
                case (int)Buttons.GiveByTarget:
                    {
                        from.Target = new Distribute.DupeTarget(false, m_Amount, GiveRule, Access);
                        from.SendMessage("What do you wish to give out?");
                        break;
                    }
                case (int)Buttons.IncAmount:
                    {
                        from.SendGump(new DistributeGump(GiveRule, Access, TypeName, ++m_Amount));
                        break;
                    }
                case (int)Buttons.DecAmount:
                    {
                        if (m_Amount > 1)
                            m_Amount -= 1;
                        else
                            from.SendMessage("You cannot give less than 1 item.");
                        from.SendGump(new DistributeGump(GiveRule, Access, TypeName, m_Amount));
                        break;
                    }
            }

        }

        public enum Buttons
        {
            Cancel,
            GiveByTarget,
            GiveByType,
            IncAmount,
            DecAmount
        }

        public enum Switches
        {
            Owner = 1,
            Developer = 2,
            Administrator = 4,
            GameMaster = 8,
            Seer = 16,
            Counselor = 32,
            GiveToAccount = 100,
            GiveToCharacter = 200,
            GiveToAccessLevel = 300
        }
    }
}