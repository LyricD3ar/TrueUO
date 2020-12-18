using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Engines.Quests
{
    public abstract class MondainQuester : BaseVendor
    {
        protected readonly List<SBInfo> m_SBInfos = new List<SBInfo>();
        private DateTime m_Spoken;
        public MondainQuester()
            : base(null)
        {
            SpeechHue = 0x3B2;
        }

        public MondainQuester(string name)
            : this(name, null)
        {
        }

        public MondainQuester(string name, string title)
            : base(title)
        {
            Name = name;
            SpeechHue = 0x3B2;
        }

        public MondainQuester(Serial serial)
            : base(serial)
        {
        }

        public override void CheckMorph()
        {
            // Don't morph me!
        }
        public override bool IsActiveVendor => false;
        public override bool IsInvulnerable => true;
        public override bool DisallowAllMoves => false;
        public override bool ClickTitle => false;
        public override bool CanTeach => true;
        public virtual int AutoTalkRange => -1;
        public virtual int AutoSpeakRange => 10;
        public virtual TimeSpan SpeakDelay => TimeSpan.FromMinutes(1);
        public abstract Type[] Quests { get; }
        protected override List<SBInfo> SBInfos => m_SBInfos;
        public override void InitSBInfo()
        {
        }

        public virtual void OnTalk(PlayerMobile player)
        {
            if (QuestHelper.DeliveryArrived(player, this))
                return;

            if (QuestHelper.InProgress(player, this))
                return;

            if (QuestHelper.QuestLimitReached(player))
                return;

            // check if this quester can offer any quest chain (already started)
            foreach (KeyValuePair<QuestChain, BaseChain> pair in player.Chains)
            {
                BaseChain chain = pair.Value;

                if (chain != null && chain.Quester != null && chain.Quester == GetType())
                {
                    BaseQuest quest = QuestHelper.RandomQuest(player, new[] { chain.CurrentQuest }, this);

                    if (quest != null)
                    {
                        player.CloseGump(typeof(MondainQuestGump));
                        player.SendGump(new MondainQuestGump(quest));
                        return;
                    }
                }
            }

            BaseQuest questt = QuestHelper.RandomQuest(player, Quests, this);

            if (questt != null)
            {
                player.CloseGump(typeof(MondainQuestGump));
                player.SendGump(new MondainQuestGump(questt));
            }
        }

        public virtual void OnOfferFailed()
        {
            Say(1080107); // I'm sorry, I have nothing for you at this time.
        }

        public virtual void Advertise()
        {
            Say(Utility.RandomMinMax(1074183, 1074223));
        }

        public override bool CanBeDamaged()
        {
            return false;
        }

        public override void InitBody()
        {
            if (Race != null)
            {
                HairItemID = Race.RandomHair(Female);
                HairHue = Race.RandomHairHue();
                FacialHairItemID = Race.RandomFacialHair(Female);
                FacialHairHue = Race.RandomHairHue();
                Hue = Race.RandomSkinHue();
            }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m.Alive && !m.Hidden && m is PlayerMobile pm)
            {
                int range = AutoTalkRange;

                if (range >= 0 && InRange(m, range) && !InRange(oldLocation, range))
                    OnTalk(pm);

                range = AutoSpeakRange;

                if (InLOS(pm) && range >= 0 && InRange(m, range) && !InRange(oldLocation, range) && DateTime.UtcNow >= m_Spoken + SpeakDelay)
                {
                    if (Utility.Random(100) < 50)
                        Advertise();

                    m_Spoken = DateTime.UtcNow;
                }
            }
        }

        public override void OnDoubleClick(Mobile m)
        {
            if (m.Alive && m is PlayerMobile mobile)
            {
                OnTalk(mobile);
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1072269); // Quest Giver
        }

        public void FocusTo(Mobile to)
        {
            QuestSystem.FocusTo(this, to);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            if (CantWalk)
                Frozen = true;
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Spoken = DateTime.UtcNow;

            if (CantWalk)
                Frozen = true;
        }
    }
}
