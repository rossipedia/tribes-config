
$HudClean::Leave["crossHairHud"] = "TRUE";
$HudClean::Leave["clockHud"] = "TRUE";
$HudClean::Leave["sensorHud"] = "TRUE";
$HudClean::Leave["healthHud"] = "TRUE";
$HudClean::Leave["jetPackHud"] = "TRUE";
$HudClean::Leave["compassHud"] = "TRUE";
$HudClean::Leave["weaponHud"] = "TRUE";
$HudClean::Leave["chatDisplayHud"] = "TRUE";

function HudClean::Exit()
{
  %playGuiID = nameToId(playGui);
  %objs = Group::objectCount(%playGuiID);
  for(%i = 0; %i < %objs; %i++) 
  {
    %objid = Group::getObject(%playGuiID, %i);
    %obj = Object::GetName(%objid);
    if(%obj != false && $HudClean::Leave[%obj]!="TRUE")
    {
      %execstr = %execstr @ "removeFromSet(PlayGui,"@%objid@"); deleteObject("@%objid@"); ";
    }
  }
  eval(%execstr);
}

Event::Attach(EventExit,HudClean::Exit);
