// TODO:
// more wood: sticks, logs
// maybe more coin/ingot types?
// leather
// non modern adhesive? (short stick with white gluey stuff?)

// todo: remove the resource grouping system, instead opting for showing and hiding nodes based on the dropped resource amount 
// ^ this will cut down on datablock spam a bit and would overall be much better code wise

datablock ItemData(rsrc_GunCaseItem : hammerItem)
{
	iconName = "";
	image = "";
	doColorShift = true;
	colorShiftColor = "1.0 1.0 1.0 1.0";

	isResource = true;

	shapeFile = "./dts/guncase.dts";
	uiName = "R: Gun Case";
	resourceTitle = "Gun Case";
	resourceName = "gun";
	resourceSound = rsrc_PickupSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 1000;
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
	shapeFile = "./dts/moneyBill.dts";
	uiName = "R: Money";
	resourceTitle = "Money";
	resourceName = "money";
	resourceGroupFrom = "moneybundle";
	resourceSound = rsrc_PickupPaperSound;
	resourceGive = 100;
	resourceMax = 0;
	resourceValue = 1;
};

datablock ItemData(rsrc_MoneyBundleItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/moneyBundle.dts";
	uiName = "R: Money Bundle";
	resourceTitle = "Money Bundle";
	resourceName = "moneybundle";
	resourceGroupTo = "money";
	resourceGroupFrom = "moneystack";
	resourceSound = rsrc_PickupPaperSound;
	resourceGive = 500;
	resourceMax = 0;
};

datablock ItemData(rsrc_MoneyStackItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/moneyStack.dts";
	uiName = "R: Money Stack";
	resourceTitle = "Money Stack";
	resourceName = "moneystack";
	resourceGroupTo = "moneybundle";
	resourceSound = rsrc_PickupPaperSound;
	resourceGive = 2500;
	resourceMax = 0;
};

datablock ItemData(rsrc_GoldCoinItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/goldCoin.dts";
	uiName = "R: Gold Coin";
	resourceTitle = "Gold Coin";
	resourceName = "goldcoin";
	resourceGroupFrom = "goldcoinpile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 500;
};

datablock ItemData(rsrc_GoldCoinPileItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/goldCoinPile.dts";
	uiName = "R: Gold Coin Pile";
	resourceTitle = "Gold Coin Pile";
	resourceName = "goldcoinpile";
	resourceGroupTo = "goldcoin";
	resourceGroupFrom = "goldcoinstack";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 8;
	resourceMax = 0;
};

datablock ItemData(rsrc_GoldCoinStackItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/goldCoinStack.dts";
	uiName = "R: Gold Coin Stack";
	resourceTitle = "Gold Coin Stack";
	resourceName = "goldcoinstack";
	resourceGroupTo = "goldcoinpile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 35;
	resourceMax = 0;
};

datablock ItemData(rsrc_GoldIngotItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/goldIngot.dts";
	uiName = "R: Gold Ingot";
	resourceTitle = "Gold Ingot";
	resourceName = "goldingot";
	resourceGroupFrom = "goldingotpile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 1500;
};

datablock ItemData(rsrc_GoldIngotPileItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/goldIngotPile.dts";
	uiName = "R: Gold Ingot Pile";
	resourceTitle = "Gold Ingot Pile";
	resourceName = "goldingotpile";
	resourceGroupTo = "goldingot";
	resourceGroupFrom = "goldingotstack";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 4;
	resourceMax = 0;
};

datablock ItemData(rsrc_GoldIngotStackItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/goldIngotStack.dts";
	uiName = "R: Gold Ingot Stack";
	resourceTitle = "Gold Ingot Stack";
	resourceName = "goldingotstack";
	resourceGroupTo = "goldingotpile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 12;
	resourceMax = 0;
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
	shapeFile = "./dts/emerald.dts";
	uiName = "R: Emerald";
	resourceTitle = "Emerald";
	resourceName = "emerald";
	resourceGroupFrom = "emeraldpile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 2500;
};

datablock ItemData(rsrc_EmeraldPileItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/emeraldPile.dts";
	uiName = "R: Emerald Pile";
	resourceTitle = "Emerald Pile";
	resourceName = "emeraldpile";
	resourceGroupTo = "emerald";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 5;
	resourceMax = 0;
};

datablock ItemData(rsrc_RubyItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/ruby.dts";
	uiName = "R: Ruby";
	resourceTitle = "Ruby";
	resourceName = "ruby";
	resourceGroupFrom = "rubypile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 4000;
};

datablock ItemData(rsrc_RubyPileItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/rubyPile.dts";
	uiName = "R: Ruby Pile";
	resourceTitle = "Ruby Pile";
	resourceName = "rubypile";
	resourceGroupTo = "ruby";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 4;
	resourceMax = 0;
};

datablock ItemData(rsrc_DiamondItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/diamond.dts";
	uiName = "R: Diamond";
	resourceTitle = "Diamond";
	resourceName = "diamond";
	resourceGroupFrom = "diamondpile";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 1;
	resourceMax = 0;
	resourceValue = 6000;
};

datablock ItemData(rsrc_DiamondPileItem : rsrc_GunCaseItem)
{
	shapeFile = "./dts/diamondPile.dts";
	uiName = "R: Diamond Pile";
	resourceTitle = "Diamond Pile";
	resourceName = "diamondpile";
	resourceGroupTo = "diamond";
	resourceSound = rsrc_PickupCoinsSound;
	resourceGive = 3;
	resourceMax = 0;
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

// if you fuck up resourceGroupTo/From and pick up the item it'll crash the server. fun

// resourceMax probably shouldn't be set for groupable resources
// please do note that the max resource setting is a very half baked attempt at allowing for resource caps
// not much effort was put into making it or testing it and it might not work properly

function ItemData::onResourcePickup(%db, %item, %pl) // picked up resource item
{
	%item.resourceShapeName();
}

function ItemData::onResourceDrop(%db, %item, %pl) // dropped resource item
{
	%item.resourceShapeName();
}

function ItemData::onResourceGiven(%db, %amt, %pl) // gained resource
{

}

function ItemData::onResourceTaken(%db, %amt, %pl) // lost resource
{

}

function Item::resourceShapeName(%item)
{
	if(isObject(BrickRandomItemSpawnData))
	{
		if(isObject(%item.spawnBrick) && %item.spawnBrick.getDatablock() == BrickRandomItemSpawnData.getID())
			return;
	}

	%db = %item.getDatablock();
	if(%db.isResource)
	{
		if(%item.resources)
			%item.schedule(0, setShapeName, %item.resources @ "x " @ %db.resourceTitle);
		else
			%item.schedule(0, setShapeName, %db.resourceGive @ "x " @ %db.resourceTitle);
		%item.schedule(0, setShapeNameDistance, 15);
		%item.schedule(0, setShapeNameColor, "0.1 1.0 0.1");
	}
}

package ResourcePickupPackage
{
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
						%item.respawn();
					else
						%item.schedule(0, delete);
				}
				else
					%item.respawn();
			}
		}
		else
		{
			%pl.giveResourceItem(%db.resourceIdx, %db.resourceGive, true);
			if(isObject(%item.spawnBrick) && %item.static)
				%item.respawn();
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
		if($RSRC::DropAllOnDeath)
			%pl.dropAllResources();

		return Parent::onDisabled(%db, %pl, %state);
	}

	function Armor::onRemove(%db, %pl, %x, %y)
	{
		if($RSRC::DropAllOnDeath)
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
			%itm.resourceShapeName();
			%itm.schedule(0, setNodeColor, "ALL", %db.colorShiftColor);
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
				%item.schedule(0, startFade, 1, 1, 0);
			}
		}
		
		return Parent::fadeIn(%item, %del);
	}

	function onExit(%a)
	{
		export("$RSRC::*", "config/server/resourcesys.cs");
		Parent::onExit(%a);
	}
};
activatePackage(ResourcePickupPackage);

function Player::noResources(%pl)
{
	if(isObject(%cl = %pl.Client))
	{
		for(%i = 0; %i < getFieldCount($RSRC::PickupExcludeTeams); %i++)
		{
			%str = getField($RSRC::PickupExcludeTeams, %i);
			if(%cl.slyrTeam.name $= %str || %cl.team.name $= %str || %cl.team $= %str)
			{
				if(!$RSRC::PickupExcludeInverse)
					return 1;
				else
					return 0;
			}
		}
	}
	else if(%pl.rsrcDisable)
		return 1;

	if(!$RSRC::PickupExcludeInverse)
		return 0;
	else
		return 1;
}