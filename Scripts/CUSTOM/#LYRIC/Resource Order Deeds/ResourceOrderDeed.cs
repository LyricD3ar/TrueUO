//Resource Orders by Tresdni
//www.uofreedom.com
using System;
using Server;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	public class ResourceOrderTarget : Target 
	{
		private ResourceOrderDeed m_Deed;

		public ResourceOrderTarget( ResourceOrderDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target )
		{
			if( target == null || from == null )
				return;
				
			if( target is BaseIngot )
			{
				BaseIngot ingot = (BaseIngot)target as BaseIngot;
				
				if( ingot != null && ingot.Resource == m_Deed.Material )
				{
					int NumberOfIngotsTargeted = ingot.Amount;
					int NeededAmount =  m_Deed.AmountToGather - m_Deed.AmountGathered;
					int AmountLeft = 0;

					if( NeededAmount <= NumberOfIngotsTargeted )
					{
						AmountLeft = NumberOfIngotsTargeted - NeededAmount;
						ingot.Amount = AmountLeft;
						
						if(ingot.Amount <= 0)
							ingot.Delete();
						
						
						m_Deed.AmountGathered = m_Deed.AmountToGather;
						m_Deed.Completed = true;
						from.SendMessage("Your order has been filled completely.  Please return it to the person whom gave it to you.");
					}
					else
					{
						from.SendMessage( String.Format( "You have added {0} {1} to the order.", ingot.Amount.ToString(), m_Deed.MaterialName.ToString() ) );
						m_Deed.AmountGathered = m_Deed.AmountGathered + ingot.Amount;
						ingot.Delete();
					}
				}
				else
				{
					from.SendMessage( "That is not the correct type of resource required to fill this order." );
				}
			}
			else if( target is BaseLeather )
			{
				BaseLeather ingot = (BaseLeather)target as BaseLeather;
				
				if( ingot != null && ingot.Resource == m_Deed.Material )
				{
					int NumberOfIngotsTargeted = ingot.Amount;
					int NeededAmount =  m_Deed.AmountToGather - m_Deed.AmountGathered;
					int AmountLeft = 0;

					if( NeededAmount <= NumberOfIngotsTargeted )
					{
						AmountLeft = NumberOfIngotsTargeted - NeededAmount;
						ingot.Amount = AmountLeft;
						
						if(ingot.Amount <= 0)
							ingot.Delete();
						
						
						m_Deed.AmountGathered = m_Deed.AmountToGather;
						m_Deed.Completed = true;
						from.SendMessage("Your order has been filled completely.  Please return it to the person whom gave it to you.");
					}
					else
					{
						from.SendMessage( String.Format( "You have added {0} {1} to the order.", ingot.Amount.ToString(), m_Deed.MaterialName.ToString() ) );
						m_Deed.AmountGathered = m_Deed.AmountGathered + ingot.Amount;
						ingot.Delete();
					}
				}
				else
				{
					from.SendMessage( "That is not the correct type of resource required to fill this order." );
				}
			}
			else if( target is Board )
			{
				Board ingot = (Board)target as Board;
				
				if( ingot != null && ingot.Resource == m_Deed.Material )
				{
					int NumberOfIngotsTargeted = ingot.Amount;
					int NeededAmount =  m_Deed.AmountToGather - m_Deed.AmountGathered;
					int AmountLeft = 0;

					if( NeededAmount <= NumberOfIngotsTargeted )
					{
						AmountLeft = NumberOfIngotsTargeted - NeededAmount;
						ingot.Amount = AmountLeft;
						
						if(ingot.Amount <= 0)
							ingot.Delete();
						
						
						m_Deed.AmountGathered = m_Deed.AmountToGather;
						m_Deed.Completed = true;
						from.SendMessage("Your order has been filled completely.  Please return it to the person whom gave it to you.");
					}
					else
					{
						from.SendMessage( String.Format( "You have added {0} {1} to the order.", ingot.Amount.ToString(), m_Deed.MaterialName.ToString() ) );
						m_Deed.AmountGathered = m_Deed.AmountGathered + ingot.Amount;
						ingot.Delete();
					}
				}
				else
				{
					from.SendMessage( "That is not the correct type of resource required to fill this order." );
				}
			}
			else
			{
					from.SendMessage( "Please target the correct type of resource to fill this order." );
			}
		}
	}

	public class ResourceOrderDeed : Item
	{
		private bool m_Completed = false;
		private Mobile m_AssignedTo;
		private CraftResource m_Material;
		private string m_MaterialName;
		private int m_AmountToGather = 0;
		private int m_AmountGathered;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Completed{ get{ return m_Completed; } set { m_Completed = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Material{ get{ return m_Material; } set { m_Material = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string MaterialName{ get{ return m_MaterialName; } set { m_MaterialName = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountToGather{ get{ return m_AmountToGather; } set { m_AmountToGather = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountGathered{ get{ return m_AmountGathered; } set { m_AmountGathered= value; InvalidateProperties(); } }
		
		[Constructable]
		public ResourceOrderDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			Name = "A Resource Order Deed";
			LootType = LootType.Blessed;
			
			switch(Utility.Random(25) )
			{
				//Ingot Types
				case 0:  { Material = CraftResource.Iron; } break;
				case 1:  { Material = CraftResource.Agapite; } break;
				case 2:  { Material = CraftResource.Bronze; } break;
				case 3:  { Material = CraftResource.Copper; } break;
				case 4:  { Material = CraftResource.DullCopper; } break;
				case 5:  { Material = CraftResource.Gold; } break;
				case 6:  { Material = CraftResource.ShadowIron; } break;
				case 7:  { Material = CraftResource.Valorite; } break;
				case 8:  { Material = CraftResource.Verite; } break;
				
				//Leather Types
				case 9:  { Material = CraftResource.RegularLeather; } break;
				case 10: { Material = CraftResource.SpinedLeather; } break;
				case 11: { Material = CraftResource.HornedLeather; } break;
				case 12: { Material = CraftResource.BarbedLeather; } break;
				
				/*
				//Scale Types
				case 13:  { Material = CraftResource.RedScales; } break;
				case 14: { Material = CraftResource.YellowScales; } break;
				case 15: { Material = CraftResource.BlackScales; } break;
				case 16: { Material = CraftResource.GreenScales; } break;
				case 17: { Material = CraftResource.WhiteScales; } break;
				case 18: { Material = CraftResource.BlueScales; } break;
				*/
				
				//Board Types
				case 19: { Material = CraftResource.RegularWood; } break;
				case 20: { Material = CraftResource.OakWood; } break;
				//case 21: { Material = CraftResource.AshWood; } break;
				case 22: { Material = CraftResource.YewWood; } break;
				case 23: { Material = CraftResource.Heartwood; } break;
				case 24: { Material = CraftResource.Bloodwood; } break;
				case 25: { Material = CraftResource.Frostwood; } break;
			}
			
			switch(Material)
			{
				//Ingot Types
				case CraftResource.Iron: { MaterialName = "Iron Ingots"; } break;
				case CraftResource.Agapite : { MaterialName = "Agapite Ingots"; } break;
				case CraftResource.Bronze : { MaterialName = "Bronze Ingots"; } break;
				case CraftResource.Copper : { MaterialName = "Copper Ingots"; } break;
				case CraftResource.DullCopper : { MaterialName = "Dull Copper Ingots"; } break;
				case CraftResource.Gold : { MaterialName = "Gold Ingots"; } break;
				case CraftResource.ShadowIron : { MaterialName = "Shadow Iron Ingots"; } break;
				case CraftResource.Valorite : { MaterialName = "Valorite Ingots"; } break;
				case CraftResource.Verite : { MaterialName = "Verite Ingots"; } break;
				
				//Leather Types
				case CraftResource.RegularLeather : { MaterialName = "Regular Leather"; } break;
				case CraftResource.SpinedLeather : { MaterialName = "Spinded Leather"; } break;
				case CraftResource.HornedLeather : { MaterialName = "Horned Leather"; } break;
				case CraftResource.BarbedLeather : { MaterialName = "Barbed Leather"; } break;
				
				/*
				//Scale Types
				case CraftResource.RedScales : { MaterialName = "Red Scales"; } break;
				case CraftResource.YellowScales : { MaterialName = "Yellow Scales"; } break;
				case CraftResource.BlackScales : { MaterialName = "Black Scales"; } break;
				case CraftResource.GreenScales : { MaterialName = "Green Scales"; } break;
				case CraftResource.WhiteScales : { MaterialName = "White Scales"; } break;
				case CraftResource.BlueScales : { MaterialName = "Blue Scales"; } break;
				*/
				
				//Board Types
				case CraftResource.RegularWood : { MaterialName = "Regular Boards"; } break;
				case CraftResource.OakWood : { MaterialName = "Oak Boards"; } break;
				case CraftResource.AshWood : { MaterialName = "Ash Boards"; } break;
				case CraftResource.YewWood : { MaterialName = "Yew Boards"; } break;
				case CraftResource.Heartwood : { MaterialName = "Heartwood Boards"; } break;
				case CraftResource.Bloodwood : { MaterialName = "Bloodwood Boards"; } break;
				case CraftResource.Frostwood : { MaterialName = "Frostwood Boards"; } break;
			}
			
			switch(Material)
			{
				//Ingot Types
				case CraftResource.Iron: { AmountToGather =  Utility.Random(20000, 30000); } break;
				case CraftResource.Agapite : { AmountToGather = Utility.Random(7000, 8000); } break;
				case CraftResource.Bronze : { AmountToGather = Utility.Random(10000, 13000); } break;
				case CraftResource.Copper : { AmountToGather = Utility.Random(13000, 14000); } break;
				case CraftResource.DullCopper : { AmountToGather = Utility.Random(18000, 20000);; } break;
				case CraftResource.Gold : { AmountToGather = Utility.Random(10000, 12000); } break;
				case CraftResource.ShadowIron : { AmountToGather = Utility.Random(16000, 18000); } break;
				case CraftResource.Valorite : { AmountToGather = Utility.Random(1000, 2000); } break;
				case CraftResource.Verite : { AmountToGather = Utility.Random(3000, 6000); } break;
				
				//Leather Types
				case CraftResource.RegularLeather : { AmountToGather = Utility.Random(20000, 30000); } break;
				case CraftResource.SpinedLeather : { AmountToGather = Utility.Random(10000, 20000);; } break;
				case CraftResource.HornedLeather : { AmountToGather = Utility.Random(7000, 10000); } break;
				case CraftResource.BarbedLeather : { AmountToGather = Utility.Random(1000, 6000);} break;
				
				//Scale Types //Original code commented out, replaced with fix - Lyric, 12/26/21
				/*case CraftResource.RedScales : { AmountToGather = "Red Scales"; } break;
				case CraftResource.YellowScales : { AmountToGather = "Yellow Scales"; } break;
				case CraftResource.BlackScales : { AmountToGather = "Black Scales"; } break;
				case CraftResource.GreenScales : { AmountToGather = "Green Scales"; } break;
				case CraftResource.WhiteScales : { AmountToGather = "White Scales"; } break;
				case CraftResource.BlueScales : { AmountToGather = "Blue Scales"; } break;*/
				
				case CraftResource.RedScales : { AmountToGather = Utility.Random(1000, 6000);} break;
				case CraftResource.YellowScales : { AmountToGather = Utility.Random(1000, 6000);} break;
				case CraftResource.BlackScales : { AmountToGather = Utility.Random(1000, 6000);} break;
				case CraftResource.GreenScales : { AmountToGather = Utility.Random(1000, 6000);} break;
				case CraftResource.WhiteScales : { AmountToGather = Utility.Random(1000, 6000);} break;
				case CraftResource.BlueScales : { AmountToGather = Utility.Random(1000, 6000);} break;
				
				//Board Types
				case CraftResource.RegularWood : { AmountToGather = Utility.Random(20000, 30000); } break;
				case CraftResource.OakWood : { AmountToGather = Utility.Random(10000, 20000); } break;
				//case CraftResource.AshWood : { AmountToGather = Utility.Random(7000, 10000) / 4; } break;
				case CraftResource.YewWood : { AmountToGather = Utility.Random(5000, 7000); } break;
				case CraftResource.Heartwood : { AmountToGather = Utility.Random(3000, 5000); } break;
				case CraftResource.Bloodwood : { AmountToGather = Utility.Random(2000, 3000); } break;
				case CraftResource.Frostwood : { AmountToGather = Utility.Random(1000, 2500); } break;
			}
			
			
			Hue = CraftResources.GetHue( Material );
		}

		public ResourceOrderDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (bool) m_Completed );
			writer.Write( (int) m_Material );
			writer.Write( (string) m_MaterialName );
			writer.Write( (int) m_AmountToGather );
			writer.Write( (int) m_AmountGathered );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
			
			m_Completed = reader.ReadBool();
			m_Material = (CraftResource)reader.ReadInt();
			m_MaterialName = reader.ReadString();
			m_AmountToGather = reader.ReadInt();
			m_AmountGathered = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				 from.SendLocalizedMessage( 1042001 );
			}
			else if( this.Completed )
			{
				from.SendMessage("Your order has been filled completely.  Please return it to the person whom gave it to you.");
			}
			else
			{
				from.SendMessage( "Target the resource you wish to combine with this order." ); 
				from.Target = new ResourceOrderTarget( this );
			 }
		}

		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			
			if(m_Completed)
			{
				list.Add( String.Format( "Order Fulfilled [{0} {1}]", m_AmountToGather, m_MaterialName) );
			}
			else
			{
				list.Add( String.Format( "{0} / {1} {2} Obtained", m_AmountGathered, m_AmountToGather, m_MaterialName ) );
			}
		}
	}
}



