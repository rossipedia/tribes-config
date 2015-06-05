Event::Attach(EventClientChangeTeam,Team::SetTeam);
Event::Attach(EventClientJoin,Team::ClientJoin);
Event::Attach(EventClientDrop,Team::ClientDrop);
Event::Attach(EventConnectionAccepted,Team::Reset);

function Team::Reset()
{
  deleteVariables("Team::*");
}

function Team::AddClient(%team,%client)
{
  if ($Team::Index[%team,%client]!="")
    return;
  %idx = $Team::Count[%team]++;
  $Team::Index[%team,%client] = %idx;
  $Team::Client[%team,%idx] = %client;
  $Team::Current[%client] = %team;

  if (%client==getManagerID())
  {
    $Team::Event = EventTeamMateJoined;
    Team::ForAllExcept(%team,getManagerID(),Team::ReportEvent);
  }
  else if (%team == Client::GetTeam(getManagerID()))
  {
    Event::Trigger(EventTeamMateJoined,%client);
  }
}

function Team::RemClient(%team,%client)
{
  if ($Team::Index[%team,%client]=="")
    return;
  %idx = $Team::Index[%team,%client];
  while ($Team::Client[%team,%idx+1]!="")
  {
    $Team::Client[%team,%idx] = $Team::Client[%team,%idx+1];
    $Team::Index[%team,$Team::Client[%team,%idx]] = %idx;
    %idx++;
  }
  $Team::Client[%team,%idx] = "";
  $Team::Index[%team,%client] = "";
  $Team::Current[%client] = "";
  $Team::Count[%team]--;

  if (%client==getManagerID())
  {
    $Team::Event = EventTeamMateLeft;
    Team::ForAllExcept(%team,getManagerID(),Team::ReportEvent);
  }
  else if (%team == Client::GetTeam(getManagerID()))
  {
    Event::Trigger(EventTeamMateLeft,%client);
  }
}

function Team::SetTeam(%client,%team)
{
  %oldteam = $Team::Current[%client];
  if (%team == %oldteam) { return; }
  Team::RemClient(%oldteam,%client);
  Team::AddClient(%team,%client);
}

function Team::ReportEvent(%client)
{
  Event::Trigger($Team::Event,%client);
}

function Team::ClientJoin(%client)
{
  %team = Client::GetTeam(%client);
  Team::AddClient(%team,%client);  
}

function Team::ClientDrop(%client)
{
  %team = $Team::Current[%client];
  Team::RemClient(%team,%client);
}

function Team::ForAllClients(%team,%func)
{
  for (%i=1;(%client = $Team::Client[%team,%i])!="";%i++)
  {
    eval(%func@"(%client,%team);");
  }
}

function Team::ForAllExcept(%team,%exclude,%func)
{
  for (%i=1;(%client = $Team::Client[%team,%i])!="";%i++)
  {
    if (%client != %exclude)
      eval(%func@"(%client,%team);");
  }
}