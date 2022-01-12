using System;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
[CorpseName("Corpse of remindernpc")]
public class remindernpc:Pixie
{

      private static bool m_Talked;
          string[]remindernpcSay = new string[]
          {
"Don't forget to vote!!",
"Did you water your plants today?",
"Is it BOD time already?",
"Happy hunting!",
"Will we be having company over for dinner?",
"Have a nice day!!",
};

[Constructable]
public remindernpc() : base()
{

Name = "Tinkerbelle";
Blessed = true;
Hue = 0;
SetStr (150) ;
SetDex (144) ;
SetInt (150) ;

SetDamage (100) ;

SetDamageType( ResistanceType.Physical,15) ;
SetDamageType( ResistanceType.Cold,50) ;
SetDamageType( ResistanceType.Fire,40) ;
SetDamageType( ResistanceType.Energy,15) ;
SetDamageType( ResistanceType.Poison,30) ;


SetResistance( ResistanceType.Physical,15) ;
SetResistance( ResistanceType.Cold,20) ;
SetResistance( ResistanceType.Fire,20) ;
SetResistance( ResistanceType.Energy,15) ;
SetResistance( ResistanceType.Poison,11) ;



VirtualArmor = 500;




}

          public override bool Unprovokable{ get{ return true; } }
          public override bool BleedImmune{ get{ return true; } }


		public remindernpc( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
            public override void OnMovement( Mobile m, Point3D oldLocation )
            {

                 if( m_Talked == false )
                 {
                      if ( m.InRange( this, 3 ) && m is PlayerMobile)
                       {

                             m_Talked = true;
                             SayRandom(remindernpcSay, this );
                             this.Move( GetDirectionTo( m.Location ) );
                             SpamTimer t = new SpamTimer();
                             t.Start();
                        }
                   }
              }

              private class SpamTimer : Timer
              {
                   public SpamTimer() : base( TimeSpan.FromSeconds( 12 ) )
                   {
                        Priority = TimerPriority.OneSecond;
                   }

                   protected override void OnTick()
                   {
                           m_Talked = false;
                   }
               }

               private static void SayRandom( string[] say, Mobile m )
               {
                    m.Say( say[Utility.Random( say.Length )] );
               }
	}
}
