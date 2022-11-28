if($AddOn__Event_Variables && isFile("Add-Ons/Event_Variables/server.cs")) // only force-load vce if it's already enabled and exists
{
	if(!$AddOnLoaded__Event_Variables && forceRequiredAddon("Event_Variables") == $Error::AddOn_NotFound)
	{
		warn("Server_Resources Warning: Failed to load add-on Event_Variables!"); // this should never occur since we're already checking if vce is enabled and exists
	}
}

function quickRegisterPref(%name, %category, %pref, %type, %default)
{
	%folder = fileBase(filePath($Con::File));

	if ($RTB::Hooks::ServerControl)
		RTB_registerPref(%name, %category, %pref, %type, %folder, %default, false, false, "");
	else
		eval("if(" @ %pref @ " $= \"\") " @ %pref @ " = \"" @ %default @ "\";");
}

quickRegisterPref("Drop all on death", "Resources", "$Pref::Resources::DropAllOnDeath", "bool", 1);
quickRegisterPref("Allow manual dropping", "Resources", "$Pref::Resources::AllowManualDrop", "bool", 1);
quickRegisterPref("Disable /resources", "Resources", "$Pref::Resources::DisableResourceCheck", "bool", 0);
quickRegisterPref("Disable pickup text", "Resources", "$Pref::Resources::DisablePickupText", "bool", 0);
quickRegisterPref("Disable pickup sound", "Resources", "$Pref::Resources::DisablePickupSound", "bool", 0);

if(isFile("config/server/resourcesaves.cs"))
	exec("config/server/resourcesaves.cs");

datablock AudioProfile(rsrc_PickupSound)
{
	filename    = "./wav/pickup.wav";
	description = AudioClosest3D;
	preload = true;
};

datablock AudioProfile(rsrc_PickupAmmoSound : rsrc_PickupSound) { filename    = "./wav/pickup_ammo.wav"; };
datablock AudioProfile(rsrc_PickupBottleSound : rsrc_PickupSound) { filename    = "./wav/pickup_bottle.wav"; };
datablock AudioProfile(rsrc_PickupClothSound : rsrc_PickupSound) { filename    = "./wav/pickup_cloth.wav"; };
datablock AudioProfile(rsrc_PickupWoodSound : rsrc_PickupSound) { filename    = "./wav/pickup_wood.wav"; };
datablock AudioProfile(rsrc_PickupScrapSound : rsrc_PickupSound) { filename    = "./wav/pickup_scrap.wav"; };
datablock AudioProfile(rsrc_PickupPaperSound : rsrc_PickupSound) { filename    = "./wav/pickup_paper.wav"; };
datablock AudioProfile(rsrc_PickupCoinsSound : rsrc_PickupSound) { filename    = "./wav/pickup_coins.wav"; };
datablock AudioProfile(rsrc_PickupHelmetSound : rsrc_PickupSound) { filename    = "./wav/pickup_helmet.wav"; };

function rsrcMakeItemTable()
{
	if(isObject(ResourceItemSet))
		ResourceItemSet.delete();
	
	new SimSet(ResourceItemSet);

	%evstr = "list Random 0";
	%evstr2 = "list All 0";
	%cts = 0;

	for(%i = 0; %i < dataBlockGroup.getCount(); %i++)
	{
		%db = dataBlockGroup.getObject(%i);
		if(%db.resourceName !$= "" && %db.isResource)
		{
			ResourceItemSet.add(%db);
			%db.resourceIdx = %cts+1;
			%res = strReplace(%db.resourceName, " ", "_");
			if(%db.resourceGroupTo $= "")
			{
				%evstr = %evstr SPC %res SPC %cts+1;
				%evstr2 = %evstr2 SPC %res SPC %cts+1;
			}

			if(%registered[%res])
				warn("rsrcMakeItemTable() - resource " @ %res @ " is already registered!");

			if(isFunction("registerSpecialVar"))
			{
				registerSpecialVar("Player", "res_" @ %res, "%this.rsrcCount[" @ %db @ "]", "setResourceCount", %cts);
				registerSpecialVar("fxDtsBrick", "res_" @ %res, "%this.rsrcCount[" @ %db @ "]");
			}
			%registered[%res] = true;
			%cts++;
		}
	}

	registerOutputEvent("Player", "giveResourceItem", %evstr @ "\tint 1 999999 1\tbool");
	registerOutputEvent("Bot", "giveResourceItem", %evstr @ "\tint 1 999999 1\tbool");
	registerOutputEvent("Player", "takeResourceItem", %evstr @ "\tint 1 999999 1\tbool");
	registerOutputEvent("Bot", "takeResourceItem", %evstr @ "\tint 1 999999 1\tbool");
	registerOutputEvent("Player", "setResourceItem", %evstr @ "\tint 0 999999 1");
	registerOutputEvent("Bot", "setResourceItem", %evstr @ "\tint 0 999999 1");
	registerOutputEvent("fxDtsBrick", "setResourceItem", %evstr @ "\tint 1 999999 1");

	registerOutputEvent("Player", "sellResourceItem", %evstr2 @ "\tint 0 999999 1\tbool");

	if(isFunction("registerSpecialVar"))
	{
		registerSpecialVar("Player", "lastResource", "%this.lastResourceTitle");
		registerSpecialVar("fxDtsBrick", "storageTitle", "%this.resourceTitle");
	}
}

function Player::setResourceItem(%pl, %idx, %amt)
{
	%db = getResourceFromIdx(%idx); //ResourceItemSet.getObject(%idx);
	%pl.rsrcCount[%db] = %amt;
}

function Player::getResourceCountFromIdx(%pl, %idx)
{
	return %pl.rsrcCount[getResourceFromIdx(%idx)];
}

function Player::getResourceCountFromName(%pl, %itm)
{
	return %pl.rsrcCount[getResourceFromName(%itm)];
}

function Player::getResourceCountFromTitle(%pl, %itm)
{
	return %pl.rsrcCount[getResourceFromTitle(%itm)];
}

function getResourceFromIdx(%idx)
{
	for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
	{
		%db = ResourceItemSet.getObject(%i);

		if(%db.resourceIdx == %idx)
			return %db;
	}
	return -1;
}

function getResourceFromName(%itm)
{
	if(strStr(strLwr(%itm), "res_") >= 0)
		%itm = getSubStr(%itm, 4, strLen(%itm));
	
	for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
	{
		%db = ResourceItemSet.getObject(%i);

		if(%db.resourceName $= %itm)
			return %db;
	}
	return -1;
}

function getResourceFromTitle(%itm, %part)
{
	for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
	{
		%db = ResourceItemSet.getObject(%i);

		if(!%part && %db.resourceTitle $= %itm || %part && strstr(strlwr(%db.resourceTitle), strlwr(%itm)) >= 0)
			return %db;
	}
	return -1;
}

exec("./support_brickshiftmenu.cs");
exec("./event.cs");
exec("./items.cs");
exec("./commands.cs");

// ...

function addTabbedItemToList(%string,%item)
{
	if(hasTabbedItemOnList(%string,%item))
		return %string;

	if(%string $= "")
		return %item;
	else
		return %string TAB %item;
}

function hasTabbedItemOnList(%string,%item)
{
	for(%i=0;%i<getFieldCount(%string);%i++)
	{
		if(getField(%string,%i) $= %item)
			return 1;
	}
	return 0;
}

function removeTabbedItemFromList(%string,%item)
{
	if(!hasTabbedItemOnList(%string,%item))
		return %string;

	for(%i=0;%i<getFieldCount(%string);%i++)
	{
		if(getField(%string,%i) $= %item)
		{
			if(%i $= 0)
				return getFields(%string,1,getFieldCount(%string));
			else if(%i $= getFieldCount(%string)-1)
				return getFields(%string,0,%i-1);
			else
				return getFields(%string,0,%i-1) TAB getFields(%string,%i+1,getFieldCount(%string));
		}
	}
}

function getRecipeTitle(%idx)
{
	if(!strLen(%idx))
		return;
	
	%rec = trim(strReplace(%idx, ",", "\t"));

	for(%i = 0; %i < getFieldCount(%rec); %i++)
	{
		%str = trim(getField(%rec, %i));
		%itm = trim(firstWord(%str));
		%amt = trim(restWords(%str));

		%idb = getResourceFromIdx(%itm);
		%stf = %stf @ %amt @ "x " @ %idb.resourceTitle;

		if(%i+1 < getFieldCount(%rec))
			%stf = %stf @ ", ";
	}
	
	return %stf;
}