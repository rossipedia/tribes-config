$Favs::Name[1] = "Light Sniper Grens";
$Favs::Name[2] = "Light Sniper Chaingun";
$Favs::Name[3] = "Light Versatile";
$Favs::Name[4] = "Light Repair";
$Favs::Name[5] = "Heavy O";
$Favs::Name[6] = "Heavy D";
$Favs::Name[7] = "Medium Turret";

$Favs::Key[1] = "alt 1";
$Favs::Key[2] = "alt 2";
$Favs::Key[3] = "alt 3";
$Favs::Key[4] = "alt 4";
$Favs::Key[5] = "alt 5";
$Favs::Key[6] = "alt 6";
$Favs::Key[7] = "alt 7";

// ==========================================================================
// Setup
for (%i=1;$Favs::Key[%i]!="";%i++)
{
  bind($Favs::Key[%i],"Favs::Select("@%i@");");
  $numFavs++;
}
$curFavorites = $pref::curFavorites;

Event::Attach(EventStationOn,"Favs::Buy(); $Favs::AtStation = \"TRUE\";");
Event::Attach(EventStationOff,"$Favs::AtStation = \"FALSE\";");
Event::Attach(EventGuiOpen,Favs::OnGuiOpen);

// ==========================================================================
// Favs Stuff
function Favs::Select(%loadout)
{
  $pref::curFavorites = $curFavorites = %loadout;
  Favs::Show(%loadout);
  if ($Favs::AtStation) 
  { 
    Favs::Buy(); 
    if ($Favs::GuiOpened)
      Favs::OnGuiOpen(CmdInventoryGui);
  }
}

function Favs::Show(%loadout)
{
 if ($ServerFavoritesKey == "")
    %mod = "Base";
  else
    %mod = $ServerFavoritesKey;
 //remoteBP(2048,"<JC><F1>Currently Selected Loadout (<F2> "@%mod@" <F1>): \n<F2>"@$Favs::Name[%loadout @ $ServerFavoritesKey],3);
 HUD::Update(LoadoutHud,"<F0>Loadout (<F1>"@%mod@"<F0>):\n<F2>"@$Favs::Name[%loadout @ $ServerFavoritesKey]);
}

function Favs::Buy()
{
  %fav = $pref::favoriteList[$curFavorites @ $ServerFavoritesKey];
  %first = getWord(%fav, 0);
  if(%first == -1)
     return;

  %cmd = "remoteEval(2048, buyFavorites, " @ %first;
  for(%i = 1; (%word = getWord(%fav, %i)) != -1; %i++)
     %cmd = %cmd @ ", " @ %word;
  %cmd = %cmd @ ");";
  eval(%cmd);
}

function Favs::OnGuiOpen(%gui)
{
  if (%gui!=CmdInventoryGui) { return; }
  //hilite the favs button
  if ($curFavorites >= 1 && $curFavorites <= $numFavs)
  {
    for (%i = 1; %i <= $numFavs; %i++)
    {
      %btnName = "FavButton" @ %i;
        Control::setValue(%btnName, FALSE);
    }
    %btnName = "FavButton" @ $curFavorites;
      Control::setValue(%btnName, TRUE);

    //and perform the click
    CmdInventoryGui::favoritesSel($curFavorites);
  }
  $Favs::GuiOpened = "TRUE";
}
// ==========================================================================

// ==========================================================================
// Scavenge stuff

$Favs::ScavengeKey = "alt g";
$Favs::ScavengeBypass = "g";
$Favs::ScavengeToggle = "control g";

bind($Favs::ScavengeKey, "Favs::Scavenge();");
bind($Favs::ScavengeBypass, "Favs::ScavengeBypass();","Favs::ScavengeBypassOff();");
bind($Favs::ScavengeToggle, "Toggle::Var(\"$pref::Favs::Scavenge\",\"Scavenging\");");

Event::Attach(EventItemReceived,Favs::ItemReceived);

function Favs::ItemReceived(%desc)
{
  if ($pref::Favs::Scavenge)
  {
    %fav = $pref::favoriteList[$curFavorites @ $ServerFavoritesKey];
    if (String::FindSubStr(" "@%fav," "@$INV::Num[%desc]@" ")==-1)
    {
      drop($INV::Name[$INV::Num[%desc]]);
      Scavenge::Check($INV::Name[$INV::Num[%desc]]);
      return "FALSE";
    }
  }
}

function Scavenge::Check(%desc)
{
  if (getItemCount(%desc))
  {
    drop(%desc);
    Schedule::Add("Scavenge::Check(\""@%desc@"\");",0.1);
  }
}

function Favs::Scavenge()
{
  if ($pref::Favs::Scavenge)
    for (%i=1;%i<=200;%i++)
      if ($INV::Name[%i]!="")
        Favs::ItemReceived($INV::Name[%i]);
}

function Favs::ScavengeBypass()
{
  if ($pref::Favs::Scavenge)
  {
    $pref::Favs::Scavenge = "FALSE";
    remoteBP(2048,"<JC><F1>Bypassing Auto Scavenge");
    $Favs::ScavengeBP = "TRUE";
  }
}

function Favs::ScavengeBypassOff()
{
  if ($Favs::ScavengeBP)
  {
    $pref::Favs::Scavenge="TRUE";
    remoteBP(2048,"");
    $Favs::ScavengeBP = "";
  }
}
// ==========================================================================


// =================================================================
// Favs Hud
HUD::New(LoadoutHud,0,"100%",200,28);
function LoadoutHud::OnConnected()
{
  Favs::Show($pref::curFavorites);
}
Event::Attach(EventConnected,LoadoutHud::OnConnected);
// =================================================================