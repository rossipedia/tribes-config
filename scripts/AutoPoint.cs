// AutoPoint.cs
// Automatically sets a waypoint to the friendly capper

function AutoPoint::onFlagGrabbed(%name,%team)
{
	%client = getClientByName(%name);
	if(%client)
	{
		echo("================================");
		echo("Flag Grab (Team " @ %team @ ")");
		echo("Client: " @ %client);
		echo("Name: " @ %name);
		echo("Team: " @ $Team::Current[%client]);
		echo("My Client: " @ getManagerId());
		echo("My Name: " @ Client::getName(getManagerId()));
		echo("My Team: " @ $MyTeam);
		echo("================================");

		if($Team::Current[%client] != $MyTeam)
		{
			// Grabbed my flag
			if($AutoPoint::EscortingFriendly)
				return;
			%cmd = "Attack " @ %name @ ".~wattway";
			remoteEval(2048, "IssueTargCommand", 0, %cmd, %client - 2048, getManagerId());
			$AutoPoint::AttackingEnemy = "TRUE";
			$AutoPoint::EscortingFriendly = "";
		}
		else
		{
			%cmd = "Escort " @ %name @ ".~wescfr";
			remoteEval(2048, "IssueTargCommand", 0, %cmd, %client - 2048, getManagerId());
			$AutoPoint::AttackingEnemy = "";
			$AutoPoint::EscortingFriendly = "TRUE";
		}		
	}
}
Event::Attach(EventFlagGrabbed,AutoPoint::onFlagGrabbed);

function AutoPoint::onFlagDropped(%name,%team)
{
	%client = getClientByName(%name);
	if(%client)
	{
		// Dropped our flag
		remoteEval(2048,CStatus,0,"AutoPoint off.");
		if($Team::Current[%client] != $MyTeam)
			$AutoPoint::AttackingEnemy = "";
		else
			$AutoPoint::EscortingFriendly = "";
	}
}
Event::Attach(EventFlagDropped,AutoPoint::onFlagDropped);
Event::Attach(EventFlagCaptured,AutoPoint::onFlagDropped);