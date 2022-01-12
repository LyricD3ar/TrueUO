using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Commands;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a human corpse" )]
	public class ResourceGatherer : Mobile
	{
        public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public ResourceGatherer()
		{
			InitStats(31, 41, 51);

			Hue = Utility.RandomSkinHue(); 
			Body = 0x190;
			Blessed = true;

			AddItem( new FancyShirt( Utility.RandomNeutralHue() ) );
			AddItem( new Boots( ) );
			AddItem( new FullApron() );
			AddItem( new Pickaxe() );
			AddItem( new LongPants( Utility.RandomNeutralHue() ) );
			Utility.AssignRandomHair( this );
			Direction = Direction.South;
			Name = NameList.RandomName( "male" ); 
			Title = "the Resource Gatherer"; 
		}

		public ResourceGatherer( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new ResourceGatherEntry( from, this ) ); 
	        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class ResourceGatherEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public ResourceGatherEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				if( !( m_Mobile is PlayerMobile ) )
					return;
					
				if(m_Mobile == null)
					return;
				
				ResourceOrderDeed od = new ResourceOrderDeed();
				
				m_Mobile.AddToBackpack(od);
				m_Mobile.SendMessage(273, "A resource order has been placed in your backpack.  Once it is filled, return it to this resource gatherer." );
			}
		}
		
		public void RewardForResourceGather( Mobile from, ResourceOrderDeed deed )
		{
			int GoldScaler = 1;
			int PSChance = 0;
			
			//Generate a defaulted powerscroll, and we'll manipulate it later.  If it is never given, it is deleted at the end (to keep it out of the internal map).
			PowerScroll ps = new PowerScroll(SkillName.Blacksmith, 135);
			
			switch(deed.Material )
			{
				//Ingot Types
				case CraftResource.Iron: 
				{ 
					GoldScaler = Utility.Random(1,2); 
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(8);
					
				} break;
				case CraftResource.DullCopper : 
				{ 
					GoldScaler = Utility.Random(1, 2); 
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(8);
					
				} break;
				case CraftResource.ShadowIron : 
				{ 
					GoldScaler = Utility.Random(1,2); 
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(8);
					
				} break;
				case CraftResource.Copper : 
				{ 
					GoldScaler = Utility.Random(1,2);
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(9);
					
				} break;
				case CraftResource.Bronze : 
				{
					GoldScaler = Utility.Random(1,2);
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(10);
					
				} break;
				case CraftResource.Gold : 
				{ 
					GoldScaler = Utility.Random(1,2); 
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(11 );
					
				} break;
				case CraftResource.Agapite : 
				{ 
					GoldScaler = Utility.Random(1, 3); 
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
					}
					
					PSChance = Utility.Random(12);
					
				} break;
				case CraftResource.Verite : 
				{ 
					GoldScaler = Utility.Random(1, 3); 
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 2 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(15);
					
				} break;
				case CraftResource.Valorite : 
				{ 
					GoldScaler = Utility.Random(1, 4); 
					
					switch( Utility.Random(2) )
					{
						case 0: { ps.Skill = SkillName.Blacksmith; } break;
						case 1: { ps.Skill = SkillName.Tinkering; } break;
					}
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(20);
					
				} break;
				
				//Leather Types
				case CraftResource.RegularLeather: 
				{ 
					GoldScaler = Utility.Random(1,2); 
					
					ps.Skill = SkillName.Tailoring;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(8);
					
				} break;
				case CraftResource.SpinedLeather: 
				{ 
					GoldScaler = Utility.Random(1, 2); 
					
					ps.Skill = SkillName.Tailoring;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
					}
					
					PSChance = Utility.Random(9);
					
				} break;
				case CraftResource.HornedLeather: 
				{ 
					GoldScaler = Utility.Random(1, 2); 
					
					ps.Skill = SkillName.Tailoring;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
					}
					
					PSChance = Utility.Random(15);
					
				} break;
				case CraftResource.BarbedLeather: 
				{ 
					GoldScaler =  Utility.Random(1, 3); 
					
					ps.Skill = SkillName.Tailoring;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
					}
					
					PSChance = Utility.Random(20);
					
				} break;
				
				//Board Types
				case CraftResource.RegularWood: 
				{ 
					GoldScaler = Utility.Random(1,2); 
					
					ps.Skill = SkillName.Carpentry;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(6);
					
				} break;
				case CraftResource.OakWood: 
				{ 
					GoldScaler = Utility.Random(1,2); 
					
					ps.Skill = SkillName.Carpentry;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(7);
					
				} break;
				//As Wood doesn't work for some reason, removing it - Lyric, 12/26/21
				//case CraftResource.AshWood: 
				//{ 
				//	GoldScaler = Utility.Random(1, 2); 
				//	
				//	ps.Skill = SkillName.Carpentry;
				//	
				//	switch( Utility.Random( 3 ) )
				//	{
				//		case 0: { ps.Value = 135; } break;
				//		case 1: { ps.Value = 150; } break;
				//		case 2: { ps.Value = 175; } break;
				//		//case 3: { ps.Value = 120; } break;
				//	}
					
				//	PSChance = Utility.Random(8);
					
				//} break;
				case CraftResource.YewWood: 
				{ 
					GoldScaler = Utility.Random(1, 2); 
					
					ps.Skill = SkillName.Carpentry;
					
					switch( Utility.Random( 2 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 3: { ps.Value = 175; } break;
					}
					
					PSChance = Utility.Random(9);
					
				} break;
				case CraftResource.Heartwood: 
				{ 
					GoldScaler = Utility.Random(1, 3); 
					
					ps.Skill = SkillName.Carpentry;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(10);
					
				} break;
				case CraftResource.Bloodwood: 
				{ 
					GoldScaler = Utility.Random(1, 3); 
					
					ps.Skill = SkillName.Carpentry;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(10);
				} break;
				case CraftResource.Frostwood: 
				{ 
					GoldScaler = Utility.Random(1, 4); 
					
					ps.Skill = SkillName.Carpentry;
					
					switch( Utility.Random( 3 ) )
					{
						case 0: { ps.Value = 135; } break;
						case 1: { ps.Value = 150; } break;
						case 2: { ps.Value = 175; } break;
						//case 3: { ps.Value = 120; } break;
					}
					
					PSChance = Utility.Random(20);
					
				} break;
			}
			
			//Handles gold reward for all resource types.
			if( Utility.Random( PSChance ) > 2 )  //simple math eh?
			{
				from.AddToBackpack( ps );
			}
			else
			{
				ps.Delete();  //No powerscroll reward this time.
			}
			
			from.AddToBackpack( new BankCheck(GoldScaler * deed.AmountToGather) );
		}

		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
         			if(dropped is ResourceOrderDeed )
         			{
						ResourceOrderDeed rod = (ResourceOrderDeed)dropped;
						
						if(!rod.Completed)
						{
							this.PrivateOverheadMessage(MessageType.Regular, 39, false, "This order is not fulfilled!  I want resources, not just empty deeds!", mobile.NetState); return false;
						}
						else
						{
							RewardForResourceGather(from, rod);
							
							this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "You have fulfilled my order!  Here is your reward.  Now maybe I can get some work done around here!", mobile.NetState); return true;
							rod.Delete();
						}
         			}
			}
			else
			{
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState); return false;
			}
			return false;
		}
	}
}
