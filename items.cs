// TODO:
// more wood: sticks, logs
// maybe more coin/ingot types?
// leather
// non modern adhesive? (short stick with white gluey stuff?)

datablock ItemData(rsrc_GunCaseItem : hammerItem)
{
	iconName = "";
	image = "";
	doColorShift = true;
	colorShiftColor = "1.0 1.0 1.0 1.0";

	isResource = true;

	shapeFile = "./dts/guncase.dts";
	uiName = "R: Gun Case";
	resourceTitle = "Gun Case"; // ui display name
	resourceName = "gun"; // event name
	resourceSound = rsrc_PickupSound; // sound to play when picking up this resource
	resourceTakeSound = ""; // sound to play when dropping/losing this resource
	resourceGive = 1; // default amount
	resourceMax = 0;
	resourceValue = 1000; // score to give when sold
	resourceGroupNode = ""; // node name prefix for grouping
	resourceGroupDiv = 1; // increase grouping every n resources (e.g. money increases grouping every 100 resources)
	resourceGroupMax = 0; // max node grouping (zero-indexed! goes from 0 to n-1)
};

datablock ItemData(rsrc_WoodPlanksItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/wood.dts";
	uiName = "R: Wood Planks";
	resourceTitle = "Wood";
	resourceName = "wood";
	resourceSound = rsrc_PickupWoodSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 150;
};

datablock ItemData(rsrc_GlueBottleItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/glue.dts";
	uiName = "R: Glue";
	resourceTitle = "Glue Bottle";
	resourceName = "glue";
	resourceSound = rsrc_PickupBottleSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_ClothItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/cloth.dts";
	uiName = "R: Cloth";
	resourceTitle = "Cloth";
	resourceName = "cloth";
	resourceSound = rsrc_PickupClothSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
	resourceGroupNode = "cloth";
	resourceGroupDiv = 1;
	resourceGroupMax = 5;
};

datablock ItemData(rsrc_GunPowderItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/gunpowder.dts";
	uiName = "R: Gunpowder";
	resourceTitle = "Gunpowder Bottle";
	resourceName = "gunpowder";
	resourceSound = rsrc_PickupBottleSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 500;
};

datablock ItemData(rsrc_MetalScrapItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/scrap.dts";
	uiName = "R: Metal Scrap";
	resourceTitle = "Metal Scrap";
	resourceName = "scrap";
	resourceSound = rsrc_PickupScrapSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 150;
	resourceGroupNode = "scrap";
	resourceGroupDiv = 1;
	resourceGroupMax = 4;
};

datablock ItemData(rsrc_PlasticPipesItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/plastic.dts";
	uiName = "R: Plastic Pipes";
	resourceTitle = "Plastic";
	resourceName = "plas";
	resourceSound = rsrc_PickupSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 150;
	resourceGroupNode = "plas";
	resourceGroupDiv = 1;
	resourceGroupMax = 4;
};

datablock ItemData(rsrc_TapeRollItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/tape.dts";
	uiName = "R: Tape Roll";
	resourceTitle = "Tape";
	resourceName = "tape";
	resourceSound = rsrc_PickupClothSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_StringRollItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/string.dts";
	uiName = "R: String Roll";
	resourceTitle = "String";
	resourceName = "string";
	resourceSound = rsrc_PickupHelmetSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_CopperRollItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/coppwire.dts";
	uiName = "R: Copper Roll";
	resourceTitle = "Copper Wire";
	resourceName = "copwire";
	resourceSound = rsrc_PickupHelmetSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_RopeItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/rope.dts";
	uiName = "R: Rope";
	resourceTitle = "Rope";
	resourceName = "rope";
	resourceSound = rsrc_PickupClothSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_MetalRopeItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/metalrope.dts";
	uiName = "R: Metal Rope";
	resourceTitle = "Metal Rope";
	resourceName = "metalrope";
	resourceSound = rsrc_PickupHelmetSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 150;
};

datablock ItemData(rsrc_GlassBottleItem : rsrc_GunCaseItem)
{
	colorShiftColor = "1.0 1.0 1.0 0.5";

	shapeFile = "./dts/bottle.dts";
	uiName = "R: Glass Bottle";
	resourceTitle = "Glass Bottle";
	resourceName = "glassbott";
	resourceSound = rsrc_PickupBottleSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_MoneyItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/money.dts";
	uiName = "R: Money";
	resourceTitle = "Money";
	resourceName = "money";
	resourceSound = rsrc_PickupPaperSound;
	resourceGive = 100;
	resourceMax = 0;
	resourceValue = 1;
	resourceGroupNode = "bill";
	resourceGroupDiv = 100;
	resourceGroupMax = 51;
};

datablock ItemData(rsrc_GoldCoinItem : rsrc_GunCaseItem)
{
	colorShiftColor = "1.0 0.9 0.1 1.0";

	shapeFile = "./dts/coins.dts";
	uiName = "R: Gold Coin";
	resourceTitle = "Gold Coin";
	resourceName = "goldcoin";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 500;
	resourceGroupNode = "coin";
	resourceGroupDiv = 1;
	resourceGroupMax = 23;
};

datablock ItemData(rsrc_GoldIngotItem : rsrc_GunCaseItem)
{
	colorShiftColor = "1.0 0.9 0.1 1.0";

	shapeFile = "./dts/ingots.dts";
	uiName = "R: Gold Ingot";
	resourceTitle = "Gold Ingot";
	resourceName = "goldingot";
	resourceGroupFrom = "goldingotpile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 1500;
	resourceGroupNode = "ingot";
	resourceGroupDiv = 1;
	resourceGroupMax = 13;
};

datablock ItemData(rsrc_PaperItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/paper.dts";
	uiName = "R: Paper";
	resourceTitle = "Paper";
	resourceName = "paper";
	resourceSound = rsrc_PickupPaperSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_BatteryItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/battery.dts";
	uiName = "R: Battery";
	resourceTitle = "Battery";
	resourceName = "battery";
	resourceSound = rsrc_PickupHelmetSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_PowerCellItem : rsrc_GunCaseItem)
{
	colorShiftColor = "0.1 1.0 0.1 1.0";

	shapeFile = "./dts/powercell.dts";
	uiName = "R: Power Cell";
	resourceTitle = "Power Cell";
	resourceName = "powercell";
	resourceSound = rsrc_PickupSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 300;
};

datablock ItemData(rsrc_PistolCasingItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/casingsPistol.dts";
	uiName = "R: Pistol Casings";
	resourceTitle = "Pistol Casings";
	resourceName = "pistolshell";
	resourceSound = rsrc_PickupSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_RifleCasingItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/casingsRifle.dts";
	uiName = "R: Rifle Casings";
	resourceTitle = "Rifle Casings";
	resourceName = "rifleshell";
	resourceSound = rsrc_PickupSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_ShotgunCasingsItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/casingsShotgun.dts";
	uiName = "R: Shotgun Casings";
	resourceTitle = "Shotgun Casings";
	resourceName = "shotgunshell";
	resourceSound = rsrc_PickupSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

datablock ItemData(rsrc_EmeraldItem : rsrc_GunCaseItem)
{
	colorShiftColor = "0.1 1.0 0.1 1.0";

	shapeFile = "./dts/emeralds.dts";
	uiName = "R: Emerald";
	resourceTitle = "Emerald";
	resourceName = "emerald";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 2500;
	resourceGroupNode = "gem";
	resourceGroupDiv = 1;
	resourceGroupMax = 10;
};

datablock ItemData(rsrc_RubyItem : rsrc_GunCaseItem)
{
	colorShiftColor = "1.0 0.0 0.05 1.0";

	shapeFile = "./dts/ruby.dts";
	uiName = "R: Ruby";
	resourceTitle = "Ruby";
	resourceName = "ruby";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 4000;
	resourceGroupNode = "gem";
	resourceGroupDiv = 1;
	resourceGroupMax = 10;
};

datablock ItemData(rsrc_DiamondItem : rsrc_GunCaseItem)
{
	colorShiftColor = "0.1 0.4 1.0 1.0";

	shapeFile = "./dts/diamonds.dts";
	uiName = "R: Diamond";
	resourceTitle = "Diamond";
	resourceName = "diamond";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 6000;
	resourceGroupNode = "gem";
	resourceGroupDiv = 1;
	resourceGroupMax = 6;
};

datablock ItemData(rsrc_SeedsItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/seeds.dts";
	uiName = "R: Seeds";
	resourceTitle = "Seeds";
	resourceName = "seeds";
	resourceSound = rsrc_PickupPaperSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 50;
};

function ItemData::onResourcePickup(%db, %item, %pl) // picked up resource item
{
	%item.updateResource();
}

function ItemData::onResourceDrop(%db, %item, %pl) // dropped resource item
{
	%item.updateResource();
}

function ItemData::onResourceGiven(%db, %amt, %pl) // gained resource
{

}

function ItemData::onResourceTaken(%db, %amt, %pl) // lost resource
{

}

function Item::updateResource(%item)
{
	if(isObject(BrickRandomItemSpawnData))
	{
		if(isObject(%item.spawnBrick) && %item.spawnBrick.getDatablock() == BrickRandomItemSpawnData.getID())
			return;
	}

	%db = %item.getDatablock();
	if(%db.isResource)
	{
		%res = %item.resources;

		if(!%res)
			%res = %db.resourceGive;
		
		%item.schedule(0, setShapeName, %res @ "x " @ %db.resourceTitle);
		
		%item.schedule(0, setShapeNameDistance, 15);
		%item.schedule(0, setShapeNameColor, "0.1 1.0 0.1");

		if(%db.resourceGroupNode !$= "" && %db.resourceGroupMax > 0)
		{
			if(%res >= %db.resourceGroupMax && %db.resourceGroupDiv <= 0 || (%res / %db.resourceGroupDiv) >= %db.resourceGroupMax && %db.resourceGroupDiv > 0)
				%item.unHideNode("ALL");
			else
			{
				%item.hideNode("ALL");

				%cts = %res;

				if(%db.resourceGroupDiv > 0)
					%cts = %res / %db.resourceGroupDiv;
				
				for(%i = 0; %i < %cts; %i++)
					%item.unHideNode(%db.resourceGroupNode @ %i);
			}
		}
	}
}

package ResourcePickupPackage
{
	function onMissionLoaded(%x)
	{
		Parent::onMissionLoaded(%x);

		rsrcMakeItemTable();
	}

	function Player::Pickup(%pl, %item)
	{
		%db = %item.getDatablock();
		if(%pl.isBody || %pl.isCorpse || %pl.getDamagePercent() >= 1.0) // without this, SMM corpses can pick up resources
			return 0;
		
		if(!%db.isResource || !%item.canPickup)
			return Parent::Pickup(%pl, %item);

		if(%pl.noResources())
			return 0;

		if(%db.resourceMax && %item.resources $= "")
			%item.resources = %db.resourceGive;
		
		if(%item.resources)
		{
			%amt = %pl.giveResourceItem(%db.resourceIdx, %item.resources, true);

			%item.setCollisionTimeout(%pl);

			if(%amt > 0)
			{
				if(!isObject(%item.spawnBrick) || !%item.static || %item.spawnBrick.item != %item)
					%item.resources -= %amt;

				if(!%item.resources)
				{
					if(isObject(%item.spawnBrick))
					{
						// %item.respawn();
						%item.fadeOut();
						%item.fadeIn(1000);
					}
					else
						%item.schedule(0, delete);
				}
				else
				{
					// %item.respawn();
					%item.fadeOut();
					%item.fadeIn(1000);
				}
			}
		}
		else
		{
			%pl.giveResourceItem(%db.resourceIdx, %db.resourceGive, true);
			if(isObject(%item.spawnBrick) && %item.static)
			{
				// %item.respawn();
				%item.fadeOut();
				%item.fadeIn(1000);
			}
			else
				%item.schedule(0, delete);
		}

		if(isObject(%item.spawnBrick))
			%item.spawnBrick.resourcePickedUp(%pl.Client);
		
		if(isFunction(%db.getName(), "onResourcePickup"))
			%db.onResourcePickup(%item, %pl);

		return 1;
	}

	function Armor::onDisabled(%db, %pl, %state)
	{
		if($Pref::Resources::DropAllOnDeath)
			%pl.dropAllResources();

		return Parent::onDisabled(%db, %pl, %state);
	}

	function Armor::onRemove(%db, %pl, %x, %y)
	{
		if($Pref::Resources::DropAllOnDeath)
			%pl.dropAllResources();
		
		return Parent::onRemove(%db, %pl, %x, %y);
	}

	function Armor::onAdd(%db, %pl, %x, %y)
	{
		%r = Parent::onAdd(%db, %pl, %x, %y);

		if(isObject(%pl))
		{
			for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
			{
				%idb = ResourceItemSet.getObject(%i);
				if(%pl.rsrcCount[%idb] $= "")
					%pl.rsrcCount[%idb] = 0;
			}
		}

		return %r;
	}

	function ItemData::onAdd(%db, %itm, %x)
	{
		%r = Parent::onAdd(%db, %itm, %x);

		if(%db.isResource)
		{
			%itm.updateResource();
			%itm.schedule(0, setNodeColor, "ALL", %db.colorShiftColor);

			if(getWord(%db.colorShiftColor, 3) < 1.0)
				%itm.schedule(0, startFade, 1, 1, 0);
		}

		return %r;
	}

	function Item::fadeIn(%item, %del)
	{
		if(%del <= 0 || %del $= "")
		{
			%db = %item.getDatablock();
			if(%db.isResource)
			{
				%item.schedule(0, setNodeColor, "ALL", %db.colorShiftColor);

				if(getWord(%db.colorShiftColor, 3) < 1.0)
					%item.schedule(0, startFade, 1, 1, 0);
			}
		}
		
		return Parent::fadeIn(%item, %del);
	}
};
activatePackage(ResourcePickupPackage);

function Player::noResources(%pl)
{
	if(%pl.rsrcDisable)
		return 1;

	return 0;
	// if(isObject(%cl = %pl.Client))
	// {
	// 	for(%i = 0; %i < getFieldCount($Pref::Resources::PickupExcludeTeams); %i++)
	// 	{
	// 		%str = getField($Pref::Resources::PickupExcludeTeams, %i);
	// 		if(%cl.slyrTeam.name $= %str || %cl.team.name $= %str || %cl.team $= %str)
	// 		{
	// 			if(!$Pref::Resources::PickupExcludeInverse)
	// 				return 1;
	// 			else
	// 				return 0;
	// 		}
	// 	}
	// }

	// if(!$Pref::Resources::PickupExcludeInverse)
	// 	return 0;
	// else
	// 	return 1;
}