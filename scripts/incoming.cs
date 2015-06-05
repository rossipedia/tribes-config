// =================================================================
$Incoming::Trigger = "numpadenter";
$Incoming::Timeout = "2";
// =================================================================

bind( $Incoming::Trigger , "Incoming::Trigger();" , "", IncomingInit );

$Incoming::Direction[0] = "HEAVIES";
$Incoming::Direction[1] = "SOUTHWEST";
$Incoming::Direction[2] = "SOUTH";
$Incoming::Direction[3] = "SOUTHEAST";
$Incoming::Direction[4] = "WEST";
$Incoming::Direction[5] = "ENEMIES";
$Incoming::Direction[6] = "EAST";
$Incoming::Direction[7] = "NORTHWEST";
$Incoming::Direction[8] = "NORTH";
$Incoming::Direction[9] = "NORTHEAST";

newActionMap("IncomingMap.sae");
for (%i=0;%i<10;%i++)
{
  bind( "numpad" @ %i, "Incoming::Announce(" @ %i @ ");", "", INCOMING_ @ %i, "IncomingMap.sae" );
}

function Incoming::Trigger()
{
  pushActionMap("IncomingMap.sae");
  remoteBP( 2048, "<JC>Incoming Announce Enabled. Cancelling in " @ $Incoming::Timeout @ " second(s).");
  Schedule::Add("Incoming::Cancel();",$Incoming::Timeout);
  $Incoming::Active = "TRUE";
}

function Incoming::Announce(%dir)
{
  say(1,"INCOMING: " @ $Incoming::Direction[%dir] @ "~wincom2");
  Incoming::Cancel();
}

function Incoming::Cancel()
{
  if ($Incoming::Active!="TRUE")
    return;
  popActionMap("IncomingMap.sae");
  remoteBP(2048, "");
  $Incoming::Active = "";
}