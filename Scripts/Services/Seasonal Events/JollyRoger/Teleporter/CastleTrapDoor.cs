using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class CastleTrapDoor : Item
    {
        private Point3D _PointDest;
        private Map _Map;
        private bool _Exit;

        [Constructable]
        public CastleTrapDoor(Point3D point, Map map, bool exit)
            : base(0xA1CB)
        {
            _PointDest = point;
            _Map = map;
            _Exit = exit;
            Movable = false;
        }

        public CastleTrapDoor(Serial serial)
            : base(serial)
        {
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m.IsPlayer())
            {
                if (FellowshipMedallion.IsDressed(m) || _Exit)
                {
                    BaseCreature.TeleportPets(m, _PointDest, _Map);
                    m.MoveToWorld(_PointDest, _Map);
                }
                else
                {
                    m.PrivateOverheadMessage(MessageType.Regular, 0x47E, 1159385,
                        m.NetState); // * Your connection to the ethereal void is not honed, you cannot pass... *
                }
            }

            return base.OnMoveOver(m);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version

            writer.Write(_PointDest);
            writer.Write(_Map);
            writer.Write(_Exit);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            _PointDest = reader.ReadPoint3D();
            _Map = reader.ReadMap();
            _Exit = reader.ReadBool();
        }
    }
}
