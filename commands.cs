function serverCmdResources(%cl)
{
	if($Pref::Resources::DisableResourceCheck)
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
	if(!$Pref::Resources::AllowManualDrop)
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