function serverCmdResources(%cl)
{
	if($RSRC::DisableResourceCheck)
		return;

	if(isObject(%pl = %cl.Player))
	{
		messageClient(%cl, '', "\c6Your resources:");

		%cts = 0;

		for(%i = 0; %i < ResourceItemSet.getCount(); %i++)
		{
			%idb = ResourceItemSet.getObject(%i);
			if(%pl.rsrcCount[%idb])
			{
				%str = %str @ " \c2" @ %pl.rsrcCount[%idb] @ "\c2x \c6" @ %idb.resourceTitle;

				if(%cts % 4 == 3)
				{
					messageClient(%cl, '', trim(%str));
					%str = "";
				}
				else
					%str = %str @ "  \c6| ";

				%cts++;
			}
		}

		if(%str !$= "")
		{
			if(getWord(%str, getWordCount(%str)-1) $= "\c6|")
				%str = removeWord(%str, getWordCount(%str)-1);
			
			messageClient(%cl, '', trim(%str));
		}

		if(!%cts)
			messageClient(%cl, '', "\c7No resources...");
		else
			messageClient(%cl, '', "<color:888888>Use /Drop [count] [name] to drop resources.");
	}
}

function serverCmdDropRes(%cl, %amt, %a, %b, %c, %d, %e, %f, %g, %h)
{
	if(!$RSRC::AllowManualDrop)
		return;

	%amt = mClamp(%amt, 1, 999999);
	
	if(isObject(%pl = %cl.Player))
	{
		%id = trim(%a SPC %b SPC %c SPC %d SPC %e SPC %f SPC %g SPC %h);

		%db = getResourceFromIdx(%id);
		if(!isObject(%db))
		{
			%db = getResourceFromTitle(%id, true);
			if(!isObject(%db) || %id $= "")
			{
				messageClient(%cl, '', "\c5Invalid resource");
				return messageClient(%cl, '', "\c5Usage: /DropRes [count] [name]");
			}
		}

		if(!%db.canDrop)
			return messageClient(%cl, '', "\c5You can't drop this");

		%sdb = %db;
		while(isObject(%idb = getResourceFromName(%db.resourceGroupTo)))
		{
			%amt = %amt * %idb.resourceGive;
			%db = %idb;
			
			if(%i > 64)
				return warn("serverCmdDropRes() - stuck in resource group loop!");
			
			%i++;
		}

		%max = %pl.rsrcCount[%db];

		if(!%max || %amt > %max)
			return messageClient(%cl, '', "\c5Not enough resources");
		
		while(isObject(%idb = getResourceFromName(%sdb.resourceGroupFrom)))
		{
			if(%amt >= %idb.resourceGive)
				%sdb = %idb;
			else
				break;
			
			if(%i > 64)
				return warn("serverCmdDropRes() - stuck in resource group loop!");
			
			%i++;
		}

		%pl.takeResourceItem(%db.resourceIdx, %amt, false);

		%itm = new Item()
		{
			dataBlock = %sdb;
			resources = %amt;
			position = %pl.getEyePoint();
			canPickup = true;
			static = false;
			minigame = %cl.minigame;
			bl_id = %cl.getBLID();
		};

		%itm.setCollisionTimeout(%pl);
		%itm.setVelocity(vectorScale(%pl.getEyeVector(), 15));
		%itm.schedulePop();

		if(isFunction(%db.getName(), "onResourceDrop"))
			%db.onResourceDrop(%itm, %pl);

		MissionCleanup.add(%itm);

		messageClient(%cl, '', "\c6Dropped \c2" @ %amt @ "x \c6" @ %db.resourceTitle);
	}
}

function serverCmdDrop(%cl, %amt, %a, %b, %c, %d, %e, %f, %g, %h)
{
	serverCmdDropRes(%cl, %amt, %a, %b, %c, %d, %e, %f, %g, %h);
}

function serverCmdResourceID(%cl, %a, %b, %c, %d)
{
	%str = trim(%a SPC %b SPC %c SPC %d);

	if(%str $= "")
		return messageClient(%cl, '', "\c5Invalid resource");

	if(!isObject(%db = getResourceFromTitle(%str, true)))
	{
		if(!isObject(%db = getResourceFromName(%str)))
		{
			if(!isObject(%db = getResourceFromIdx(%str)))
				return messageClient(%cl, '', "\c5Couldn't find resource " @ %str);
		}
	}

	messageClient(%cl, '', "\c2Found resource: " @ %db.resourceTitle @ " (vce res_" @ %db.resourceName @ ", id " @ %db.resourceIdx @ ")");
}

// Yes, I really am too lazy to make this work with glass prefs
function serverCmdRsys(%cl, %id, %act, %a, %b, %c, %d, %e)
{
	%str = trim(%a SPC %b SPC %c SPC %d SPC %e);

	if(!%cl.isAdmin)
		return messageClient(%cl, '', "\c5You are not an admin.");
	
	switch$(%id)
	{
		case "dropAll":
			if(%act $= "")
				return messageClient(%cl, '', "\c5Invalid value");
			
			%prev = $RSRC::DropAllOnDeath;
			$RSRC::DropAllOnDeath = (%res = mClamp(%act, 0, 1));
			
			if(%prev != %res)
				messageClient(%cl, '', "\c2" @ (%res ? "Enabled" : "Disabled") @ " drop on death");
		case "exclude":
			if(%act $= "add")
			{
				if(%str !$= "")
				{
					%t = $RSRC::PickupExcludeTeams;
					if(!hasTabbedItemOnList(%t, %str))
					{
						$RSRC::PickupExcludeTeams = addTabbedItemToList(%t,%str);
						messageClient(%cl, '', "\c2Excluding team " @ %str @ " from picking up resources");
					}
					else
						messageClient(%cl, '', "\c2This team is already excluded");
				}
				else
					messageClient(%cl, '', "\c5Invalid team name");
			}
			else if(%act $= "remove")
			{
				if(%str !$= "")
				{
					%t = $RSRC::PickupExcludeTeams;
					if(hasTabbedItemOnList(%t, %str))
					{
						$RSRC::PickupExcludeTeams = removeTabbedItemFromList(%t,%str);
						messageClient(%cl, '', "\c2Un-excluded team " @ %str @ " from picking up resources");
					}
					else
						messageClient(%cl, '', "\c2This team is not excluded");
				}
				else
					messageClient(%cl, '', "\c5Invalid team name");
			}
			else if(%act $= "list")
			{
				%t = $RSRC::PickupExcludeTeams;
				for(%i = 0; %i < getFieldCount(%t); %i++)
				{
					%str = trim(getField(%t, %i));
					messageClient(%cl, '', "\c2" @ %str);
				}
			}
			else
			{
				messageClient(%cl, '', "\c5Invalid action");
				messageClient(%cl, '', "\c5Usage: /rsys exclude [add | remove | list]");
			}
		case "excludeInverse":
			if(%act $= "")
				return messageClient(%cl, '', "\c5Invalid value");
			
			%prev = $RSRC::PickupExcludeInverse;
			$RSRC::PickupExcludeInverse = (%res = mClamp(%act, 0, 1));
			
			if(%prev != %res)
				messageClient(%cl, '', "\c2" @ (%res ? "Inverted" : "Reverted") @ " exclude list");
		case "manualDrop":
			if(%act $= "")
				return messageClient(%cl, '', "\c5Invalid value");
			
			%prev = $RSRC::AllowManualDrop;
			$RSRC::AllowManualDrop = (%res = mClamp(%act, 0, 1));
			
			if(%prev != %res)
				messageClient(%cl, '', "\c2" @ (%res ? "Enabled" : "Disabled") @ " manual resource dropping");
		case "noCheck":
			if(%act $= "")
				return messageClient(%cl, '', "\c5Invalid value");
			
			%prev = $RSRC::DisableResourceCheck;
			$RSRC::DisableResourceCheck = (%res = mClamp(%act, 0, 1));
			
			if(%prev != %res)
				messageClient(%cl, '', "\c2" @ (%res ? "Disabled" : "Enabled") @ " resources command");
		case "noPickupText":
			if(%act $= "")
				return messageClient(%cl, '', "\c5Invalid value");
			
			%prev = $RSRC::DisablePickupText;
			$RSRC::DisablePickupText = (%res = mClamp(%act, 0, 1));
			
			if(%prev != %res)
				messageClient(%cl, '', "\c2" @ (%res ? "Disabled" : "Enabled") @ " pickup text");
		case "noPickupSound":
			if(%act $= "")
				return messageClient(%cl, '', "\c5Invalid value");
			
			%prev = $RSRC::DisablePickupSound;
			$RSRC::DisablePickupSound = (%res = mClamp(%act, 0, 1));
			
			if(%prev != %res)
				messageClient(%cl, '', "\c2" @ (%res ? "Disabled" : "Enabled") @ " pickup sound");
		default:
			messageClient(%cl, '', "\c5Invalid command");
			messageClient(%cl, '', "\c5Usage: /rsys [setting] [value]");
			messageClient(%cl, '', "\c5Valid settings: dropAll, exclude, excludeInverse, manualDrop, noCheck, noPickupText, noPickupSound");
	}

	export("$RSRC::*", "config/server/resourcesys.cs");
}