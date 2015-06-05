function PKT::Message(%msg)
{
  if (String::FindSubStr(%msg,"%3")!=-1)
  {
    PKT::Message(sprintf(%msg,"%1","%2","his"));   
    PKT::Message(sprintf(%msg,"%1","%2","her"));
    return;
  }
  for (%i=1;$PKT::Msg[%i]!="";%i++)
    if ($PKT::Msg[%i]==%msg) { return; }
  $PKT::Msg[%i] = %msg;
}

function PKT::ClientJoin(%client)
{  
  if (Client::GetName(%client)!=$PCFG::Name)
  {
    for (%i=1;$PKT::Msg[%i]!="";%i++)
    {
      if (String::FindSubStr($PKT::Msg[%i],"%1")!=-1)
      {
        %msg = sprintf($PKT::Msg[%i],Client::GetName(%client),$PCFG::Name);
        MSGCB::Add(%msg,"Event::Trigger(EventYouDied);");
        %msg = sprintf($PKT::Msg[%i],$PCFG::Name,Client::GetName(%client));
        MSGCB::Add(%msg,"Event::Trigger(EventYouKilled);");
      }
    }
  }
  else
  {
    for (%i=1;$PKT::Msg[%i]!="";%i++)
    {
      if (String::FindSubStr($PKT::Msg[%i],"%1")==-1)
      {
        %msg = sprintf($PKT::Msg[%i],"",Client::GetName(getManagerID()));
        MSGCB::Add(%msg,"Event::Trigger(EventYouDied);");
      }
    }
  }
}
Event::Attach(EventClientJoin,PKT::ClientJoin);

function PKT::ClientDrop(%client)
{  
  if (Client::GetName(%client)!=$PCFG::Name)
  {
    for (%i=1;$PKT::Msg[%i]!="";%i++)
    {
      if (String::FindSubStr($PKT::Msg[%i],"%1")!=-1)
      {
        %msg = sprintf($PKT::Msg[%i],Client::GetName(%client),$PCFG::Name);
        MSGCB::Remove(%msg,"Event::Trigger(EventYouDied);");
        %msg = sprintf($PKT::Msg[%i],$PCFG::Name,Client::GetName(%client));
        MSGCB::Remove(%msg,"Event::Trigger(EventYouKilled);");
      }
    }
  }
  else
  {
    for (%i=1;$PKT::Msg[%i]!="";%i++)
    {
      if (String::FindSubStr($PKT::Msg[%i],"%1")==-1)
      {
        %msg = sprintf($PKT::Msg[%i],"",Client::GetName(getManagerID()));
        MSGCB::Remove(%msg,"Event::Trigger(EventYouDied);");
      }
    }
  }
}
Event::Attach(EventClientDrop,PKT::ClientDrop);


Event::Attach(EventYouDied,"echo(); echo(\"You died!\"); echo();");
Event::Attach(EventYouKilled,"echo(); echo(\"You killed!\"); echo();");


// ==========================================================================
// for Event Spawned
Event::Attach(EventChangeMission,Spawn::Check);
Event::Attach(EventConnected, Spawn::Check);

function Spawn::Check(%mission)
{
  $Spawn::Weapon = getMountedItem(0);
  Spawn::Recheck();
}

function Spawn::Recheck()
{
  if (getMountedItem(0)!=$Spawn::Weapon)
  {
    Schedule::Cancel(SPAWN_CHECK);
    Event::Trigger(EventSpawn);
    return;
  }
  Schedule::Add("Spawn::Recheck();",0.2,SPAWN_CHECK);
}
// ==========================================================================

// ==========================================================================
// For Event Respawned
Event::Attach(EventYouDied,Respawn::Check);
Event::Attach(EventChangeMission,Respawn::OnChangeMission);
Event::Attach( EventMatchStarted, "Event::Trigger( EventSpawn );" );

$Respawn::Timeout = 2;

function Respawn::Check()
{
  Tap::Start(RESPAWN_TIMEOUT,$Respawn::Timeout);
  $Respawn::Weapon = getMountedItem(0);
  $Respawn::WeaponSwitch = "FALSE";
  
  Event::Attach(EventWeaponBreak,Respawn::WeaponBreak);
  
  Respawn::Recheck();
}

function Respawn::Recheck()
{
  if ($Respawn::Weapon != getMountedItem(0) && $Respawn::WeaponSwitch == "FALSE")
  {
    $Respawn::Weapon = getMountedItem(0);
    $Respawn::WeaponSwitch = "TRUE";
    Event::Detach(EventWeaponBreak, Respawn::WeaponBreak);
  }
  if (!tapped(RESPAWN_TIMEOUT) && $Respawn::Weapon != getMountedItem(0))
  {
    Schedule::Cancel(RESPAWN_CHECK);
    Event::Detach(EventWeaponBreak,Respawn::WeaponBreak);
    Event::Trigger(EventRespawn);
    return;
  }
  Schedule::Add("Respawn::Recheck();",0.2,RESPAWN_CHECK);
}

function Respawn::WeaponBreak()
{
  if (tapped(RESPAWN_TIMEOUT)) { return; }

  Schedule::Cancel(RESPAWN_CHECK);
  Event::Detach(EventWeaponBreak,Respawn::WeaponBreak);
  schedule("Event::Trigger(EventRespawn);",0.1);
  return;
}

function Respawn::OnChangeMission()
{
  //Event::Detach(EventWeaponBreak,Respawn::WeaponBreak);
}
// ==========================================================================

//test
Event::Attach(EventRespawn,"echo(); echo(\"RESPAWNING\"); echo();");
Event::Attach(EventSpawn,"echo(); echo(\"SPAWNING\"); echo();");

// ==========================================================================

PKT::Message("%2 falls to %3 death.");
PKT::Message("%2 forgot to tie %3 bungie cord.");
PKT::Message("%2 bites the dust in a forceful manner.");
PKT::Message("%2 fall down go boom.");

PKT::Message("%1 makes quite an impact on %2.");
PKT::Message("%2 becomes the victim of a fly-by from %1.");
PKT::Message("%2 leaves a nasty dent in %1's fender.");
PKT::Message("%1 says, 'Hey %2, you scratched my paint job!'");

PKT::Message("%1 ventilates %2 with %3 chaingun.");
PKT::Message("%1 gives %2 an overdose of lead.");
PKT::Message("%1 fills %2 full of holes.");
PKT::Message("%1 guns down %2.");

PKT::Message("%2 dies of turret trauma.");
PKT::Message("%2 is chewed to pieces by a turret.");
PKT::Message("%2 walks into a stream of turret fire.");
PKT::Message("%2 ends up on the wrong side of a turret.");
PKT::Message("%2 dies.");

PKT::Message("%2 feels the warm glow of %1's plasma.");
PKT::Message("%1 gives %2 a white-hot plasma injection.");
PKT::Message("%1 asks %2, 'Got plasma?'");
PKT::Message("%1 gives %2 a plasma transfusion.");

PKT::Message("%2 catches a Frisbee of Death thrown by %1.");
PKT::Message("%1 blasts %2 with a well-placed disc.");
PKT::Message("%1's spinfusor caught %2 by surprise.");
PKT::Message("%2 falls victim to %1's Stormhammer.");

PKT::Message("%1 blows %2 up real good.");
PKT::Message("%2 gets a taste of %1's explosive temper.");
PKT::Message("%1 gives %2 a fatal concussion.");
PKT::Message("%2 never saw it coming from %1.");

PKT::Message("%1 adds %2 to %3 list of sniper victims.");
PKT::Message("%1 fells %2 with a sniper shot.");
PKT::Message("%2 becomes a victim of %1's laser rifle.");
PKT::Message("%2 stayed in %1's crosshairs for too long.");

PKT::Message("%1 mortars %2 into oblivion.");
PKT::Message("%2 didn't see that last mortar from %1.");
PKT::Message("%1 inflicts a mortal mortar wound on %2.");
PKT::Message("%1's mortar takes out %2.");

PKT::Message("%2 gets a blast out of %1.");
PKT::Message("%2 succumbs to %1's rain of blaster fire.");
PKT::Message("%1's puny blaster shows %2 a new world of pain.");
PKT::Message("%2 meets %1's master blaster.");

PKT::Message("%2 gets zapped with %1's ELF gun.");
PKT::Message("%1 gives %2 a nasty jolt.");
PKT::Message("%2 gets a real shock out of meeting %1.");
PKT::Message("%1 short-circuits %2's systems.");

PKT::Message("%2 didn't stay away from the moving parts.");
PKT::Message("%2 is crushed.");
PKT::Message("%2 gets smushed flat.");
PKT::Message("%2 gets caught in the machinery.");

PKT::Message("%2 is a victim among the wreckage.");
PKT::Message("%2 is killed by debris.");
PKT::Message("%2 becomes a victim of collateral damage.");
PKT::Message("%2 got too close to the exploding stuff.");

PKT::Message("%2 takes a missile up the keister.");
PKT::Message("%2 gets shot down.");
PKT::Message("%2 gets real friendly with a rocket.");
PKT::Message("%2 feels the burn from a warhead.");

PKT::Message("%2 ends it all.");
PKT::Message("%2 takes %3 own life.");
PKT::Message("%2 kills %3 own dumb self.");
PKT::Message("%2 decides to see what the afterlife is like.");

PKT::Message("%1 mows down %3 teammate, %2");
PKT::Message("%1 killed %3 teammate, %2 with a mine.");
