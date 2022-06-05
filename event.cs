registerOutputEvent("Player", "dropAllResources", "bool");
registerOutputEvent("Bot", "dropAllResources", "");

registerOutputEvent("Player", "giveRandomResourceItem", "string 200 200\tint 1 999999 1\tint 1 999999 1\tbool");
registerOutputEvent("Bot", "giveRandomResourceItem", "string 200 200\tint 1 999999 1\tint 1 999999 1");
registerOutputEvent("fxDtsBrick", "setRandomResourceItem", "string 200 200\tint 1 999999 1\tint 1 999999 1");
registerOutputEvent("fxDtsBrick", "spawnRandomResourceItem", "string 200 200\tvector\tint 1 999999 1\tint 1 999999 1");

registerInputEvent("fxDtsBrick", "onResourcePickup", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");

registerOutputEvent("fxDtsBrick", "attemptCraftCheck", "string 200 200\tbool", 1);
registerInputEvent("fxDtsBrick", "onCraftCheckSuccess", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
registerInputEvent("fxDtsBrick", "onCraftCheckFail", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");

registerOutputEvent("fxDtsBrick", "attemptQuickCraft", "string 200 200\tbool\tbool", 1);
registerInputEvent("fxDtsBrick", "onQuickCraftSuccess", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
registerInputEvent("fxDtsBrick", "onQuickCraftFail", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");

registerOutputEvent("fxDtsBrick", "attemptInventoryCheck", "int 1 16 1", 1);
registerInputEvent("fxDtsBrick", "onInventoryCheckSuccess", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
registerInputEvent("fxDtsBrick", "onInventoryCheckFail", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");

registerOutputEvent("fxDtsBrick", "makeResourceStorage", "list Personal 0 Global 1 None 2" TAB "string 200 200" TAB "string 24 50", 1);
registerOutputEvent("fxDtsBrick", "clearResourceStorage", "", 1);
registerOutputEvent("fxDtsBrick", "openResourceStorage", "", 1);
registerOutputEvent("fxDtsBrick", "exportResourceStorage", "string 200 200", 1);
registerOutputEvent("fxDtsBrick", "importResourceStorage", "string 200 200", 1);
registerInputEvent("fxDtsBrick", "onResourceStored", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
registerInputEvent("fxDtsBrick", "onResourceTaken", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
registerInputEvent("fxDtsBrick", "onStorageOpened", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");
registerInputEvent("fxDtsBrick", "onStorageClosed", "Self fxDtsBrick" TAB "Player Player" TAB "Client GameConnection" TAB "Minigame Minigame");

if(isFile($f = "Add-Ons/Script_EventDescriptions/server.cs"))
	exec($f);

$OutputDescription_["fxDtsBrick", "makeResourceStorage"] = "[type] [filter] [title]" NL
																													 "Sets this brick up into a resource storage brick." NL
																													 "Can open storage by using the 'openResourceStorage' output event." NL
																													 "type:" NL
																													 "  Personal: separate storage per player, can have multiple users at once" NL
																													 "  Global: single server-wide storage, can only have one user at a time" NL
																													 "  None: removes storage" NL
																													 "filter: List of storable resources and limits (e.g. 'scrap 14, wood 4', 0 is infinite)" NL
																													 "title: Custom center-print title, defaults to 'Resource Storage'";

$OutputDescription_["fxDtsBrick", "clearResourceStorage"] = "Empties this brick's resource storage.";

$OutputDescription_["fxDtsBrick", "openResourceStorage"] = "Opens this brick's resource storage for the triggering player." NL
																													 "Must first be set up using the 'makeResourceStorage' output event.";

$OutputDescription_["fxDtsBrick", "exportResourceStorage"] = "[name]" NL
																														 "Saves a backup of this brick's resource storage to a file." NL
																														 "name (200): Save file name";

$OutputDescription_["fxDtsBrick", "importResourceStorage"] = "[name]" NL
																														 "Loads a previously saved resource storage backup." NL
																														 "name (200): Save file name";
// RESOURCE STORAGE vv

function fxDTSBrick::exportResourceStorage(%brk, %id)
{
	if((%path = trim(fileBase(%id))) $= "")
		return;
	
	%file = new FileObject();
	if(%file.openForWrite("config/server/resourceStorage/" @ %path @ ".rsrc"))
	{
		%file.writeLine("USERCOUNT " @ %brk.rsrcUsers);
		for(%i = 0; %i < %brk.rsrcUsers; %i++)
			%file.writeLine("USER " @ %brk.rsrcUser[%i]);
		
		for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
		{
			%res = ResourceItemSet.getObject(%i);

			%file.writeLine("RSRC GLOBAL " @ %res.getName() SPC %brk.rsrcCount[%res.getID()]);

			for(%o = 0; %o < %brk.rsrcUsers; %o++)
			{
				%file.writeLine("RSRC PERSONAL " @ %res.getName() SPC %brk.rsrcCount[%res.getID(), %brk.rsrcUser[%o]] SPC %brk.rsrcUser[%o]);
			}
		}

		echo("Exported resources with name " @ %path @ " from brick " @ %brk);
	}
	else
	{
		error("fxDTSBrick::exportResourceStorage() - Failed to export with name " @ %path);
	}

	%file.close();
	%file.delete();
}

function fxDTSBrick::importResourceStorage(%brk, %id)
{
	if((%path = trim(fileBase(%id))) $= "" || !isFile("config/server/resourceStorage/" @ %path @ ".rsrc"))
		return;

	if(%brk.storageInUse)
	{
		for(%i = 0; %i < clientGroup.getCount(); %i++)
		{
			%cc = clientGroup.getObject(%i);
			if(%cc.brickShiftMenu.class $= "ResourceStorageBSM" && %cc.brickShiftMenu.sourceBrick == %brk)
			{
				warn("fxDTSBrick::importResourceStorage() - Client " @ %cc @ " currently inspecting resource storage from brick " @ %brk @ "...");
				%cc.brickShiftMenuEnd();
			}
		}
	}
	
	%file = new FileObject();
	%cts = 0;
	if(%file.openForRead("config/server/resourceStorage/" @ %path @ ".rsrc"))
	{
		while(!%file.isEOF())
		{
			%str = trim(%file.readLine());
			%first = firstWord(%str);
			%rest = restWords(%str);
			%second = firstWord(%rest);
			%restAlt = restWords(%rest);

			if(%first $= "USERCOUNT")
				%brk.rsrcUsers = %rest;
			else if(%first $= "USER")
			{
				%brk.rsrcUser[%cts] = %rest;
				%cts++;
			}
			else if(%first $= "RSRC")
			{
				%res = firstWord(%restAlt).getID();

				if(!isObject(%res))
				{
					warn("fxDTSBrick::importResourceStorage() - Couldn't find resource with name " @ firstWord(%restAlt));
					continue;
				}

				if(%second $= "GLOBAL")
				{
					%count = getWord(%restAlt, 1);
					%brk.rsrcCount[%res] = %count;
				}
				else if(%second $= "PERSONAL")
				{
					%count = getWord(%restAlt, 1);
					%source = getWord(%restAlt, 2);
					%brk.rsrcCount[%res, %source] = %count;
				}
			}
		}

		echo("Imported resources from name " @ %path @ " to brick " @ %brk);
	}
	else
	{
		error("fxDTSBrick::importResourceStorage() - Failed to import from name " @ %path);
	}

	%file.close();
	%file.delete();
}

function fxDTSBrick::makeResourceStorage(%brk, %type, %caps, %name)
{
	if(%type != 2)
	{
		%brk.resourceStore = true;
		%brk.resourcePersonal = !%type;
		%brk.resourceCaps = %caps;
		%brk.resourceTitle = %name;

		for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
		{
			%res = ResourceItemSet.getObject(%i);
			if(%brk.rsrcCount[%res.getId()] $= "")
				%brk.rsrcCount[%res.getID()] = 0;
			for(%o = 0; %o < ClientGroup.getCount(); %o++)
			{
				%cc = ClientGroup.getObject(%o);
				if(%brk.rsrcCount[%res.getID(), %cc.getBLID()] $= "")
					%brk.rsrcCount[%res.getID(), %cc.getBLID()] = 0;
			}
		}

		// %brk.shareResourceStorage(%share);
	}
	else
	{
		%brk.resourceStore = false;
		%brk.resourcePersonal = "";
		%brk.resourceCaps = "";
		%brk.resourceTitle = "";
	}
}

function fxDTSBrick::clearResourceStorage(%brk)
{
	for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
	{
		%res = ResourceItemSet.getObject(%i);

		%brk.rsrcCount[%res.getID()] = 0;

		for(%o = 0; %o < %brk.rsrcUsers; %o++)
		{
			%brk.rsrcCount[%res.getID(), %brk.rsrcUser[%o]] = 0;
		}
	}
}

// function fxDTSBrick::shareResourceStorage(%brk, %target)
// {
// 	if(!%brk.resourceStore)
// 		return;
	
// 	%gr = %brk.getGroup();
// 	for(%i = 0; %i < %gr.getCount(); %i++)
// 	{
// 		%brick = %gr.getObject(%i);
// 		if(!%brick.resourceStore)
// 			continue;
		
// 		if(%brick.getID() == %brk.getID())
// 			continue;
		
// 		if(%brick.getName() !$= ("_" @ %target))
// 			continue;

// 		for(%u = 0; %u < ResourceItemSet.getCount(); %u++)
// 		{
// 			%res = ResourceItemSet.getObject(%u);
// 			%rid = %res.getID();

// 			%brick.rsrcCount[%rid] = %brk.rsrcCount[%rid];

// 			for(%o = 0; %o < %brk.rsrcUsers; %o++)
// 			{
// 				%brick.rsrcCount[%rid, %brk.rsrcUser[%o]] = %brk.rsrcCount[%rid, %brk.rsrcUser[%o]];
// 				%brick.rsrcUser[%o] = %brk.rsrcUser[%o];
// 				%brick.rsrcUsers = %brk.rsrcUsers;
// 				%brick.resourceShare = %brk.resourceShare;
// 				%brick.resourcePersonal = %brk.resourcePersonal;
// 				%brick.resourceCaps = %brk.resourceCaps;
// 			}
// 		}
// 	}
// }

function fxDTSBrick::modResourceInStorage(%brk, %cl, %res, %amt)
{
	if(!%brk.resourceStore)
		return;
	
	%res = %res.getID();

	$inputTarget_Client = %cl;
	$inputTarget_Minigame = %cl.minigame;
	$inputTarget_Player = %cl.Player;
	$inputTarget_Self = %brk;

	// if(%brk.resourceShare !$= "")
	// {
	// 	%brk.shareResourceStorage(%brk.resourceShare);
	// }

	if((%cap = %brk.getResourceCap(%res)) > 0)
	{			
		if(%brk.resourcePersonal)
		{
			%cts = %brk.rsrcCount[%res, %cl.getBLID()];

			if(%cts + %amt > %cap)
				%amt = %cap - %cts;
			else if(%cts + %amt < 0 && %cts > 0)
				%amt = %cts * -1;
			else if(%cts + %amt < 0 && %cts <= 0)
				return 0;
			
			%brk.rsrcCount[%res, %cl.getBLID()] += %amt;
		}
		else
		{
			%cts = %brk.rsrcCount[%res];

			if(%cts + %amt > %cap)
				%amt = %cap - %cts;
			else if(%cts + %amt < 0 && %cts > 0)
				%amt = %cts * -1;
			else if(%cts + %amt < 0 && %cts <= 0)
				return 0;
			
			%brk.rsrcCount[%res] += %amt;
		}

		if(%amt > 0)
			%brk.processInputEvent("onResourceStored", %cl);
		else
			%brk.processInputEvent("onResourceTaken", %cl);
		
		return %amt;
	}
	else
	{
		if(%brk.resourcePersonal)
		{
			%cts = %brk.rsrcCount[%res, %cl.getBLID()];

			if(%cts + %amt < 0 && %cts > 0)
				%amt = %cts * -1;
			else if(%cts + %amt < 0 && %cts <= 0)
				return 0;
			
			%brk.rsrcCount[%res, %cl.getBLID()] += %amt;
		}
		else
		{
			%cts = %brk.rsrcCount[%res];

			if(%cts + %amt < 0 && %cts > 0)
				%amt = %cts * -1;
			else if(%cts + %amt < 0 && %cts <= 0)
				return 0;
			
			%brk.rsrcCount[%res] += %amt;
		}

		if(%amt > 0)
			%brk.processInputEvent("onResourceStored", %cl);
		else
			%brk.processInputEvent("onResourceTaken", %cl);
		return %amt;
	}

	return 0;
}

function fxDTSBrick::getStorageCap(%brk)
{
	if(!%brk.resourceStore)
		return;

	%cap = trim(%brk.resourceCaps);

	if(%cap $= "")
		return;
	
	%fields = trim(strReplace(%cap, ",", "\t"));
	for(%i = 0; %i < getFieldCount(%fields); %i++)
	{
		%str = trim(getField(%fields, %i));
		%itm = firstWord(%str);

		if(!isObject(%res = getResourceFromName(%itm)) && !isObject(%res = getResourceFromIdx(%itm)))
			continue;

		if(isObject(getResourceFromName(%itm.resourceGroupTo)))
			continue;
		
		%amt = restWords(%str);
		%amt = mClamp(%amt, 0, 1000000);

		%full = %full TAB %res.getID() SPC %amt;
	}

	return trim(%full);
}

function fxDTSBrick::getStorageResources(%brk)
{
	%fields = %brk.getStorageCap();

	if(%fields $= "")
		return;
	
	for(%i = 0; %i < getFieldCount(%fields); %i++)
	{
		%str = getField(%fields, %i);
		%full = %full TAB firstWord(%str);
	}

	return trim(%full);
}

function fxDTSBrick::getResourceCap(%brk, %res)
{
	%fields = %brk.getStorageCap();

	if(%fields $= "")
		return 0;
	
	for(%i = 0; %i < getFieldCount(%fields); %i++)
	{
		%str = getField(%fields, %i);
		if((%found = firstWord(%str)).getID() == %res.getID())
			return restWords(%str);
	}

	return -1;
}

function fxDTSBrick::openResourceStorage(%brk, %cl)
{
	if(!%brk.resourceStore)
		return;
	
	if(!isObject(%pl = %cl.Player) || !isObject(%cl) || %pl.noResources())
		return;
	
	if(%brk.storageInUse && !%brk.resourcePersonal || %cl.brickShiftMenu.sourceBrick == %brk)
	{
		messageClient(%cl, '', "\c5Storage in use...");
		return;
	}
	
	%title = (%brk.resourcePersonal ? "Personal " : "") @ "Resource Storage";

	if((%alt = trim(stripMLControlChars(%brk.resourceTitle))) !$= "")
		%title = %alt;

	%bsm = new ScriptObject()
	{
		superClass = "BSMObject";
		class = "ResourceStorageBSM";
		
		hideOnDeath = true;

		title = "<font:arial bold:18>\c2" @ %title;
		format = "tahoma:16" TAB "\c2" TAB "<div:1>\c6" TAB "<div:1>\c2" TAB "\c7";

		disableSelect = true;

		sourceBrick = %brk;

		sourceVector = %pl.getEyeVector();
		sourcePoint = %pl.getHackPosition();
	};

	MissionCleanup.add(%bsm);

	%cl.brickShiftMenuEnd();
	%cl.brickShiftMenuStart(%bsm);

	if(getSimTime() - %cl.lastStorageOpenTime > 20000)
	{
		%cl.lastStorageOpenTime = getSimTime();
		messageClient(%cl, '', "\c5Use the brick shift keys to navigate the storage menu.");
		messageClient(%cl, '', "\c5Shift left to take resources, shift right to store resources and plant to confirm.");
		messageClient(%cl, '', "\c5You can also rotate clockwise to take all stored resources, or counter clockwise to store all your resources.");
	}

	if(%brk.rsrcUsers $= "")
		%brk.rsrcUsers = 0;

	for(%i = 0; %i < %brk.rsrcUsers; %i++)
	{
		%user = %brk.rsrcUser[%i];
		if(%user != %cl.getBLID())
			continue;
		else
		{
			%noted = true;
			break;
		}
	}

	if(!%noted)
	{
		%brk.rsrcUser[%brk.rsrcUsers] = %cl.getBLID();
		%brk.rsrcUsers++;
	}
}

$storewarn = true;

function ResourceStorageBSM::onUserMove(%obj, %cl, %id, %move, %val)
{
	if(!isObject(%pl = %cl.Player) || !isObject(%brk = %obj.sourceBrick))
		return Parent::onUserMove(%obj, %cl, %id, %move, %val);

	if(!isObject(%res = firstWord(%id)))
	{
		if(%res $= "confirmAll")
		{
			if(%move == $BSM::PLT)
			{
				%fields = %obj.confirmList;
				for(%i = 0; %i < getFieldCount(%fields); %i++)
				{
					%str = getField(%fields, %i);

					%res = firstWord(%str);
					%amt = %obj.resourcePick[%res];

					if(%amt == 0)
						continue;
					
					if(%amt > 0)
						%cts = %pl.takeResourceItem(%res.resourceIdx, mAbs(%amt), $storewarn);
					else
						%cts = %pl.giveResourceItem(%res.resourceIdx, mAbs(%amt), $storewarn) * -1;

					%brk.modResourceInStorage(%cl, %res, %cts);
					
					%obj.resourcePick[%res] = 0;
				}
				%obj.updateHighlight();
				return;
			}
			else if(%move == $BSM::ROT)
			{
				for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
				{
					%res = ResourceItemSet.getObject(%i).getID();

					if(isObject(getResourceFromName(%res.resourceGroupTo)))
						continue;
					
					if(%brk.getResourceCap(%res) < 0)
						continue;
					
					if(%val > 0)
					{
						if(%obj.resourcePick[%res] < 0)
							%obj.resourcePick[%res] = 0;
						else
							%obj.resourcePick[%res] = %pl.rsrcCount[%res];
					}
					else
					{
						if(%obj.resourcePick[%res] > 0)
							%obj.resourcePick[%res] = 0;
						else
						{
							if(%brk.resourcePersonal)
								%obj.resourcePick[%res] = %brk.rsrcCount[%res, %cl.getBLID()];
							else
								%obj.resourcePick[%res] = %brk.rsrcCount[%res];
						}

						%obj.resourcePick[%res] *= -1;
					}

					%obj.resourcePick[%res] = %obj.clampSelection(%cl, %res, %obj.resourcePick[%res]);
					
					if(%obj.resourcePick[%res] != 0)
						%obj.confirmList = addTabbedItemToList(%obj.confirmList, %res);
					else
						%obj.confirmList = removeTabbedItemFromList(%obj.confirmList, %res);
				}

				%obj.updateHighlight();
				return;
			}
		}
		else
			return Parent::onUserMove(%obj, %cl, %id, %move, %val);
	}
	
	%amt = restWords(%id);

	if(%move == $BSM::MOV)
	{
		if((%side = mClamp(firstWord(%val), -1, 1)) != 0)
		{
			%amt = %obj.resourcePick[%res] + %side;

			%amt = %obj.clampSelection(%cl, %res, %amt);

			%obj.resourcePick[%res] = %amt;

			if(%obj.resourcePick[%res] != 0)
				%obj.confirmList = addTabbedItemToList(%obj.confirmList, %res);
			else
				%obj.confirmList = removeTabbedItemFromList(%obj.confirmList, %res);
			
			%obj.updateHighlight();
			return;
		}
	}
	else if(%move == $BSM::ROT)
	{
		if(%val > 0)
		{
			if(%obj.resourcePick[%res] < 0)
				%obj.resourcePick[%res] = 0;
			else
				%obj.resourcePick[%res] = %pl.rsrcCount[%res];
		}
		else
		{
			if(%obj.resourcePick[%res] > 0)
				%obj.resourcePick[%res] = 0;
			else
			{
				if(%brk.resourcePersonal)
					%obj.resourcePick[%res] = %brk.rsrcCount[%res, %cl.getBLID()];
				else
					%obj.resourcePick[%res] = %brk.rsrcCount[%res];
			}

			%obj.resourcePick[%res] *= -1;
		}

		%obj.resourcePick[%res] = %obj.clampSelection(%cl, %res, %obj.resourcePick[%res]);
		
		if(%obj.resourcePick[%res] != 0)
			%obj.confirmList = addTabbedItemToList(%obj.confirmList, %res);
		else
			%obj.confirmList = removeTabbedItemFromList(%obj.confirmList, %res);

		%obj.updateHighlight();
		return;
	}
	else if(%move == $BSM::PLT)
	{
		if((%amt = %obj.resourcePick[%res]) != 0)
		{
			if(%amt > 0)
				%cts = %pl.takeResourceItem(%res.resourceIdx, mAbs(%amt), $storewarn);
			else
				%cts = %pl.giveResourceItem(%res.resourceIdx, mAbs(%amt), $storewarn) * -1;

			%brk.modResourceInStorage(%cl, %res, %cts);
			%obj.resourcePick[%res] = 0;
			%obj.updateHighlight();
		}
		return;
	}

	Parent::onUserMove(%obj, %cl, %id, %move, %val);
}

function ResourceStorageBSM::clampSelection(%obj, %cl, %res, %amt)
{
	%brk = %obj.sourceBrick;
	%cap = %brk.getResourceCap(%res);

	%pl = %cl.Player;

	if(%amt > %pl.rsrcCount[%res])
		%amt = %pl.rsrcCount[%res];

	if(!%brk.resourcePersonal)
	{
		if(%brk.rsrcCount[%res] + %amt > %cap && %cap > 0 && %amt > 0)
			%amt = %cap - %brk.rsrcCount[%res];

		if(mAbs(%amt) > %brk.rsrcCount[%res] && %amt < 0)
			%amt = %brk.rsrcCount[%res] * -1;
	}
	else
	{
		if(%brk.rsrcCount[%res, %cl.getBLID()] + %amt > %cap && %cap > 0 && %amt > 0)
			%amt = %cap - %brk.rsrcCount[%res, %cl.getBLID()];

		if(mAbs(%amt) > %brk.rsrcCount[%res, %cl.getBLID()] && %amt < 0)
			%amt = %brk.rsrcCount[%res, %cl.getBLID()] * -1;
	}

	return %amt;
}

function ResourceStorageBSM::updateHighlight(%obj)
{
	for(%i = 0; %i < %obj.entryCount; %i++)
	{
		%ent = %obj.entry[%i];
		%id = getField(%ent, 1);

		%obj.highlight[%id] = %obj.resourcePick[firstWord(%id)] != 0;
	}
}

function ResourceStorageBSM::onUserLoop(%obj, %cl)
{
	if(!isObject(%brk = %obj.sourceBrick) || !%brk.resourceStore)
	{
		%cl.brickShiftMenuEnd();
		return;
	}

	%pl = %cl.Player;
	%dist = vectorDist(%pl.getHackPosition(), %obj.sourcePoint);
	%dot = vectorDot(%pl.getEyeVector(), %obj.sourceVector);

	if(%dot < 0.4 || %dist > 1)
	{
		%cl.brickShiftMenuEnd();
		return;
	}

	%obj.entry[0] = "Close Storage" TAB "closeMenu";
	%obj.entry[1] = "Confirm All" TAB "confirmAll";
	%obj.entryCount = 2;

	if((%cap = %brk.getStorageCap()) !$= "")
	{
		%fields = getFieldCount(%cap);

		if(%fields == 1) %obj.entryCount = 1;
		
		for(%i = 0; %i < %fields; %i++)
		{
			%str = getField(%cap, %i);
			%res = firstWord(%str);
			%max = restWords(%str);
			if(%obj.resourcePick[%res] $= "")
				%obj.resourcePick[%res] = 0;

			%cts = %obj.entryCount;

			if(%brk.resourcePersonal)
			{
				%obj.entry[%cts] = %res.resourceTitle @ " [" @ %brk.rsrcCount[%res, %cl.getBLID()] @ (%max > 0 ? "/" @ %max : "") @ "]";
			}
			else
			{
				%obj.entry[%cts] = %res.resourceTitle @ " [" @ %brk.rsrcCount[%res] @ (%max > 0 ? "/" @ %max : "") @ "]";
			}

			if((%pick = %obj.resourcePick[%res]) != 0)
				%obj.entry[%cts] = %obj.entry[%cts] @ (%pick > 0 ? " +" : " -") @ mAbs(%pick);
			
			%obj.entry[%cts] = %obj.entry[%cts] TAB %res SPC %max;
			
			%obj.entryCount++;
		}
	}
	else
	{
		for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
		{
			%res = ResourceItemSet.getObject(%i).getID();

			if(isObject(getResourceFromName(%res.resourceGroupTo)))
				continue;
			
			if(%obj.resourcePick[%res] $= "")
				%obj.resourcePick[%res] = 0;

			%cts = %obj.entryCount;

			if(%brk.resourcePersonal)
			{
				%obj.entry[%cts] = %res.resourceTitle @ " [" @ %brk.rsrcCount[%res, %cl.getBLID()] @ "]";
			}
			else
			{
				%obj.entry[%cts] = %res.resourceTitle @ " [" @ %brk.rsrcCount[%res] @ "]";
			}

			if((%pick = %obj.resourcePick[%res]) != 0)
				%obj.entry[%cts] = %obj.entry[%cts] SPC (%pick > 0 ? "+" : "") @ %pick;

			%obj.entry[%cts] = %obj.entry[%cts] TAB %res;

			%obj.entryCount++;
		}
	}

	Parent::onUserLoop(%obj, %cl);
}

function ResourceStorageBSM::onUserStart(%obj, %cl)
{
	if(isObject(%brk = %obj.sourceBrick))
	{
		$inputTarget_Client = %cl;
		$inputTarget_Minigame = %cl.minigame;
		$inputTarget_Player = %cl.Player;
		$inputTarget_Self = %brk;

		%brk.processInputEvent("onStorageOpened", %cl);

		%brk.storageInUse = true;
	}
	
	Parent::onUserStart(%obj, %cl);
}

function ResourceStorageBSM::onUserEnd(%obj, %cl)
{
	if(!isObject(%obj.parent))
		%obj.schedule(0, delete);
	
	if(isObject(%brk = %obj.sourceBrick))
	{
		$inputTarget_Client = %cl;
		$inputTarget_Minigame = %cl.minigame;
		$inputTarget_Player = %cl.Player;
		$inputTarget_Self = %brk;

		%brk.processInputEvent("onStorageClosed", %cl);

		%brk.storageInUse = false;
	}
	
	Parent::onUserEnd(%obj, %cl);
}

// RESOURCE STORAGE ^^

function Player::giveRandomResourceItem(%pl, %str, %min, %max, %tell)
{
	if(%min >= %max)
		%val = %max;
	else
		%val = getRandom(%min, %max);
	
	if(!strLen(%str))
		return;
	
	%idx = getWord(%str, getRandom(0, getWordCount(%str)-1));

	if(isObject(%idb = getResourceFromName(%idx)))
		%idx = %idb.resourceIdx;

	%pl.giveResourceItem(%idx, %val, %tell);
}

function fxDTSBrick::setRandomResourceItem(%brk, %str, %min, %max)
{
	if(%min >= %max)
		%val = %max;
	else
		%val = getRandom(%min, %max);
	
	if(!strLen(%str))
		return;
	
	%idx = getWord(%str, getRandom(0, getWordCount(%str)-1));

	if(isObject(%idb = getResourceFromName(%idx)))
		%idx = %idb.resourceIdx;
	
	%idb = getResourceFromIdx(%idx);

	if(isObject(%idb))
	{
		%brk.setItem(%idb);
		%brk.item.resources = %val;
		%brk.item.resourceShapeName();
	}
}

function fxDTSBrick::setResourceItem(%brk, %idx, %val)
{
	%idb = getResourceFromIdx(%idx);

	if(isObject(%idb))
	{
		%brk.setItem(%idb);
		%brk.item.resources = %val;
		%brk.item.resourceShapeName();
	}
}

function fxDTSBrick::spawnRandomResourceItem(%brk, %str, %dir, %min, %max)
{
	if(%min >= %max)
		%val = %max;
	else
		%val = getRandom(%min, %max);
	
	if(!strLen(%str))
		return;
	
	%idx = getWord(%str, getRandom(0, getWordCount(%str)-1));

	if(isObject(%idb = getResourceFromName(%idx)))
		%idx = %idb.resourceIdx;
	
	%idb = getResourceFromIdx(%idx);

	if(isObject(%idb))
	{
		%itm = new Item()
		{
			dataBlock = %idb;
			resources = %val;
			position = %brk.getPosition();
			canPickup = true;
			static = false;
			minigame = getMinigameFromObject(%brk);
			bl_id = %brk.getGroup().bl_id;
		};

		%itm.setCollisionTimeout(%brk); // I don't know if this actually does anything when called on a brick
		%itm.setVelocity(%dir);
		%itm.schedulePop();
	}
}

function Player::giveResourceItem(%pl, %idx, %amt, %tell)
{
	if(!%idx)
		%db = ResourceItemSet.getObject(getRandom(0, ResourceItemSet.getCount()-1));
	else
		%db = ResourceItemSet.getObject(%idx-1);
	
	if(%amt > 0)
		mClamp(%amt, 1, 9999999);
	else
	{
		warn("Player::giveResourceItem() - invalid amount");
		return -1;
	}

	if(%db.resourceGroupTo !$= "" && isObject(%idb = getResourceFromName(%db.resourceGroupTo)))
	{
		return %pl.giveResourceItem(%idb.resourceIdx, %amt, %tell);
	}

	if(%db.resourceMax)
	{
		if(%pl.rsrcCount[%db] < %db.resourceMax)
		{
			if(%pl.rsrcCount[%db] + %amt <= %db.resourceMax)
				%pl.rsrcCount[%db] += %amt;
			else
			{
				%amt = %db.resourceMax - %pl.rsrcCount[%db];
				%pl.rsrcCount[%db] += %amt;
			}
		}
		else
			%amt = 0;
	}
	else
		%pl.rsrcCount[%db] += %amt;

	%pl.lastResourceTitle = %db.resourceTitle;

	if(isFunction(%db.getName(), onResourceGiven))
		%db.onResourceGiven(%amt, %pl);
	
	if(%tell && isObject(%cl = %pl.Client) && %amt > 0)
	{
		if(%amt > 0 && !$RSRC::DisablePickupSound)
			serverPlay3D(%db.resourceSound, %pl.getHackPosition());

		if(!$RSRC::DisablePickupText)
		{
			%str = "\c2+" @ %amt @ " \c6" @ %db.resourceTitle;
			messageClient(%cl, '', %str, 2);
		}
	}

	return %amt;
}

function Player::takeResourceItem(%pl, %idx, %amt, %tell)
{
	if(!%idx)
		%db = ResourceItemSet.getObject(getRandom(0, ResourceItemSet.getCount()-1));
	else
		%db = ResourceItemSet.getObject(%idx-1);
	
	if(%amt > 0)
		mClamp(%amt, 1, 9999999);
	else
	{
		warn("Player::takeResourceItem() - invalid amount");
		return -1;
	}

	if(%db.resourceGroupTo !$= "" && isObject(%idb = getResourceFromName(%db.resourceGroupTo)))
	{
		return %pl.takeResourceItem(%idb.resourceIdx, %amt, %tell);
	}

	if(%pl.rsrcCount[%db] > 0)
	{
		if(%pl.rsrcCount[%db] - %amt >= 0)
			%pl.rsrcCount[%db] -= %amt;
		else
		{
			%amt = %pl.rsrcCount[%db];
			%pl.rsrcCount[%db] = 0;
		}
	}
	else
		%amt = 0;
	
	if(isFunction(%db.getName(), onResourceTaken))
		%db.onResourceTaken(%amt, %pl);
	
	if(%tell && isObject(%cl = %pl.Client) && %amt > 0)
	{
		if(!$RSRC::DisablePickupSound)
			serverPlay3D(%db.resourceTakeSound, %pl.getHackPosition());

		if(!$RSRC::DisablePickupText)
		{
			%str = "\c0-" @ %amt @ " \c6" @ %db.resourceTitle;
			messageClient(%cl, '', %str, 2);
		}
	}

	return %amt;
}

function Player::sellResourceItem(%pl, %idx, %amt, %tell)
{
	if(%idx == 0)
	{
		for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
		{
			%pl.sellResourceItem(%i+1, %amt, %tell);
		}
	}
	else
	{
		if(!isObject(%idb = getResourceFromIdx(%idx)))
			return;
		
		%cts = %pl.getResourceCountFromIdx(%idx);

		if(%amt <= 0)
			%amt = %cts;
		
		if(%amt > %cts)
			%amt = %cts;

		if(%amt > 0)
		{
			%score = %amt * %idb.resourceValue;
			%pl.Client.incScore(%score);
			%pl.takeResourceItem(%idx, %amt, false);
			if(%tell)
			{
				%str = "\c6Sold \c2" @ %amt @ "x \c6" @ %idb.resourceTitle @ " for \c2" @ %score @ "\c6 points";
				messageClient(%pl.Client, '', %str, 2);
			}
		}
	}
}

function Player::dropAllResources(%pl, %tell)
{
	for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
	{
		%idb = ResourceItemSet.getObject(%i);
		if((%amt = %pl.rsrcCount[%idb]) > 0)
		{
			%sdb = %idb;
			while(isObject(%db = getResourceFromName(%sdb.resourceGroupFrom)))
			{
				if(%amt >= %db.resourceGive)
					%sdb = %db;
				else
					break;
				
				if(%i > 64)
					return warn("serverCmdDropRes() - stuck in resource group loop!");
				
				%i++;
			}

			%itm = new Item()
			{
				dataBlock = %sdb;
				resources = %amt;
				position = %pl.getHackPosition();
				canPickup = true;
				static = false;
				minigame = getMinigameFromObject(%pl);
				bl_id = (isObject(%pl.Client) && %pl.client.getClassName() !$= "AIPlayer" ? %pl.Client.getBLID() : -1);
			};

			%itm.setCollisionTimeout(%pl);
			%itm.setVelocity(getRandom(-6, 6) SPC getRandom(-6, 6) SPC getRandom(-6, 6));
			%itm.schedulePop();

			if(isFunction(%idb.getName(), "onResourceDrop"))
				%idb.onResourceDrop(%itm, %pl);
			
			%pl.takeResourceItem(%idb.resourceIdx, %amt, %tell);

			MissionCleanup.add(%itm);
		}
	}
}

function fxDtsBrick::resourcePickedUp(%brick, %source)
{
	$inputTarget_Self = %brick;
	$inputTarget_Player = %source.player;
	$inputTarget_Client = %source;
	$inputTarget_Minigame = getMinigameFromObject(%source);
	%brick.processInputEvent("onResourcePickup",%source);
}

function VCE_Player_setResourceCount(%pl, %amt, %idx)
{
	%pl.setResourceItem(%idx, %amt);
}

// crafting events

function fxDtsBrick::attemptQuickCraft(%brk, %idx, %tellfail, %tell, %cl)
{
	if(!strLen(%idx))
		return;
	
	%pl = %cl.Player;

	if(!isObject(%pl))
		return;
	
	%rec = trim(strReplace(%idx, ",", "\t"));
	
	$inputTarget_Player = %pl;
	$inputTarget_Client = %cl;
	$inputTarget_Minigame = getMinigameFromObject(%pl);

	%success = true;

	for(%i = 0; %i < getFieldCount(%rec); %i++)
	{
		%str = trim(getField(%rec, %i));
		%itm = trim(firstWord(%str));
		%amt = trim(restWords(%str));

		if(!isObject(%idb = getResourceFromIdx(%itm)))
		{
			if(!isObject(%idb = getResourceFromName(%itm)))
				continue;
			else
				%cts = %pl.getResourceCountFromName(%itm);
		}
		else
			%cts = %pl.getResourceCountFromIdx(%itm);
		
		if(%cts < %amt)
		{
			%success = false;
			if(!%tellfail)
				break;
			else
				messageClient(%cl, '', "\c6Missing \c2" @ %idb.resourceTitle @ " \c6(" @ %cts @ "/" @ %amt @ ")");
		}
	}

	if(%success)
	{
		for(%i = 0; %i < getFieldCount(%rec); %i++)
		{
			%str = trim(getField(%rec, %i));
			%itm = trim(firstWord(%str));
			%amt = trim(restWords(%str));

			if(!isObject(getResourceFromIdx(%itm)))
			{
				if(!isObject(%idb = getResourceFromName(%itm)))
					continue;
				else
					%itm = %idb.resourceIdx;
			}
			
			%pl.takeResourceItem(%itm, %amt, %tell);
		}

		%brk.processInputEvent("onQuickCraftSuccess",%cl);
	}
	else
		%brk.processInputEvent("onQuickCraftFail",%cl);
}

function fxDtsBrick::attemptCraftCheck(%brk, %idx, %tellfail, %cl)
{
	if(!strLen(%idx))
		return;
		
	%pl = %cl.Player;

	if(!isObject(%pl))
		return;
	
	%rec = trim(strReplace(%idx, ",", "\t"));
	
	$inputTarget_Player = %pl;
	$inputTarget_Client = %cl;
	$inputTarget_Minigame = getMinigameFromObject(%pl);

	%success = true;

	for(%i = 0; %i < getFieldCount(%rec); %i++)
	{
		%str = trim(getField(%rec, %i));
		%itm = trim(firstWord(%str));
		%amt = trim(restWords(%str));

		if(!isObject(%idb = getResourceFromIdx(%itm)))
		{
			if(!isObject(%idb = getResourceFromName(%itm)))
				continue;
			else
				%cts = %pl.getResourceCountFromIdx(%idb.resourceIdx);
		}
		else
			%cts = %pl.getResourceCountFromIdx(%itm);
		
		if(%cts < %amt)
		{
			%success = false;
			if(!%tellfail)
				break;
			else
				messageClient(%cl, '', "\c6Missing \c2" @ %idb.resourceTitle @ " \c6(" @ %cts @ "/" @ %amt @ ")");
		}
	}

	if(%success)
		%brk.processInputEvent("onCraftCheckSuccess",%cl);
	else
		%brk.processInputEvent("onCraftCheckFail",%cl);
}

function fxDtsBrick::attemptInventoryCheck(%brk, %amt, %cl)
{
	%pl = %cl.Player;

	if(!isObject(%pl))
		return;
	
	$inputTarget_Player = %pl;
	$inputTarget_Client = %cl;
	$inputTarget_Minigame = getMinigameFromObject(%pl);

	%success = false;

	%cts = 0;

	for(%i = 0; %i < %pl.getDatablock().maxTools; %i++)
	{
		if(!isObject(%pl.tool[%i]))
			%cts++;
		
		if(%cts >= %amt)
		{
			%success = true;
			break;
		}
	}

	if(%success)
		%brk.processInputEvent("onInventoryCheckSuccess",%cl);
	else
		%brk.processInputEvent("onInventoryCheckFail",%cl);
}