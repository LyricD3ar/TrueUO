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

namespace Server.Commands
{
    public class Distribute
    {
        public static void Initialize()
        {
            CommandSystem.Register("Distribute", AccessLevel.Administrator, new CommandEventHandler(Distribute_OnCommand));
        }

        private static void Distribute_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new DistributeGump());
        }

        private static void PlaceItemIn(Container parent, int x, int y, Item item)
        {
            parent.AddItem(item);
            item.Location = new Point3D(x, y, 0);
        }

        public class DupeTarget : Target
        {
            private bool m_InBag;
            private int m_Amount;
            private int m_GiveRule;
            private int m_Access;

            public DupeTarget(bool inbag, int amount, int give, int access)
                : base(15, false, TargetFlags.None)
            {
                m_InBag = inbag;
                m_Amount = amount;
                m_GiveRule = give;
                m_Access = access;
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if (!(targ is Item))
                {
                    from.SendMessage("You can only dupe items.");
                    return;
                }

                from.SendMessage("Placing {0} into bank boxes...", ((Item)targ).Name == null ? "an item" : ((Item)targ).Name.ToString());
                CommandLogging.WriteLine(from, "{0} {1} adding {2} to bank boxes )", from.AccessLevel, CommandLogging.Format(from), CommandLogging.Format(targ));

                GiveItem(from, (Item)targ, m_Amount, m_GiveRule, m_Access);
            }
        }

        public static void GiveItem(Mobile from, Item item, int amount, int give, int access)
        {
            bool done = true;
            if (give == (int)DistributeGump.Switches.GiveToAccount)
            {
                done = Distribute.GiveItemToAccounts(item, amount);
            }
            else if (give == (int)DistributeGump.Switches.GiveToCharacter)
            {
                done = Distribute.GiveItemToCharacters(item, amount);
            }
            else if (give == (int)DistributeGump.Switches.GiveToAccessLevel)
            {
                done = Distribute.GiveItemToAccessLevel(item, amount, access);
            }

            if (!done)
            {
                from.SendMessage("Unable to give out to 1 or more players.");
            }
            else
            {
                from.SendMessage("Completed.");
            }

        }

        private static bool GiveItemToAccounts(Item item, int amount)
        {
            bool success = true;

            foreach (Account acct in Accounts.GetAccounts())
            {
                if (acct[0] != null)
                {
                    if (!CopyItem(item, amount, acct[0].BankBox))
                    {
                        Console.WriteLine("Could not give item to {0}", acct[0].Name);
                        success = false;
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[1] != null)
                    {

                        if (!CopyItem(item, amount, acct[1].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[1].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[2] != null)
                    {

                        if (!CopyItem(item, amount, acct[2].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[2].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[3] != null)
                    {

                        if (!CopyItem(item, amount, acct[3].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[3].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[4] != null)
                    {

                        if (!CopyItem(item, amount, acct[4].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[4].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[5] != null)
                    {

                        if (!CopyItem(item, amount, acct[5].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[5].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[6] != null)
                    {

                        if (!CopyItem(item, amount, acct[6].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[6].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[7] != null)
                    {

                        if (!CopyItem(item, amount, acct[7].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[7].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[8] != null)
                    {

                        if (!CopyItem(item, amount, acct[8].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[8].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[9] != null)
                    {

                        if (!CopyItem(item, amount, acct[9].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[9].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[10] != null)
                    {

                        if (!CopyItem(item, amount, acct[10].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[10].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[11] != null)
                    {

                        if (!CopyItem(item, amount, acct[11].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[11].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[12] != null)
                    {

                        if (!CopyItem(item, amount, acct[12].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[12].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[13] != null)
                    {

                        if (!CopyItem(item, amount, acct[13].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[13].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[14] != null)
                    {

                        if (!CopyItem(item, amount, acct[14].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[14].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[15] != null)
                    {

                        if (!CopyItem(item, amount, acct[15].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[15].Name);
                            success = false;
                        }
                    }
                }
                else if (acct[0] == null)
                {
                    if (acct[16] != null)
                    {

                        if (!CopyItem(item, amount, acct[16].BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", acct[16].Name);
                            success = false;
                        }
                    }
                }


            }
            return success;
        }

        private static bool GiveItemToCharacters(Item item, int amount)
        {
            bool success = true;
            List<Mobile> mobs = new List<Mobile>(World.Mobiles.Values);
            foreach (Mobile m in mobs)
            {
                if (m is PlayerMobile)
                {
                    if (!CopyItem(item, amount, m.BankBox))
                    {
                        Console.WriteLine("Could not give item to {0}", m.Name);
                        success = false;
                    }
                }
            }
            return success;
        }

        private static bool GiveItemToAccessLevel(Item item, int amount, int access)
        {
            bool success = true;
            List<Mobile> mobs = new List<Mobile>(World.Mobiles.Values);
            foreach (Mobile m in mobs)
            {
                if (m is PlayerMobile)
                {
                    bool give = false;
                    if ((access & (int)DistributeGump.Switches.Administrator) != 0 && m.AccessLevel == AccessLevel.Administrator)
                    {
                        give = true;
                    }
                    else if ((access & (int)DistributeGump.Switches.GameMaster) != 0 && m.AccessLevel == AccessLevel.GameMaster)
                    {
                        give = true;
                    }
                    else if ((access & (int)DistributeGump.Switches.Seer) != 0 && m.AccessLevel == AccessLevel.Seer)
                    {
                        give = true;
                    }
                    else if ((access & (int)DistributeGump.Switches.Counselor) != 0 && m.AccessLevel == AccessLevel.Counselor)
                    {
                        give = true;
                    }

                    if (give)
                    {
                        if (!CopyItem(item, amount, m.BankBox))
                        {
                            Console.WriteLine("Could not give item to {0}", m.Name);
                            success = false;
                        }
                    }
                }
            }
            return success;
        }

        private static bool CopyItem(Item item, int count, Container container)
        {
            bool m_Success = false;
            Type t = item.GetType();

            ConstructorInfo[] info = t.GetConstructors();

            foreach (ConstructorInfo c in info)
            {
                ParameterInfo[] paramInfo = c.GetParameters();

                if (paramInfo.Length == 0)
                {
                    object[] objParams = new object[0];

                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            object o = c.Invoke(objParams);

                            if (o != null && o is Item)
                            {
                                Item newItem = (Item)o;
                                CopyProperties(newItem, item);
                                newItem.Parent = null;

                                // recurse if container
                                if (item is Container && newItem.Items.Count == 0)
                                {
                                    for (int x = 0; x < item.Items.Count; x++)
                                    {
                                        m_Success = CopyItem((Item)item.Items[x], 1, (Container)newItem);
                                    }
                                }

                                if (container != null)
                                    PlaceItemIn(container, 20 + (i * 10), 10, newItem);
                            }
                        }
                        m_Success = true;
                    }
                    catch
                    {
                        m_Success = false;
                    }
                }

            } // end foreach
            return m_Success;

        } // end function

        private static void CopyProperties(Item dest, Item src)
        {
            PropertyInfo[] props = src.GetType().GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                try
                {
                    if (props[i].CanRead && props[i].CanWrite)
                    {
                        //Console.WriteLine( "Setting {0} = {1}", props[i].Name, props[i].GetValue( src, null ) );
                        if (src is Container && (props[i].Name == "TotalWeight" || props[i].Name == "TotalItems"))
                        {
                            // don't set these props
                        }
                        else
                        {
                            props[i].SetValue(dest, props[i].GetValue(src, null), null);
                        }
                    }
                }
                catch
                {
                    //Console.WriteLine( "Denied" );
                }
            }
        }
    }
}
