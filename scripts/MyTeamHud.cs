Event::Attach(EventClientChangeTeam,MTH::ChangeTeam);
HUD::New(MyTeamHud,200,200,200,17,"Team: <f2>");
function MTH::ChangeTeam(%client,%team)
{
  if (%client==getManagerID())
  {
    HUD::Update(MyTeamHUD,"Team: <f2>"@$Team::Name[%team]);
  }
}
function MTH::Reset()
{
  HUD::Update(MyTeamHUD,"Team: <f2>");
}