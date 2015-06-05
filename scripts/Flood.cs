// ==========================================================================
$Flood::Msg[1] = "~wbuysellsound.wav";  
// ==========================================================================

function Flood::Check(%msg)
{
  for (%i=1;$Flood::Msg[%i]!="";%i++)
    if (String::FindSubStr(%msg,$Flood::Msg[%i])!=-1)
      if (IsFlooded($Flood::Msg[%i]))
        return "FALSE";
}
Event::Attach(EventServerMessage,Flood::Check);