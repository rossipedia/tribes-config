// ==========================================================================
// Messages
function onClientMessage(%client, %msg)
{
  // get rid of the tags
  if ((%idx = String::FindSubStr(%msg,"~w"))!=-1)
    %clip = String::GetSubStr(%msg,0,%idx);
  else
    %clip = %msg;
  //Take care of the message callbacks, without any ~ tags
  for (%i=1;$MSGCB[%clip,%i]!="";%i++)
  {
    if (String::FindSubStr($MSGCB[%clip,%i],";")==-1)
      %ret = eval(
        $MSGCB[%clip,%i]@
        "(%client,%msg);"
      );
    else
      %ret = eval($MSGCB[%clip,%i]);
    if (%return!="FALSE")
      %return = %ret;
  }
  if (%return == "FALSE")
    return "FALSE";

  if(%client==0)
    return Event::Trigger(EventServerMessage,%msg)!="FALSE";
  
  $lastClientMessage = %client;
  
  
  // Flood protect messages from other clients
  %tag = escapestring(%client @ "||" @ %msg);
  if (IsFlooded(%tag) && %client != getManagerID())
    return "FALSE";


  // Trigger the EventClientMessage
  return Event::Trigger(EventClientMessage,%client,%msg)!="FALSE";
}
// ==========================================================================

function remoteMissionChangeNotify(%serverManagerId, %nextMission)
{
  if(%serverManagerId == 2048)
  {
    Event::Trigger(EventChangeMission, %nextMission);
    if ( $AtStation )
      Event::Trigger( EventStationOff );
    echo("Server mission complete - changing to mission: ", %nextMission);
    echo("Flushing Texture Cache");
    flushTextureCache();
    schedule("purgeResources(true);", 3);
  }
}

function onClientJoin(%client) {
  Event::Trigger(EventClientJoin, %client);
  //Setup for Flag Callbacks
  if (%client == getManagerID())
  {
    %name = "You";
    %nm = Client::GetName(%client);
  }
  else
    %name = %nm = Client::GetName(%client);

  MSGCB::Add(sprintf($FlagTakenMessage[0],%name),"Event::Trigger(EventFlagGrabbed,\""@%nm@"\",0);");
  MSGCB::Add(sprintf($FlagDroppedMessage[0],%name),"Event::Trigger(EventFlagDropped,\""@%nm@"\",0);");
  MSGCB::Add(sprintf($FlagCapturedMessage[0],%name),"Event::Trigger(EventFlagCaptured,\""@%nm@"\",0);");
  MSGCB::Add(sprintf($FlagReturnedMessage[0],%name),"Event::Trigger(EventFlagReturned,\""@%nm@"\",0);");
  MSGCB::Add(sprintf($FlagOOBReturnedMessage[0],%name),"Event::Trigger(EventFlagOOBReturn,\""@%nm@"\",0);");

  MSGCB::Add(sprintf($FlagTakenMessage[1],%name),"Event::Trigger(EventFlagGrabbed,\""@%nm@"\",1);");
  MSGCB::Add(sprintf($FlagDroppedMessage[1],%name),"Event::Trigger(EventFlagDropped,\""@%nm@"\",1);");
  MSGCB::Add(sprintf($FlagCapturedMessage[1],%name),"Event::Trigger(EventFlagCaptured,\""@%nm@"\",1);");
  MSGCB::Add(sprintf($FlagReturnedMessage[1],%name),"Event::Trigger(EventFlagReturned,\""@%nm@"\",1);");
  MSGCB::Add(sprintf($FlagOOBReturnedMessage[1],%name),"Event::Trigger(EventFlagOOBReturn,\""@%nm@"\",1);");

}

function onClientDrop(%client) {
	Event::Trigger(EventClientDrop, %client);
  //Setup for Flag Callbacks
  if (%client == getManagerID())
  {
    %name = "You";
    %nm = Client::GetName(%client);
  }
  else
    %name = %nm = Client::GetName(%client);

  MSGCB::Remove(sprintf($FlagTakenMessage[0],%name),"Event::Trigger(EventFlagGrabbed,\""@%nm@"\",0);");
  MSGCB::Remove(sprintf($FlagDroppedMessage[0],%name),"Event::Trigger(EventFlagDropped,\""@%nm@"\",0);");
  MSGCB::Remove(sprintf($FlagCapturedMessage[0],%name),"Event::Trigger(EventFlagCaptured,\""@%nm@"\",0);");
  MSGCB::Remove(sprintf($FlagReturnedMessage[0],%name),"Event::Trigger(EventFlagReturned,\""@%nm@"\",0);");
  MSGCB::Remove(sprintf($FlagOOBReturnedMessage[0],%name),"Event::Trigger(EventFlagOOBReturn,\""@%nm@"\",0);");

  MSGCB::Remove(sprintf($FlagTakenMessage[1],%name),"Event::Trigger(EventFlagGrabbed,\""@%nm@"\",1);");
  MSGCB::Remove(sprintf($FlagDroppedMessage[1],%name),"Event::Trigger(EventFlagDropped,\""@%nm@"\",1);");
  MSGCB::Remove(sprintf($FlagCapturedMessage[1],%name),"Event::Trigger(EventFlagCaptured,\""@%nm@"\",1);");
  MSGCB::Remove(sprintf($FlagReturnedMessage[1],%name),"Event::Trigger(EventFlagReturned,\""@%nm@"\",1);");
  MSGCB::Remove(sprintf($FlagOOBReturnedMessage[1],%name),"Event::Trigger(EventFlagOOBReturn,\""@%nm@"\",1);");
}

function onClientChangeTeam(%client, %team) {
	Event::Trigger(EventClientChangeTeam, %client, %team);
  if(%client == GetManagerID()) { $MyTeam = %team; }
}

function onExit()
{
   Event::Trigger(EventExit);

   if(isObject(playGui))
      storeObject(playGui, "config\\play.gui");

   saveActionMap("config\\config.cs", "actionMap.sae", "playMap.sae", "pdaMap.sae");

	$pref::VideoFullScreen = isFullScreenMode(MainWindow);

   checkMasterTranslation();
	 echo("exporting pref::* to prefs.cs");
   export("pref::*", "config\\ClientPrefs.cs", False);
   export("Server::*", "config\\ServerPrefs.cs", False);
   export("pref::lastMission", "config\\ServerPrefs.cs", True);
   BanList::export("config\\banlist.cs");
}


function onConnection(%message)
{
   echo("Connection ", %message);
   $dataFinished = false;
   if(%message == "Accepted")
   {
      resetSimTime();
		if ($PCFG::Script != "")
		{
			exec($PCFG::Script);
		}

      resetPlayDelegate();
      remoteEval(2048, "SetCLInfo", $PCFG::SkinBase, $PCFG::RealName, $PCFG::EMail, $PCFG::Tribe, $PCFG::URL, $PCFG::Info, $pref::autoWaypoint, $pref::noEnterInvStation, $pref::messageMask);

		if ($Pref::PlayGameMode == "JOIN")
		{
			cursorOn(MainWindow);
	      GuiLoadContentCtrl(MainWindow, "gui\\Loading.gui");
			renderCanvas(MainWindow);
		}
		Event::Trigger(EventConnectionAccepted);
   }
   else if(%message == "Rejected")
   {
		Quickstart();
      $errorString = "Connection to server rejected:\n" @ $errorString;
      GuiPushDialog(MainWindow, "gui\\MessageDialog.gui");
		schedule("Control::setValue(MessageDialogTextFormat, $errorString);", 0);
		Event::Trigger(EventConnectionRejected);
   }
   else
   {
		Quickstart();

      if(%message == "Dropped")
      {
         if($errorString == "")
            $errorString = "Connection to server lost:\nServer went down.";
         else
            $errorString = "Connection to server lost:\n" @ $errorString;

	   Event::Trigger(EventConnectionLost, $errorString);

         GuiPushDialog(MainWindow, "gui\\MessageDialog.gui");
		   schedule("Control::setValue(MessageDialogTextFormat, $errorString);", 0);
      }
      else if(%message == "TimedOut")
      {
         $errorString = "Connection to server timed out.";
         GuiPushDialog(MainWindow, "gui\\MessageDialog.gui");
		   schedule("Control::setValue(MessageDialogTextFormat, $errorString);", 0);
	   Event::Trigger(EventConnectionTimeout);
      }
   }
}


function dataFinished()
{

    if ($cdMusic && !$pref::userCDOverride)
    {
      rbSetPlayMode (CD, 0);
      rbStop (CD);
    }
   Control::setValue(ProgressText, "<jc><f0>Get ready to rock n' roll!");
   Control::setValue(ProgressSlider, 0.9);

   $dataFinished = true;
   remoteEval(2048, dataFinished);

	 Event::Trigger(EventConnected);

   echo("Flushing Texture Cache");
   flushTextureCache();
}

function CmdInventoryGui::onOpen()
{
   if($curFavorites == -1)
   {
      CmdInventoryGui::favoritesSel(1);
      Control::performClick("FavButton1");
   }
  Event::Trigger(EventGuiOpen, CmdInventoryGui);
}

function CmdInventoryGui::OnClose()
{
  Event::Trigger(EventGuiClose, CmdInventoryGui);
}

function CommandGui::onOpen()
{

	if ($pref::mapFilter & 0x0001) Control::setValue(IDCTG_CMDO_PLAYERS, "TRUE");
	else Control::setValue(IDCTG_CMDO_PLAYERS, "FALSE");

	if ($pref::mapFilter & 0x0002) Control::setValue(IDCTG_CMDO_TURRETS, "TRUE");
	else Control::setValue(IDCTG_CMDO_TURRETS, "FALSE");

	if ($pref::mapFilter & 0x0004) Control::setValue(IDCTG_CMDO_ITEMS, "TRUE");
	else Control::setValue(IDCTG_CMDO_ITEMS, "FALSE");

	if ($pref::mapFilter & 0x0008) Control::setValue(IDCTG_CMDO_OBJECTS, "TRUE");
	else Control::setValue(IDCTG_CMDO_OBJECTS, "FALSE");

	if (String::ICompare($pref::mapSensorRange, "TRUE") == 0) Control::setValue(IDCTG_CMDO_RADAR, "TRUE");
	else Control::setValue(IDCTG_CMDO_RADAR, "FALSE");

	if (String::ICompare($pref::mapNames, "TRUE") == 0) Control::setValue(IDCTG_CMDO_EXTRA, "TRUE");
	else Control::setValue(IDCTG_CMDO_EXTRA, "FALSE");

   Control::setValue("TVButton", false);

  Event::Trigger(EventGuiOpen,CommandGui);

}

function CommandGui::onClose()
{
  Event::Trigger(EventGuiClose,CommandGui);
}

function PlayGui::OnOpen()
{
  Event::Trigger(EventGuiOpen,PlayGui);
  Event::Trigger(EventPlayMode);
  if (IsObject(PlayGui) && $PlayGuiCreated!="TRUE")
  {
    Event::Trigger(EventPlayGuiCreated);
    $PlayGuiCreated = "TRUE";
  }
}

function PlayGui::OnClose()
{
  Event::Trigger(EventGuiClose,PlayGui);
}


function onTeamAdd(%team, %name)
{
  if (%team == 0)
    %name = "Observer";
  $Team::Name[%team-1] = %name;
  $Team::Num[%name] = %team-1;
  Event::Trigger(EventTeamAdded,%team-1,%name);
  if (%team!=0)
  {
    // Set up for Flag Callbacks
    $FlagTakenMessage[%team-1] = sprintf("%2 took the %1 flag! ",%name,"%1");
    $FlagDroppedMessage[%team-1] = sprintf("%2 dropped the %1 flag!",%name,"%1");
    $FlagCapturedMessage[%team-1] = sprintf("%2 captured the %1 flag!",%name,"%1");
    $FlagReturnedMessage[%team-1] = sprintf("%2 returned the %1 flag!",%name,"%1");
    $FlagOOBReturnedMessage[%team-1] = sprintf("%2 left the mission area while carrying the %1 flag!",%name,"%1");
  }
  // Set up server return messages.
  MSGCB::Add( "The " @ %name @ " flag was returned to base.", "Event::Trigger( EventFlagReturn, -1, " @ ( %team - 1 ) @ " );" );
}

function Team::Friendly(%client)
{
	if (%client == "")
		%client = getManagerID();
	return Client::GetTeam(%client);
}

function Team::Enemy(%client,%num)
{
	if (%client == "")
		%client = getManagerID();
	return !Client::GetTeam(%client);
}


Event::Attach(
  EventChangeMission,
  "Flag::ResetTimer(\"\",1); Flag::ResetTimer(\"\",0);"
  );

Event::Attach(EventFlagGrabbed,Flag::ResetTimer);
Event::Attach(EventFlagReturned,Flag::ResetTimer);
Event::Attach(EventFlagOOBReturn,Flag::ResetTimer);
Event::Attach(EventFlagCaptured,Flag::ResetTimer);
Event::Attach(EventFlagDropped,Flag::ResetTimer);

function Flag::ResetTimer(%name,%team)
{
  Schedule::Cancel("Flag::DroppedTimer("@%team@");");
}

Event::Attach(EventFlagDropped,Flag::Dropped);
function Flag::Dropped(%name,%team)
{
  $Flag::DropTime[%team]=48;
  Flag::DroppedTimer(%team);
}

function Flag::DroppedTimer(%team)
{
  if ($Flag::DropTime[%team]<=0) 
  {
    Event::Trigger(EventFlagTimeout,"Timeout",%team);
    return;
  }
  Event::Trigger(EventFlagTimer,%team,$Flag::DropTime[%team]);
  $Flag::DropTime[%team]--;
  Schedule::Add("Flag::DroppedTimer("@%team@");",1);
}



function buy(%desc)
{
	if (IsNum(%desc))
    %type = %desc;
  else
    %type = getItemType(%desc);
	if (%type != -1) {
		remoteEval(2048,buyItem,%type);
		Event::Trigger(EventBuyItem,%type);
	}
	else {
		echo("Unknown item \"" @ %desc @ "\"");
	}
}
function sell(%desc)
{
	if (IsNum(%desc))
    %type = %desc;
  else
    %type = getItemType(%desc);
	if (%type != -1) {
		remoteEval(2048,sellItem,%type);
		Event::Trigger(EventSellItem,%type);
	}
	else {
		echo("Unknown item \"" @ %desc @ "\"");
	}
}

function use(%desc)
{
	if (IsNum(%desc))
    %type = %desc;
  else
    %type = getItemType(%desc);
	if (%type != -1) {
		useItem(%type);
		Event::Trigger(EventUseItem,%desc);
	}
	else {
		echo("Unknown item \"" @ %desc @ "\"");
	}
}

function drop(%desc,%amt)
{
	if (IsNum(%desc))
    %type = %desc;
  else
    %type = getItemType(%desc);
	if (%type != -1) {
		remoteEval(2048,dropItem,%type,%amt);
		Event::Trigger(EventDropItem,%desc,%amt);
	}
	else {
		echo("Unknown item \"" @ %desc @ "\"");
	}
}

function deploy(%desc)
{
	if (IsNum(%desc))
    %type = %desc;
  else
    %type = getItemType(%desc);
	if (%type != -1) {
		remoteEval(2048,deployItem,%type);
		Event::Trigger(EventDeployItem,%desc);
	}
	else {
		echo("Unknown item \"" @ %desc @ "\"");
	}
}


function throw(%desc,%strength)
{
  if (IsNum(%desc))
    %type = %desc;
  else
    %type = getItemType(%desc);
	if (%type != -1) {
		remoteEval(2048,throwItem,%type,%strength);
    Event::Trigger(EventThrowItem,%type,%strength);
	}
	else {
		echo("Unknown item \"" @ %desc @ "\"");
	}
}


function throwStart()
{
	$throwStartTime = getSimTime();
  Event::Trigger(EventThrowStart);
}

function throwRelease(%desc)
{
	%type = getItemType(%desc);
	if (%type != -1) {
		%delta = getSimTime() - $throwStartTime;
		if (%delta > 1)
			%delta = 100;
		else
			%delta = floor(%delta * 100);
		remoteEval(2048,throwItem,%type,%delta);
    Event::Trigger(EventThrowItem,%type,%delta);
	}
	else {
		echo("Unknown item \"" @ %desc @ "\"");
	}
}

function nextWeapon()
{
	%curwep = getMountedItem(0);
	remoteEval(2048,nextWeapon);
	Schedule::Add("NW::Check(Next,"@%curwep@");",0.01,CHECK_Next_WEP);
}	

function prevWeapon()
{
	%curwep = getMountedItem(0);
	remoteEval(2048,prevWeapon);
	Schedule::Add("NW::Check(Prev,"@%curwep@");",0.01,CHECK_Prev_WEP);
}

function NW::Check(%dir,%fromwep)
{
	if ($NW::check>=300)
	{
		$NW::check=0;
		return;
	}
   if (getMountedItem(0)!=%fromwep)
  {
    $NW::check=0;
    Schedule::Cancel(CHECK_@%dir@_WEP);
    Event::Trigger(Event @ %dir @ Weapon,getMountedItem(0),%fromwep);
  }
  else
  {
    $NW::check++;
    %cmd = "NW::Check("@%dir@","@%fromwep@");";
    Schedule::Add(%cmd,0.01,CHECK_ @ %dir @ _WEP);
  }
}

MSGCB::Add("Station Access On","Event::Trigger(EventStationOn);");
MSGCB::Add("Station Access Off","Event::Trigger(EventStationOff);");
MSGCB::Add("Match Started.","Event::Trigger(EventMatchStarted);");
MSGCB::Add("Resupply Complete", "Event::Trigger(EventResupplied);");

Event::Attach(EventStationOff,"$AtStation = \"\";");

function remoteITXT(%manager, %msg)
{
  if(%manager == 2048)
    Control::setValue(EnergyDisplayText, %msg);
  
  if ($AtStation != "")
    return;
  $AtStation = "TRUE";  
  if (String::FindSubStr(%msg,"Station")!=-1)
  {
    Event::Trigger(EventAtStation,"TRUE");
    return;
  }
  Event::Trigger(EventAtStation,"FALSE");
}

function Item::Parse(%msg)
{
  if (String::FindSubStr(%msg,"You received ")!=-1)
	{
		%amt = getWord(%msg,2);
    if (%amt=="a") { %amt = 1; }
		%lenamount = String::Len(%amt);
		%item = String::Slice(String::GetSubStr(%msg,14+%lenamount,100)," backpack");
    return Event::Trigger(EventItemReceived,%item,%amt)!="FALSE";
	}
}
Event::Attach(EventServerMessage,Item::Parse);