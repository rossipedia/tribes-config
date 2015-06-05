// =================================================================
$Drop::Set[1] = "r, Repair Pack";
$Drop::Set[2] = "t, Turret";
$Drop::Set[3] = "shift t, Inventory Station";
$Drop::Set[4] = "p, Pulse Sensor";
// =================================================================

newActionMap("DropMap.sae");

$Drop::Popup = "\tDrop Groups:\n";
for (%i=1;$Drop::Set[%i]!="";%i++)
{
  %idx = String::FindSubStr($Drop::Set[%i],",");
  %key = String::Trim(String::GetSubStr($Drop::Set[%i],0,%idx));
  %item = String::Trim(String::GetSubStr($Drop::Set[%i],%idx+1,100));
  bind(%key,"Drop::Start(\""@%item@"\");","Drop::Stop();",DropSet @ %i,"DropMap.sae");
  $Drop::Popup = $Drop::Popup @ "\t\t\t\t\t<f0>Press <f2>"@%key@" <f0>for <f2>"@%item@"s.\n";
}

function Drop::BuyCheck(%msg)
{
  if (String::FindSubStr(%msg,"You couldn't buy ")!=-1)
    $Drop::Dropping = "";
}

Event::Attach(EventServerMessage,Drop::BuyCheck);
Event::Attach(EventStationOn,Drop::StationOn);
Event::Attach(EventStationOff,Drop::StationOff);

function Drop::StationOn()
{
  pushActionMap("DropMap.sae");
  remoteBP(2048, $Drop::Popup);
}

function Drop::StationOff()
{
  popActionMap("DropMap.sae");
  remoteBP(2048, "");
}

function Drop::Start(%item)
{
  $Drop::Dropping = "TRUE";
  $Drop::Item = %item;
  Drop::Loop();
}

function Drop::Loop()
{
  buy($Drop::Item);
  drop($Drop::Item);
  schedule::add("Drop::Loop();",0.1,DROPLOOP);
}

function Drop::Stop()
{
  schedule::cancel(DROPLOOP);
  Favs::Buy();
}