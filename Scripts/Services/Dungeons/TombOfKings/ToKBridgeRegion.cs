using Server.Items;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Server.Regions
{
    public class ToKBridgeRegion : BaseRegion
    {
        public ToKBridgeRegion(XmlElement xml, Map map, Region parent)
            : base(xml, map, parent)
        {
            Timer.DelayCall(TimeSpan.Zero, Ensure);
        }

        private List<Item> m_Blocks;
        private Timer m_FadingTimer;
        private int m_HuePointer;

        public void Ensure()
        {
            m_Blocks = new List<Item>();

            for (var index = 0; index < Area.Length; index++)
            {
                Rectangle3D r3d = Area[index];

                Rectangle2D r2d = new Rectangle2D(r3d.Start, r3d.End);

                foreach (Item item in Map.GetItemsInBounds(r2d))
                {
                    if (item is Static)
                        m_Blocks.Add(item);
                }
            }

            if (m_Blocks.Count == 0)
            {
                m_Blocks = null;
                return;
            }

            for (var index = 0; index < m_Blocks.Count; index++)
            {
                Item item = m_Blocks[index];

                item.Hue = 0x807;
                item.Visible = false;
            }

            m_FadingTimer = Timer.DelayCall(TimeSpan.Zero, TimeSpan.FromSeconds(0.33), OnTick);
        }

        private static readonly int[] m_Hues =
        {
            0,
            0x807, 0x806, 0x805, 0x804, 0x803,
            0x7E3, 0x7E4, 0x7E5, 0x7E6, 0x7E7,
            0x7E8, 0x7E9, 0x3E3, 0x3E4, 0x380,
            0
        };

        public void WakeUp()
        {
            if (m_Blocks == null)
                Ensure();

            if (m_FadingTimer != null && !m_FadingTimer.Running)
                m_FadingTimer.Start();
        }

        public void OnTick()
        {
            m_HuePointer += GetPlayerCount() > 0 ? 1 : -1;

            Utility.FixMinMax(ref m_HuePointer, 0, m_Hues.Length - 1);

            for (var index = 0; index < m_Blocks.Count; index++)
            {
                Item item = m_Blocks[index];

                item.Hue = m_Hues[m_HuePointer];
            }

            if (m_HuePointer == 0)
            {
                for (var index = 0; index < m_Blocks.Count; index++)
                {
                    Item item = m_Blocks[index];

                    item.Visible = false;
                }

                m_FadingTimer.Stop();
            }
            else if (m_HuePointer == 1)
            {
                for (var index = 0; index < m_Blocks.Count; index++)
                {
                    Item item = m_Blocks[index];

                    item.Visible = true;
                }
            }
            else if (m_HuePointer == m_Hues.Length - 1)
            {
                m_FadingTimer.Stop();
            }
        }

        public override void OnEnter(Mobile m)
        {
            WakeUp();
        }

        public override void OnExit(Mobile m)
        {
            WakeUp();
        }
    }
}
