//bind("space","Ski( 1, 1 );","Ski( 0, 0 );");
$Jump::SkiRate = 0.007;

bind( "space", "Ski( 1 );", "Ski( 0 );" );
// ============================================================================
function ski( %press )
{
  if( %press )
  {
    if ( Client::GetTeam( getManagerID() ) == -1 ) 
    {
      postAction( 2048, IDACTION_MOVEUP, 1 );
      return;
    }
    function skiing()
    {
      postAction( 2048, IDACTION_MOVEUP, 1 );
      schedule( "skiing();", $Jump::SkiRate );
    }
    skiing();
  }
  else
  {
    function skiing()
    {}
  }
}
// ============================================================================

// =================================================================
// Jump'n'Jet
bind("control numpadenter","Toggle::Var(\"$JJ::Active\",\"Jump Jet\");","",JUMP_JET_TOGGLE);
bind("button1","JJ::Start();","JJ::Stop();",JUMP_JET);

$JJ::DoubleTap = "TRUE";
$JJ::ThreshHold = "0.175";
$JJ::Active = "TRUE";

function JJ::Start()
{
  if ($JJ::DoubleTap)
  {
    DoubleTap::Start(JUMP_JET,$JJ::ThreshHold);
    if (DoubleTapped(JUMP_JET))
      Toggle::Var("$JJ::Active","Jump Jet");
  }
  if ($JJ::Active=="TRUE")
    postAction(2048, IDACTION_MOVEUP, -0);
  postAction(2048, IDACTION_JET,1.000000);
}

function JJ::Stop()
{
  postAction(2048,IDACTION_JET, 0.000000);
}
// =================================================================