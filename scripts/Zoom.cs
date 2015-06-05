// ==========================================================================
// Zooming. Yay.

// Use This key to zoom
$Zoom::Key = "e";

// Use this key to increase magnification
$Zoom::IncreaseKey = "x";
// Use this key to decrease magnification
$Zoom::DecreaseKey = "z";

// The default zoom level.
// 0 = 2x; 1 = 5x; 2 = 10x; 3 = 20x;
$Zoom::Default = 1;
// ==========================================================================

bind($Zoom::Key,"Zoom::Pressed();","Zoom::Released();",ZOOM);
bind($Zoom::IncreaseKey,"Zoom::Inc();","",ZOOM_INC);
bind($Zoom::DecreaseKey,"Zoom::Dec();","",ZOOM_DEC);

function Zoom::Pressed()
{
  $Zoom::Pressed = "TRUE";
  if (Event::Trigger(EventZoomPressed,$Zoom::Current)!="FALSE")
    Zoom::In();
}
function Zoom::Released()
{
  $Zoom::Pressed = "";
  if (Event::Trigger(EventZoomReleased,$Zoom::Current)!="FALSE")
    Zoom::Out();
}

function Zoom::In()
{
  $Zoomed = "TRUE";
  postAction(2048, IDACTION_SNIPER_FOV, 1);
  Event::Trigger(EventZoomIn, $Zoom::Current);
}

function Zoom::Out()
{
  $Zoomed = "";
  postAction(2048, IDACTION_SNIPER_FOV, 0);
  Event::Trigger(EventZoomOut, $Zoom::Current);
}

function Zoom::Inc()
{
  %cur = $Zoom::Current;
  $Zoom::Current++;
  if ($Zoom::Current>3) $Zoom::Current = 0;
  postAction(2048, IDACTION_INC_SNIPER_FOV,1.000000);
  Event::Trigger(EventZoomChange,$Zoom::Current,%cur);
}

function Zoom::Dec()
{
  %cur = $Zoom::Current;
  $Zoom::Current--;
  if ($Zoom::Current<0) $Zoom::Current = 3;
  postAction(2048, IDACTION_INC_SNIPER_FOV,0.000000);
  Event::Trigger(EventZoomChange,$Zoom::Current,%cur);
}

function Zoom::To(%mag)
{
  if (%mag>=4||%mag<0) return;
  Zoom::Set(%mag);
  Zoom::In();
}

function Zoom::Set(%mag)
{
  if (%mag>=4||%mag<0) return;
  while ($Zoom::Current!=%mag)
  {
    postAction(2048, IDACTION_INC_SNIPER_FOV, 1);
    $Zoom::Current++;
    if ($Zoom::Current==4) $Zoom::Current = 0;
  }
}
Event::Attach(EventConnectionAccepted,"$Zoom::Current = 1;Zoom::Set($Zoom::Default);");