// ==========================================================================
// Key to use
$QT::Key = "";

// The time/speed
$QT::TurnAroundSpeed = 0.66;
$QT::TurnAroundTime = 0.16;	
// ==========================================================================

bind($QT::Key, "RV::Turn();");

function RV::Turn()
{
  postAction(2048, IDACTION_TURNLEFT, $QT::TurnAroundSpeed);
	$QT::Turning = "TRUE";
	Schedule::Add("postAction(2048, IDACTION_TURNLEFT, -0);$QT::Turning = \"\";", $QT::TurnAroundTime);
}