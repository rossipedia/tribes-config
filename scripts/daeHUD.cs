// ==========================================================================
// My Personal CTF hud

// Auto Start.
$daeHUD::AutoStart = 1;

// Position
$daeHUD::Left = 0;
$daeHUD::Top = 0;

// Toggle Hud Visibility
$daehud::ToggleHud = "control d";
// Reset Hud
$daeHUD::ResetKey = "control r";
// Hud Transparency (Clear)
$daeHUD::Transparent = 0;
// Toggle Transparency on / off
$daeHUD::ToggleTrans = "control t";
// Default text for when a flag is at base
$daeHUD::FriendlyHome = "";
$daeHUD::EnemyHome = "";

// These are used to bring the score up to date
// when joining a server in the middle of a game
$daeHUD::AddFriendlyPoint = "control numpad+";
$daeHUD::SubFriendlyPoint = "control numpad-";
$daeHUD::AddEnemyPoint = "alt numpad+";
$daeHUD::SubEnemyPoint = "alt numpad-";

// Use this menu to set up who has the flag when in the middle of a game.
$daeHud::CarrierKey = "alt f";

// ==========================================================================

bind($daeHUD::ToggleHud,"daeHUD::ToggleHud();","",daeHUDToggle);
bind($daeHUD::ResetKey,"daeHUD::reset();","",daeHUDReset);
bind($daeHUD::AddFriendlyPoint,"daeHUD::AddScore(\"Friendly\",1);","",daeHUDAddFriendly);
bind($daeHUD::SubFriendlyPoint,"daeHUD::AddScore(\"Friendly\",-1);","",daeHUDSubFriendly);
bind($daeHUD::AddEnemyPoint,"daeHUD::AddScore(\"Enemy\",1);","",daeHUDAddEnemy);
bind($daeHUD::SubEnemyPoint,"daeHUD::AddScore(\"Enemy\",-1);","",daeHUDSubFriendly);
bind($daeHUD::ToggleTrans,"daeHUD::ToggleTrans();","",daeHUDToggleTrans);




function daeHUD::GetText()
{
  %Text = "<jc><f0>Friendly\n"@"<f1><jl>Score:\t<f2>%1\n<f1>Flag:\n<f2>%3\n" 
@ "<jc><f0>Enemy\n"   @"<f1><jl>Score:\t<f2>%2\n<f1>Flag:\n<f2>%4";

  return sprintf(%text,$daeHUD::OurScore+0,$daeHUD::EnemyScore+0,daeHUD::MakeLegal($daeHUD::OurFlagCarrier),daeHUD::MakeLegal($daeHUD::EnemyFlagCarrier));
}

function daeHUD::MakeLegal(%text)
{
  %text = String::Replace(%text,"<","<<");
  %text = String::Replace(%text,"\\","\\\\");
  return escapestring(%text);
}

function daeHUD::Init()
{
  HUD::New(daeHUD,$daeHUD::Top, $daeHUD::Left,200,112,daeHUD::GetText());
  if (!$daeHUD::AutoStart)
    HUD::SetVisible(daeHUD,"FALSE");
  if ($daeHUD::Transparent)
    HUD::MakeTrans(daeHUD,"TRUE");
	$daeHUD::Loaded = "TRUE";
}

if ($daeHUD::Loaded!="TRUE")
 	daeHUD::Init();


function daeHUD::Update()
{
	HUD::Update(daeHUD,daeHUD::GetText());
}

function daeHUD::reset()
{
	$daeHUD::OurScore = 0;
	$daeHUD::EnemyScore = 0;	
	$daeHUD::OurFlagCarrier = $daeHUD::FriendlyHome;
	$daeHUD::EnemyFlagCarrier = $daeHUD::EnemyHome;
	daeHUD::Update();
}
Event::Attach(EventConnectionAccepted, daeHUD::reset);
Event::Attach(EventChangeMission, daeHUD::reset);

function daeHUD::FlagGrab(%name, %team)
{
		if(%team == $MyTeam)
			$daeHUD::OurFlagCarrier = %name;
		else
			$daeHUD::EnemyFlagCarrier = %name;
		daeHUD::Update();
}
Event::Attach(EventFlagGrabbed,daeHUD::FlagGrab);


function daeHUD::FlagTimer(%team, %status)
{
  if (%team == $MyTeam)
    $daeHUD::OurFlagCarrier = "DROPPED "@%status;
  else
    $daeHUD::EnemyFlagCarrier = "DROPPED "@%status;
  daeHUD::Update();
}
Event::Attach(EventFlagTimer,daeHUD::FlagTimer);

function daeHUD::FlagReturn(%name,%team)
{
	if (%team == $MyTeam) 
		$daeHUD::OurFlagCarrier = $daeHUD::FriendlyHome;
	else 
		$daeHUD::EnemyFlagCarrier = $daeHUD::EnemyHome;
	daeHUD::Update();
}
Event::Attach(EventFlagTimeout, daeHUD::FlagReturn);
Event::Attach(EventFlagReturned,daeHUD::FlagReturn);
Event::Attach(EventFlagOOBReturn,daeHUD::FlagReturn);

function daeHUD::FlagCap(%name,%team)
{
	daeHUD::FlagReturn(%name,%team);
	if (%team == $MyTeam)
		daeHUD::AddScore("Enemy",1);
	else 
		daeHUD::AddScore("Friendly",1);
	daeHUD::Update();
}
Event::Attach(EventFlagCaptured,daeHUD::FlagCap);

function daeHUD::AddScore(%who,%points)
{
	if (%who == "Friendly")
	{
		$daeHUD::OurScore+=%points;
		if($daeHUD::OurScore<0) $daeHUD::OurScore=0;
	}
	else
	{
		$daeHUD::EnemyScore+=%points;
		if ($daeHUD::EnemyScore<0) $daeHUD::EnemyScore=0;
	}
	daeHUD::Update();
}

function daeHUD::changeteam(%client,%team)
{
	if (%client != getManagerID())
		return;
	
	%hold = $daeHUD::OurScore;
	$daeHUD::OurScore = $daeHUD::EnemyScore;
	$daeHUD::EnemyScore = %hold;

	%hold = $daeHUD::EnemyFlagCarrier;
	$daeHUD::EnemyFlagCarrier = $daeHUD::OurFlagCarrier;
	$daeHUD::OurFlagCarrier = %hold;

  daeHUD::Update();
}
Event::Attach(EventClientChangeTeam,daeHUD::changeteam);

function daeHUD::ToggleHud()
{
	HUD::SetVisible(daeHUD,!HUD::GetVisible(daeHUD));
}

function daeHUD::ToggleTrans()
{
	HUD::MakeTrans(daeHUD,!HUD::IsTrans(daeHUD));
}

bind( $daeHud::CarrierKey, "daeHUD::ShowCarrierMenu();", "", daeHUD_ShowCarrierMenu );

function daeHUD::ShowCarrierMenu() 
{
  %menu = "Flag Carrier Menu";
  MS::NewMenu( %menu );
  MS::AddChoice( %menu, "f", "Friendly", "daeHUD::ShowCarrierList( Team::Enemy() );" );
  MS::AddChoice( %menu, "e", "Enemy", "daeHUD::ShowCarrierList( Team::Friendly() );" );
  MS::Display( %menu );
}

function daeHUD::ShowCarrierList( %team )
{
  %menu = "Carrier List";
  MS::NewMenu( %menu );
  %keys = "0123456789abcdefghijklmnopqrstuvwxyz,.;'/";
  for ( %idx = 1; $Team::Client[%team,%idx] != ""; %idx++ )
  {
    %key = String::GetSubStr( %keys, %idx - 1, 1 );
    %name = daeHUD::MakeLegal( Client::GetName( $Team::Client[%team,%idx] ) );
    MS::AddChoice( %menu, %key, %name, "daeHUD::SetCarrier( " @ $Team::Client[%team,%idx] @ " );" );
  }
  MS::Display( %menu );
}

function daeHUD::SetCarrier( %client )
{
  if ( Client::GetTeam( %client ) == Team::Friendly() )
    $daeHUD::EnemyFlagCarrier = Client::GetName( %client );
  else
    $daeHUD::OurFlagCarrier = Client::GetName( %client );
  daeHUD::Update();
}