// ==========================================================================
// Quick Message filtering
// Just add a phrase/msg to search for, and any message from the server
// containing that phrase will be muted
$Filter::Msg[0] = "You couldn't buy ";
$Filter::Msg[1] = "Repairing ";
$Filter::Msg[2] = "Station Access ";
$Filter::Msg[3] = "Repair Done";
$Filter::Msg[4] = "AutoRepair ";
$Filter::Msg[5] = " deployed";
$Filter::Msg[6] = "Repair Stopped";
$Filter::Msg[7] = "Unit is disabled";
$Filter::Msg[8] = "Nothing in range";
$Filter::Msg[9] = "Bought Laser Rifle - Auto buying Energy Pack";
$Filter::Msg[10] = "SOLD ";
$Filter::Msg[11] = " has no ammo";
// ==========================================================================

function Filter::Check(%msg)
{
	for (%i=0;$Filter::Msg[%i]!="";%i++)
		if(String::FindSubStr(%msg,$Filter::Msg[%i])!=-1)
			return "FALSE";
}
Event::Attach(EventServerMessage,Filter::Check);