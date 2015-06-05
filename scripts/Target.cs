bind("alt t","Target::Menu();");

function Target::Menu()
{
  MS::NewMenu("Target Menu");
  MS::AddChoice("Target Menu","f","Friendly Team Members","Target::FMenu();");
  MS::AddChoice("Target Menu","e","Enemy Team Members","Target::EMenu();");
  MS::AddChoice("Target Menu","o","Objectives","Target::ObjMenu();");
  MS::Display("Target Menu");
}
// =================================================================
// Friendly Team Menu
function Target::FMenu()
{
  MS::NewMenu("Friendly Team Members:");
  %keys = "1234567890abcdefghijklmnopqrstuvwxyz";
  %keyidx = 0;
  %myteam = Client::GetTeam(GetManagerID());
  for (%cl=2100; %cl>2048; %cl--)
  {
    %name = Client::GetName(%cl);
    %team = Client::GetTeam(%cl);
    if (%name != "" && %team == %myteam)
    {
      %key = String::GetSubStr(%keys,%keyidx,1);
      %keyidx++;
      MS::AddChoice("Friendly Team Members:",%key,%name,"Target::Client("@%cl@");");
    }    
  }
  MS::Display("Friendly Team Members:");
}

function Target::EMenu()
{
  MS::NewMenu("Enemy Team Members:");
  %keys = "1234567890abcdefghijklmnopqrstuvwxyz";
  %keyidx = 0;
  %checkteam = Team::Enemy();
  for (%cl=2100; %cl>2048; %cl--)
  {
    %name = Client::GetName(%cl);
    %team = Client::GetTeam(%cl);
    if (%name != "" && %team == %checkteam)
    {
      %key = String::GetSubStr(%keys,%keyidx,1);
      %keyidx++;
      MS::AddChoice("Enemy Team Members:",%key,%name,"Target::Client("@%cl@");");
    }    
  }
  MS::Display("Enemy Team Members:");
}

function Target::Client(%client)
{
  remoteEval(2048, "IssueTargCommand", 0, "Targeting: "@Client::GetName(%client), %client - 2048, getManagerId());
}

// =================================================================
// For Adding Waypoints
// =================================================================



// =================================================================
// For taunts
$Target::Taunt[0] = "Alright!~wcheer3";
$Target::Taunt[1] = "Aarrg!~wdsgst4";
$Target::Taunt[2] = "Ah Crap!~wcolor7";
$Target::Taunt[3] = "Come get some~wtaunt4";
$Target::Taunt[4] = "Damnit!~wcolor6";
$Target::Taunt[5] = "Dance!~wtaunt3";
$Target::Taunt[6] = "Doh!~woops1";
$Target::Taunt[7] = "Duh~wdsgst1";
$Target::Taunt[8] = "Hmmm!~wcolor3";
$Target::Taunt[9] = "No Problem~wnoprob";
$Target::Taunt[10] = "Oops!~woops2";
$Target::Taunt[11] = "Retreat!~wretreat";
$Target::Taunt[12] = "Shazbot!~wcolor2";
$Target::Taunt[13] = "Uhhhh!~wdsgst5";
$Target::Taunt[14] = "Whoohoo~wcheer2";
$Target::Taunt[15] = "Yeeah~wcheer1";
$Target::Taunt[16] = "You Idiot!~wdsgst2";
$Target::Taunt[17] = "Yoohoo~wtaunt1";
// =================================================================